using System;
using UnityEngine;


public class SolarSystem : MonoBehaviour
{
    public int hexID;
    public float fleets;
    public int tier;
    public int crystal;
    public int fusion;
    public int metal;
    public int terrain;
    public float randomRotate;
    public AnimationCurve resourceCurve;
    public AnimationCurve fleetCurve;

    float CurveWeightedRandom(AnimationCurve curve, int maxNumber)
    {
        return curve.Evaluate(UnityEngine.Random.value)*maxNumber;
    }

    Resource[] GenerateResources()
    {
        Resource[] resources = new Resource[4];
        crystal = Convert.ToInt16((tier + 1 / 2) + CurveWeightedRandom(resourceCurve, 2 + tier));
        fusion = Convert.ToInt16((tier + 1 / 2) + CurveWeightedRandom(resourceCurve, 2 + tier));
        metal = Convert.ToInt16((tier + 1 / 2) + CurveWeightedRandom(resourceCurve, 2 + tier));
        terrain = Convert.ToInt16((tier + 1 / 2) + CurveWeightedRandom(resourceCurve, 2 + tier));

       // resources.crystal = 

        return resources;

    }


    public void Awake()
    {
        // TODO: move this to its own fleets/resource class
        tier = 3 - Convert.ToInt16(Math.Log(Convert.ToDouble(UnityEngine.Random.Range(1, 5000)), 15.0));
        randomRotate = UnityEngine.Random.Range(1, 360);

        // fore debugging tier = 0;
        crystal = Convert.ToInt16((tier + 1 / 2) + CurveWeightedRandom(resourceCurve, 2 + tier));
        fusion =  Convert.ToInt16((tier + 1 / 2) + CurveWeightedRandom(resourceCurve, 2 + tier));
        metal =   Convert.ToInt16((tier + 1 / 2) + CurveWeightedRandom(resourceCurve, 2 + tier));
        terrain = Convert.ToInt16((tier + 1 / 2) + CurveWeightedRandom(resourceCurve, 2 + tier));

        if (crystal > 5) crystal = 5;
        if (fusion > 5) fusion = 5;
        if (metal > 5) metal = 5;
        if (terrain > 5) terrain = 5;

        // TODO: if no res, reroll

        int totalRes = crystal + fusion + metal + terrain;
        fleets = Convert.ToInt16((tier + 1) * (totalRes/4 + CurveWeightedRandom(fleetCurve, totalRes/2 + 3)));


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
