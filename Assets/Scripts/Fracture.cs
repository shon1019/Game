using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Fracture : MonoBehaviour
{
    public List<Collider> _objCollider = new List<Collider>();  //part building
    public float _vThreshold = 0.5f;
    public float _lifeTime = 10;

    private void Start()
    {
        if (Application.isPlaying)
        {
            AddChild();
        }
    }

    private void AddChild()
    {
        _objCollider = new List<Collider>(this.GetComponentsInChildren<Collider>());
        if (_objCollider.Count == 0)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody otherRig = other.GetComponent<Rigidbody>();
            float v = otherRig.velocity.magnitude;
            if (v > _vThreshold)
            {
                //Destroy(this.GetComponent<Collider>(), 1);
                Destroy(this.gameObject, _lifeTime);
                foreach (Collider c in _objCollider)
                {
                    Rigidbody rb = c.GetComponent<Rigidbody>();
                    if (rb) {
                        rb.isKinematic = false;
                        rb.AddForce((otherRig.transform.position - this.transform.position).normalized * v);
                    }
                }
            }
        }
    }
}
