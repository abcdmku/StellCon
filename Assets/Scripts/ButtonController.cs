using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DeployControler.DeployType ButtonDeploy;
    private IDeployable iDeployable;
    public void BtnClicked()
    {
        Debug.Log(this.GetComponent<ButtonController>().ButtonDeploy);

        DeployControler DeployControllerGO = GameObject.Find("GameController").GetComponent<DeployControler>();
        DeployControllerGO.HandlePowerUpType(this.ButtonDeploy);

        DeployControllerGO.Deploy();
    }

}
