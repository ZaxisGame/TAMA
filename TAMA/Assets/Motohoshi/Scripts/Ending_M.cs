using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending_M : MonoBehaviour {
    [SerializeField]
    GameObject OctPivot;
    [SerializeField]
    GameObject BossPerformance;
    [SerializeField]
    GameObject wave;
    [SerializeField]
    GameObject Bomb;
    GameObject BigBang;
    [SerializeField]
    GameObject core;
    [SerializeField]
    GameObject EndingCam;
    [SerializeField]
    Image panel;
    [SerializeField]
    SpriteRenderer thx;
    GameObject[] Bombs;
    int state;
    AudioSource audioSource;
    public List<AudioClip> audioClip = new List<AudioClip>();
    bool fadeout;
    float alpha;
	// Use this for initialization
	void Start () {
        fadeout = false;
        Bombs = new GameObject[8];
        Debug.Log("unko");
        Bombs[0] = Instantiate(Bomb, new Vector3(80, 10, 37), Quaternion.identity);
        Bombs[1] = Instantiate(Bomb, new Vector3(87, 8, 45), Quaternion.identity);
        Bombs[2] = Instantiate(Bomb, new Vector3(75, 4, 43), Quaternion.identity);
        Bombs[3] = Instantiate(Bomb, new Vector3(77, 7, 41), Quaternion.identity);
        Bombs[4] = Instantiate(Bomb, new Vector3(83, 10, 40), Quaternion.identity);
        Bombs[5] = Instantiate(Bomb, new Vector3(82, 5, 45), Quaternion.identity);
        Bombs[6] = Instantiate(Bomb, new Vector3(90, 2, 39), Quaternion.identity);
        Bombs[7] = Instantiate(Bomb, new Vector3(88, 6, 48), Quaternion.identity);
        for (int i = 0; i < 8;i++){
            Bombs[i].GetComponent<ParticleSystem>().Stop();
        }
        BigBang = Instantiate(Bomb,new Vector3(83,3.8f,30),Quaternion.identity);
        BigBang.transform.localScale = new Vector3(3,3,3);
        BigBang.GetComponent<ParticleSystem>().Stop();
        state = 0;
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (core.GetComponent<BossLife_M>().life == 0 && state == 0)
        {
            StartCoroutine("Bomber");
            Shake(5.6f, 1f, EndingCam);
            state = 1;
        }

        if (fadeout)
        {
            alpha += 0.1f;
            panel.color = new Color(0, 0, 0, alpha);
        }
        if(alpha>=1){
            thx.enabled = true;
        }
	}


    public void Shake(float duration, float magnitude,GameObject obj)
    {
                   StartCoroutine(DoShake(duration, magnitude,obj));
    }

    private IEnumerator DoShake(float duration, float magnitude,GameObject obj)
    {
        var pos = obj.transform.localPosition;

        var elapsed = 0f;

        while (elapsed < duration)
        {
            var x = pos.x + Random.Range(-1f, 1f) * magnitude;
            var y = pos.y + Random.Range(-1f, 1f) * magnitude;

            obj.transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        obj.transform.localPosition = pos;
    }

    IEnumerator Bomber(){
        Bombs[0].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.5f);
        Bombs[1].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.5f);
        Bombs[2].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.5f);
        Bombs[3].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.5f);
        Bombs[4].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.5f);
        Bombs[5].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.5f);
        Bombs[6].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.5f);
        Bombs[7].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.5f);
        Bombs[5].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.5f);
        Bombs[2].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.5f);
        Bombs[4].GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(1.5f);
        BigBang.GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(audioClip[0]);
        yield return new WaitForSeconds(0.3f);
        BossPerformance.SetActive(false);
        core.SetActive(false);
        OctPivot.SetActive(false);
        wave.SetActive(false);
        fadeout = true;
    }
}
