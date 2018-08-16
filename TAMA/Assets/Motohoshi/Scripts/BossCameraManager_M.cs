using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraManager_M : MonoBehaviour {
    public GameObject BossCam, BossSwing;
    Vector3 Position2D = new Vector3(0,0,-28.7f);
    Vector3 Position3D = new Vector3(0,16.8f,-83.2f);
    Quaternion Rotation2D = Quaternion.Euler(1.75f, 0, 0);
    Quaternion Rotation3D = Quaternion.Euler(20, 0, 0);
    int state,camState;
    float y_angle, t;
    public float camSpeed = 2.5f;
	// Use this for initialization
	void Start () {
        BossCam.GetComponent<Camera>().orthographic = true;
        state = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CameraManager()
    {
        //ジャンプ中でない時にスペースキーを押すと
        if (PlayerMove_M.isJump == false && Input.GetKeyDown(KeyCode.Return))
        {
            camState = -1;//回転中

            if (BossCam.GetComponent<Camera>().orthographic && state != 1)
            {
                BossCam.GetComponent<Camera>().orthographic = false;
                BossCam.transform.position = Position3D;
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
}
