using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeployControler : MonoBehaviour
{
    public enum DeployType
    {
        StellarBomb,
        DefenceNet,
        Terraform,
        Wormhole,
        PlaceFleet
    }


    private IDeployable iDeployable;

    // TODO Get this working
    public System.Object GetDeployType(DeployType deployType)
    {
        var ns = typeof(DeployType).Namespace;
        var typeName = ns + "." + deployType.ToString();

        return System.Activator.CreateInstance(System.Type.GetType(typeName));
    }


    public void HandlePowerUpType(DeployType deployType)
    {

        //To prevent Unity from creating multiple copies of the same component in inspector at runtime
        Component c = gameObject.GetComponent<IDeployable>() as Component;

        if (c != null)
        {
            Destroy(c);
        }

        switch (deployType)
        {
            case DeployType.StellarBomb:
                iDeployable = gameObject.AddComponent<StellarBomb>();
                break;

            case DeployType.DefenceNet:
                iDeployable = gameObject.AddComponent<DefenceNet>();
                break;

            case DeployType.Terraform:
                iDeployable = gameObject.AddComponent<Terraform>();
                break;

            case DeployType.Wormhole:
                iDeployable = gameObject.AddComponent<Wormhole>();
                break;

            case DeployType.PlaceFleet:
                iDeployable = gameObject.AddComponent<PlaceFleet>();
                break;
        }
    }

    public void Deploy()
    {
        iDeployable.Deploy();
    }

    public void ValidLocations()
    {
        iDeployable.ValidLocations();
    }

    public void InitialReaction()
    {
        iDeployable.InitialReaction();
    }
    

    void Start()
    {
    }


}


