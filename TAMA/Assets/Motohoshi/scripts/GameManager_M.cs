using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_M : MonoBehaviour
{

    public GameObject swing, player;
    public GameObject cam;
    public float TAMAspeed = 6.0F;
    public Vector3 TAMAJumpSpeed = new Vector3(1, 8, 1);
    public float TAMAGravity = 20.0F;
    public int TAMA_HP = 3;
    public float camSpeed = 2.5f;
    public float enemyDefaultSpeed = 3f;

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
        swingPos = swing.GetComponent<Transform>().position;
        //カメラとプレイヤーの距離を取得
        offset = swingPos - player.GetComponent<Transform>().position;
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
