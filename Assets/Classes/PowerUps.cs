using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public Research[] powerUps = new Research[4];


    public void Awake()
    {
        foreach (Research pu in powerUps)
        {
           print(pu);
        }
    }

}
