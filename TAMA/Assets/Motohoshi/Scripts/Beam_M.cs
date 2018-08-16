using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_M : MonoBehaviour
{
    public GameObject canvas;
    LifeManager_M lifeManager;
	private void Start()
	{
        lifeManager = canvas.GetComponent<LifeManager_M>();
	}
	private void OnParticleCollision(GameObject other)
	{
        Debug.Log(other);
        if(other.CompareTag("Player")){
            lifeManager.Damage();
        }
	}
}
