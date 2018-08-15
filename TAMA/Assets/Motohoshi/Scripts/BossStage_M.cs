using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage_M : MonoBehaviour {
    public GameObject missile;
    GameObject[] missiles;
	// Use this for initialization
	void Start () {
        missiles = new GameObject[8];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator BossRoutine(){
        for (int i = 0; i < 8;i++){
            missiles[i] = Instantiate(missile, new Vector3(59 + (i * 6), -20, 17), Quaternion.Euler(-90, 0, 0));
        }
        yield return new WaitForSeconds(5f);
    }
}
