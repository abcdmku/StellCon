using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeployManager : MonoBehaviour{}

public interface IDeployable
{
    void Deploy();
    List<GameObject> ValidLocations();
}

public class StellarBomb : MonoBehaviour, IDeployable
{
    HexMap hexMap;
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
    public List<GameObject> ValidLocations()
    {
        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();
        List<GameObject> ValidSystem = hexMap.GetSystemsByPlayerID(mainPlayer.playerID);

        return ValidSystem;
    }
}

public class DefenceNet : MonoBehaviour, IDeployable
{
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
        List<GameObject> GOList = new List<GameObject>();
        return GOList;
    }
}

public class Terraform : MonoBehaviour, IDeployable
{
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
        List<GameObject> GOList = new List<GameObject>();
        return GOList;
    }
}

public class Wormhole : MonoBehaviour, IDeployable
{
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
        List<GameObject> GOList = new List<GameObject>();
        return GOList;
    }
}

public class Fleet : MonoBehaviour, IDeployable
{
    public void Deploy()
    {
        // origin = system.name
        // is origin owned by mainPlayer
        Debug.Log("<color=white>Fleet</color>");
    }
    public List<GameObject> ValidLocations()
    {
        List<GameObject> GOList = new List<GameObject>();
        return GOList;
    }
}



