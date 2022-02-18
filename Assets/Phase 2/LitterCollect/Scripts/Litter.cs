using UnityEngine;
using System.Collections;

public class Litter : MonoBehaviour {

	float time  = 0;
	Rigidbody rg;
	Vector3 torqueDir;

	// Use this for initialization
	void Start () {
	
		rg = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		time += Time.deltaTime;

		if(time > 4)
		{
			time = 0;
			GetRandomTorgueDirection();

		}
		rg.AddTorque(torqueDir * 200);
	}

	void GetRandomTorgueDirection()
	{
		int ranNo = Random.Range(0, 10000);

		if(ranNo < 2500)
		{
			torqueDir = Vector3.right;
		}
		else if(ranNo < 5000)
		{
			torqueDir = Vector3.left;
		}
		else if(ranNo < 7500)
		{
			torqueDir = Vector3.forward;
		}
		else
		{
			torqueDir = Vector3.back;
		}
	}
}
