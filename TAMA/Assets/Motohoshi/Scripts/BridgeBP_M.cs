using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBP_M : MonoBehaviour {
    GameObject[] boards;
    int count;
    // Use this for initialization
    void Start()
    {
        count = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(count==8){
            gameObject.AddComponent<Rigidbody>();
        }
        if(transform.position.y<-30){
            Destroy(gameObject);
        }
	}

	private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Missile")){
            Debug.Log(other.tag);
            count++;
        }
	}
}
