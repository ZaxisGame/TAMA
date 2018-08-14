using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBP_M : MonoBehaviour {

    GameObject player, gm;
    MissileManager_M MissileManager;
    int shotState;
    Vector3 pos;
	// Use this for initialization
	void Start () {
        shotState = 0;
        gm = GameObject.Find("BossPerformance");
        MissileManager = gm.GetComponent<MissileManager_M>();
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if(player.transform.position.x>106&&shotState==0){
            StartCoroutine("Missiles");
            shotState=1;
        }
        else if(shotState==2){
            MissileManager.TargetLockOn(new Vector3(0, transform.position.y, transform.position.z), gameObject);
        }
        else if(shotState==4){
            if(transform.position.x<pos.x-92.5+((pos.z-10)/10)*3.5f){
                MissileManager.ShotStop(gameObject);
                MissileManager.TargetLockOn(new Vector3(transform.position.x, -7, 0), gameObject);
            }
        }
	}

    IEnumerator Missiles(){
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(6f);
        MissileManager.ShotStop(gameObject);
        shotState=2;
        yield return new WaitForSeconds(1.5f);
        shotState=3;
        MissileManager.shotSpeed = 70;
        pos = transform.position;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(0.2f);
        shotState=4;
        yield return new WaitForSeconds(2f);
        shotState = 5;
        MissileManager.shotSpeed = 50;
        MissileManager.Shot(gameObject);
    }
}
