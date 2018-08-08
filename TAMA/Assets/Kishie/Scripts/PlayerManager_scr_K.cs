using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager_scr_K : MonoBehaviour {
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        //当たった敵は生きている
        EnemyController_scr_K.isAlive = true;

        //ジャンプ中に頭をふむと
        if (col.CompareTag("EnemyTop") && PlayerMove_scr_K.isJump && EnemyController_scr_K.isAlive == true)
        {
            Debug.Log("敵を倒した！");
            //敵を消去。プレイヤーにダメージが入らないようにする
            EnemyController_scr_K.isAlive = false;

            gameObject.GetComponent<PlayerMove_scr_K>().Jump();


            Destroy(col.gameObject.transform.parent.gameObject);

        }

        //敵に当たると
        else if (col.CompareTag("Enemy") && EnemyController_scr_K.isAlive == true )
        {
            Debug.Log("ダメージ！");
            Destroy(col.gameObject);
           
            //プレイヤーにダメージ判定
            // LifeManager_M.Damage();的な
        }
    }
}
