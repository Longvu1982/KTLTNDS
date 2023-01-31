using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScene : MonoBehaviour
{
    public static MapScene mapScene;
    public int something;
    public mapSelect mapselectt;
    public int mapIndex;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        something = mapselectt.capturedIterator;
    }
    void Awake()
    {
        mapScene = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
