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
    //EnemyController_scr_K enemyController = null;
	void Start () 
    {
        //ゲームマネージャー取得
        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_scr_K>();
         //ライフマネージャー取得
        canbas = GameObject.Find("Canvas");
        Life_M = canbas.GetComponent<LifeManager_scr_K>();

    }

	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        //当たった敵のスクリプトを取得
        //enemyController = col.gameObject.GetComponent<EnemyController_scr_K>();

        //当たった敵は生きている
        EnemyController_scr_K.isAlive = true;


        //ジャンプ中に頭をふむと
        if (col.CompareTag("EnemyTop") && PlayerMove_scr_K.isJump && EnemyController_scr_K.isAlive == true)
        {
            Debug.Log("敵を倒した！");
            //プレイヤーにダメージが入らないようにする
            EnemyController_scr_K.isAlive = false;
            //ジャンプする
            gameObject.GetComponent<PlayerMove_scr_K>().Jump();
            //敵を消去
            Destroy(col.gameObject.transform.parent.gameObject);
        }

        //敵に当たると
        else if (col.CompareTag("Enemy") && EnemyController_scr_K.isAlive == true )
        {
            Life_M.Damage();
            Destroy(col.gameObject);   
           
        }
    }
}
