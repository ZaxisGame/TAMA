using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightaudioR_M : MonoBehaviour {
    int state;
    AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();
	// Use this for initialization
	void Start () {
        state = 0;
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(GetComponent<Camera>().enabled&&state==0){
            audioSource.PlayOneShot(audioClip[0]);
        }
	}
}
