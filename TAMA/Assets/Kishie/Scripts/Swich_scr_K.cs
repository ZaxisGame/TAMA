using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swich_scr_K : MonoBehaviour {

    public GameObject enemys;

	void Start () {
		
	}
	

	void Update () {
		
	}

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            GameObject EnemysInstance = Instantiate(enemys,enemys.transform.position, Quaternion.identity);
        }

		
	}
}
