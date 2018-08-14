using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_scr_K : MonoBehaviour
{

    public GameObject swing, player;
    public GameObject cam , ajiMuzzle;

    public float TAMASpeed = 6.0F;
    public Vector3 TAMAJumpSpeed;
    public int TAMA_HP = 3;
    public float TAMAGravity = 20.0F;
    public float camSpeed = 2.5f;

    public Vector3 Edelta;
    public float enemyDefaultSpeed = 3f;
    public float enemyDespawnDis = 10f;

    public float IwashiActiveDis = 20f;


    public float AjiActiveDis = 20f;
    public float ajiAttackDis = 5f;
    public float AjiAttackTime = 1f;

    public float ManbouActiveDis = 20f;
    public float manbouAttackDis = 5f;



    float y_angle, t;
    private Vector3 offset, Ppos, swingPos;
    private int state;
    private int camState;//(0 : 2D , 1 : 3D , -1 : 回転中)
    public int CamState
    {
        set
        {
            this.camState = value;
        }
        get
        {
            return this.camState;
        }
    }

    void Start()
    {

        Ppos = player.GetComponent<Transform>().position;
        //カメラの初期位置
        //swingPos = swing.GetComponent<Transform>().position;
        swingPos = new Vector3(Ppos.x + 7.8f, Ppos.y + 1.9f, 0.0f);
        //カメラとプレイヤーの距離を取得
        offset = swingPos - Ppos;
        //カメラを２Dにしておく
        cam.GetComponent<Camera>().orthographic = true;


    }

    void Update()
    {
        CameraManager();
    }

    void CameraManager()
    {
        //ジャンプ中でない時にスペースキーを押すと
        if (PlayerMove_scr_K.isJump == false && Input.GetKeyDown(KeyCode.Return))
        {
            camState = -1;//回転中

            if (cam.GetComponent<Camera>().orthographic && state != 1)
            {
                cam.GetComponent<Camera>().orthographic = false;
                state = 1;
            }
            else if (!cam.GetComponent<Camera>().orthographic && state != 2)
            {
                state = 2;
            }
        }

        if (state == 1)
        {
            t = 0;
            if (y_angle < 90f)
            {
                t += Time.deltaTime;
                y_angle += Mathf.Rad2Deg * Time.deltaTime * camSpeed;
                var rot = Quaternion.Euler(0, y_angle, 0);
                swing.transform.rotation = rot;
            }
            else
            {
                swing.transform.rotation = Quaternion.Euler(0, 90, 0);
                camState = 1;//3D
                state = 0;
            }
        }


        else if (state == 2)
        {
            if (y_angle > 0f)
            {
                y_angle -= (Mathf.Rad2Deg * Time.deltaTime * camSpeed);
                var rot = Quaternion.Euler(0, y_angle, 0);
                swing.transform.rotation = rot;
            }
            else
            {
                swing.transform.rotation = Quaternion.Euler(0, 0, 0);
                cam.GetComponent<Camera>().orthographic = true;
                camState = 0;//2D
                state = 0;
            }
        }


        //2D
        if (camState == 0)
        {
            //カメラ追尾（y軸固定）
            swing.GetComponent<Transform>().position = new Vector3(player.transform.position.x + offset.x, swingPos.y, player.transform.position.z + offset.z);
        }
        //3D
        else if (camState == 1)
        {
            //カメラの追尾(fps)
            swing.GetComponent<Transform>().position = player.GetComponent<Transform>().position + offset;
        }
    }
}