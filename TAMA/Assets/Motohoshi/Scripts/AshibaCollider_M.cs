using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshibaCollider_M : MonoBehaviour {
    public GameObject Boss;
    AnimationBS_M AnimationBS;
    int state;
	private void Start()
	{
        AnimationBS = Boss.GetComponent<AnimationBS_M>();
        state = 0;
	}
	private void OnCollisionEnter(Collision other)
	{
        if (AnimationBS.getBossState() == 11&&other.collider.CompareTag("Player")&&state==0)
        {
            other.transform.position = new Vector3(other.transform.position.x - 5, other.transform.position.y, other.transform.position.z);
            state = 1;
        }
	}
	private void OnTriggerEnter(Collider other)
	{
        if(AnimationBS.getBossState()!=11&&state==1){
            state = 0;
        }
	}
}
