using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager_scr_K : MonoBehaviour {
    //private int currentLife = 3;
    int HP = 3;

    private GameObject[] lifesObj;
    public GameObject player;
    PlayerMove_scr_K pMove;
    private GameObject gm;
    GameManager_scr_K GameManager;
    // Use this for initialization
    void Start()
    {
        gm = GameObject.Find("GameManeger");
        GameManager = gm.GetComponent<GameManager_scr_K>();

        HP = GameManager.HP;

        pMove = player.GetComponent<PlayerMove_scr_K>();
        lifesObj = new GameObject[HP];
        for (int i = 0; i < HP; i++)
        {
            lifesObj[i] = new GameObject("cat" + i);
            lifesObj[i].transform.parent = gameObject.transform;
            lifesObj[i].AddComponent<RectTransform>().anchoredPosition = new Vector2(-300 + 100 * i, -180);
            lifesObj[i].GetComponent<RectTransform>().localScale = new Vector3(0.05f, 0.05f, 0.05f);
            lifesObj[i].AddComponent<Image>().sprite = Resources.Load<Sprite>("Images_M/cat_icon");
            lifesObj[i].GetComponent<Image>().preserveAspect = true;
            lifesObj[i].GetComponent<Image>().SetNativeSize();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Damage();
        }
    }

    public void Damage()
    {
        HP--;
        DrawLife(HP);
        pMove.Damage();
        if (HP == 0)
        {
            pMove.Die();
            //SceneManager.LoadScene("GameOver");
        }
    }



    void DrawLife(int n)
    {
        for (int i = 0; i < HP; i++)
        {
            if (i < n)
                lifesObj[i].GetComponent<Image>().enabled = true;
            else
                lifesObj[i].GetComponent<Image>().enabled = false;
        }
    }
}