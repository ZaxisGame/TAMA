using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManbouBeam_K : MonoBehaviour 
{
    public GameObject canvas;
    LifeManager_scr_K lifeManager;

    int timer;
    private void Start()
    {
        lifeManager = canvas.GetComponent<LifeManager_scr_K>();
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
            lifeManager.Damage();
        }
    }
}