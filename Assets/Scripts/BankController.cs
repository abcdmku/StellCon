using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BankController : MonoBehaviour
{

    private Sprite sprite;
    void Start()
    {
    }

    void Update()  // TODO: instead of on update, this would be on turn start
    {
        PlayerData mainPlayer = GameObject.Find("MainPlayer").GetComponent<PlayerData>();

        int fusionPerTurn = mainPlayer.fusionPerTurn;
        int fusionInResearch = mainPlayer.fusionInResearch;
        int terrainPerTurn = mainPlayer.terrainPerTurn;
        int terrainInResearch = mainPlayer.terrainInResearch;
        int metalInResearch = mainPlayer.metalInResearch;
        int metalPerTurn = mainPlayer.metalPerTurn;
        int crystalInResearch = mainPlayer.crystalInResearch;
        int crystalPerTurn = mainPlayer.crystalPerTurn;

        // TODO: Move this out of this class
        List<int> resoursesPerTurn = new List<int>();
        resoursesPerTurn.Add(crystalPerTurn);
        resoursesPerTurn.Add(metalPerTurn);
        resoursesPerTurn.Add(terrainPerTurn);
        resoursesPerTurn.Add(fusionPerTurn);
        int minResource = resoursesPerTurn.Min(res => res);
        mainPlayer.fleetsToPlace = minResource;
        //

        int fleetsToPlace = mainPlayer.fleetsToPlace;


        gameObject.transform.Find("FleetsToPlace").GetComponent<Text>().text = fleetsToPlace.ToString();
        gameObject.transform.Find("FusionSlider").GetComponent<Slider>().value = fusionPerTurn;
        gameObject.transform.Find("FusionResearchSlider").GetComponent<Slider>().value = fusionInResearch;
        gameObject.transform.Find("TerrainSlider").GetComponent<Slider>().value = terrainPerTurn;
        gameObject.transform.Find("TerrainResearchSlider").GetComponent<Slider>().value = terrainInResearch;
        gameObject.transform.Find("MetalResearchSlider").GetComponent<Slider>().value = metalInResearch;
        gameObject.transform.Find("MetalSlider").GetComponent<Slider>().value = metalPerTurn;
        gameObject.transform.Find("CrystalResearchSlider").GetComponent<Slider>().value = crystalInResearch;
        gameObject.transform.Find("CrystalSlider").GetComponent<Slider>().value = crystalPerTurn;

        if (crystalInResearch >= 20)
        {
            sprite = Resources.Load<Sprite>("UI/DefenceNet");
            this.transform.Find("DefenceNetBtn").GetComponent<Image>().sprite = sprite;
        } else
        {
            sprite = Resources.Load<Sprite>("UI/DefenceNet-D");
            this.transform.Find("DefenceNetBtn").GetComponent<Image>().sprite = sprite;
        }

        if (terrainInResearch >= 20)
        {
            sprite = Resources.Load<Sprite>("UI/Terraform");
            this.transform.Find("TerraformBtn").GetComponent<Image>().sprite = sprite;
        }
        else
        {
            sprite = Resources.Load<Sprite>("UI/Terraform-D");
            this.transform.Find("TerraformBtn").GetComponent<Image>().sprite = sprite;
        }

        if (fusionInResearch >= 20)
        {
            sprite = Resources.Load<Sprite>("UI/Wormhole");
            this.transform.Find("WormholeBtn").GetComponent<Image>().sprite = sprite;
        }
        else
        {
            sprite = Resources.Load<Sprite>("UI/Wormhole-D");
            this.transform.Find("WormholeBtn").GetComponent<Image>().sprite = sprite;
        }
        
        if (metalInResearch >= 20)
        {
            sprite = Resources.Load<Sprite>("UI/StellarBomb");
            this.transform.Find("StellarBombBtn").GetComponent<Image>().sprite = sprite;
        }
        else
        {
            sprite = Resources.Load<Sprite>("UI/StellarBomb-D");
            this.transform.Find("StellarBombBtn").GetComponent<Image>().sprite = sprite;
        }


    }




}
