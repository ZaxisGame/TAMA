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
        if(player.transform.position.x>pos.x-10&&shotState==0&&pos.x<40){
            StartCoroutine("Missiles1to3");
        }
        if(player.transform.position.x>40&&shotState==0&&pos.x<60){
            StartCoroutine("Missiles4to6");
        }
        if(player.transform.position.x > 65 && shotState == 0 && pos.x >60){
            StartCoroutine("Missiles7and8");
        }

        if(shotState==2){
            MissileManager.TargetLockOn(player.transform.position, gameObject);
        }
	}

    IEnumerator Missiles1to3(){
        shotState++;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(1.5f);
        MissileManager.ShotStop(gameObject);
        shotState++;
        yield return new WaitForSeconds(1f);
        shotState++;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
    IEnumerator Missiles4to6(){
        shotState++;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(2f);
        MissileManager.ShotStop(gameObject);
        shotState++;
        yield return new WaitForSeconds(1f);
        shotState++;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
    IEnumerator Missiles7and8(){
        shotState++;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(1.5f);
        MissileManager.ShotStop(gameObject);
        shotState++;
        yield return new WaitForSeconds(2f);
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(3f);
        shotState++;
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
