using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameFinishPanel : MonoBehaviour {

	public GameObject	WinSection;
	public GameObject	LostSection;
	public GameObject	StartMenuPanel;

	public GameObject  Panda;
	public GameObject	Rhino;

	public void SetGameFinishPanel(bool _hasWon)
	{
        TownController.GetInstance().StatScreen.alpha = 0;

		if(_hasWon)
		{
			WinSection.SetActive(true);
			LostSection.SetActive(false);
		}
		else
		{
			WinSection.SetActive(false);
			LostSection.SetActive(true);
		}
	}

	public void OnYesTryAgainClicked()
	{
		gameObject.SetActive(false);

	}

	public void OnNoTryAgainClicked()
	{
		Application.Quit();
	}

	public void OnDoneClicked()
	{
		gameObject.SetActive(false);

		Rhino.GetComponent<Rhino>().enabled = false;
		Rhino.GetComponent<FirstPersonController>().enabled = false;
		Rhino.transform.GetChild(0).gameObject.SetActive(false);

		Rhino.transform.GetChild(1).gameObject.SetActive(true);
		Rhino.transform.Find("RhinoLitterPicker").gameObject.SetActive(false);
		Rhino.transform.Find("RhinoGameWon").gameObject.SetActive(false);

		Panda.GetComponent<Panda>().enabled = true;
		Panda.GetComponent<FirstPersonController>().enabled = false;
		Panda.transform.GetChild(0).gameObject.SetActive(false);
		Panda.transform.Find("AmandaPanda").gameObject.SetActive(false);

//		Panda.GetComponent<Panda>().PlayPandaClapAnim();
	}
}
