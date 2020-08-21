using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeployManager : MonoBehaviour{}

public interface IDeployable
{
    void Deploy();
    List<GameObject> ValidLocations();
    void InitialReaction();
}

//   ███████╗████████╗██████╗  █████╗ ████████╗███████╗ ██████╗██╗   ██╗    ██████╗  █████╗ ████████╗████████╗███████╗██████╗ ███╗   ██╗
//   ██╔════╝╚══██╔══╝██╔══██╗██╔══██╗╚══██╔══╝██╔════╝██╔════╝╚██╗ ██╔╝    ██╔══██╗██╔══██╗╚══██╔══╝╚══██╔══╝██╔════╝██╔══██╗████╗  ██║
//   ███████╗   ██║   ██████╔╝███████║   ██║   █████╗  ██║  ███╗╚████╔╝     ██████╔╝███████║   ██║      ██║   █████╗  ██████╔╝██╔██╗ ██║
//   ╚════██║   ██║   ██╔══██╗██╔══██║   ██║   ██╔══╝  ██║   ██║ ╚██╔╝      ██╔═══╝ ██╔══██║   ██║      ██║   ██╔══╝  ██╔══██╗██║╚██╗██║
//   ███████║   ██║   ██║  ██║██║  ██║   ██║   ███████╗╚██████╔╝  ██║       ██║     ██║  ██║   ██║      ██║   ███████╗██║  ██║██║ ╚████║
//   ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   ╚══════╝ ╚═════╝   ╚═╝       ╚═╝     ╚═╝  ╚═╝   ╚═╝      ╚═╝   ╚══════╝╚═╝  ╚═╝╚═╝  ╚═══╝

public class StellarBomb : MonoBehaviour, IDeployable // TODO: move these out to their own file
{
    public void InitialReaction()
    {
        List<GameObject> ValidSystem = ValidLocations();
        foreach (GameObject system in ValidSystem)
        {
            system.GetComponent<SolarSystem>().highlighted = true;
        }

    }

    public List<GameObject> ValidLocations()
    {
        HexMap hexMap = GameObject.Find("HexMap").GetComponent<HexMap>();
        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();

        List<GameObject> MainPlayerSystems = hexMap.GetSystemsByPlayerID(mainPlayer.playerID);
        List<GameObject> Neighbors = new List<GameObject>();

        foreach (GameObject system in MainPlayerSystems)
        {
            Neighbors.AddRange(system.GetComponent<SolarSystem>().GetNeighbors(1, false));
        }
        Neighbors = Neighbors.Distinct().ToList();

        List<GameObject> AllSystems = hexMap.GetAllSystems();
        List<GameObject> EnemySystems = AllSystems.Except(MainPlayerSystems).ToList();

        List<GameObject> ValidSystem = EnemySystems.Intersect(Neighbors).ToList();

        return ValidSystem;
    }

    public void Deploy()
    {
        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();
        int metal = mainPlayer.metalInResearch;

        if (metal >= 20)
        {
            Debug.Log("<color=yellow>StellarBomb " + metal + "</color>");
            mainPlayer.metalInResearch = 0;
        }
        else
        {
            Debug.Log("<color=yellow>" + metal + " metal is not enough to deploy a StellarBomb</color>");
        }
    }
}

public class DefenceNet : MonoBehaviour, IDeployable
{
    public void InitialReaction()
    {
        List<GameObject> ValidSystem = ValidLocations();
        foreach (GameObject system in ValidSystem)
        {
            system.GetComponent<SolarSystem>().highlighted = true;
        }
    }

    public void Deploy()
    {
        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();
        int crystal = mainPlayer.crystalInResearch;

        if (crystal >= 20)
        {
            Debug.Log("<color=cyan>DefenceNet " + crystal + "</color>");
            mainPlayer.crystalInResearch = 0;
        }
        else
        {
            Debug.Log("<color=cyan>" + crystal + " crystal is not enough to deploy a DefenceNet</color>");
        }
    }
    public List<GameObject> ValidLocations()
    {
        HexMap hexMap = GameObject.Find("HexMap").GetComponent<HexMap>();

        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();
        List<GameObject> ValidSystem = hexMap.GetSystemsByPlayerID(mainPlayer.playerID);
        return ValidSystem;
    }
}

public class Terraform : MonoBehaviour, IDeployable
{
    public void InitialReaction()
    {
        List<GameObject> ValidSystem = ValidLocations();
        foreach (GameObject system in ValidSystem)
        {
            system.GetComponent<SolarSystem>().highlighted = true;
        }
    }

    public void Deploy()
    {
        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();
        int terrain = mainPlayer.terrainInResearch;
 
        if (terrain >= 20)
        {
            Debug.Log("<color=green>Terraform " + terrain + "</color>");
            mainPlayer.terrainInResearch = 0;
        }
        else
        {
            Debug.Log("<color=green>" + terrain + " terrain is not enough to deploy a Terraform</color>");
        }
    }
    public List<GameObject> ValidLocations()
    {
        HexMap hexMap = GameObject.Find("HexMap").GetComponent<HexMap>();

        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();
        List<GameObject> ValidSystem = hexMap.GetSystemsByPlayerID(mainPlayer.playerID);
        return ValidSystem;
    }
}

public class Wormhole : MonoBehaviour, IDeployable
{
    public void InitialReaction()
    {
        List<GameObject> ValidSystem = ValidLocations();
        foreach (GameObject system in ValidSystem)
        {
            system.GetComponent<SolarSystem>().highlighted = true;
        }
    }

    public void Deploy()
    {
        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();
        int fusion = mainPlayer.fusionInResearch;

        if (fusion >= 20)
        {
            Debug.Log("<color=purple>Wormhole " + fusion + "</color>");
            mainPlayer.fusionInResearch = 0;
        }
        else
        {
            Debug.Log("<color=purple>" + fusion + " fusion is not enough to deploy a Wormhole</color>");
        }
    }
    public List<GameObject> ValidLocations()
    {
        HexMap hexMap = GameObject.Find("HexMap").GetComponent<HexMap>();

        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();
        List<GameObject> ValidSystem = hexMap.GetSystemsByPlayerID(mainPlayer.playerID);
        return ValidSystem;
    }
}

public class PlaceFleet : MonoBehaviour, IDeployable
{
    public void InitialReaction()
    {
        List<GameObject> ValidSystem = ValidLocations();
        foreach (GameObject system in ValidSystem)
        {
            system.GetComponent<SolarSystem>().highlighted = true;
        }
    }

    public void Deploy()
    {
        // origin = system.name
        // is origin owned by mainPlayer
        Debug.Log("<color=white>Fleet</color>");
    }
    public List<GameObject> ValidLocations()
    {
        HexMap hexMap = GameObject.Find("HexMap").GetComponent<HexMap>();

        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();
        List<GameObject> ValidSystem = hexMap.GetSystemsByPlayerID(mainPlayer.playerID);
        return ValidSystem;
    }
}



