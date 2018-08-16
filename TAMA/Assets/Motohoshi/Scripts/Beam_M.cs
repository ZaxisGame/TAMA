using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_M : MonoBehaviour {
    RaycastHit hit;
    ParticleSystem beam;

    [SerializeField]
    bool isEnable = false;
    int count = 0;
    bool isHits;
    float radius;
	private void Start()
	{
        beam = GetComponent<ParticleSystem>();
        radius = transform.lossyScale.x * 0.5f;
	}

	private void Update()
	{
        count++;
            if (count > 200)
        {
            Vector3 rol = transform.rotation.eulerAngles;
            rol.x++;
            transform.rotation = Quaternion.Euler(rol);
        }
        if(Physics.SphereCast(transform.position, radius, transform.forward * 10, out hit)){
            //beam.startLifetime = hit.distance;
        }
	}
}
