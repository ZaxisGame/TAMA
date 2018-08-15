using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_scr_K : MonoBehaviour {
    //　弾のゲームオブジェクト
    [SerializeField]
    public GameObject bulletPrefab;
    public GameObject gun;
    public float shotSpeed = 3000f;
    public GameObject Target;
    Rigidbody rb;

    void Start()
    {
    }

    void Update()
    {
       
    }

    //　敵を撃つ
    public  void Shot()
    {
       
       
        this.transform.forward =  Target.transform.position - gun.transform.position;
        if (this.transform.forward.x <= -0.4f)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            rb = bulletInstance.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * shotSpeed);
            Destroy(bulletInstance, 2f);
            //Debug.Log(this.transform.forward);
        }
    }

    public void Shot_3(){
        Invoke("Shot", 0.1f);
        Invoke("Shot", 0.4f);
        Invoke("Shot", 0.7f);
    }
}
