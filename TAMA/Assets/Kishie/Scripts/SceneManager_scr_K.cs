using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager_scr_K : MonoBehaviour {

    //アニメーター宣言１
    Animator animator;
    public GameObject tama;
    public Image panel_black;
    bool fadeout = false;
    float alfa;

	void Start () {
        //アニメーター宣言２
        animator = tama.GetComponent<Animator>();
        animator.SetBool("is_Sleeping", true);

	}
	

	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fadeout = true;
            StartCoroutine("ToMainScene");
        }

        if(fadeout){
            alfa += 0.01f;
            panel_black.color = new Color(0,0,0,alfa);
        }
		
	}



    IEnumerator ToMainScene()
    {
        yield return new WaitForSeconds(1f);
     
        SceneManager.LoadScene("GameScene_K");
    }

    IEnumerator ToOpeningScene()
    {
        yield return new WaitForSeconds(0f);

        SceneManager.LoadScene("OpeningScene_K");
    }
}
