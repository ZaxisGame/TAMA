using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforBoss : MonoBehaviour {

    public GameObject player;
    public GameObject missile;
    GameObject[] missiles;

    GameObject gm;
    int[] shotState;
	// Use this for initialization
	void Start () {
        shotState = new int[8];
        missiles = new GameObject[8];
        for (int i = 0; i < 8;i++){
            shotState[i] = 0;

            switch(i){
                case 0: missiles[i] = Instantiate(missile, new Vector3((i + 1.5f) * 10, -40, 20), Quaternion.Euler(-90, 0, 0));break;
                case 1: missiles[i] = Instantiate(missile, new Vector3((i + 1.5f) * 10, 50, 20), Quaternion.Euler(90, 0, 0)); break;
                case 2: missiles[i] = Instantiate(missile, new Vector3((i + 1.5f) * 10, -40, 20), Quaternion.Euler(-90, 0, 0)); break;
                case 3: missiles[i] = Instantiate(missile, new Vector3((i + 1.5f) * 10, -40, 10), Quaternion.Euler(-90, 0, 0)); break;
                case 4: missiles[i] = Instantiate(missile, new Vector3((i + 1f) * 10, -40, 40), Quaternion.Euler(-90, 0, 0)); break;
                case 5: missiles[i] = Instantiate(missile, new Vector3((i + 0.5f) * 10, -40, 10), Quaternion.Euler(-90, 0, 0)); break;
                case 6: missiles[i] = Instantiate(missile, new Vector3((i + 0.5f) * 10, -40, 20), Quaternion.Euler(-90, 0, 0)); break;
                case 7: missiles[i] = Instantiate(missile, new Vector3((i + 2f) * 10, 10, -20), Quaternion.Euler(5, 0, 90)); break;
            }

            missiles[i].AddComponent<MissileBB_M>();
        }
	}
	
	// Update is called once per frame
    void Update () {
    }
}
