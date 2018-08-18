using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_M : MonoBehaviour
{

    private GameObject gamemanager;
    GameManager_M Game_M;
    //ライフマネージャー取得
    private GameObject canbas;
    LifeManager_M Life_M;

    private float speed, gravity, dz;
    public static bool is2D = true;
    public static bool isJump = false;
    public static bool isBack = false;
    public static bool isStop = false;
    public static bool isZensin = false;
    public static bool isGround;
    public static bool isDead = false;
    public bool isKill = false;
    public bool force_z = true;
    private Vector3 jumpSpeed;
    private bool InputKey = true;

    private float mutekiTime;

    private Vector3 moveDirection2D, moveDirection3D, pos, mousePos;
    CharacterController controller;
    //アニメーター宣言１
    Animator animator;
    int t;
    float timer;


    ///////////////////////////////////////////////////////
    AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();
    ///////////////////////////////////////////////////////


    void Start()



    {
        //ゲームマネージャー取得
        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_M>();
        speed = 6.0f;
        jumpSpeed = Game_M.TAMAJumpSpeed;
        gravity = Game_M.TAMAGravity;

        mutekiTime = 2.0f;
        //初期座標の取得
        pos = this.transform.position;

        //ライフマネージャー取得
        canbas = GameObject.Find("Canvas");
        Life_M = canbas.GetComponent<LifeManager_M>();

        //キャラコンの取得
        controller = GetComponent<CharacterController>();
        //アニメーター宣言２
        animator = GetComponent<Animator>();

        isDead = false;
        isStop = false;

        ///////////////////////////////////////////
        audioSource = GetComponent<AudioSource>();
        //////////////////////////////////////////

    }

    void Update()
    {



        if (Input.GetKeyDown(KeyCode.C))
        {

            animator.SetBool("is_Sleeping", false);

        }

        if (isDead == false)
        {
            pos = this.transform.position;

            if (this.transform.position.y <= -8 && Mathf.Abs(this.transform.position.z) <= 5)
            {
                DropOut(50);
            }
            else if (this.transform.position.y <= -8)
            {
                DropOut(0);
            }


            if (Game_M.CamState == 0)//camstate=0に
            {


                Walk_2D();

            }

            else if (Game_M.CamState == 1)
            {


                Walk_3D();
            }

        }
    }




    public void Walk_2D()
    {


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
            isKill = false;
            gravity = Game_M.TAMAGravity;
            //ジャンプしていない
            isJump = false;
            isGround = true;

            animator.SetBool("is_Jump", false);
            animator.SetBool("is_BackJump", false);
            animator.SetBool("is_UpJump", false);


            moveDirection2D = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            moveDirection2D *= speed;

            if ((Input.GetKeyDown(KeyCode.JoystickButton14) || Input.GetKeyDown(KeyCode.Space)) && InputKey)
            {
                Jump();
                InputKey = false;
                Invoke("JumpInterval", 1.0f);
            }



        }
        else
        {

            isGround = false;
            //空中でもx軸移動可能にする
            moveDirection2D.x = Input.GetAxis("Horizontal");
            moveDirection2D.z = 0;
            moveDirection2D.x = moveDirection2D.x * speed * jumpSpeed.x;
        }

        //アニメーション
        if (moveDirection2D.x == 0)
        {   //止まっている時

            animator.SetBool("is_Walk", false);
            animator.SetBool("is_Back", false);
            animator.SetBool("is_Idle", true);
            isBack = false;
            isZensin = false;

            //////////
            timer = 0;
            ///////////



        }
        else if (moveDirection2D.x < 0)
        {   //後退
            Back();
            if (isGround)
            {
                //////////////////////////////////////////////
                // StartCoroutine("SE", 0);
                PlaySE(0);
                //////////////////////////////////////////////
            }
        }
        else if (moveDirection2D.x > 0)
        {   //前進
            isZensin = true;////
            isBack = false;
            animator.SetBool("is_Back", false);
            animator.SetBool("is_Walk", true);
            if (isGround)
            {
                //////////////////////////////////////////////
                // StartCoroutine("SE", 0);
                PlaySE(0);
                //////////////////////////////////////////////
            }
        }

        if (isStop)
        {
            isZensin = false;
            moveDirection2D.x = 0;
            moveDirection2D.z = 0;
        }

        moveDirection2D.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection2D * Time.deltaTime);

    }


    public void Walk_3D()
    {

        if (controller.isGrounded)
        {
            //地面についている時
            isGround = true;
            isKill = false;
            gravity = Game_M.TAMAGravity;

            //ジャンプしていない
            isJump = false;
            animator.SetBool("is_Jump", false);
            animator.SetBool("is_BackJump", false);
            animator.SetBool("is_UpJump", false);


            moveDirection3D = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal") * -1);
            moveDirection3D *= speed;

            if ((Input.GetKeyDown(KeyCode.JoystickButton14) || Input.GetKeyDown(KeyCode.Space)) && InputKey)
            {
                Jump();
                InputKey = false;
                Invoke("JumpInterval", 1.0f);

            }
        }
        else//空中にいる時
        {
            isGround = false;
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
            isZensin = false;
            animator.SetBool("is_Walk", false);
            animator.SetBool("is_Back", false);
            //////////
            timer = 0;
            ///////////
        }

        else if (moveDirection3D.x < 0)
        {
            Back();
            if (isGround)
            {
                //////////////////////////////////////////////
                // StartCoroutine("SE", 0);
                PlaySE(0);
                //////////////////////////////////////////////
            }
        }

        else if (moveDirection3D.x > 0 || moveDirection3D.z > 0 || moveDirection3D.z < 0)
        {
            //前進
            isZensin = true;////
            isBack = false;
            animator.SetBool("is_Back", false);
            animator.SetBool("is_Walk", true);
            if (isGround)
            {
                //////////////////////////////////////////////
                // StartCoroutine("SE", 0);
                PlaySE(0);
                //////////////////////////////////////////////
            }
        }
        if (isStop)
        {
            isZensin = false;
            moveDirection3D.x = 0;
            moveDirection3D.z = 0;
        }

        //重力換算
        moveDirection3D.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection3D * Time.deltaTime);
    }


    public void Jump()
    {
        if (isStop == false)
        {
            isGround = false;
            isJump = true;


            if (isZensin == false && isBack == false)
            {
                animator.SetBool("is_UpJump", true);

                if (isKill == false)
                {
                    //////////////////////////////////////////////
                    audioSource.PlayOneShot(audioClip[1]);
                    //////////////////////////////////////////////
                }

                jumpSpeed.x = 0.5f;
                jumpSpeed.z = 0.5f;
                Debug.Log("|||ジャンプ|||");
            }
            else if (isZensin && isBack == false)
            {
                animator.SetBool("is_Jump", true);

                if (isKill == false)
                {
                    //////////////////////////////////////////////
                    audioSource.PlayOneShot(audioClip[1]);
                    //////////////////////////////////////////////
                }

                jumpSpeed.x = 1f;
                jumpSpeed.z = 1f;
                Debug.Log("ジャンプ>>>>>");
            }
            else if (isZensin == false && isBack)
            {
                animator.SetBool("is_BackJump", true);

                if (isKill == false)
                {
                    //////////////////////////////////////////////
                    audioSource.PlayOneShot(audioClip[1]);
                    //////////////////////////////////////////////
                }

                jumpSpeed.x = 1f;
                jumpSpeed.z = 1f;
                Debug.Log("<<<<<ジャンプ");
            }

            moveDirection2D.y = jumpSpeed.y;
            moveDirection3D.y = jumpSpeed.y;
        }
    }

    public void JumpInterval()
    {
        InputKey = true;
        Debug.Log("jumpIntervalCalled");
    }


    public IEnumerator Damage()
    {

        isStop = true;

        Life_M.isMuteki = true;

        Debug.Log("ダメージ");
        animator.SetBool("is_Damage", true);
        //////////////////////////////////////////////
        audioSource.PlayOneShot(audioClip[2]);
        //////////////////////////////////////////////


        yield return new WaitForSeconds(0.25f);
        //animator.SetBool("is_Idle", true);
        animator.SetBool("is_Damage", false);

        yield return new WaitForSeconds(0.5f);
        isStop = false;

        yield return new WaitForSeconds(mutekiTime - 0.75f);
        Life_M.isMuteki = false;

    }

    public void Back()
    {
        isZensin = false;
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
        Invoke("GameOver", 1f);
    }


    public void Kill()
    {
        Debug.Log("敵を倒した！");
        animator.Play("TAMA_jump", 0, 0.0f);
        isKill = true;
        //////////////////////////////////////////////
        audioSource.PlayOneShot(audioClip[3]);
        //////////////////////////////////////////////

    }

    public void DropOut(int n)
    {
        isStop = true;
        Invoke("Life_M_Damage", 1.5f);
        this.transform.position = new Vector3(this.transform.position.x - n, 30.0f, 0.0f);
    }
    public void Life_M_Damage()
    {
        isStop = false;
        Life_M.Damage();
    }
    public void GameOver()
    {
        isStop = true;
        InputKey = false;
        isDead = true;
        Destroy(Life_M);

    }

    //public IEnumerator SE(int audio_num)
    //{
    //    float audioLengs = audioClip[audio_num].length;
    //    //audioSource.PlayOneShot(audioClip[audio_num]);
    //    yield return new WaitForSeconds(audioLengs);
    //    PlaySE(audio_num);

    //}
    //public void PlaySE(int audio_num){
    //    audioSource.PlayOneShot(audioClip[audio_num]);
    //}

    public void PlaySE(int audio_num)
    {
        float audioLengs = audioClip[audio_num].length;

        if (timer == 0)
        {
            timer = 60 * audioLengs - 1;
        }

        timer++;
        if (timer >= 60 * audioLengs)
        {
            timer = 1;
            audioSource.PlayOneShot(audioClip[audio_num]);
        }

    }

}