﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager_scr_K : MonoBehaviour {
    
    //ゲームマネージャー取得
    private GameObject gamemanager;
    GameManager_scr_K Game_M;

    private int HP;
    private GameObject[] lifesObj;
    private GameObject player;
    PlayerMove_scr_K pMove;


    private int currentLife;
    public bool isMuteki = false;
  
    void Start()
    {
        //ゲームマネージャー取得


        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_scr_K>();
        player = Game_M.player;
        HP = Game_M.TAMA_HP;
        currentLife = HP;//最初の体力はmax

        pMove = player.GetComponent<PlayerMove_scr_K>();

        lifesObj = new GameObject[HP];



        for (int i = 0; i < HP; i++)
        {
            lifesObj[i] = new GameObject("cat" + i);
            lifesObj[i].transform.parent = gameObject.transform;
            lifesObj[i].AddComponent<RectTransform>().anchoredPosition = new Vector2(-300 + 100 * i, -180);
            lifesObj[i].GetComponent<RectTransform>().localScale = new Vector3(0.05f, 0.05f, 0.05f);
            lifesObj[i].AddComponent<Image>().sprite = Resources.Load<Sprite>("Images_M/cat_icon");
            lifesObj[i].GetComponent<Image>().preserveAspect = true;
            lifesObj[i].GetComponent<Image>().SetNativeSize();
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Damage();
        }
    }

    public void Damage()
    {
       
        if (isMuteki == false)
        {

            //現在の体力を減らす
            currentLife--;
            //今の体力を引数にする
            DrawLife(currentLife);
            ///////////プレイヤーにダメージ////////////
            StartCoroutine(pMove.Damage());
        }

        if (currentLife == 0)
            {
                pMove.Die();
                //SceneManager.LoadScene("GameOver");
            }
        
    }



    void DrawLife(int n)//nは今の体力
    {
        for (int i = 0; i < HP; i++)
        {
            if (i < n)
                lifesObj[i].GetComponent<Image>().enabled = true;
            else
                lifesObj[i].GetComponent<Image>().enabled = false;
        }
    }
}