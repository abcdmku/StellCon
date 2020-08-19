using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    EventsGroup Listeners = new EventsGroup();
    public int range = 1;
    public PlayerData[] Players;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameManager initialized");

        Listeners.Add("SOLARSYSTEM_CLICKED", SolarSystemClicked);
        Listeners.StartListening("SOLARSYSTEM_CLICKED");

    }
    void OnDestroy()
    {
        Listeners.StopListening();
    }

    void SolarSystemClicked()
    {
        var eventData = EventManager.GetSender("SOLARSYSTEM_CLICKED");

        if (eventData != null)
        {
            GameObject SolarSystemGO = (GameObject)eventData;
           
            
                Debug.Log("<color=green>SS Clicked: " + SolarSystemGO.name + "</color>\n");
               // SolarSystemGO.transform.Find("Tile").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

                List<GameObject> Systems = SolarSystemGO.GetComponent<SolarSystem>().GetNeighbors(range, false);
                
            foreach (GameObject system in Systems)
            {
                system.transform.Find("Tile").GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);

                //Debug.Log(system.name);
            }
            
        }
    }


}
