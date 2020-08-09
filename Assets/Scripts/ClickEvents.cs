using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class ClickEvents : MonoBehaviour
{

    private void OnAnimatorMove()
    {
       //TODO: Move to solar System
    }
    void OnMouseDown()
    {
        EventManager.SetDataGroup("SOLARSYSTEM_CLICKED", gameObject.name);
        EventManager.EmitEvent("SOLARSYSTEM_CLICKED");
    }

    void OnMouseEnter()
    {
       // gameObject.transform.Find("Tile").GetComponent<SpriteRenderer>().color = new Color(.9f, .9f, .9f, 1);

    }
    void OnMouseExit()
    {
       // gameObject.transform.Find("Tile").GetComponent<SpriteRenderer>().color = new Color(.75f, .75f, .75f, 1);
    }
}
