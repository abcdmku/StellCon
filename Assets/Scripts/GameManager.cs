using System;
using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEditorInternal;
using UnityEngine;
using System.Linq;


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
            SolarSystem System = SolarSystemGO.GetComponent<SolarSystem>();
            List<GameObject> Systems = System.GetNeighbors(range, false);

            Debug.Log("<color=green>SS Clicked: " + SolarSystemGO.name + "</color>\n");
            int playerID = System.playerID;

            foreach (GameObject system in Systems)
                system.GetComponent<SolarSystem>().playerID = playerID;            
        }
    }


}
