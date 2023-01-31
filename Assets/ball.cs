using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ball : MonoBehaviour
{
    
    Vector3 initialPos;
    public string hitter;
    public int playerScore;
    public int botScore;
    public int playerScoreSet;
    public int botScoreSet;
    int playerScoreIndex = 0;
    int botScoreIndex = 0;
    
    
    public int hitIndex;
    public int[] scores = { 0, 15, 30, 40 };
    public bool winning = false;
    public bool isWin = false;
    public bool isLose = false;
    public bool GameWin = false;

    public GameObject outScreen;
    Animator OutAnimator;

    //audio
    public AudioSource BallHitPlayerRacket;
    public AudioSource BallHitBotRacket;
    public AudioSource BallHitGround;
    public AudioSource OutEffectSound;

    [SerializeField] Text playerTextScore;
    [SerializeField] Text botTextScore;
    [SerializeField] Text playerTextSet;
    [SerializeField] Text botTextSet;
    

    void Start()
    {
        outScreen.SetActive(false);
        OutAnimator = outScreen.GetComponent<Animator>();
        initialPos = transform.position;
        playerScore = 0;
        botScore = 0;
        playerScoreSet = 0;
        botScoreSet = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -15 || transform.position.z > 15)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = initialPos;
            GameObject.Find("player").GetComponent<player>().Reset();
            winning = true;
        }
        
    }
    void updateScore()
    {
        playerTextScore.text = "" + playerScore;
        botTextScore.text = "" + botScore;
        playerTextSet.text = "" + playerScoreSet;
        botTextSet.text = "" + botScoreSet;
    }
    void result()
    {
        if(playerScoreSet == 2)
        {
            GameWin = true;
            Debug.Log(GameWin);
            playerScoreSet = 0;
            botScoreSet = 0;
        }
        if(botScoreSet == 2)
        {
            GameWin = false;
            Debug.Log(GameWin);
            playerScoreSet = 0;
            botScoreSet = 0;
        }
        
    }
    public void playerScoreCal()
    {
        playerScoreIndex++;
        if (playerScoreIndex > 3)
        {
            playerScoreIndex = 0;
            botScoreIndex = 0;
            botScore = 0;
            isWin = true;
            playerScoreSet++;
        }
        playerScore = scores[playerScoreIndex];
    }
    public void botScoreCal()
    {
        botScoreIndex++;
        if (botScoreIndex > 3)
        {
            botScoreIndex = 0;
            playerScoreIndex = 0;
            playerScore = 0;
            isLose = true;
            botScoreSet++;
        }
        botScore = scores[botScoreIndex];
    }
    public void BallAndPlayerReset()
    {
        GameObject.Find("player").GetComponent<player>().Reset();
        transform.position = initialPos;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            BallHitPlayerRacket.Play();

        }

        if (other.CompareTag("bot"))
        {
            BallHitBotRacket.Play();

        }
        if (other.CompareTag("out")|| other.CompareTag("in"))
        {
            BallHitGround.Play();

        }
        if (hitter == "bot")
        {
            
            winning = false;
            if (other.CompareTag("wallout"))
            {
                winning = true;
            }
            hitIndex = 1;
            if (other.CompareTag("in"))
            {
                hitIndex++;
                
            }
            if ((hitIndex == 1 && other.CompareTag("out")) || other.CompareTag("net"))
            {
                playerScoreCal();
                result();
                updateScore();
                Invoke("BallAndPlayerReset", 0);
                outScreen.SetActive(true);
                OutAnimator.Play("out");
                OutEffectSound.Play();
               
            }
            if (winning)
            {
                Debug.Log("getScore");
                botScoreCal();
                result();
                updateScore();
                Invoke("BallAndPlayerReset", 0);
            }


        }


        if (hitter == "player")
        {
            
            //winning = false;
            winning = false;
            if (other.CompareTag("wallout"))
            {
                winning = true;
            }
            hitIndex = 1;
            if (other.CompareTag("in"))
            {
                hitIndex++;
            }
            if ((hitIndex == 1 && other.CompareTag("out")) || other.CompareTag("net"))
            {
                botScoreCal();
                result();
                updateScore();              
                Invoke("BallAndPlayerReset", 0);
                outScreen.SetActive(true);
                OutAnimator.Play("out");
                OutEffectSound.Play();
                
            }
            if (winning)
            {
                playerScoreCal();
                result();
                updateScore();

                Invoke("BallAndPlayerReset", 0);
            }

        }
        

    }

}
