using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class player : MonoBehaviour
{
    public UDPReceive udpReceive;
    public static player playermain;
    public Transform mainCam;
    public Vector3 newcamPos;
    public Vector3 newRot;
    Vector3 initcamPos;
    Vector3 initRot;
    public Transform aimTarget;
    float speed = 3.5f;
    public Vector3 initPos;
    public Vector3 initBallPos;
    Vector3 FPVcamPos;
    // Start is called before the first frame update
    bool hitting;

    Animator animator;
    public Transform ball;
    Vector3 initAimTarget;
    shotManager shotManager;
    Shot currentShot;
    int previousShot;

    bool isFPV;
    public bool isEasy;
    public bool isHT;

    public TextMeshProUGUI FPVText;
    public TextMeshProUGUI HTText;
    public TextMeshProUGUI DifText;
    //[SerializeField] Transform serveRight;
    //[SerializeField] Transform serveLeft;

    //bool servedLeft = true;
    bool isMove;  //for  handtracking

    void Start()
    {
        isFPV = false;
        isEasy = true;
        isHT = false;
        initRot = mainCam.eulerAngles;
        initcamPos = mainCam.position;
        newcamPos = initcamPos;
        newRot = initRot;

        initBallPos = ball.position;
        initPos = transform.position;
        animator = GetComponent<Animator>();
        initAimTarget = aimTarget.position;
        shotManager = GetComponent<shotManager>();
        currentShot = shotManager.topSpin;
        previousShot = 0;   //0 for top spin
    }

    // Update is called once per frame
    void Update()
    {
        // get handtracking data
       
        if(Input.GetKey(KeyCode.Q))
        {
            transform.position += new Vector3(0, 0.5f, 0);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position -= new Vector3(0, 0.5f, 0);
        }
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        // append handtracking to player

        if (isHT)
        {

		// get data from UDPRecieve
            string handData = udpReceive.data;

		// xoá dấu ngoặc vuông ở đầu và cuối
            handData = handData.Remove(0, 1);
            handData = handData.Remove(handData.Length - 1, 1); // remove []   '[100,100,100,100]'

		// convert string về mảng dữ liệu
            string[] points = handData.Split(',');

		// gán toạ độ của hai điểm 0 và 12 ( toạ độ x y ) vào 4 biến
            float bottomFingerX = float.Parse(points[0]) / 100;
            float bottomFingerY = float.Parse(points[1]) / 100;
            float topFingerX = float.Parse(points[2]) / 100;
            float topFingerY = float.Parse(points[3]) / 100;

            float topFingerXOffsetRight = topFingerX + 0.5f;
            float topFingerXOffsetLeft = topFingerX - 0.5f;
            if (topFingerY > bottomFingerY)
            {
                isMove = true;
            }

            if (topFingerY < bottomFingerY)
            {
                isMove = false;
            }
            print(bottomFingerX + "   " + bottomFingerY);

		// transform.position là lấy ra toạ độ của nhân vật

            transform.position = new Vector3(bottomFingerX - 5, transform.position.y, (-2 * bottomFingerY) + 12); // di chuyen

		// khi nghiêng tay thì điểm đánh sẽ sang trái hoặc sang phải
            if (bottomFingerX < topFingerXOffsetLeft && isMove)
            {
                aimTarget.position = new Vector3(2.75f, 0.2f, -6.188f);

            }
            if (bottomFingerX > topFingerXOffsetRight && isMove)
            {
                aimTarget.position = new Vector3(-2.65f, 0.2f, -6.19f);
            }
            if (bottomFingerX >= topFingerXOffsetLeft && bottomFingerX <= topFingerXOffsetRight)
            {
                aimTarget.position = new Vector3(0f, 0.2f, -6.188f);
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            if(previousShot == 1 )
            {
                animator.Play("topSpinShow");
            }
            hitting = true;
            currentShot = shotManager.topSpin;
            previousShot = 0;
        }
        else if(Input.GetKeyUp(KeyCode.F))
        {
            
            hitting = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {

            if(previousShot == 0)
            {
                animator.Play("flatShow");
            }
            
            hitting = true;
            currentShot = shotManager.flat;
            previousShot = 1;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            
            hitting = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = initPos;
            ball.position = initBallPos;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (hitting)
        {
            aimTarget.Translate(new Vector3(h, 0, 0) * speed * 2f * Time.deltaTime);
        }
        // player movement

        // handtracking off - using WASD
        if( (h != 0 || v != 0) && !hitting && !isHT )
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime); 
        }

        // camera

        if (isFPV == false)
        {
            mainCam.position = initcamPos;
            if (transform.position.z > 10.2f)
            {

                newcamPos.z += 0.04f;
                if (newcamPos.z >= 19) newcamPos.z = 19;
                newRot.y -= 0.02f;
                if (newRot.y <= 182) newRot.y = 182f;
            }
            else
            {

                newcamPos.z -= 0.04f;
                if (newcamPos.z <= initcamPos.z) newcamPos.z = initcamPos.z;
                newRot.y += 0.02f;
                if (newRot.y >= 187.585) newRot.y = 187.585f;

            }
            mainCam.position = newcamPos;
            mainCam.eulerAngles = newRot;
        }

        

        if (isFPV == true)
        {
            FPVcamPos.x = transform.position.x;
            FPVcamPos.y = transform.position.y + 1.6f;
            FPVcamPos.z = transform.position.z + 0.8f;
            mainCam.position = FPVcamPos;
            mainCam.eulerAngles = transform.eulerAngles;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball")) {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
            Vector3 ballDir = ball.position - transform.position;
            if(ballDir.x >= 0 )
            {
                animator.Play("rightSwing");
            }else animator.Play("leftSwing");
            aimTarget.position = initAimTarget;
            ball.GetComponent<ball>().hitter = "player";
        }
    }

    public void Reset()
    {
        transform.position = initPos;
    }
    public void changeToFPV()
    {
        if (isFPV == true)
        {
            isFPV = false;
            FPVText.text = "FPV: OFF";
        } else
        {
            isFPV = true;
            FPVText.text = "FPV: ON";
        }
    }
    public void changeToHT()
    {
        if (isHT == true)
        {
            isHT = false;
            HTText.text = "HT: OFF";
        }
        else
        {
            isHT = true;
            HTText.text = "HT: ON";
        }
    }
    public void changeDif()
    {
        if (isEasy == true)
        {
            isEasy = false;
            DifText.text = "HARD";
            speed = 3.2f;
        }
        else
        {
            isEasy = true;
            DifText.text = "EASY";
            speed = 3.8f;
        }
    }
}
