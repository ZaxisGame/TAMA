using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBB_M : MonoBehaviour {

    GameObject player,gm;
    MissileManager_M MissileManager;
    int shotState;
    new Vector3 pos;
	// Use this for initialization
	void Start () {
        shotState = 0;
        gm = GameObject.Find("BeforBoss");
        MissileManager = gm.GetComponent<MissileManager_M>();
        pos = transform.position;
        player = GameObject.FindWithTag("Player");

	}
	
	// Update is called once per frame
    void Update () {
        if(player.transform.position.x>pos.x-10&&shotState==0){
            StartCoroutine("Sample");
        }

        if(shotState==2){
            MissileManager.TargetLockOn(player, gameObject);
        }
	}

    IEnumerator Sample(){
        shotState++;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(0.5f);
        MissileManager.ShotStop(gameObject);
        shotState++;
        yield return new WaitForSeconds(0.5f);
        shotState++;
        MissileManager.Shot(gameObject);
    }
}
