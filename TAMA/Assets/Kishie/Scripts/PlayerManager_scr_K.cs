using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerManager_scr_K : MonoBehaviour {
    
    //ゲームマネージャー取得
    private GameObject gamemanager;
    GameManager_scr_K Game_M;
    //ライフマネージャー取得
    private GameObject canbas;
    LifeManager_scr_K Life_M;
    //
    private GameObject tama;
    PlayerMove_scr_K playerMove;

    EnemyManager_scr_K enemyManager;

	void Start () 
    {
        //ゲームマネージャー取得
        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_scr_K>();
         //ライフマネージャー取得
        canbas = GameObject.Find("Canvas");
        Life_M = canbas.GetComponent<LifeManager_scr_K>();

        tama = GameObject.Find("TAMA");
        playerMove = tama.GetComponent<PlayerMove_scr_K>();



    }

	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {

       // ジャンプ中に頭をふむと
        if (col.CompareTag("EnemyTop") && PlayerMove_scr_K.isJump)
        {
            //当たった敵のスクリプトを取得
            enemyManager = col.gameObject.transform.parent.gameObject.GetComponent<EnemyManager_scr_K>();
            //プレイヤーにダメージが入らないようにする
            enemyManager.isAlive = false;

            playerMove.Kill();

            //ジャンプする
            playerMove.Jump();


            //敵を消去
            Destroy(col.gameObject.transform.parent.gameObject,0.1f);
        }

        //敵に当たると
        else if (col.CompareTag("Enemy") && col.gameObject.GetComponent<EnemyManager_scr_K>().isAlive )
        {
           
            Life_M.Damage();
            Destroy(col.gameObject);   
           
        }
        else if(col.CompareTag("Beam")){
            Life_M.Damage();
            Destroy(col.gameObject);  
            
        }


        if (col.CompareTag("Wall"))
        {
            Debug.Log("enter");
            playerMove.force_z = false;
        }

        if (col.CompareTag("Trap"))
        {
            //Debug.Log("Scene");
            SceneManager.LoadScene("BossStage");
        }


    }

   
    private void  OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Wall"))
        {
            Debug.Log("exit");
            playerMove.force_z = true;
        }
    }

}
