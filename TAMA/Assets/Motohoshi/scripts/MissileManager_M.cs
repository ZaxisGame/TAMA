using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager_M : MonoBehaviour {


    public GameObject target;
    public GameObject missile;
    private GameObject[] missiles;

    //Transform To;
    //Transform From;
    //Quaternion rotation;
    public float rotateSpeed = 10f;
    public float shotSpeed = 10f;
    //float step;
    AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();

	// Use this for initialization
	void Start () {
        missiles = new GameObject[8];
        audioSource = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            
            createMissiles();
        }
        if(Input.GetKey(KeyCode.Alpha2)){
            for (int i = 0; i < 8; i++)
            {
                TargetLockOn(target.transform.position, missiles[i]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < 8;i++){
                Shot(missiles[i]);
            }

        }

	}

    public void TargetLockOn(Vector3 tar, GameObject bullet){        
        Vector3 targetvec = tar - bullet.transform.position;
        bullet.transform.rotation = Quaternion.Slerp(bullet.transform.rotation, Quaternion.LookRotation(targetvec), Time.deltaTime * rotateSpeed);
        
    }

    public void Shot(GameObject bullet){
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * shotSpeed;
        audioSource.PlayOneShot(audioClip[0]);
    }

    public void ShotStop(GameObject bullet){
        bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
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
            missiles[i].name = "missile" + (i + 1);
            missiles[i].tag = "Missile";
        }
    }
}
