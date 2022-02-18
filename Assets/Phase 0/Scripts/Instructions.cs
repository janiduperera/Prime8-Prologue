using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour {

	public void NavigateScene(int index){
        GameObject go = GameObject.Find("MusicLoop");
        if (go != null)
        {
            Destroy(go);
        }

		SceneManager.LoadSceneAsync (index);
	}

}
