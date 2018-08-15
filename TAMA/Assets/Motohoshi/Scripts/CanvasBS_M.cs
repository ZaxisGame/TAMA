using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBS_M : MonoBehaviour {
    public GameObject player;
    int state;
    CanvasGroup CanvasGroup;
	// Use this for initialization
	void Start () {
        state = 0;
        CanvasGroup = GetComponent<CanvasGroup>();
        Debug.Log(CanvasGroup);
	}
	
	// Update is called once per frame
	void Update () {
        if(player.transform.position.x>106&&state==0){
            CanvasGroup.alpha = 0f;
            Debug.Log(CanvasGroup.alpha);
            state++;
        }
	}
}
