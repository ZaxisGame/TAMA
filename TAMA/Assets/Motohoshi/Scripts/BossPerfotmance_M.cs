using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPerfotmance_M : MonoBehaviour
{
    public GameObject player;
    public GameObject Boss;
    public GameObject OctPivot;
    public GameObject missile;
    public GameObject bridge;
    public Camera BBcam;
    public GameObject BossCamera;
    public GameObject Performancecam1;
    public GameObject Performancecam2R;
    public GameObject Performancecam2L;
    public GameObject Performancecam3;
    public GameObject LensL;
    public GameObject LensR;
    public Material Lens;
    public GameObject SearchLights;
    public GameObject SearchLightL;
    public GameObject SearchLightR;
    public GameObject Wave;
    public Light SpotLightL;
    public Light SpotLightR;
    public float camSpeed = 40;
    public float camSize = 10;
    GameObject[] missiles;
    GameObject PerformanceCam, LightL,LightR;
    MissileManager_M MissileManager;
    Camera Bcam;
    Vector3 wavePos;
    int performState;
    // Use this for initialization
    AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();
    void Start()
    {
        performState = 0;
        missiles = new GameObject[8];
        MissileManager = GetComponent<MissileManager_M>();
        PerformanceCam = transform.GetChild(0).gameObject;
        PerformanceCam.GetComponent<Camera>().enabled = false;
        Performancecam1.GetComponent<Camera>().enabled = false;
        Performancecam2R.GetComponent<Camera>().enabled = false;
        Performancecam2L.GetComponent<Camera>().enabled = false;
        Performancecam3.GetComponent<Camera>().enabled = false;
        BossCamera.GetComponent<Camera>().enabled = false;
        LightR = SearchLightR.transform.GetChild(0).gameObject;
        LightL = SearchLightL.transform.GetChild(0).gameObject;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        wavePos = new Vector3(OctPivot.transform.position.x, Wave.transform.position.y, OctPivot.transform.position.z);
        Wave.transform.position = wavePos;
        if (player.transform.position.x > 106 && performState == 0)
        {
            StartCoroutine("ShotMissiles");
            bridge.AddComponent<BridgeBP_M>();
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
        else if (performState == 3)
        {
            camSize++;
            PerformanceCam.GetComponent<Camera>().orthographicSize = camSize;
            if (camSize > 20)
            {
                performState = 4;
            }
        }
        else if (performState == 4)
        {
            if (PerformanceCam.transform.position.x > 83)
            {
                PerformanceCam.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                PerformanceCam.GetComponent<Camera>().enabled = false;
                BossCamera.GetComponent<Camera>().enabled = true;
                performState = 5;
            }
        }
        //else if(performState==5){
        //    camSize--;
        //    PerformanceCam.GetComponent<Camera>().orthographicSize = camSize;
        //    if (camSize <10)
        //    {
        //        performState = 6;
        //    }
        //}
        else if (performState == 5)
        {
            SearchLights.GetComponent<Rigidbody>().velocity = Vector3.up * 10;
            if (SearchLights.transform.position.y > 20)
            {
                SearchLights.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        else if (performState == 6 || performState == 7)
        {
            Vector3 targetA = new Vector3(83, SearchLightR.transform.position.y, 45) - SearchLightR.transform.position;
            audioSource.PlayOneShot(audioClip[0]);
            SearchLightR.transform.rotation = Quaternion.Slerp(SearchLightR.transform.rotation, Quaternion.LookRotation(targetA), Time.deltaTime * 1f);
            if (performState == 7)
            {
                //GameObject Light = SearchLightR.transform.GetChild(0).gameObject;
                Vector3 targetB = new Vector3(83, -10, 45) - LightR.transform.position;
                //Debug.Log(Light.transform.forward);
                LightR.transform.rotation = Quaternion.Slerp(LightR.transform.rotation, Quaternion.LookRotation(targetB), Time.deltaTime * 1f);
                //audioSource.Stop();
                //audioSource.Play();
                //audioSource.PlayOneShot(audioClip[0]);
            }
        }
        else if (performState == 8 || performState == 9)
        {
            Vector3 targetA = new Vector3(83, SearchLightL.transform.position.y, 45) - SearchLightL.transform.position;
            //audioSource.Stop();
            //audioSource.Play();
            audioSource.PlayOneShot(audioClip[0]);
            SearchLightL.transform.rotation = Quaternion.Slerp(SearchLightL.transform.rotation, Quaternion.LookRotation(targetA), Time.deltaTime * 1f);
            if (performState == 9)
            {
                //GameObject Light = SearchLightL.transform.GetChild(0).gameObject;
                Vector3 targetB = new Vector3(83, -10, 45) - LightL.transform.position;
                //Debug.Log(Light.transform.forward);
                LightL.transform.rotation = Quaternion.Slerp(LightL.transform.rotation, Quaternion.LookRotation(targetB), Time.deltaTime * 1f);
                //audioSource.Stop();
                //audioSource.Play();
                //audioSource.PlayOneShot(audioClip[0]);
            }
        }
        else if (performState == 10){
            //audioSource.Stop();
            //audioSource.Play();
            //audioSource.PlayOneShot(audioClip[2]);
            Vector3 targetVector = new Vector3(Boss.transform.position.x,Boss.transform.position.y+17,Boss.transform.position.z) - Performancecam3.transform.position;
            Quaternion targetAngle = Quaternion.LookRotation(targetVector);
            //Debug.Log(targetAngle.eulerAngles.x);
            if(targetAngle.eulerAngles.x<30||(targetAngle.eulerAngles.x < 360&&targetAngle.eulerAngles.x > 50)){
                Performancecam3.transform.LookAt(new Vector3(Boss.transform.position.x,Boss.transform.position.y+17,Boss.transform.position.z));
                //Debug.Log("Look");
            }
            //Debug.Log(Boss.transform.position);
            if(Boss.transform.position.y>-10.5){
                Boss.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            SearchLightL.transform.LookAt(new Vector3(Boss.transform.position.x, SearchLightL.transform.position.y, Boss.transform.position.z));
            SearchLightR.transform.LookAt(new Vector3(Boss.transform.position.x, SearchLightR.transform.position.y, Boss.transform.position.z));
            LightL.transform.LookAt(new Vector3(Boss.transform.position.x, Boss.transform.position.y + 17, Boss.transform.position.z));
            LightR.transform.LookAt(new Vector3(Boss.transform.position.x, Boss.transform.position.y + 17, Boss.transform.position.z));
        }

    }

    IEnumerator ShotMissiles()
    {
        yield return new WaitForSeconds(6f);
        //PerformanceCam.GetComponent<Rigidbody>().velocity = -PerformanceCam.transform.right * camSpeed;

        PerformanceCam.GetComponent<Rigidbody>().velocity = new Vector3(-1*camSpeed,0.14f*camSpeed,0);
        performState++;
        yield return new WaitForSeconds(1.3f);
        PerformanceCam.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        performState++;
        yield return new WaitForSeconds(2f);
        performState = 4;
        //yield return new WaitForSeconds(5f);
        //Vector3 ToBossCam = BossCamera.transform.position - PerformanceCam.transform.position;
        //PerformanceCam.GetComponent<Rigidbody>().velocity = ToBossCam;
        //yield return new WaitForSeconds(1.3f);
        yield return new WaitForSeconds(5f);
        PerformanceCam.GetComponent<Camera>().enabled = false;
        Performancecam1.GetComponent<Camera>().enabled = true;
        player.transform.position = new Vector3(83, -2.8f, 0);
        yield return new WaitForSeconds(1f);
        performState = 5;
        yield return new WaitForSeconds(4f);
        Performancecam1.GetComponent<Camera>().enabled = false;
        Performancecam2R.GetComponent<Camera>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        performState = 6;
        yield return new WaitForSeconds(0.5f);
        performState = 7;
        yield return new WaitForSeconds(3f);
        LensR.GetComponent<Renderer>().material = Lens;
        SpotLightR.enabled = true;
        //audioSource.Stop();

        //audioSource.PlayOneShot(audioClip[1]);
        //audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        Performancecam2R.GetComponent<Camera>().enabled = false;
        Performancecam2L.GetComponent<Camera>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        performState = 8;
        yield return new WaitForSeconds(0.5f);
        performState = 9;
        yield return new WaitForSeconds(3f);
        LensL.GetComponent<Renderer>().material = Lens;
        SpotLightL.enabled = true;
        //audioSource.Stop();

        //audioSource.PlayOneShot(audioClip[1]);
        //audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        Performancecam2L.GetComponent<Camera>().enabled = false;
        Performancecam3.GetComponent<Camera>().enabled = true;
        yield return new WaitForSeconds(1f);
        performState = 10;
        Boss.GetComponent<Rigidbody>().velocity = new Vector3(0, 3, 0);
        //yield return new WaitForSeconds(15f);
        //Boss.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(14.5f);
        Performancecam3.GetComponent<Camera>().enabled = false;
        BossCamera.GetComponent<Camera>().enabled = true;


    }

}