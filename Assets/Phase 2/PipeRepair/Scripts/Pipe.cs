using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Pipe : MonoBehaviour {

	public bool IsObjectMoving = false;
	private Transform m_Transform;
	private int[] 	indexArray = new int[2];

	private Renderer m_PipeRenderer;

	private int m_PipeIndex = 0;
	public Texture2D[] PipeTextures;
	public GameObject PipeBack;

	public void SetTexture(int _index, bool _isObjMoving)
	{
		m_PipeIndex = _index;

		m_PipeRenderer.material.SetTexture("_MainTex", PipeTextures[m_PipeIndex]);

		IsObjectMoving = _isObjMoving;
	}

	void Awake ()
	{
		m_Transform = transform;

		m_PipeRenderer = GetComponent<Renderer>();
	}
	
	public void MoveObjectInGrid() // This is used to move this particular object in the relevant Grid with snapping
	{
		StartCoroutine(MovePipe());
	}

	IEnumerator MovePipe()
	{
		while(IsObjectMoving == true)
		{
			if(PipeBack.activeSelf == false) PipeBack.SetActive(true);

			indexArray = GetIndexForPosition(m_Transform.position.x, m_Transform.position.y);

			m_Transform.position = new Vector3 (indexArray[0], indexArray[1], -1);

			yield return 0;
		}

		if(PipeBack.activeSelf == true) PipeBack.SetActive(false);
	}

	int[] GetIndexForPosition(float _x, float _y)
	{
		int[] array = new int[2];

		array[0] = (int)Math.Ceiling(_x);
		array[1] = (int)Math.Ceiling(_y);

		return array;
	}
}
