using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvents : MonoBehaviour
{


    void OnMouseDown()
    {
        print(gameObject);
        gameObject.transform.Find("Tile").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    void OnMouseEnter()
    {
        gameObject.transform.Find("Tile").GetComponent<SpriteRenderer>().color = new Color(.9f, .9f, .9f, 1);

    }
    void OnMouseExit()
    {
        gameObject.transform.Find("Tile").GetComponent<SpriteRenderer>().color = new Color(.75f, .75f, .75f, 1);
    }
}
