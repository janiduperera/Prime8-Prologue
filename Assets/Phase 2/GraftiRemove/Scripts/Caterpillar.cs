using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Caterpillar : MonoBehaviour {

	public Material		CaterpillarMat;
	public Material		ClothMat;

	private Caterpillar m_Next;

	public void SetNext(Caterpillar _in)
	{
		m_Next = _in;
	}

	public Caterpillar GetNext()
	{
		return m_Next;
	}

	public void RemoveTail()
	{
		Destroy(this.gameObject);
	}

	public void SetClothTexture()
	{
		GetComponent<Renderer>().material = ClothMat;
	}

	public void SetCaterpillarTexture()
	{
		GetComponent<Renderer>().material = CaterpillarMat;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Grafti")
		{
			Destroy(other.gameObject);
			GrafiController.GetInstance().SetMaxSize();

			GameObject[] leftObjs = GameObject.FindGameObjectsWithTag("Grafti");

			//Debug.Log("Left Obj Count : " + leftObjs.Length);

			if(leftObjs.Length == 1)
			{
				Debug.Log("Grafiti Won");
				// Win
				GrafiController.GetInstance().GameWon();
			}
		}
		else if(other.tag == "Obstacle" || other.tag == "Caterpillar")
		{
			//Call Game Over
			//GrafiController.GetInstance().ShowGameFinishPanel();
			StartCoroutine(GrafiController.GetInstance().GameOverAudioPlay());
		}
	}
}
