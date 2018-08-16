using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManbouBeam_M : MonoBehaviour {
    public GameObject gun;
    // Use this for initialization
    int state;
	void Start () {
        state = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(state==0){
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, Quaternion.Euler(0, 0, -70), Time.deltaTime * 1f);
            Debug.Log(gun.transform.eulerAngles.z);
            if(gun.transform.eulerAngles.z<300&&gun.transform.eulerAngles.z>10){
                state = 1;
            }
        }else if(state==1){
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, Quaternion.Euler(0, 0, 10), Time.deltaTime * 1f);
            Debug.Log(gun.transform.eulerAngles.z);
            if (gun.transform.eulerAngles.z > 359)
            {
                state = 0;
            }
        }
	}
}
