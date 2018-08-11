using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCol_M : MonoBehaviour {
    //ParticleSystem bomb;
    MissileManager_M missileManager;
    ParticleSystem bomb,afterbruner;
	// Use this for initialization
	void Start () {
        //bomb = this.GetComponent<ParticleSystem>();

        //missile = transform.root.gameObject;

        bomb = transform.FindChild("Fireball").GetComponent<ParticleSystem>();
        afterbruner = transform.FindChild("Afterburner").GetComponent<ParticleSystem>();
        afterbruner.Play();
        bomb.Stop();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.A)){
            bomb.Play();
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
        afterbruner.Stop();
        bomb.Play();
        Destroy(gameObject);
	}
}
