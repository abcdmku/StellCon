using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class MainPlayer : MonoBehaviour
{

    public int fleetsToPlace, playerID;

    EventsGroup Listeners = new EventsGroup();

    void Start()
    {
        Listeners.Add("SOLARSYSTEM_CLICKED", SolarSystemClicked);
        Listeners.StartListening();
    }

    void SolarSystemClicked()
    {
        var eventData = EventManager.GetDataGroup("SOLARSYSTEM_CLICKED");

        if (eventData != null)
        {
            string solarSystemName = eventData[0].ToString();
            Debug.Log("<color=green>SS Clicked: " + solarSystemName + "</color>\n");
            GameObject SolarSystemGO = GameObject.Find(solarSystemName);
            SolarSystemGO.transform.Find("Tile").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    void OnDestroy()
    {
        Listeners.StopListening();
    }
}
