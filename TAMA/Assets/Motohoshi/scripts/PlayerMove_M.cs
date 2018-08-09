using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_M : MonoBehaviour 
{

    public GameObject cam, swing, player;
    private Rigidbody rg;

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
        pos = this.transform.position;//初期座標の取得
        controller = GetComponent<CharacterController>();
        gm = GameObject.Find("GameManeger");
        CamM = gm.GetComponent<GameManager_scr_K>();

        rg = gameObject.GetComponent<Rigidbody>();

    }

    void Update()
    {
        float dz;
        int z2D = 0;
        pos = this.transform.position;
        //Debug.Log(pos);

        if (CamM.CamState == 0)//camstate=0に
        {
            //Debug.Log("2D");

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
                //Debug.Log("ジャンプしていない");
                moveDirection2D = new Vector3(Input.GetAxis("Horizontal"), 0, z2D);
                moveDirection2D *= speed;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }
            moveDirection2D.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection2D * Time.deltaTime);
        }
        else if (CamM.CamState == 1)
        {
            Debug.Log("3D");

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

            moveDirection3D.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection3D * Time.deltaTime);
        }
    }


    public void Jump()
    {
        moveDirection2D.z = 0;
        moveDirection2D.y = jumpSpeed;

        moveDirection3D.y = jumpSpeed;

        Debug.Log("ジャンプ");
        isJump = true;

    }
}