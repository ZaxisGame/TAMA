using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCol_M : MonoBehaviour {
    //ParticleSystem bomb;
    MissileManager_M missileManager;
    ParticleSystem bomb,afterbruner;
    GameObject bombCol,ab,tentacle;
    bool bombed;
    public bool Bombed{
        set{
            this.bombed = value;
        }
        get{
            return this.bombed;
        }
    } 
    AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();
	// Use this for initialization
	void Start () {
        bomb = transform.FindChild("Fireball").GetComponent<ParticleSystem>();
        afterbruner = transform.FindChild("Afterburner").GetComponent<ParticleSystem>();
        ab = transform.GetChild(2).gameObject;
        tentacle = transform.GetChild(0).gameObject;
        afterbruner.Play();
        bomb.Stop();
        bombed = false;
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void MissileComponent(){
        bomb = transform.FindChild("Fireball").GetComponent<ParticleSystem>();
        afterbruner = transform.FindChild("Afterburner").GetComponent<ParticleSystem>();
        ab = transform.FindChild("Afterburner").GetComponent<GameObject>();
        tentacle = transform.FindChild("Tentacle").GetComponent<GameObject>();
        afterbruner.Play();
        bomb.Stop();
        bombed = false;
    }

	private void OnTriggerEnter(Collider collision)
	{
        //bombCol.AddComponent<SphereCollider>();
        //bombCol.GetComponent<SphereCollider>().radius = 3;
        //afterbruner.Stop();
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<SphereCollider>().enabled = true;
        Destroy(tentacle);
        Destroy(GetComponent<Rigidbody>());
        Destroy(ab);
        bomb.Play();
        audioSource.PlayOneShot(audioClip[0]);
        bombed = true;
	}
}
