﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;
using TigerForge;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SolarSystem : MonoBehaviour, IGameComponent
{
    public int fleet;
    public int playerID;
    public int tier;
    public int totalRes;
    public float randomRotate;
    public bool hasDefenceNet = false;
    public bool hasBeenTerraformed = false;
    public bool highlighted = false;

    public AnimationCurve tierCurve;
    public AnimationCurve resourceCurve;
    public AnimationCurve fleetCurve;
    public AnimationCurve tempPlayerCurve;

    public GameObject DefenseNet;
    public Resource[] resources;

    public void Generate()
    {
        GenerateSolarSystemTier();
        GenerateSolarSystemResources();
        GenerateSolarSystemFleet();

        playerID = Convert.ToInt16(tempPlayerCurve.Evaluate(4 + UnityEngine.Random.value * 4)); // make random player colors
    }

    void Start()
    {

    }

    void Update() // change to on new turn 
    {
        UpdateFleet(fleet);
        ChangeColor();
        HighlightSystem(highlighted);
    }

    public void OnClick()
    {
        Debug.Log(this.name + " Clicked");
        EventManager.EmitEvent("SOLARSYSTEM_CLICKED", gameObject);
    }

    float CurveWeightedRandom(AnimationCurve curve, int maxNumber)
    {
        return curve.Evaluate(UnityEngine.Random.value) * maxNumber;
    }

    // TODO: Move to RenderHandler
    public void RenderSprite(string gameObjName, string spriteName)
    {
        Sprite sprite = Resources.Load<Sprite>(spriteName);
        this.transform.Find(gameObjName).GetComponent<SpriteRenderer>().sprite = sprite;
    }


    public void UpdateFleet(int updateFleet)
    {
        fleet = updateFleet;
        this.transform.Find("Fleets").gameObject.GetComponentInChildren<TextMesh>().text = fleet.ToString();
    }


    public void GenerateSolarSystemTier()
    {
        float neighbors = 1; // GetNeighbors(1,false).Count; 
        // TODO: find a different way to generate high tiers in remote locations. checking neighbors for every system is laggy

        neighbors = (6 - neighbors) / 2;

        float tierF = tierCurve.Evaluate(UnityEngine.Random.value * neighbors);
        tier = Convert.ToInt16(tierF);

        randomRotate = UnityEngine.Random.Range(1, 360);
        string spriteName = "Tier-" + tier.ToString();
        RenderSprite("Tier", spriteName);
        this.transform.Find("Tier").transform.Rotate(new Vector3(0f, 0f, randomRotate));
    }

    public void GenerateSolarSystemResources()
    {
        totalRes = 0;
        foreach (Resource Res in resources)
        {
            string resName = Res.resourceName;
            int resValue = Convert.ToInt16(((tier + 1) / 3) + CurveWeightedRandom(resourceCurve, 2 + tier));
            if (resValue > 5) resValue = 5;
            string spriteName = resName + "-" + resValue.ToString();
            RenderSprite(resName, spriteName);

            totalRes += resValue;
        }
    }

    // TODO: Move textRenderer to RenderHandler

    public void GenerateSolarSystemFleet()
    {
        int generatedFleet = Convert.ToInt16(Convert.ToInt16((tier + 1) * (totalRes / 4 + CurveWeightedRandom(fleetCurve, totalRes / 2 + 3))));
        UpdateFleet(generatedFleet);
    }

    public void ChangeColor()
    {
        SystemColors systemColors = GameObject.Find("GameController").GetComponent<SystemColors>();
        Color32 newColor = systemColors.colorList.ElementAt(playerID);
        this.transform.Find("Tile").GetComponent<SpriteRenderer>().color = newColor;
    }

    public void HighlightSystem(bool highlighted)
    {
        if (highlighted)
            this.transform.Find("Highlight").GetComponent<Light2D>().intensity = 1f;
        else
            this.transform.Find("Highlight").GetComponent<Light2D>().intensity = 0;
    }

    public void StellarBomb()
    {
        if (!hasDefenceNet)
            UpdateFleet(fleet / 2);
        else
        {
            hasDefenceNet = false;
            GameObject DefenseNetGO = GameObject.Find("DefenceNetGO(Clone)");
            Destroy(DefenseNetGO);
        }
    }

    public void DefenceNet()
    {
        if (!hasDefenceNet)
        {
            hasDefenceNet = true;
            Instantiate(DefenseNet);
        }
    }

    public void Terraform()
    {
        if (tier < 3 && !hasBeenTerraformed)
        {
            hasBeenTerraformed = true;
            tier += 1;
            string spriteName = "Tier-" + tier.ToString();
            RenderSprite("Tier", spriteName);
            gameObject.transform.Find("Tier").GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.6f, 0.4f, 1);
            GenerateSolarSystemResources();
        }
    }

    public void WormHole()
    {
        // display purple ring around system or something
    }


    //works well to 6 neighbors
    public List<GameObject> GetNeighbors(int range, bool fillCenter)
    {
        List<GameObject> GOList = new List<GameObject>();
        GameObject[] AllSystems = GameObject.FindGameObjectsWithTag("SolarSystemTag");
        Vector3 currentPosition = transform.position;

        double innerRange = Math.Floor(Math.Pow(range * 1.7, 2) + Math.Pow(((0.577677009 * (range % 2)) * 1.7),2));
        double outerRange = Math.Ceiling(Math.Pow(range * 1.982051, 2));

        if (fillCenter)
            innerRange = 0;

        foreach (GameObject system in AllSystems)
        {
            Vector3 TargetSystemCoords = system.transform.position - currentPosition;
            system.transform.Find("Coords").gameObject.GetComponentInChildren<TextMesh>().text = TargetSystemCoords.sqrMagnitude.ToString();
            if (TargetSystemCoords.sqrMagnitude < outerRange && TargetSystemCoords.sqrMagnitude > innerRange)
                GOList.Add(system);
        }
        return GOList;
    }


    // not tested
    public bool IsNeighbor(GameObject originSystem)
    {
        bool isNeighbor = false;

        Vector3 currentPosition = transform.position;
        Vector3 TargetSystemCoords = originSystem.transform.position - currentPosition;
        float distance = TargetSystemCoords.sqrMagnitude;
        if (distance < 4 && distance > 0)
            isNeighbor = true;

       return isNeighbor;
    }

    public bool IsAttackable(GameObject originSystem)
    {
        bool isAttackable = false;

        bool isNeighbor = IsNeighbor(originSystem);
        bool isDifferentPlayer = this.playerID != originSystem.GetComponent<SolarSystem>().playerID;
        if (isNeighbor && isDifferentPlayer)
            isAttackable = true;

        return isAttackable;
    }

    public GameObject GetClosestSystem()
    {
        GameObject bestTarget = new GameObject();
        float shortestDistance = Mathf.Infinity;

        GameObject[] AllSystems = GameObject.FindGameObjectsWithTag("SolarSystemTag");
        Vector3 currentPosition = transform.position;

        foreach (GameObject system in AllSystems)
        {
            Vector3 TargetSystemCoords = system.transform.position - currentPosition;

            if (TargetSystemCoords.sqrMagnitude < shortestDistance)
            {
                bestTarget = system;
                shortestDistance = TargetSystemCoords.sqrMagnitude;
            }

        }
        return bestTarget;
    }

    public void GetInfo()
    {
        PropertyInfo[] propertyInfo;
        propertyInfo = typeof(SolarSystem).GetProperties(BindingFlags.Public);

        MethodInfo[] methodInfo;
        methodInfo = typeof(SolarSystem).GetMethods();

        FieldInfo[] feildInfo;
        feildInfo = typeof(SolarSystem).GetFields();

        GameObject.Find("TextProperites").GetComponent<UnityEngine.UI.Text>().text = "Properites:";
        foreach (PropertyInfo prop in propertyInfo)
        {
            GameObject.Find("TextProperites").GetComponent<UnityEngine.UI.Text>().text += "\n" + prop.Name;
        }

        GameObject.Find("TextMethods").GetComponent<UnityEngine.UI.Text>().text = "Methods:";
        foreach (MethodInfo method in methodInfo)
        {
            GameObject.Find("TextMethods").GetComponent<UnityEngine.UI.Text>().text += "\n" + method.Name;
        }

        GameObject.Find("TextFeilds").GetComponent<UnityEngine.UI.Text>().text = "Feilds:";
        foreach (FieldInfo feild in feildInfo)
        {
            GameObject.Find("TextFeilds").GetComponent<UnityEngine.UI.Text>().text += "\n" + feild.Name;
        }
    }
}
