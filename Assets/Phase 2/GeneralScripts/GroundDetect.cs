using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : MonoBehaviour
{
    private Rigidbody m_MyRigidBody;

    // Start is called before the first frame update
    void Awake()
    {
        m_MyRigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Ground")
        {
            m_MyRigidBody.isKinematic = true;
            Destroy(this);
        }
    }
}
