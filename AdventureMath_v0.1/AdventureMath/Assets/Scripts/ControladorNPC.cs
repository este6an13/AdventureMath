using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNPC : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rigidBody2D;
    public bool isWalking;
    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;
    private int walkDirection;

    [SerializeField]
    private Animator animador;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        walkCounter = walkTime;
        waitCounter = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    rigidBody2D.velocity = new Vector2(0, moveSpeed);
                    animador.SetFloat("x", new Vector2(0, moveSpeed).x);
                    animador.SetFloat("y", new Vector2(0, moveSpeed).y);
                    break;
                case 1:
                    rigidBody2D.velocity = new Vector2(moveSpeed, 0);
                    animador.SetFloat("x", new Vector2(moveSpeed, 0).x);
                    animador.SetFloat("y", new Vector2(moveSpeed, 0).y);
                    break;
                case 2:
                    rigidBody2D.velocity = new Vector2(0, -moveSpeed);
                    animador.SetFloat("x", new Vector2(0, -moveSpeed).x);
                    animador.SetFloat("y", new Vector2(0, -moveSpeed).y);
                    break;
                case 3:
                    rigidBody2D.velocity = new Vector2(-moveSpeed, 0);
                    animador.SetFloat("x", new Vector2(-moveSpeed, 0).x);
                    animador.SetFloat("y", new Vector2(-moveSpeed, 0).y);
                    break;
            }

            if (walkCounter < 0)
            {
                isWalking = false;
                animador.SetLayerWeight(1, 0);
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            rigidBody2D.velocity = Vector2.zero;
            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
        
    }

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        animador.SetLayerWeight(1, 1);
        walkCounter = walkTime;
    }
}
