using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager_M : MonoBehaviour {
    private int currentLife = 3;
    private int firstLife = 3;

    private GameObject[] lifesObj;

	// Use this for initialization
	void Start () {
        lifesObj = new GameObject[firstLife];
        for (int i = 0; i < firstLife; i++)
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
        if(Input.GetKeyDown(KeyCode.B)){
            Damage();
        }
    }

    public void Damage(){
        currentLife--;
        DrawLife(currentLife);
        if(currentLife==0){
            SceneManager.LoadScene("GameOver");
        }
    }



	void DrawLife(int n){
        for (int i = 0; i < firstLife;i++){
            if(i<n)
                lifesObj[i].GetComponent<Image>().enabled = true;
            else
                lifesObj[i].GetComponent<Image>().enabled = false;
        }
    }
}