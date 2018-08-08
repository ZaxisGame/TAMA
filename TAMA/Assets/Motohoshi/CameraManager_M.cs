using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager_M : MonoBehaviour
{

	public GameObject camera,swing,player;
	public Transform target;
	private Vector3 offset, pos;
	float r, x, y, z, y_angle, t;
	public float force=10f,speed=2.5f;
	private int state,camState;
    public int CamState{
        set{
            this.camState = value;
        }
        get{
            return this.camState;
        }
    }



	// Use this for initialization
	void Start ()
	{
		offset = swing.GetComponent<Transform> ().position - target.position;
		pos = target.position;
		camera.GetComponent<Camera> ().orthographic = true;
		r = 10f;
		speed = 2.5f;
		x = 0;
		z = -10;
		state = 0;
        //camState = 0;
		force = 10;
	}

	// Update is called once per frame
	void Update ()
	{
		swing.GetComponent<Transform> ().position = target.position + offset;
		if (Input.GetKeyDown (KeyCode.A)) {
            camState = -1;
			if (camera.GetComponent<Camera> ().orthographic && state != 1) {
				camera.GetComponent<Camera> ().orthographic = false;
				state = 1;
			} else if (!camera.GetComponent<Camera> ().orthographic && state != 2) {
				state = 2;
			}
		}

		if (state == 1) {
			t = 0;
			if (y_angle < 90f) {
				//Debug.Log (Time.time);
				t += Time.deltaTime;
				y_angle += Mathf.Rad2Deg * Time.deltaTime * speed;
				var rot = Quaternion.Euler (0, y_angle, 0);
				swing.transform.rotation = rot;
			} else {
				swing.transform.rotation = Quaternion.Euler (0, 90, 0);
                camState = 1;
                //Debug.Log("timpo!!");
				state = 0;
			}
		} else if (state == 2){
			if (y_angle > 0f) {
				//Debug.Log (Time.time);
				y_angle -= (Mathf.Rad2Deg * Time.deltaTime * speed);
				var rot = Quaternion.Euler (0, y_angle, 0);
				swing.transform.rotation = rot;
			} else {
				swing.transform.rotation = Quaternion.Euler (0, 0, 0);
				camera.GetComponent<Camera> ().orthographic = true;
                camState = 0;
                //Debug.Log("unko!!");
				state = 0;
			}
		}

	}

    void Jumping(){
        
    }



	//void FixedUpdate () {
	//	float hor = Input.GetAxis ("Horizontal");
	//	float ver= Input.GetAxis ("Vertical");

	//	Rigidbody rig = player.GetComponent<Rigidbody> ();
	//	if(camera.GetComponent<Camera> ().orthographic)
	//		rig.AddForce (hor*force,ver*force, 0);
	//	else
	//		rig.AddForce ( ver*force, 0,-hor*force);
	//}
}
