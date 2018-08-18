using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager_scr_K : MonoBehaviour {

    //アニメーター宣言１
    Animator animator;
    public GameObject tama;
    public Image panel_fadeout;
    public Image panel_fadein;
    bool fadeout = false;
    float alfa;
    float alfa2;

	void Start () {
        //アニメーター宣言２
        animator = tama.GetComponent<Animator>();
        animator.SetBool("is_Sleeping", true);

	}
	

	void Update () {
        alfa2 -= 0.01f;
        panel_fadein.color = new Color(0, 0, 0, alfa2);
        panel_fadeout.color = new Color(0, 0, 0, alfa);

        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.JoystickButton13))
        {
            fadeout = true;
            StartCoroutine("ToMainScene");
        }

        if(fadeout){
            alfa += 0.01f;
            panel_fadeout.color = new Color(0,0,0,alfa);
        }


		
	}



    IEnumerator ToMainScene()
    {
        yield return new WaitForSeconds(1.5f);
     
        SceneManager.LoadScene("GameScene_K");
    }



}
