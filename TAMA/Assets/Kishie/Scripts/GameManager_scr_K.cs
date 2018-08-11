using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_scr_K : MonoBehaviour {

    public GameObject camera, swing, player;
    private Vector3 offset,swingPos;
    float y_angle, t;
    float force = 10f, speed = 2.5f;
    public int HP = 3;
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
        //カメラとプレイヤーの距離
        offset = swingPos - player.GetComponent<Transform>().position;
     

        camera.GetComponent<Camera>().orthographic = true;
        speed = 2.5f;
        force = 10;
    }

    void Update()
    {
        CameraManager();

    }

    void CameraManager(){
        //ジャンプ中でない時にスペースキーを押すと
        if (PlayerMove_scr_K.isJump == false && Input.GetKeyDown(KeyCode.Return))
        {
            camState = -1;
            if (camera.GetComponent<Camera>().orthographic && state != 1)
            {
                camera.GetComponent<Camera>().orthographic = false;
                state = 1;
            }
            else if (!camera.GetComponent<Camera>().orthographic && state != 2)
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
                y_angle += Mathf.Rad2Deg * Time.deltaTime * speed;
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
                //Debug.Log (Time.time);
                y_angle -= (Mathf.Rad2Deg * Time.deltaTime * speed);
                var rot = Quaternion.Euler(0, y_angle, 0);
                swing.transform.rotation = rot;
            }
            else
            {
                swing.transform.rotation = Quaternion.Euler(0, 0, 0);
                camera.GetComponent<Camera>().orthographic = true;
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
