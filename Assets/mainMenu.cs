using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    public AudioSource HoverSound;
    public AudioSource ClickSound;
    public AudioSource OpeningScene;
    public Animator transition;
    public float transitionTime = 0f;
    public void PlayGame ()
    {
        transition.SetTrigger("Start");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);      
    }
    //IEnumerator LoadLevel(int LevelIndex)
    //{
    //    transition.SetTrigger("Start");

    //    yield return new WaitForSeconds(transitionTime);

    //    SceneManager.LoadScene(LevelIndex);
    //}
    public void ExitGame ()
    {
        Application.Quit();
    }
    public void onHover()
    {
        HoverSound.Play();
    }
    public void onClick()
    {
        
        ClickSound.Play();
    }
}
