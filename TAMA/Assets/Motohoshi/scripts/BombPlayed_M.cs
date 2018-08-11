using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlayed_M : MonoBehaviour {
    GameObject missile;
    MissileCol_M missileCol;
	// Use this for initialization
	void Start () {
        missile = transform.root.gameObject;
        missileCol = missile.GetComponent<MissileCol_M>();
	}
	
    void OnParticleSystemStopped(){
        //missile.GetComponent<MeshRenderer>().enabled = false;
        //missile.GetComponent<BoxCollider>().enabled = false;
        if(missileCol.Bombed){
            Destroy(missile.gameObject);
        }

    }
}
