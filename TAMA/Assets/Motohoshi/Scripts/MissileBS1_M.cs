using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBS1_M : MonoBehaviour {
    GameObject gm;
    MissileManager_M MissileManager;
    int ShotState;
	// Use this for initialization
	void Start () {
        gm = GameObject.Find("BossStage");
        MissileManager = gm.GetComponent<MissileManager_M>();
        ShotState = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(ShotState==0){
            StartCoroutine("Missile");
            ShotState = 1;
        }
	}

    IEnumerator Missile(){
        yield return new WaitForSeconds(2f);
        MissileManager.shotSpeed = 50;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(2f);
        transform.rotation = Quaternion.Euler(90, 0, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, 3);
        MissileManager.shotSpeed = 20;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(3f);
        MissileManager.ShotStop(gameObject);
        yield return new WaitForSeconds(2f);
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    }
}
