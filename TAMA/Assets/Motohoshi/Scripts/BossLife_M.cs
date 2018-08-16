using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife_M : MonoBehaviour
{
    public GameObject Boss;
    public GameObject OctPivot;
    public GameObject Ending;
    public Light light;
    public Camera BossCam;
    public Camera EndingCam;
    public Material oct;
    public int life;
    int state;
    AnimationBS_M AnimationBS;
	private void Start()
	{
        life = 1;
        state = 0;
        AnimationBS = Boss.GetComponent<AnimationBS_M>();
        EndingCam.enabled = false;
	}

	private void Update()
	{
        if (AnimationBS.getBossState() != 11)
        {
            state = 0;
        }
        if (Input.GetKey(KeyCode.H))
        {
            life = 0;
            state = 2;
        }
        if(life==0&&state==2){
            state = 3;
            BossCam.enabled = false;
            EndingCam.enabled = true;
            Destroy(Boss.GetComponent<AnimationBS_M>());
            OctPivot.transform.position = new Vector3(83, 3.8f, 40);
            OctPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
            Boss.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
	}

	// Use this for initialization
	private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")&&state==0){
            life--;
            state = 1;
            if (life != 0)
            {
                StartCoroutine("Tenmetsu");
            }
            //if (life == 0)
                //life = 3;
            if(life==0){
                state = 2;
            }
        }
        switch(life){
            case 3: light.color = new Color(255, 0, 0);light.intensity = 0.09f; break;
            case 2: light.color = new Color(255, 0, 255);light.intensity = 0.09f; break;
            case 1: light.color = new Color(0, 0, 255);light.intensity = 0.09f; break;
        }
    }

    IEnumerator Tenmetsu(){
        Color color = oct.color;
        Debug.Log("call");
        color.a = 0.5f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0.5f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0.5f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0.5f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0.5f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 0.5f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
        color.a = 1f;
        oct.color = color;
        yield return new WaitForSeconds(0.2f);
    }
}
