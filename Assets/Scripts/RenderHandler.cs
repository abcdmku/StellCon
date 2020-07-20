using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RednerHandler : MonoBehaviour
{
    public void Render(IRenderable renderable)
    {
        Sprite resourceSprite = renderable.resourceSprite;
        GameObject gameObject = renderable.hexGO;

        // Render something

    }
}
