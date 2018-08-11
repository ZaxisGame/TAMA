using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCol_M : MonoBehaviour {
    //ParticleSystem bomb;
    MissileManager_M missileManager;
    ParticleSystem bomb,afterbruner;
    bool bombed;
    public bool Bombed{
        set{
            this.bombed = value;
        }
        get{
            return this.bombed;
        }
    } 
	// Use this for initialization
	void Start () {
        //bomb = this.GetComponent<ParticleSystem>();

        //missile = transform.root.gameObject;

        bomb = transform.FindChild("Fireball").GetComponent<ParticleSystem>();
        afterbruner = transform.FindChild("Afterburner").GetComponent<ParticleSystem>();
        afterbruner.Play();
        bomb.Stop();
        bombed = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if(Input.GetKeyDown(KeyCode.A)){
        //    bomb.Play();
        //}
	}

	private void OnCollisionEnter(Collision collision)
	{
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        afterbruner.Stop();
        bomb.Play();
        bombed = true;
	}
}
