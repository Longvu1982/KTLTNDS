using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuObject;
    public GameObject ScoreSection;
    public GameObject mainCam;
    public Transform player;
    public Transform mainCammove;
    Animator animator;
    void Start()
    {
        pauseMenuObject.SetActive(false);
        animator = mainCam.GetComponent<Animator>();
        animator.enabled = false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }else
            {
                Pause();
                Debug.Log(mainCam.transform.eulerAngles);
            }
        }
    }

    public void Resume ()
    {
        animator.enabled = false;
        pauseMenuObject.SetActive(false);
        ScoreSection.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        mainCammove.position = new Vector3(player.position.x, transform.position.y + 1.5f, transform.position.z + 0.1f);
    }
    void Pause()
    {
        animator.enabled = true;
        animator.Play("pauseCam");
        pauseMenuObject.SetActive(true);
        ScoreSection.SetActive(false);
        Time.timeScale = 0.05f;
        isPaused = true;
       
        

    }
}
