﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_scr_K : MonoBehaviour
{
    private GameObject gamemanager;
    GameManager_scr_K Game_M;

    public int state;
    public float manbouDis = 15f;

    private float enemySpeed;
    public static float dis;
    public static bool isAlive = true;//これ消す予定
    public bool is_Alive = true;
    private GameObject player;
    Vector3 Epos, Ppos;



    void Start()
    {
        //ゲームマネージャー取得
        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_scr_K>();
        enemySpeed = Game_M.enemyDefaultSpeed;
        player = Game_M.player;
                           
        Epos = this.transform.position;//初期座標の取得
    }


    void Update()
    {
        Move();
    }

    public void Move()
    {

        if (state == 0)
        {//ビームなど
            return;
        }

        else if (state == 1)
        {//アジ
            this.transform.position = Epos;
            Epos.x += enemySpeed * -0.01f;
        }

        else if (state == 2)
        {//マンボウ
            this.transform.position = Epos;
            //プレイヤーとの距離を取得
            Ppos = player.transform.position;
            dis = Vector3.Distance(Epos, Ppos);

            //近ずくと止まって、、
            if (dis <= manbouDis)
            {
                //Debug.Log("マンボウ接近");
                //ビーム
            }
            else if (dis > manbouDis){
                Epos.x += enemySpeed * -0.01f;
            }
            else
            {
                Debug.Log("ERROR");
                return;
            }

        }
        else
        {
            Debug.Log("ERROR");
            state = 0;
        }

    }

}
