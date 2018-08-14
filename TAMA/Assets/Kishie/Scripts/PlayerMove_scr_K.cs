using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_scr_K : MonoBehaviour
{
    
    private GameObject gamemanager;
    GameManager_scr_K Game_M;

    private float speed , gravity , dz;
    public static bool is2D = true;
    public static bool isJump = false;
    public static bool isBack = false;
    public static bool isStop = false;
    public bool force_z = true;
    private Vector3 jumpSpeed;

    //public static bool isDrive = false;
    private Vector3 moveDirection2D , moveDirection3D , pos , mousePos;
    CharacterController controller;
    //アニメーター宣言１
    Animator animator;

    void Start()
    {
        //ゲームマネージャー取得
        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_scr_K>();
        speed = Game_M.TAMASpeed;
        jumpSpeed = Game_M.TAMAJumpSpeed;
        gravity = Game_M.TAMAGravity;
        //初期座標の取得
        pos = this.transform.position;
        //キャラコンの取得
        controller = GetComponent<CharacterController>();
        //アニメーター宣言２
        animator = GetComponent<Animator>();

        isStop = false;

    }

    void Update()
    {
       
            pos = this.transform.position;

            if (Game_M.CamState == 0)//camstate=0に
            {
            
           
                Walk_2D();
            }

            else if (Game_M.CamState == 1)
            {
                Walk_3D();
            }

    }




    public void Walk_2D(){


        //z軸修正
        if (force_z)
        {
            dz = this.transform.position.z;
            if (dz > 0f)
            {
                dz -= 0.1f;
                if (dz <= 0f)
                {
                    dz = 0f;
                }
            }
            else if (dz < 0f)
            {
                dz += 0.1f;
                if (dz >= 0f)
                {

                    dz = 0f;
                }
            }
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, dz);
        }


        if (controller.isGrounded)
        {
                //ジャンプしていない
                isJump = false;

                animator.SetBool("is_Jump", false);
                animator.SetBool("is_BackJump", false);
                animator.SetBool("is_UpJump", false);
                //animator.SetBool("is_Idle", true);
                moveDirection2D = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                moveDirection2D *= speed;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
        }
        else
        {     //空中でもx軸移動可能にする
                moveDirection2D.x = Input.GetAxis("Horizontal");
                moveDirection2D.z = 0;
                moveDirection2D.x = moveDirection2D.x * speed * jumpSpeed.x;
        }
      
        //アニメーション
        if(moveDirection2D.x == 0)
        {   //止まっている時
            
            animator.SetBool("is_Walk", false);
            animator.SetBool("is_Back", false);
            animator.SetBool("is_Idle", true);
            isBack = false;
        }
        else if (moveDirection2D.x < 0 )
        {   //後退
            Back();
        
        }
        else if(moveDirection2D.x > 0)
        {   //前進
            
            isBack = false;
            animator.SetBool("is_Back", false);
            animator.SetBool("is_Walk", true);
        }

        if(isStop){
            
            moveDirection2D.x = 0;
            moveDirection2D.z = 0;
        }

        moveDirection2D.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection2D * Time.deltaTime);

    }


    public void Walk_3D(){
       
        if (controller.isGrounded)//地面についている時
        {
            //ジャンプしていない
            isJump = false;
            animator.SetBool("is_Jump", false);
            animator.SetBool("is_BackJump", false);
            animator.SetBool("is_UpJump", false);
            animator.SetBool("is_Idle", true);

            moveDirection3D = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal") * -1);
            moveDirection3D *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else//空中にいる時
        {
            //空中でもx、z軸移動可能にする
            moveDirection3D.x = Input.GetAxis("Vertical");
            moveDirection3D.z = Input.GetAxis("Horizontal") * -1;
            moveDirection3D.x = moveDirection3D.x * speed * jumpSpeed.x;
            moveDirection3D.z = moveDirection3D.z * speed * jumpSpeed.z;
        }
       

        if (moveDirection3D.x == 0 && moveDirection3D.z == 0)
        {
            //止まっている時
            isBack = false;
            animator.SetBool("is_Walk", false);
            animator.SetBool("is_Back", false);
           
        }

        else if (moveDirection3D.x < 0 )
        {
            Back();
        }

        else if(moveDirection3D.x > 0 || moveDirection3D.z > 0 || moveDirection3D.z < 0)
        {
            //前進
            isBack = false;
            animator.SetBool("is_Back", false);
            animator.SetBool("is_Walk", true);
        }
        if (isStop)
        {
            moveDirection3D.x = 0;
            moveDirection3D.z = 0;
        }

        //重力換算
        moveDirection3D.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection3D * Time.deltaTime);
    }


    public void Jump()
    {
        Debug.Log("ジャンプ");
        isJump = true;

        if(isJump && ((is2D && moveDirection2D.x == 0) || (is2D == false &&(moveDirection3D.x == 0 || moveDirection3D.z == 0)))){
            animator.SetBool("is_UpJump", true);
            jumpSpeed.x = 0.5f;
            jumpSpeed.z = 0.5f;

        }
        else if (isJump && isBack == false )
        {
            animator.SetBool("is_Jump", true);
            jumpSpeed.x = 1f;
            jumpSpeed.z = 1f;

        }
        else if(isJump && isBack )
        {
            animator.SetBool("is_BackJump", true);
            jumpSpeed.x = 1f;
            jumpSpeed.z = 1f;
        }
       
        moveDirection2D.y = jumpSpeed.y;
        moveDirection3D.y = jumpSpeed.y;

    }


    public IEnumerator Damage()
    {
        isStop = true;
        Debug.Log("ダメージ");
        animator.SetBool("is_Damage", true);

        yield return new WaitForSeconds(0.25f);
        //animator.SetBool("is_Idle", true);
        animator.SetBool("is_Damage", false);
        yield return new WaitForSeconds(0.5f);
        isStop = false;
    }

    public void Back()
    {
        isBack = true;
    
        Debug.Log("後退");
        moveDirection2D.x *= 0.5f;
        moveDirection3D.x *= 0.5f;
        moveDirection3D.z *= 0.5f;
        animator.SetBool("is_Back", true);
    }



    public void Die()
    {
        Debug.Log("ゲームオーバー");
        animator.SetBool("is_Die", true);
        isStop = true;
    }


    public void Kill(){
        Debug.Log("敵を倒した！");
        animator.Play("TAMA_jump", 0, 0.0f);
    }

}