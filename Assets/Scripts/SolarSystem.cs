using System;
using UnityEngine;


public class SolarSystem : MonoBehaviour
{
    public int hexID;
    public int fleets;
    public int tier;
    public int crystal;
    public int fusion;
    public int metal;
    public int terrain;
    public float randomRotate;



    public void Awake()
    {
        // TODO: move this to its own fleets/resource class
        tier = 3 - Convert.ToInt16(Math.Log(Convert.ToDouble(UnityEngine.Random.Range(1, 5000)), 15.0));
        randomRotate = UnityEngine.Random.Range(1, 360);

        fleets = Convert.ToInt16(Math.Log(Convert.ToDouble(UnityEngine.Random.Range(1, 1000)), 4.0));

        crystal = 5 - Convert.ToInt16(Math.Log(Convert.ToDouble(UnityEngine.Random.Range(1, 1000)), 4.0));
        fusion = 5 - Convert.ToInt16(Math.Log(Convert.ToDouble(UnityEngine.Random.Range(1, 1000)), 4.0));
        metal = 5 - Convert.ToInt16(Math.Log(Convert.ToDouble(UnityEngine.Random.Range(1, 1000)), 4.0));
        terrain = 5 - Convert.ToInt16(Math.Log(Convert.ToDouble(UnityEngine.Random.Range(1, 1000)), 4.0));



        Sprite sprite = UnityEngine.Resources.Load<Sprite>("Crystal-" + crystal.ToString());
        this.transform.Find("Crystal").GetComponent<SpriteRenderer>().sprite = sprite;
        sprite = UnityEngine.Resources.Load<Sprite>("Fusion-" + fusion.ToString());
        this.transform.Find("Fusion").GetComponent<SpriteRenderer>().sprite = sprite;
        sprite = UnityEngine.Resources.Load<Sprite>("Metal-" + metal.ToString());
        this.transform.Find("Metal").GetComponent<SpriteRenderer>().sprite = sprite;
        sprite = UnityEngine.Resources.Load<Sprite>("Terrain-" + terrain.ToString());
        this.transform.Find("Terrain").GetComponent<SpriteRenderer>().sprite = sprite;

        sprite = UnityEngine.Resources.Load<Sprite>("Tier-" + tier.ToString());
        this.transform.Find("Tier").GetComponent<SpriteRenderer>().sprite = sprite;
        this.transform.Find("Tier").transform.Rotate(new Vector3(0f, 0f, randomRotate));



        this.transform.Find("Fleets").gameObject.GetComponentInChildren<TextMesh>().text = fleets.ToString();
    }

}
