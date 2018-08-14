using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_scr_K : MonoBehaviour {
    //　弾のゲームオブジェクト
    [SerializeField]
    public GameObject bulletPrefab;
    public GameObject muzzle;
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
        GameObject bulletInstance =  Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.identity);
        rb = bulletInstance.GetComponent<Rigidbody>();
        this.transform.forward =  Target.transform.position - muzzle.transform.position;
        rb.AddForce(transform.forward * shotSpeed);
        Destroy(bulletInstance, 2f);
    }
}
