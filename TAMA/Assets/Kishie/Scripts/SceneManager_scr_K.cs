using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager_scr_K : MonoBehaviour {

    //アニメーター宣言１
    Animator animator;
    private GameObject tama;

	void Start () {
        //アニメーター宣言２
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("is_Sleeping", true);
            StartCoroutine("ToMainScene");
        }
		
	}



    IEnumerator ToMainScene()
    {
        //寝る
        animator.SetBool("is_Sleeping", true);
        //ui

        yield return new WaitForSeconds(2f);
     
        SceneManager.LoadScene("GameScene_K");
    }
}
