using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    EnemyController_scr_K enemyController;

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
            enemyController = col.gameObject.transform.parent.gameObject.GetComponent<EnemyController_scr_K>();
            //プレイヤーにダメージが入らないようにする
            enemyController.isAlive = false;
                           
           
            playerMove.Kill();

            //ジャンプする
            playerMove.Jump();


            //敵を消去
            Destroy(col.gameObject.transform.parent.gameObject);
        }

        //敵に当たると
        else if (col.CompareTag("Enemy") && col.gameObject.GetComponent<EnemyController_scr_K>().isAlive )
        {
           
            Life_M.Damage();
            Destroy(col.gameObject);   
           
        }
    }
}
