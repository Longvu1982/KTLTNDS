using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    public Vector3 newcamPos;
    public Vector3 newRot;
    Vector3 initcamPos;
    Vector3 initRot;
    // Start is called before the first frame update
    void Start()
    {
        initRot = transform.eulerAngles ;
        initcamPos = transform.position;
        newcamPos= initcamPos;
        newRot = initRot;
        Debug.Log(initRot);
        Debug.Log(initcamPos);
    }

    // Update is called once per frame
    void Update() 
    {
        if (player.position.z > 10.2f)
        {
            
            newcamPos.z += 0.02f;
            if (newcamPos.z >= 19) newcamPos.z = 19;
            newRot.y -= 0.01f;
            if(newRot.y <= 182) newRot.y = 182f;
        }
        else
        {
            
            newcamPos.z -= 0.02f;
            if (newcamPos.z <= initcamPos.z) newcamPos.z = initcamPos.z;
            newRot.y += 0.01f;
            if (newRot.y >= 187.585) newRot.y = 187.585f;

        }
        transform.position = newcamPos;
        transform.eulerAngles = newRot;

    }
}
