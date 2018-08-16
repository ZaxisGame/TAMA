using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBP_M : MonoBehaviour {
    Animator TakoAnime;
    bool Floated;

	// Use this for initialization
	void Start () {
        TakoAnime = GetComponent<Animator>();
        Floated = false;
	}
	
	// Update is called once per frame
	void Update () {
        toIdling();
	}

    void toIdling(){
        if(transform.position.y>-11&&!Floated){
            TakoAnime.SetBool("Idle",true);
            Floated = true;
        }
    }
}
