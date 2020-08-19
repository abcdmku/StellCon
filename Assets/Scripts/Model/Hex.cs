using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Hex class defines the grid position, world space position, size, 
/// neighbours, etc... of a Hex Tile. However, it does NOT interact with
/// Unity directly in any way.
/// </summary>
public class Hex
{

    public Hex(int q, int r)
    {
        this.Q = q;
        this.R = r;
        this.S = -q-r;
    }


    // Q + R + S = 0
    // S = -(Q + R)

    public readonly int Q;  // Column
    public readonly int R;  // Row
    public readonly int S;

    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

    float radius = 1f;


    public Vector3 Position()
    {
        return new Vector3(
            HexHorizontalSpacing() * (this.Q ),
            HexVerticalSpacing() * this.R + (this.Q % 2f),
            0
        );
    }


    public float HexWidth()
    {
        return radius * 2;
    }


    public float HexHeight()
    {
        return WIDTH_MULTIPLIER * HexWidth();
    }

    public float HexVerticalSpacing()
    {
        return HexHeight() * 1f + 0.25f;
    }

    public float HexHorizontalSpacing()
    {
        return HexWidth() * 0.75f + 0.2f;
    }





    void Update()
    {
        /*/////not working
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }*/
    }

}