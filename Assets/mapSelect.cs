using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class mapSelect : MonoBehaviour
{
    public MapScene abababa;
    public mapSelect Mapscene;
    public Sprite[] mapImgList = new Sprite[8];
    public Button[] buttons = new Button[8];
    public Animator transition;
    public float transitionTime = 0f;
    public Scene scene;
    public int capturedIterator = 9;
    public int abc = 6;
    public Image spriteRender;
    // Start is called before the first frame update
    void Start()
    {
        spriteRender = GetComponent<Image>();
        scene = SceneManager.GetSceneByBuildIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 8; i++)
        {
            int someIndex = i;
            buttons[i].onClick.AddListener(() => TaskOnClick(someIndex));
        }
        abc++;
        Debug.Log("how " + capturedIterator);
    }
    
    void TaskOnClick(int index)
    {
        spriteRender.sprite = mapImgList[index];
        capturedIterator = index;
        
    }
    public void PlayGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }
    IEnumerator LoadLevel(int LevelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(LevelIndex);
    }
}
