using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToTitle_M : MonoBehaviour {
    int state;
    float time;
    //SceneManager SceneManager;
	// Use this for initialization
	void Start () {
        state = 0;
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(GetComponent<Image>().enabled&&state==0){
            if(time<5){
                time += Time.deltaTime;
            }else{
                state = 1;
            }
        }else if(state==1){
            SceneManager.LoadScene("OpeningScene_K");
        }
	}
}
