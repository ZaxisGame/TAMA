using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_scr_K : MonoBehaviour {


    public GameObject cam, swing;
    public Transform target;
    private Vector3 offset, pos;
    float r, x, y, z, y_angle, t;
    public float force = 10f, speed = 2.5f;
    int state;

    void Start(){
        
        offset = swing.GetComponent<Transform>().position - target.position;
        pos = target.position;
        cam.GetComponent<Camera>().orthographic = true;
        r = 10f;
        x = 0;
        z = -10;
        state = 0;
    }

    void Update(){
        swing.GetComponent<Transform>().position = target.position + offset;

        if (Input.GetKeyDown(KeyCode.Return)){
            if (cam.GetComponent<Camera>().orthographic && state != 1){
                cam.GetComponent<Camera>().orthographic = false;
                state = 1;

            }
            else if (!cam.GetComponent<Camera>().orthographic && state != 2){
                state = 2;

            }
        }

        if (state == 1){
            t = 0;
            PlayerMove_scr_K.is2D = false;
            if (y_angle < 90f){
                t += Time.deltaTime;
                y_angle += Mathf.Rad2Deg * Time.deltaTime * speed;
                var rot = Quaternion.Euler(0, y_angle, 0);
                swing.transform.rotation = rot;
            }
            else{
                swing.transform.rotation = Quaternion.Euler(0, 90, 0);
                state = 0;
            }
        }
        else if (state == 2){
            PlayerMove_scr_K.is2D = true;

            if (y_angle > 0f){
                
                y_angle -= (Mathf.Rad2Deg * Time.deltaTime * speed);
                var rot = Quaternion.Euler(0, y_angle, 0);
                swing.transform.rotation = rot;
            }
            else{
                swing.transform.rotation = Quaternion.Euler(0, 0, 0);
                cam.GetComponent<Camera>().orthographic = true;
                state = 0;
            }
        }
    }

    void FixedUpdate(){
        //float hor = Input.GetAxis("Horizontal");
        //float ver = Input.GetAxis("Vertical");

    //    Rigidbody rig = player.GetComponent<Rigidbody>();
    //    if (cam.GetComponent<Camera>().orthographic)
    //        rig.AddForce(hor * force, ver * force, 0);
    //    else
    //        rig.AddForce(ver * force, 0, -hor * force);
    }

}
