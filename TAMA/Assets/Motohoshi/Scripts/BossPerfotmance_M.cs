using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPerfotmance_M : MonoBehaviour
{
    public GameObject player;
    public GameObject missile;
    public GameObject bridge;
    public Camera BBcam;
    public float camSpeed = 40;
    public float camSize = 10;
    GameObject[] missiles;
    GameObject PerformanceCam;
    MissileManager_M MissileManager;
    int performState;
    // Use this for initialization
    void Start()
    {
        performState = 0;
        missiles = new GameObject[8];
        MissileManager = GetComponent<MissileManager_M>();
        PerformanceCam = transform.GetChild(0).gameObject;
        PerformanceCam.GetComponent<Camera>().enabled = false;
        bridge.AddComponent<BridgeBP_M>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 106 && performState==0)
        {
            StartCoroutine("ShotMissiles");
            BBcam.enabled = false;
            PerformanceCam.GetComponent<Camera>().enabled = true;
            for (int i = 0; i < 8; i++)
            {
                missiles[i] = Instantiate(missile, new Vector3((i * 3.75f) + 92.5f, -20, 10 + (i * 10)), Quaternion.Euler(-90, 0, 0));
                missiles[i].AddComponent<MissileBP_M>();
            }
            performState++;

        }
        //if(performState==2){
        //    for (int i = 0; i < 8; i++)
        //    {
        //        MissileManager.TargetLockOn(bridge.transform.position, missiles[i]);
        //    }
        //}
        else if(performState==3){
            camSize++;
            PerformanceCam.GetComponent<Camera>().orthographicSize = camSize;
            if(camSize>20){
                performState = 4;
            }
        }
        else if(performState==5){
            camSize--;
            PerformanceCam.GetComponent<Camera>().orthographicSize = camSize;
            if (camSize <10)
            {
                performState = 6;
            }
        }
    }

    IEnumerator ShotMissiles()
    {
        yield return new WaitForSeconds(6f);
        //PerformanceCam.GetComponent<Rigidbody>().velocity = -PerformanceCam.transform.right * camSpeed;
        PerformanceCam.GetComponent<Rigidbody>().velocity = new Vector3(-1*camSpeed,0.07f*camSpeed,0);
        performState++;
        yield return new WaitForSeconds(1.3f);
        PerformanceCam.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        performState++;
        yield return new WaitForSeconds(2f);
        performState = 4;
        yield return new WaitForSeconds(5f);
        PerformanceCam.GetComponent<Rigidbody>().velocity = new Vector3(1 * camSpeed, -0.07f * camSpeed, 0);
        yield return new WaitForSeconds(1.3f);
        PerformanceCam.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        performState = 5;
    }
}