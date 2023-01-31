using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot : MonoBehaviour
{
    public bool isEasy;
    public player playermain;
    float speed = 3.7f;
    Animator animator;
    public Transform ball;
    public Transform aimTarget;
    Vector3 targetPosition;
    shotManager shotManager;
    public Transform[] targets;
    Shot currentShot;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        shotManager = GetComponent<shotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        isEasy = playermain.isEasy;
        if (isEasy) speed = 3.2f;
        else speed = 3.7f;
    }
    void Move()
    {
        targetPosition.x = ball.position.x;
        targetPosition.z = ball.position.z;
        if (targetPosition.z > -8) targetPosition.z = -8;
        if (targetPosition.z < -11) targetPosition.z = -11;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    Vector3 pickTarget()
    {
        int randomValue;
        if (isEasy)
        {
            randomValue = Random.Range(2, targets.Length - 2);

        }else
        {
            randomValue = Random.Range(0, targets.Length);
        }
        return targets[randomValue].position;
    }
    Shot pickShot()
    {
        if (isEasy)
        {
            return shotManager.topSpin;
        }
        else
        {
            int shotValue = Random.Range(0, 2);
            if (shotValue == 0)
            {
                return shotManager.topSpin;
            }
            else return shotManager.flat;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            currentShot = pickShot();
            Vector3 dir = pickTarget() - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);
            Vector3 ballDir = ball.position - transform.position;
            if (ballDir.x >= 0)
            {
                animator.Play("rightSwing");
            }
            else animator.Play("leftSwing");
            ball.GetComponent<ball>().hitter = "bot";
        }
    }
}
