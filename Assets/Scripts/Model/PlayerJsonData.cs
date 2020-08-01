using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerJsonData
{
    public string PlayerTag;
    public DateTime JoinDate;
    public int totalResReceived;
    public int totalSystemsCaptured;
    public int totalSystemsLost;
    public int totalFleetsBuilt;
    public int totalFleetsDestroyed;
    public int totalFleetsLost;
    public int preferedPlayerColor;
    public int gamesWon;
    public int gamesLost;
}