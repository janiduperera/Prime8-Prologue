using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JaniduAddSeveralMonths : MonoBehaviour {

	private string s1 = "SEVERAL MONTHS AGO SOMEWHERE IN SUBURBIA...";
	private string s2 = "THIS IS WHERE IT ALL BEGAN…";

	public void OnFirstFade()
	{
		GetComponent<Text> ().text = s2;
	}

	public void OnAnimEnd()
	{

#if UNITY_IOS || UNITY_ANDROID
        SceneManager.LoadSceneAsync("Room");
#else
        SceneManager.LoadSceneAsync ("Instructions");
#endif
    }
}
