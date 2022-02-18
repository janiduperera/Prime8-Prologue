using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {
	public GameObject panda;
	public GameObject exitMenu;
	private MonoBehaviour pandaBehav;
	private bool toggleExitMenu = false;

    // Use this for initialization
    void Start () {
		pandaBehav = panda.GetComponent<MonoBehaviour> ();
	}

#if !(UNITY_IOS || UNITY_ANDROID)
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Escape)){
            OnPauseButtonClick();
        }
    }
#endif

    public void NavigateScene(int index){
        //SceneManager.LoadSceneAsync (index);
        SaveDataStatic.SceneName = "ComingToHome";
        SaveGameOnExit();
        SceneManager.LoadSceneAsync("welcome");
    }

	public void ExitGame(){
		Application.Quit();
	}

	public void Hide(){
		exitMenu.SetActive (false);
#if !(UNITY_IOS || UNITY_ANDROID)
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
#endif
		pandaBehav.enabled = true;
		toggleExitMenu = false;
	}

    private void OnApplicationPause(bool pause)
    {
        //if(pause)
        //{
        //    SaveGameOnExit();
        //}
    }

	public void SaveGameOnExit()
	{
		PlayerPrefs.SetString ("Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt("ChoosenAvatar", SaveDataStatic.ChoosenAvatar);
    }

    public void OnPauseButtonClick()
    {
        toggleExitMenu = !toggleExitMenu;
        exitMenu.SetActive(toggleExitMenu);
#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.visible = toggleExitMenu;
        Cursor.lockState = toggleExitMenu ? CursorLockMode.None : CursorLockMode.Locked;
#endif
        pandaBehav.enabled = !toggleExitMenu;
    }
}
