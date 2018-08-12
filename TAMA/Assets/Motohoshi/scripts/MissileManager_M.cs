using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager_M : MonoBehaviour {


    public GameObject target;
    public GameObject missile;
    private GameObject[] missiles;
    public GameObject bomb;
    //Transform To;
    //Transform From;
    //Quaternion rotation;
    public float rotateSpeed = 10f;
    public float shotSpeed = 10f;
    //float step;

	// Use this for initialization
	void Start () {
        missiles = new GameObject[8];
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            
            createMissiles();
        }
        if(Input.GetKey(KeyCode.A)){
            TargetLockOn(target, missiles);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shot(missiles);
        }

	}

    void TargetLockOn(GameObject tar, GameObject[] missile){
        //this.transform.LookAt(unko.transform);
        for (int i = 0; i < 8; i++)
        {
            Vector3 targetvec = tar.transform.position - missile[i].transform.position;
            missile[i].transform.rotation = Quaternion.Slerp(missile[i].transform.rotation, Quaternion.LookRotation(targetvec), Time.deltaTime * rotateSpeed);
        }
    }

    void Shot(GameObject[] bullet){
        for (int i = 0; i < 8;i++){
            bullet[i].GetComponent<Rigidbody>().velocity = bullet[i].transform.forward * shotSpeed;
        }
    }

    void createMissiles(){

        for (int i = 0; i < 8; i++)
        {

            switch (i)
            {
                case 0:
                    missiles[i] = Instantiate(missile, new Vector3(-20, 5, 0), Quaternion.Euler(0, 90, 0)); break;
                case 1:
                    missiles[i] = Instantiate(missile, new Vector3(-20, 0, 0), Quaternion.Euler(0, 90, 0)); break;
                case 2:
                    missiles[i] = Instantiate(missile, new Vector3(-20, -5, 0), Quaternion.Euler(0, 90, 0)); break;
                case 3:
                    missiles[i] = Instantiate(missile, new Vector3(-10, 7, 0), Quaternion.Euler(90, 0, 0)); break;
                case 4:
                    missiles[i] = Instantiate(missile, new Vector3(10, 7, 0), Quaternion.Euler(90, 0, 0)); break;
                case 5:
                    missiles[i] = Instantiate(missile, new Vector3(20, 5, 0), Quaternion.Euler(0, -90, 0)); break;
                case 6:
                    missiles[i] = Instantiate(missile, new Vector3(20, 0, 0), Quaternion.Euler(0, -90, 0)); break;
                case 7:
                    missiles[i] = Instantiate(missile, new Vector3(20, -5, 0), Quaternion.Euler(0, -90, 0)); break;
            }
            //missiles[i] = Instantiate(missile, new Vector3(-20,10-i*2.5f,0), Quaternion.identity);
            missiles[i].name = "missile" + (i + 1);
            missiles[i].tag = "Missile";
            missiles[i].AddComponent<Rigidbody>();
            missiles[i].GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
