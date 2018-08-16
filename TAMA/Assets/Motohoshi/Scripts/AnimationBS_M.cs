using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBS_M : MonoBehaviour {
    Animator TakoAnime;
    public GameObject BossCam;
    public GameObject OctPivot;
    public GameObject missile;
    public GameObject Beam;
    public GameObject Ashiba;
    MissileManager_M MissileManager;
    GameObject[] missiles;
    Rigidbody tako;
    int Lqqp;
    int Life;
    // Use this for initialization
    private int BossState;
    float time = 0;

    public int getBossState(){
        return BossState;
    }
	void Start () {
        Beam.GetComponent<ParticleSystem>().Stop();
        missiles = new GameObject[8];
        TakoAnime = GetComponent<Animator>();
        BossState = 0;
        tako = GetComponent<Rigidbody>();
        Lqqp = 0;
        MissileManager = GetComponent<MissileManager_M>();
	}
	
	// Update is called once per frame
	void Update () {
        if(BossCam.GetComponent<Camera>().enabled&&BossState==0){
            //OctPivot.transform.position = new Vector3(transform.position.x,transform.position.y+17,transform.position.z);
            StartCoroutine("BossMove");
            //Debug.Log("call");
        }
        else if(BossState==2){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,90,0), Time.deltaTime * 2f);
        }
        else if (BossState == 3)
        {
            if (OctPivot.transform.position.x < 115)
            {
                Vector3 go = new Vector3(OctPivot.transform.position.x + 0.5f, OctPivot.transform.position.y, OctPivot.transform.position.z);
                OctPivot.transform.position = go;
            }
            else{
                OctPivot.transform.position = new Vector3(115, OctPivot.transform.position.y, OctPivot.transform.position.z);
                BossState = 4;
            }

        }
        else if(BossState==4){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 2f);
        }
        else if(BossState==5){
            if (OctPivot.transform.position.z > 0)
            {
                Vector3 go = new Vector3(OctPivot.transform.position.x, OctPivot.transform.position.y + 0.1f, OctPivot.transform.position.z - 0.5f);
                OctPivot.transform.position = go;
            }
            else{
                OctPivot.transform.position = new Vector3(OctPivot.transform.position.x, OctPivot.transform.position.y, 0);
                BossState = 6;
            }
        }
        else if(BossState==6){
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 270, 0), Time.deltaTime * 2f);
        }
        else if (BossState == 7)
        {
            OctPivot.transform.rotation = Quaternion.Slerp(OctPivot.transform.rotation, Quaternion.Euler(0, 0, 90), Time.deltaTime * 0.5f);
            if(OctPivot.transform.rotation.eulerAngles.z>=70){
                OctPivot.transform.rotation = Quaternion.Euler(0, 0, 70);
                BossState = 8;
            }
        }
        else if (BossState==8){
            OctPivot.transform.rotation = Quaternion.Slerp(OctPivot.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 0.5f);
            if (OctPivot.transform.rotation.eulerAngles.z <= 20)
            {
                OctPivot.transform.rotation = Quaternion.Euler(0, 0, 20);
                BossState = 9;
            }
        }
        else if (BossState == 9)
        {
            OctPivot.transform.rotation = Quaternion.Slerp(OctPivot.transform.rotation, Quaternion.Euler(0, 0, -140), Time.deltaTime * 0.5f);
            //Debug.Log(OctPivot.transform.rotation.eulerAngles.z);
            if (OctPivot.transform.rotation.eulerAngles.z < 240&&OctPivot.transform.rotation.eulerAngles.z > 30)
            {
                //Debug.Log("a");
                OctPivot.transform.rotation = Quaternion.Euler(0, 0, -120);
                Beam.GetComponent<ParticleSystem>().Stop();
                BossState = 10;
            }
        }
        else if(BossState==10){
            if(OctPivot.transform.position.y>1){
                Vector3 go = new Vector3(OctPivot.transform.position.x, OctPivot.transform.position.y - 0.5f, OctPivot.transform.position.z);
                OctPivot.transform.position = go;
            }
            else{
                OctPivot.transform.position = new Vector3(OctPivot.transform.position.x, 1, OctPivot.transform.position.z);
                BossState = 11;
            }
        }
        else if(BossState==11){
            Ashiba.GetComponent<BoxCollider>().isTrigger = false;
        }
        else if(BossState==12){
            if (OctPivot.transform.position.y < 16.5)
            {
                Vector3 go = new Vector3(OctPivot.transform.position.x, OctPivot.transform.position.y + 1f, OctPivot.transform.position.z);
                OctPivot.transform.position = go;
            }
            OctPivot.transform.rotation = Quaternion.Slerp(OctPivot.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 2f);
            //float z = OctPivot.transform.eulerAngles.z;
            //Debug.Log(OctPivot.transform.rotation.eulerAngles.z);
            if(OctPivot.transform.position.y>=16.5&&OctPivot.transform.rotation.eulerAngles.z<=0){
                OctPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
                Vector3 go = new Vector3(OctPivot.transform.position.x, OctPivot.transform.position.y + 1f, OctPivot.transform.position.z);
                OctPivot.transform.position = go;
                //Debug.Log(OctPivot.transform.position);
            }
            if(OctPivot.transform.position.y>60){
                BossState = 13;
                //Debug.Log(BossState);
            }
        }
        else if(BossState==13){
            transform.rotation = Quaternion.Euler(0, 180, 0);
            Vector3 a = new Vector3(OctPivot.transform.position.x-0.25f, OctPivot.transform.position.y, OctPivot.transform.position.z+0.25f);
            if(OctPivot.transform.position.x>83&&OctPivot.transform.position.z<45){
                OctPivot.transform.position = a;
            }
            else{
                //transform.position = new Vector3(83, transform.position.y, 45);
                //Debug.Log(transform.position.z);
                OctPivot.transform.position = new Vector3(83, OctPivot.transform.position.y, 45);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                OctPivot.transform.rotation = Quaternion.Euler(0, 0, 0);
                BossState = 14;
                TakoAnime.SetBool("Idle", true);
                TakoAnime.SetBool("Move", false);
                TakoAnime.SetBool("Stop", false);
                //Debug.Log(BossState);
            }
        }
        else if(BossState==14){
            Vector3 a = new Vector3(OctPivot.transform.position.x, OctPivot.transform.position.y-0.25f, OctPivot.transform.position.z);
            if (OctPivot.transform.position.y > 7.5f)
            {
                OctPivot.transform.position = a;
            }
            else{
                BossState = 0;
            }
        }
        if (BossState >= 7 && BossState < 11)
        {
            time += Time.deltaTime;
            //Debug.Log(time);
        }

	}

    IEnumerator BossMove(){
        Lqqp++;
        if(Lqqp==4){
            Lqqp = 1;
        }
        BossState = 1;
        //Debug.Log("Called");
        for (int i = 0; i < 8;i++){
            missiles[i] = Instantiate(missile, new Vector3(62 + (i * 6), -20, 17), Quaternion.Euler(-90, 0, 0));
            switch(Lqqp){
                case 1: missiles[i].AddComponent<MissileBS1_M>(); break;
                case 2: missiles[i].AddComponent<MissileBS2_M>(); break;
                case 3: missiles[i].AddComponent<MissileBS3_M>(); break;
            }
        }
        yield return new WaitForSeconds(2f);//攻撃
        //for (int i = 0; i < 8; i++)
        //{
        //    MissileManager.shotSpeed = 50;
            //MissileManager.Shot(missile);
        //}
        TakoAnime.SetBool("Attack", true);
        yield return new WaitForSeconds(3f);
        TakoAnime.SetBool("Attack", false);
        yield return new WaitForSeconds(3f);
        BossState = 2;//左向く
        //Debug.Log("call" + BossState);
        yield return new WaitForSeconds(2f);
        TakoAnime.SetBool("Idle", false);
        TakoAnime.SetBool("Move", true);
        TakoAnime.SetBool("Stop", true);
        yield return new WaitForSeconds(3f);
        BossState = 3;//移動
        //Debug.Log("call" + BossState);
        yield return new WaitForSeconds(4f);
        BossState = 5;
        yield return new WaitForSeconds(4f);
        BossState = 7;
        Beam.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(10.3f);
        yield return new WaitForSeconds(6f);
        BossState = 12;
        Ashiba.GetComponent<BoxCollider>().isTrigger = true;


        //yield return new WaitForSeconds(5f);
        //Debug.Log("call"+BossState);
    }
}
