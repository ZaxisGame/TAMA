using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManbouBeam_K : MonoBehaviour 
{
   
    private GameObject gamemanager;
    GameManager_scr_K Game_M;
    //ライフマネージャー取得
    private GameObject canbas;
    LifeManager_scr_K Life_M;

    int timer;
    private void Start()
    {//ゲームマネージャー取得
        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_scr_K>();
        //ライフマネージャー取得
        canbas = GameObject.Find("Canvas");
        Life_M = canbas.GetComponent<LifeManager_scr_K>();
        GetComponent<ParticleSystem>().Stop();
    }


	public void Update()
    {
        timer++;
        if(timer >= 60 * 4){
            GetComponent<ParticleSystem>().Play();
        }
		
	}

	private void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other);
        if (other.CompareTag("Player"))
        {
            Life_M.Damage();
        }
    }
}