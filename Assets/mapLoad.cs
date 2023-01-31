using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapLoad : MonoBehaviour
{
    public Material[] skyboxes = new Material[8];
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skyboxes[MapScene.mapScene.something];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
