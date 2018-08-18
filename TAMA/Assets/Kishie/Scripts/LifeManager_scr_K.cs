using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeManager_scr_K : MonoBehaviour
{

    //ゲームマネージャー取得
    private GameObject gamemanager;
    GameManager_scr_K Game_M;

    private int HP;
    private GameObject[] lifesObj;
    private GameObject player_mesh;
    public GameObject player;

    public PlayerMove_scr_K pMove;


    public static int currentLife = 3;
    public bool isMuteki = false;
    private float mutekiTime;

    bool fadeout = false;

    public Image panel_black;
    float alfa;




    void Start()
    {
        //ゲームマネージャー取得


        gamemanager = GameObject.Find("GameManager");
        Game_M = gamemanager.GetComponent<GameManager_scr_K>();
        //player = Game_M.player;
        player_mesh = Game_M.player_mesh;
        HP = Game_M.TAMA_HP;
        currentLife = HP;//最初の体力はmax

        mutekiTime = Game_M.mutekiTime;

        pMove = player.GetComponent<PlayerMove_scr_K>();
        Debug.Log(pMove);

        lifesObj = new GameObject[HP];



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
    }


    void Update()
    {
//        Debug.Log(pMove);

        if (Input.GetKeyDown(KeyCode.B))
        {
            Damage();
        }

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
            /// 
            Debug.Log(pMove);
            StartCoroutine(pMove.Damage());


        }

        if (currentLife == 0)
        {
            pMove.Die();

            Invoke("SM", 1.0f);
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

    void SM(){
        SceneManager.LoadScene("OpeningScene_K");
    }
 
}