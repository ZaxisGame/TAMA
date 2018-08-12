using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCol_M : MonoBehaviour {
    //ParticleSystem bomb;
    MissileManager_M missileManager;
    ParticleSystem bomb,afterbruner;
    GameObject bombCol,ab;
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
        bomb = transform.FindChild("Fireball").GetComponent<ParticleSystem>();
        afterbruner = transform.FindChild("Afterburner").GetComponent<ParticleSystem>();
        ab = transform.FindChild("Afterburner").GetComponent<GameObject>();

        afterbruner.Play();
        bomb.Stop();
        bombed = false;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	private void OnTriggerEnter(Collider collision)
	{
        //bombCol.AddComponent<SphereCollider>();
        //bombCol.GetComponent<SphereCollider>().radius = 3;
        afterbruner.Stop();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(GetComponent<Rigidbody>());
        Destroy(ab);
        bomb.Play();
        bombed = true;
	}
}
