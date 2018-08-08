using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_3D_M : MonoBehaviour {
    int FullTimer = 10;
    int currentTimer = 10;
    float time1,time0;

    CameraManager_M Cam_M;
    LifeManager_M Life_M;
    private GameObject[] Timer3D;
    private GameObject Maincamera,gm;
    bool is3D;

	// Use this for initialization
	void Start () {
        Maincamera = GameObject.Find("Main Camera");
        gm = GameObject.Find("GameManager");
        Cam_M = gm.GetComponent<CameraManager_M>();
        Life_M = GetComponent<LifeManager_M>();
        //is3D = Maincamera.GetComponent<Camera>().orthographic;
        Timer3D = new GameObject[FullTimer];
        for (int i = 0; i < FullTimer; i++)
        {
            Timer3D[i] = new GameObject("cat" + i);
            Timer3D[i].transform.parent = gameObject.transform;
            Timer3D[i].AddComponent<RectTransform>().anchoredPosition = new Vector2(-300 + 50 * i, 180);
            Timer3D[i].GetComponent<RectTransform>().localScale = new Vector3(0.03f, 0.03f, 0.03f);
            Timer3D[i].AddComponent<Image>().sprite = Resources.Load<Sprite>("Images_M/cat_icon");
            Timer3D[i].GetComponent<Image>().enabled = false;
            Timer3D[i].GetComponent<Image>().preserveAspect = true;
            Timer3D[i].GetComponent<Image>().SetNativeSize();
        }
        time1 = 0;
        time0 = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Cam_M.CamState == 1)
            Timer_on();
        else if (Cam_M.CamState == 0 && currentTimer < 10)
            Timer_cure();
        Debug.Log(currentTimer);
	}

    void Timer_on(){
        time1 += Time.deltaTime;
        if(time1>1){
            currentTimer--;
            time1 = 0;
        }
        DrawTime();
        Debug.Log(time1);
        if(currentTimer==0){
            Life_M.Damage();
            currentTimer = 3;
        }
    }

    void Timer_cure(){
        time0 += Time.deltaTime;
        if(time0>1){
            currentTimer++;
            time0 = 0;
        }
        DrawTime();
    }

    void DrawTime(){
        for (int i = 0; i < FullTimer;i++){
            if (i < currentTimer)
                Timer3D[i].GetComponent<Image>().enabled = true;
            else
                Timer3D[i].GetComponent<Image>().enabled = false;
        }
    }
}
