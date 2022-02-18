using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullVidTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(SaveDataStatic.StorySequence != "Litter")
        {
            Destroy(gameObject);
        }
    }

	void OnTriggerEnter(Collider col)
	{
		if(TownController.GetInstance().GetActiveISelObj() != null && TownController.GetInstance().GetActiveISelObj().Name == "Panda")
		{
			TownController.GetInstance ().PlayTheSeagullVideo ();
			Destroy (gameObject);
		}
	}
}
