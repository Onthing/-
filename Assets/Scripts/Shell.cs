using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float radius;
    public LayerMask damageMask;
    public float damage = 20;
    public AudioSource audios;
    public ParticleSystem par;

    public bool isRotate = true;
    void Start()
    {
        Destroy(gameObject, 3.5f);
        if (isRotate)
        {
            GetComponent<Rigidbody>().AddTorque(transform.right * 100);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        var colliders=Physics.OverlapSphere(transform.position, radius, damageMask);
        foreach (var collider in colliders)
        {
            var target = collider.GetComponent<PlayerCharacter>();
            if (target)
            {
                target.TakeDamage(damage);
            }
        }
        audios.Play();
        par.transform.parent = null;
        par.Play();
        ParticleSystem.MainModule mainMoudule = par.main;
        Destroy(par.gameObject, mainMoudule.duration);
        Destroy(gameObject);
    }
	
}
