using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_scr_K : MonoBehaviour
{

    public GameObject cam, swing, player;
    private Rigidbody rg;
    Animator animator;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public static bool is2D = true;
    public static bool isJump = false;
    private Vector3 moveDirection2D = Vector3.zero;
    private Vector3 moveDirection3D = Vector3.zero;
    private Vector3 mousePos;
    CharacterController controller;
    private Vector3 pos;
    private GameObject gm;
    GameManager_scr_K CamM;


    void Start()
    {
        animator = GetComponent<Animator>();

        pos = this.transform.position;//初期座標の取得
        controller = GetComponent<CharacterController>();
        gm = GameObject.Find("GameManeger");
        CamM = gm.GetComponent<GameManager_scr_K>();

        rg = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        pos = this.transform.position;

        if (CamM.CamState == 0)//camstate=0に
        {
            Walk_2D();
        }

        else if(CamM.CamState == 1)
        {
            Walk_3D();
        }
    }




    public void Walk_2D(){
        float dz;
        int z2D = 0;

        //z軸補正
        dz = pos.z;
        if (pos.z != 0)
        {
            if (dz < 0)
            {
                z2D = 1;
            }
            if (dz > 0)
            {
                z2D = -1;
            }
        }
        else
        {
            z2D = 0;
        }

        if (controller.isGrounded)
        {
            isJump = false;
            moveDirection2D = new Vector3(Input.GetAxis("Horizontal"), 0, z2D);
            moveDirection2D *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {//空中でもx軸移動可能にする
            moveDirection2D.x = Input.GetAxis("Horizontal");
            moveDirection2D.z = 0;
            moveDirection2D.x *= speed;
        }
        moveDirection2D.y -= gravity * Time.deltaTime;

        if(moveDirection2D.x == 0){//止まっている時
            animator.SetBool("is_Walk", false);
            animator.SetBool("is_Back", false);
        }
        else if (moveDirection2D.x < 0)//後退
        {
            Back();
        }
        else{//前進
            animator.SetBool("is_Back", false);
            animator.SetBool("is_Walk", true);
        }

        controller.Move(moveDirection2D * Time.deltaTime);
    }

    public void Walk_3D(){
       
        if (controller.isGrounded)
        {
            isJump = false;
            moveDirection3D = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal") * -1);
            moveDirection3D *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {//空中でもx、z軸移動可能にする
            moveDirection3D.x = Input.GetAxis("Vertical");
            moveDirection3D.z = Input.GetAxis("Horizontal") * -1;
            moveDirection3D.x *= speed;
            moveDirection3D.z *= speed;
        }

        moveDirection3D.y -= gravity * Time.deltaTime;

        if (moveDirection3D.x == 0 && moveDirection3D.z == 0)
        {
            //止まっている時
            animator.SetBool("is_Walk", false);
            animator.SetBool("is_Back", false);
        }
        else if (moveDirection3D.x < 0 )
        {
            Back();
        }
        else if(moveDirection3D.x > 0 || moveDirection3D.z > 0)
        {//前進
            animator.SetBool("is_Back", false);
            animator.SetBool("is_Walk", true);
        }
        controller.Move(moveDirection3D * Time.deltaTime);
    }

    public void Jump()
    {
        Debug.Log("ジャンプ");
        isJump = true;

        moveDirection2D.z = 0;
        moveDirection2D.y = jumpSpeed;

        moveDirection3D.y = jumpSpeed;
    }

    public void Damage()
    {
        Debug.Log("ダメージ");

    }

    public void Back()
    {
        Debug.Log("後退");
        moveDirection2D.x *= 0.5f;
        moveDirection3D.x *= 0.5f;
        moveDirection3D.z *= 0.5f;
        animator.SetBool("is_Back", true);
    }

    public void Run()
    {
        Debug.Log("走る");
    }

    public void Die()
    {
        Debug.Log("ゲームオーバー");
        animator.SetBool("is_Die", true);
    }
}