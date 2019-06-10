using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager_M : MonoBehaviour {
    
    //ゲームマネージャー取得
    private GameObject gamemanager;
    GameManager_M Game_M;

    private int HP;
    private GameObject[] lifesObj;
    private GameObject player, player_mesh;
    PlayerMove_M pMove;
    float time;

    private int currentLife;
    public bool isMuteki = false;
    private float mutekiTime;


  

    void Start()
    {
        //ゲームマネージャー取得


        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_M>();
        player = Game_M.player;
        player_mesh = Game_M.player_mesh;
        HP = 10;
        //currentLife = HP;//最初の体力はmax

        mutekiTime = 2.0f;

        pMove = player.GetComponent<PlayerMove_M>();

        lifesObj = new GameObject[HP];

        currentLife = LifeManager_scr_K.currentLife;

        for (int i = 0; i < HP; i++)
        {
            lifesObj[i] = new GameObject("cat" + i);
            lifesObj[i].transform.parent = gameObject.transform;
            lifesObj[i].AddComponent<RectTransform>().anchoredPosition = new Vector2(-348 + (80.333f * i), -180);
            lifesObj[i].GetComponent<RectTransform>().localScale = new Vector3(0.05f, 0.05f, 0.05f);
            lifesObj[i].AddComponent<Image>().sprite = Resources.Load<Sprite>("Images_M/cat_icon");
            lifesObj[i].GetComponent<Image>().preserveAspect = true;
            lifesObj[i].GetComponent<Image>().SetNativeSize();
        }
        for (int i = 0; i < HP; i++)
        {
            if (i < currentLife)
                lifesObj[i].GetComponent<Image>().enabled = true;
            else
                lifesObj[i].GetComponent<Image>().enabled = false;
        }
        time = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Damage();
        }

        if(Input.GetKeyDown(KeyCode.JoystickButton13)||Input.GetKeyDown(KeyCode.JoystickButton14)||Input.anyKeyDown){
            time = 0;
        }
        if(time>60){
            SceneManager.LoadScene("OpeningScene_K");
        }
        time += Time.deltaTime;
        if (isMuteki)
        {

            StartCoroutine("Flashing");
        }
    }

    public void Damage()
    {

        if (isMuteki == false)
        {

            //現在の体力を減らす
            currentLife--;
            //今の体力を引数にする
            DrawLife(currentLife);
            ///////////プレイヤーにダメージ////////////
            StartCoroutine(pMove.Damage());
        }

        if (currentLife == 0)
        {
            pMove.Die();
            SceneManager.LoadScene("OpeningScene_K");
        }
    }



    void DrawLife(int n)//nは今の体力
    {
        for (int i = 0; i < HP; i++)
        {
            if (i < n)
                lifesObj[i].GetComponent<Image>().enabled = true;
            else
                lifesObj[i].GetComponent<Image>().enabled = false;
        }
    }

    IEnumerator Flashing()
    {

        yield return new WaitForSeconds(0.1f);
        player_mesh.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        player_mesh.SetActive(true);

    }
}