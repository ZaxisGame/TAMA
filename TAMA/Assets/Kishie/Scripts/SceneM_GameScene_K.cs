using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneM_GameScene_K : MonoBehaviour {

    public Image panel_black;
    bool fadein = true;
    float alfa = 1;

    void Start()
    {



    }
	
	// Update is called once per frame
	void Update () {
        if (fadein)
        {
            alfa -= 0.01f;
            panel_black.color = new Color(0, 0, 0, alfa);

        }


        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("OpeningScene_K");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("BossStage");
        }
	}
}
