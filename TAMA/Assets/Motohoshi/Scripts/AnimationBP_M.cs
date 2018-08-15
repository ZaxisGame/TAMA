using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBP_M : MonoBehaviour {
    Animator TakoAnime;
    bool Floated;

	// Use this for initialization
	void Start () {
        TakoAnime = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        toIdling();
	}

    void toIdling(){
        if(transform.position.y>-11){
            TakoAnime.SetBool("stop", true);
        }
    }
}
