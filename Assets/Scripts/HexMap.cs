using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{

    public GameObject HexPrefab;
    public int NumRows = 5;
    public int NumCols = 5;


    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
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
              //  hexGo.transform.Find("Coords").gameObject.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", solarSystem.Q, solarSystem.R);
            }
        }
    }
}
