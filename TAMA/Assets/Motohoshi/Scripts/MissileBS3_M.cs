using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBS3_M : MonoBehaviour {
    GameObject gm;
    GameObject player;
    MissileManager_M MissileManager;
    int ShotState;
    int i;
    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("BossStage");
        MissileManager = gm.GetComponent<MissileManager_M>();
        ShotState = 0;
        i = (int)(transform.position.x - 62) / 6;
        player = MissileManager.target;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShotState == 0)
        {
            StartCoroutine("Missile");
            ShotState = 1;
        }
        else if (ShotState == 2)
        {
            MissileManager.TargetLockOn(new Vector3(player.transform.position.x, player.transform.position.y, -4.5f), gameObject);
        }
    }

    IEnumerator Missile()
    {
        yield return new WaitForSeconds(2f);
        MissileManager.shotSpeed = 50;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(1.5f);
        if (i < 1)
        {
            transform.position = new Vector3(40, -1.5f, -0.5f);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (i >= 1 && i < 4)
        {
            transform.position = new Vector3(transform.position.x, 40, 4.5f);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else if(i>=4&&i<7){
            transform.position = new Vector3(transform.position.x, 40, -4.5f);
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            transform.position = new Vector3(126,-1.5f, 0.5f);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        MissileManager.shotSpeed = 20;
        MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(1f);
        MissileManager.ShotStop(gameObject);
        yield return new WaitForSeconds(2f);
        if(i<1)
            MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(1f);
        if (i >= 1 && i < 4)
            MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(1f);
        if (i >= 4 && i < 7)
            MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(1f);
        if (i >=7)
            MissileManager.Shot(gameObject);
        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    }
}
