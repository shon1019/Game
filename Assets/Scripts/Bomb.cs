using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public ParticleSystem _explodeParticle;
    public float _vThreshold = 0.5f;
    public float _power = 10;
    public float _upPower = 2f;
    public float _radius = 5;
    public float _lifeTime = 10;

    void Start()
    {
        _explodeParticle.Play();
        Destroy(this.gameObject, _lifeTime);
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, _radius);
        foreach (Collider c in colliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(_power, this.transform.position, _radius, _upPower, ForceMode.Force);
            }
            if (c.tag != "Player" && c.tag != "Ground")
                Destroy(c.gameObject, _lifeTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _explodeParticle.Play();
        Destroy(this.gameObject, _lifeTime);
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, _radius);
        foreach (Collider c in colliders)
        {
            Rigidbody rb = c.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(_power, this.transform.position, _radius, _upPower, ForceMode.Impulse);
            }
            if (c.tag != "Player" && c.tag != "Ground")
                Destroy(c.gameObject, _lifeTime);
        }
    }
}