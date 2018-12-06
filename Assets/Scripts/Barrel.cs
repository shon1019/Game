using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Barrel : MonoBehaviour {
    public ParticleSystem _explodeParticle;
    public float _vThreshold = 0.5f;
    public float _power = 10;
    public float _upPower = 2f;
    public float _radius = 5;
    public float _lifeTime = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody otherRig = collision.gameObject.GetComponent<Rigidbody>();
            float v = otherRig.velocity.magnitude;
            if (v > _vThreshold)
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
                }
            }
        }
    }

}
