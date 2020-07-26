using System;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using UnityEngine;


public class SolarSystem : MonoBehaviour, IGameComponent
{
    public int hexID;
    public int fleet;
    public int playerID;
    public int tier;
    public int totalRes;
    public float randomRotate;
    public bool hasDefenceNet = false;
    public bool hasBeenTerraformed = false;

    public AnimationCurve tierCurve;
    public AnimationCurve resourceCurve;
    public AnimationCurve fleetCurve;
    public AnimationCurve tempPlayerCurve;

    public GameObject DefenseNet;



    public Resource[] resources;



    float CurveWeightedRandom(AnimationCurve curve, int maxNumber)
    {
        return curve.Evaluate(UnityEngine.Random.value) * maxNumber;
    }

    // TODO: Move to RenderHandler
    public void RenderSprite(string gameObjName, string spriteName)
    {
        Sprite sprite = Resources.Load<Sprite>(spriteName);
        this.transform.Find(gameObjName).GetComponent<SpriteRenderer>().sprite = sprite;
    }


    public void UpdateFleet(int updateFleet)
    {
        fleet = updateFleet;
        this.transform.Find("Fleets").gameObject.GetComponentInChildren<TextMesh>().text = fleet.ToString();
    }


    public void GenerateSolarSystemTier()
    {
        float tierF = tierCurve.Evaluate(UnityEngine.Random.value * 3);
        tier = Convert.ToInt16(tierF);

        randomRotate = UnityEngine.Random.Range(1, 360);
        string spriteName = "Tier-" + tier.ToString();
        RenderSprite("Tier", spriteName);
        this.transform.Find("Tier").transform.Rotate(new Vector3(0f, 0f, randomRotate));
    }

    public void GenerateSolarSystemResources()
    {
        totalRes = 0;
        foreach (Resource Res in resources)
        {
            string resName = Res.resourceName;
            int resValue = Convert.ToInt16(((tier + 1) / 3) + CurveWeightedRandom(resourceCurve, 2 + tier));
            if (resValue > 5) resValue = 5;
            string spriteName = resName + "-" + resValue.ToString();
            RenderSprite(resName, spriteName);

            totalRes += resValue;
        }
    }

    // TODO: Move textRenderer to RenderHandler

    public void GenerateSolarSystemFleet()
    {
        int generatedFleet = Convert.ToInt16(Convert.ToInt16((tier + 1) * (totalRes / 4 + CurveWeightedRandom(fleetCurve, totalRes / 2 + 3))));
        UpdateFleet(generatedFleet);
    }

    public void ChangeOwnership(int playerID)
    {
        string spriteName = "Tile-" + playerID.ToString();
        RenderSprite("Tile", spriteName);
    }

    public void StellarBomb()
    {
        if (!hasDefenceNet)
            UpdateFleet(fleet / 2);
        else
        {
            hasDefenceNet = false;
            GameObject DefenseNetGO = GameObject.Find("DefenceNetGO(Clone)");
            Destroy(DefenseNetGO);
        }
    }



    public void DefenceNet()
    {
        if (!hasDefenceNet)
        {
            hasDefenceNet = true;
            Instantiate(DefenseNet);
        }
    }

    public void Terraform()
    {
        if (tier < 3 && !hasBeenTerraformed)
        {
            hasBeenTerraformed = true;
            tier += 1;
            string spriteName = "Tier-" + tier.ToString();
            RenderSprite("Tier", spriteName);
            gameObject.transform.Find("Tier").GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.6f, 0.4f, 1);
            GenerateSolarSystemResources();
        }
    }

    public void WormHole()
    {
        // need to implement player movement first
    }

    public void AttackSolarSystem(string solarSystemAtackingName)
    {
        GameObject SolarSystemAtacking = GameObject.Find(solarSystemAtackingName);
        int thisSolarSystemsFleets = Convert.ToInt16(this.transform.Find("Fleets").gameObject.GetComponentInChildren<TextMesh>().text);
        int AttackingSolarSystemsFleets = Convert.ToInt16(SolarSystemAtacking.transform.Find("Fleets").gameObject.GetComponentInChildren<TextMesh>().text);

        print("this Fleets: " + thisSolarSystemsFleets);
        print("other Fleets: " + AttackingSolarSystemsFleets);

        while (thisSolarSystemsFleets != 0 || AttackingSolarSystemsFleets != 0)
        {
            int roll = UnityEngine.Random.Range(0, 3);
            if (roll == 1) UpdateFleet(fleet--);
              if (roll == 2) SolarSystemAtacking.UpdateFleet(SolarSystemAtacking.fleet--);
           // yield return new WaitForSeconds(5);


        }

        // yield return new WaitForSeconds(0.5f);

    }


    public void Awake()
    {
        GenerateSolarSystemTier();
        GenerateSolarSystemResources();
        GenerateSolarSystemFleet();

        ChangeOwnership(Convert.ToInt16(tempPlayerCurve.Evaluate(4+UnityEngine.Random.value * 4))); // testing player colors

    }

    /*
    public void writeText(object info, string textBoxName)
    {

        Type ObjectType = info.GetType();
        GameObject.Find(textBoxName).GetComponent<UnityEngine.UI.Text>().text = "Feilds:";
        foreach (ObjectType infoElement in info)
        {
            GameObject.Find(textBoxName).GetComponent<UnityEngine.UI.Text>().text += "\n" + infoElement.Name;
        }
    }
    */

    public void GetInfo()
    {
        PropertyInfo[] propertyInfo;
        propertyInfo = typeof(SolarSystem).GetProperties(BindingFlags.Public);

        MethodInfo[] methodInfo;
        methodInfo = typeof(SolarSystem).GetMethods();

        FieldInfo[] feildInfo;
        feildInfo = typeof(SolarSystem).GetFields();

        GameObject.Find("TextProperites").GetComponent<UnityEngine.UI.Text>().text = "Properites:";
        foreach (PropertyInfo prop in propertyInfo)
        {
            GameObject.Find("TextProperites").GetComponent<UnityEngine.UI.Text>().text += "\n" + prop.Name;
        }

        GameObject.Find("TextMethods").GetComponent<UnityEngine.UI.Text>().text = "Methods:";
        foreach (MethodInfo method in methodInfo)
        {
            GameObject.Find("TextMethods").GetComponent<UnityEngine.UI.Text>().text += "\n" + method.Name;
        }

        GameObject.Find("TextFeilds").GetComponent<UnityEngine.UI.Text>().text = "Feilds:";
        foreach (FieldInfo feild in feildInfo)
        {
            GameObject.Find("TextFeilds").GetComponent<UnityEngine.UI.Text>().text += "\n" + feild.Name;
        }
    }
}
