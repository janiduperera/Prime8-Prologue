using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddColliders : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddCapsuleCollider()
    {
        ////Lamp Posts
        //for(int i = 0; i < transform.childCount; i++)
        //{
        //    CapsuleCollider capsule = gameObject.AddComponent<CapsuleCollider>();
        //    capsule.height = 15;
        //    capsule.radius = 0.5f;
        //    capsule.center = new Vector3(transform.GetChild(i).localPosition.x, 8f, transform.GetChild(i).localPosition.z);
        //}

        //Car Park
        for (int i = 0; i < transform.childCount; i++)
        {
            CapsuleCollider capsule = gameObject.AddComponent<CapsuleCollider>();
            capsule.height = 3.5f;
            capsule.radius = 0.8f;
            capsule.center = new Vector3(transform.GetChild(i).localPosition.x, 2f, transform.GetChild(i).localPosition.z);
        }
    }
}
