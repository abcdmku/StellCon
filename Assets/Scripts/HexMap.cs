using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class HexMap : MonoBehaviour
{

    public GameObject HexPrefab;
    public int NumRows = 5;
    public int NumCols = 5;
    public int density = 5;
    public float systemDensity;
    public float maxSystems;
    public int systemsPlaced;
    List<String> nameList = new List<String>();

    

    void Start()
    {
        // GenerateFullMap();
        PlacePlayers();
        RandomizePlacement();
        GenerateSystemProperties();
    }

    void GenerateFullMap()
    {
        for (int column = 0; column < NumCols; column++)
        {
            for (int row = 0; row < NumRows; row++)
            {
                Hex solarSystem = new Hex(column, row);

                GameObject hexGo = (GameObject)Instantiate(
                    HexPrefab, 
                    solarSystem.Position(),
                    Quaternion.identity, 
                    this.transform
                );
                hexGo.name = "SolarSystem (" + solarSystem.Q + ", " + solarSystem.R + ")";
                hexGo.transform.Find("Coords").gameObject.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1},{2}", solarSystem.Q, solarSystem.R, solarSystem.S);
            }
        }
    }

    public void RedrawMap()
    {
        RemoveAllSystems();
        PlacePlayers();
        RandomizePlacement();
        GenerateSystemProperties();
    }

    GameObject PlaceSolarSystem(int x, int y, bool highlight)
    {
        string name = x.ToString() + ", " + y.ToString();
        GameObject hexGO = null;

        if (!nameList.Contains(name))
        {
            Hex solarSystem = new Hex(x, y);

            hexGO = (GameObject)Instantiate(
                HexPrefab,
                solarSystem.Position(),
                Quaternion.identity,
                this.transform
                );
            hexGO.name = "SolarSystem (" + solarSystem.Q + ", " + solarSystem.R + ")";
            hexGO.transform.Find("Coords").gameObject.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1},{2}", solarSystem.Q, solarSystem.R, solarSystem.S);
            if (highlight)
                hexGO.transform.Find("Tile").GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
            nameList.Add(name);
        }
        systemsPlaced = nameList.Count;
        return hexGO;
    }

    public void RandomizePlacement()
    {        
        // Debug.Log(systemDensity + " - " + density);
        int i = 0;
        while (!ReachedSystemLimit() && i < 1000)
        {
            int x = UnityEngine.Random.Range(0, NumCols);
            int y = UnityEngine.Random.Range(0, NumRows);
            //int range = UnityEngine.Random.Range(1, 5);
            PlaceSolarSystem(x, y, false);
            PlaceSolarSystemAround(x, y, 3, 100);

            i++;  
        }
        Debug.Log("i = " + i);
    }

    void PlaceSolarSystemAround(int x, int y, int range, float probability)
    {
        for (int xx = -range; xx <= range; xx++)
        {
            for (int yy = -range; yy <= range; yy++)
            {
                float finalProb = probability / ((Math.Abs(xx) * Math.Abs(yy)+.01f));

                if (UnityEngine.Random.Range(0, 100) <= finalProb)
                {
                    int finalX = x + xx;
                    int finalY = y + yy;
                    PlaceSolarSystem(finalX, finalY, false);
                }
            }
        }
    }

    bool ReachedSystemLimit()
    {
        bool reachedLimit = false;
        systemDensity = (density / 100f);
        maxSystems = NumRows * NumCols * systemDensity;
        if (maxSystems < nameList.Count+1)
            reachedLimit = true;
        return reachedLimit;
    }

    bool IsInBounds(int x, int y)
    {
        bool isInBounds = false;
        if (x <= NumCols && x >= 0 && y <= NumRows-1 && y >= 0)
            isInBounds = true;
        return isInBounds;
    }

    void RemoveSystem(GameObject system)
    {
        string systemName = system.name;
        string systemCoord = systemName.Split('(')[1].TrimEnd(')');
        nameList.Remove(systemCoord);

        if (PlayerSystems.Contains(system))
            PlayerSystems.Remove(system);

        Destroy(system);

    }

    List<GameObject> PlayerSystems = new List<GameObject>();
    void PlacePlayers()
    {
        int playersInGame = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameManager>().Players.Length;

        while (PlayerSystems.Count < playersInGame)
        {
            int x = UnityEngine.Random.Range(0, NumCols);
            int y = UnityEngine.Random.Range(0, NumRows);

            string name = x.ToString() + ", " + y.ToString();

            if (!nameList.Contains(name))
            {
                bool isValid = false;
                bool isTooClose = false;

                int closeDist = 160 - playersInGame * 10;
                int farDist = 200 + playersInGame * 20;

                GameObject CurrentSystem = PlaceSolarSystem(x, y, true);
                Vector3 currentPosition = CurrentSystem.transform.position;

                foreach (GameObject system in PlayerSystems)
                {
                    Vector3 coordsDiff = system.transform.position - currentPosition;
                    float distance = coordsDiff.sqrMagnitude;

                    if (distance < farDist && distance > closeDist)
                        isValid = true;

                    if (distance < closeDist)
                        isTooClose = true;
                }

                if (isValid && !isTooClose || PlayerSystems.Count == 0)
                {
                    PlayerSystems.Add(CurrentSystem);
                    PlaceSolarSystemAround(x, y, 5, 200);
                } else
                {
                    RemoveSystem(CurrentSystem);
                }
            }
        }
    }

    public void GenerateSystemProperties()
    {
        List<GameObject> AllSystems = GetAllSystems();

        foreach (GameObject system in AllSystems)
            system.GetComponent<SolarSystem>().Generate();
    }


    public void AttackSolarSystem(GameObject DefendingSystem, List<GameObject> AttackingSystems)
    {
        int thisSolarSystemsFleets = Convert.ToInt16(this.transform.Find("Fleets").gameObject.GetComponentInChildren<TextMesh>().text);
        int AttackingSolarSystemsFleets = Convert.ToInt16(DefendingSystem.transform.Find("Fleets").gameObject.GetComponentInChildren<TextMesh>().text);

        print("this Fleets: " + thisSolarSystemsFleets);
        print("other Fleets: " + AttackingSolarSystemsFleets);

        while (thisSolarSystemsFleets != 0 || AttackingSolarSystemsFleets != 0)
        {
            // int roll = UnityEngine.Random.Range(0, 3);
            // if (roll == 1) UpdateFleet(fleet--);
            // if (roll == 2) SolarSystemAtacking.UpdateFleet(SolarSystemAtacking.fleet--);
            // yield return new WaitForSeconds(5);


        }
        // yield return new WaitForSeconds(0.5f);
    }



    public void GetIslands()
    {
        //recursive
        // make a checked list
        // list of all systems 
        // for each system get neighbors and add to list
        // remove duplicate values on list, and check new neighbors
        // might need a new class for a list of islands (gameobject lists)
    }


    public List<GameObject> GetAllSystems()
    {
        GameObject[] AllSystemsArray = GameObject.FindGameObjectsWithTag("SolarSystemTag");
        List<GameObject> AllSystems = new List<GameObject>();

        foreach (GameObject system in AllSystemsArray)
            AllSystems.Add(system);
        return AllSystems;
    }

    //not tested
    public List<GameObject> GetSystemsByPlayerID(int playerID)
    {
        List<GameObject> AllSystems = GetAllSystems();
        List<GameObject> GOList = new List<GameObject>();

        foreach (GameObject system in AllSystems)
            if (system.GetComponent<SolarSystem>().playerID == playerID)
                GOList.Add(system);

        return GOList;
    }

    public void RemoveAllSystems()
    {
        List<GameObject> AllSystems = GetAllSystems();

        foreach (GameObject system in AllSystems)
            RemoveSystem(system);
    }



}


