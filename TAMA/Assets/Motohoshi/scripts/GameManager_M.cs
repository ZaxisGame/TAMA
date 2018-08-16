using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_M: MonoBehaviour {

    public GameObject BossCam, BossSwing;
    Vector3 Position2D = new Vector3(83, 10, -28.7f);
    Vector3 Position3D = new Vector3(-0.2f, 26.8f, 0);
    Quaternion Rotation2D = Quaternion.Euler(1.75f, 0, 0);
    Quaternion Rotation3D = Quaternion.Euler(20, 0, 0);

    public GameObject swing, player;
    public GameObject cam;
    public float TAMAspeed = 6.0F;
    public Vector3 TAMAJumpSpeed = new Vector3(1, 8, 1);
    public float TAMAGravity = 20.0F;
    public int TAMA_HP = 3;
    public float camSpeed = 2.5f;
    public float enemyDefaultSpeed = 3f;
    int isBoss;

    float y_angle, t;
    private Vector3 offset, swingPos;
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
        //カメラの位置
        isBoss = 0;
        swingPos = swing.GetComponent<Transform>().position;
        //カメラとプレイヤーの距離を取得
        offset = swingPos - player.GetComponent<Transform>().position;
        //カメラを２Dにしておく
        cam.GetComponent<Camera>().orthographic = true;
    }

    void Update()
    {
        if (isBoss == 0)
        {
            CameraManager();
        }else if(isBoss ==2){
            BossCameraManager();
        }
        if(isBoss==0&&player.transform.position.x>106){
            isBoss = 1;
            camState = 0;
        }
        if(BossCam.GetComponent<Camera>().enabled){
            isBoss = 2;
        }
    }

    void BossCameraManager()
    {
        //ジャンプ中でない時にスペースキーを押すと
        if (PlayerMove_M.isJump == false && Input.GetKeyDown(KeyCode.Return))
        {
            camState = -1;//回転中

            if (BossCam.GetComponent<Camera>().orthographic && state != 1)
            {
                BossCam.GetComponent<Camera>().orthographic = false;
                BossCam.transform.position = new Vector3(Position2D.x, Position3D.y, Position3D.x - Position2D.x);
                BossCam.transform.rotation = Rotation3D;
                state = 1;
            }
            else if (!BossCam.GetComponent<Camera>().orthographic && state != 2)
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
                BossSwing.transform.rotation = rot;
            }
            else
            {

                BossSwing.transform.rotation = Quaternion.Euler(0, 90, 0);
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
                BossSwing.transform.rotation = rot;
            }
            else
            {
                BossCam.transform.position = Position2D;
                BossCam.transform.rotation = Rotation2D;
                BossSwing.transform.rotation = Quaternion.Euler(0, 0, 0);
                BossCam.GetComponent<Camera>().orthographic = true;
                camState = 0;//2D
                state = 0;
            }
        }
    }

    void CameraManager(){
        //ジャンプ中でない時にスペースキーを押すと
        if (PlayerMove_M.isJump == false && Input.GetKeyDown(KeyCode.Return))
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
            swing.GetComponent<Transform>().position = new Vector3(player.transform.position.x, swingPos.y, player.transform.position.z);
        }
        //3D
        else if (camState == 1)
        {
            //カメラの追尾(fps)
            swing.GetComponent<Transform>().position = new Vector3(player.transform.position.x, swingPos.y, player.transform.position.z);
        }
    }
}        
