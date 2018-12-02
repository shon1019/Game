using UnityEngine;
using System.Collections;

public class ActivateChest : MonoBehaviour
{

    public Transform lid, lidOpen, lidClose;    // Lid, Lid open rotation, Lid close rotation
    public float openSpeed = 5F;                // Opening speed
    public bool canClose;						// Can the chest be closed
    public float AutoDisstory = 20;

    [HideInInspector]
    public bool _open;							// Is the chest opened

    private void Start()
    {
        StartCoroutine(AutoDis());
    }

    void Update()
    {
        if (_open)
        {
            ChestClicked(lidOpen.rotation);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1 * Time.deltaTime, transform.position.z);
            ChestClicked(lidClose.rotation);
        }
    }

    // Rotate the lid to the requested rotation
    void ChestClicked(Quaternion toRot)
    {
        if (lid.rotation != toRot)
        {
            lid.rotation = Quaternion.Lerp(lid.rotation, toRot, Time.deltaTime * openSpeed);
        }
    }

    public void open()
    {
        _open = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        _open = true;
        if (collision.gameObject.tag == "Player")
        {
            //消失
        }
    }

    IEnumerator AutoDis()
    {
        yield return new WaitForSeconds(AutoDisstory);
        Destroy(this.gameObject);
    }
}
