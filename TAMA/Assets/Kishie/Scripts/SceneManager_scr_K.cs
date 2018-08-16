using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager_scr_K : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A))
        {
            
            StartCoroutine("ToMainScene");
        }
		
	}



    IEnumerator ToMainScene()
    {

        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("GameScene_K");
        yield return new WaitForSeconds(1f);
     
    }
}
