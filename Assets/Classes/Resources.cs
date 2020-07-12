using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public Resource[] resources = new Resource[4];

    public void Awake()
    {
        foreach (Resource res in resources)
        {
            print(res.resourceName);
        }
    }
}
