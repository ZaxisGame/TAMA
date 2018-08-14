using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager_M: MonoBehaviour {
    
    //ゲームマネージャー取得
    private GameObject gamemanager;
    GameManager_M Game_M;
    //ライフマネージャー取得
    private GameObject canbas;
    LifeManager_M Life_M;
    //
    private GameObject tama;
    PlayerMove_M playerMove;

    EnemyController_M enemyController;

	void Start () 
    {
        //ゲームマネージャー取得
        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_M>();
         //ライフマネージャー取得
        canbas = GameObject.Find("Canvas");
        Life_M = canbas.GetComponent<LifeManager_M>();

        tama = GameObject.Find("TAMA");
        playerMove = tama.GetComponent<PlayerMove_M>();

    }

	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {

       // ジャンプ中に頭をふむと
        if (col.CompareTag("EnemyTop") && PlayerMove_M.isJump)
        {
            //当たった敵のスクリプトを取得
            enemyController = col.gameObject.transform.parent.gameObject.GetComponent<EnemyController_M>();
            //プレイヤーにダメージが入らないようにする
            enemyController.isAlive = false;
                           
           
            playerMove.Kill();

            //ジャンプする
            playerMove.Jump();


            //敵を消去
            Destroy(col.gameObject.transform.parent.gameObject);
        }

        //敵に当たると
        else if (col.CompareTag("Enemy") && col.gameObject.GetComponent<EnemyController_M>().isAlive )
        {
           
            Life_M.Damage();
            Destroy(col.gameObject);   
           
        }
        else if(col.CompareTag("Missile")){
            Life_M.Damage();
        }



        if (col.CompareTag("Wall"))
        {
            Debug.Log("enter");
            playerMove.force_z = false;
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
