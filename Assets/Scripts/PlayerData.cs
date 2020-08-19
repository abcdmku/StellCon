using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int 
        fleetsToPlace,
        playerID,
        fusionPerTurn,
        terrainPerTurn,
        metalPerTurn,
        crystalPerTurn,
        fusionInResearch,
        terrainInResearch, 
        metalInResearch,
        crystalInResearch;

    public enum States
    {
        Idle,
        PlacingDeployable,
        SelectingDestination,
        TurnComplete,

    }
}