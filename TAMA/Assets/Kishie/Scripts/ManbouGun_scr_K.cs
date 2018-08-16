using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManbouGun_scr_K : MonoBehaviour {
    public GameObject gun;
    // Use this for initialization
    int state;
    private GameObject gamemanager;
    GameManager_scr_K Game_M;
    //ライフマネージャー取得
    private GameObject canbas;
    LifeManager_scr_K Life_M;

    void Start()
    {
        //ゲームマネージャー取得
        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_scr_K>();
        //ライフマネージャー取得
        canbas = GameObject.Find("Canvas");
        Life_M = canbas.GetComponent<LifeManager_scr_K>();
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, Quaternion.Euler(0, -180, -70), Time.deltaTime * 1f);
            Debug.Log(gun.transform.eulerAngles.z);
            if (gun.transform.eulerAngles.z < 300 && gun.transform.eulerAngles.z > 10)
            {
                state = 1;
            }
        }
        else if (state == 1)
        {
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, Quaternion.Euler(0, -180, 10), Time.deltaTime * 1f);
            Debug.Log(gun.transform.eulerAngles.z);
            if (gun.transform.eulerAngles.z > 359)
            {
                state = 0;
            }
        }
    }
}