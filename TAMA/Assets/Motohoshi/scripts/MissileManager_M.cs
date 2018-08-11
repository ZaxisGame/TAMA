using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager_M : MonoBehaviour {


    public GameObject target;
    Transform To;
    Transform From;
    Quaternion rotation;
    public float rotateSpeed = 360f;
    float step;

	// Use this for initialization
	void Start () {
        To = this.transform;
        From = target.transform;
	}
	
	// Update is called once per frame
	void Update () {
        TargetLockOn(target);
        step = rotateSpeed * Time.deltaTime;
        rotation = Quaternion.RotateTowards(From.rotation, Quaternion.Euler(30, 0, 0), step);
	}

    void TargetLockOn(GameObject unko){
        //this.transform.LookAt(unko.transform);

        //step = rotateSpeed * Time.deltaTime;
        //rotation = Quaternion.RotateTowards(From.rotation, Quaternion.Euler(30,30,30), step);
        //From.rotation = rotation;
    }
}
