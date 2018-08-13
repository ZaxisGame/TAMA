using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforBoss : MonoBehaviour {

    public GameObject player;
    public GameObject missile;
    GameObject[] missiles;
    MissileManager_M MissileManager;
    GameObject gm;
    bool[] shooted;
	// Use this for initialization
	void Start () {
        shooted = new bool[6];
        missiles = new GameObject[6];
        MissileManager = GetComponent<MissileManager_M>();
        for (int i = 0; i < 6;i++){
            shooted[i] = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 6; i++)
        {
            if (player.transform.position.x > (i+1) * 10&&!shooted[i])
            {
                shooted[i] = true;
                missiles[i] = Instantiate(missile, new Vector3((i + 2) * 10, 10, 0),Quaternion.Euler(90,0,0));
                Debug.Log("文化先進国である韓国で猫が人気なのは知ってたけど");
                MissileManager.Shot(missiles[i]);
                Debug.Log("日本みたいな後進国でも人気あるのか。へー");
            }
        }
	}
}
