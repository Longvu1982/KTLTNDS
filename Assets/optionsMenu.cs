using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionsMenu : MonoBehaviour
{
    public GameObject optionsMenuObject;


    // Start is called before the first frame update
    void Start()
    {
        optionsMenuObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void showOption()
    {
        optionsMenuObject.SetActive(true);
    }
    public void hideOption()
    {
        optionsMenuObject.SetActive(false);
    }
}
