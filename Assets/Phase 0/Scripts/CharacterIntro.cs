using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class CharacterIntro : MonoBehaviour {
	
	private float wait;

	#if UNITY_WEBGL
	private WebGLMovieTexture movieTexture;
	#else
	public VideoPlayer movieTexture;
	#endif
	private AudioSource audioSrc;

	private float holdTime = 0f;
	private bool skipNow = false;
	private GameObject mainmenu;
	public GameObject SeveralMonthObj;

    public Text SkipText;

	// Use this for initialization
	void Start () {
        mainmenu = GameObject.Find ("MainMenu");

        if (mainmenu)
            mainmenu.GetComponent<AudioSource> ().Stop ();

#if UNITY_IOS || UNITY_ANDROID
        SkipText.text = "Touch screen to Skip..";
#else
        SkipText.text = "Press Space to Skip..";
#endif

        GameObject go = GameObject.Find("MusicLoop");
        if (go != null)
        {
            Destroy(go);
        }

        audioSrc = GetComponent<AudioSource>();

        transform.localScale = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x,
            Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, 1);

#if UNITY_WEBGL
		movieTexture = new WebGLMovieTexture("StreamingAssets/P8_Game_Intro_Medium_640x480.mp4");
		GetComponent<Renderer>().material.mainTexture = movieTexture;
#else
        //      GetComponent<Renderer>().material.mainTexture = movie;
        //movieTexture = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        movieTexture.loopPointReached += EndReachedOnVideo;
        movieTexture.url = Application.streamingAssetsPath + "/P8_Game_Intro_Medium_640x480.mp4";          //Set Audio Output to AudioSource         movieTexture.audioOutputMode = VideoAudioOutputMode.AudioSource;          //Assign the Audio from Video to AudioSource to be played         movieTexture.EnableAudioTrack(0, true);         movieTexture.SetTargetAudioSource(0, audioSrc);         movieTexture.controlledAudioTrackCount = 1;         movieTexture.Play();
#endif
	}

	void Update(){

		if (KeyHold () && !m_HasSkipIntroCalled) {
			StartCoroutine (SkipIntro ());
		} else {
	#if UNITY_WEBGL
			movieTexture.Update ();

			if (Mathf.Approximately (movieTexture.time, movieTexture.duration)) {
				//Destroy(GetComponent<Renderer>().material.mainTexture);
				//gameObject.SetActive (false);
				//System.GC.Collect();
			LoadNextScene();
			}
		#endif
		}
	}


    float duration = 2f;
    bool KeyHold()
    {
        skipNow = false;
#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            //if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            //{
            //    holdTime += Time.deltaTime;
            //    if (holdTime >= duration) skipNow = true;
            //}
            //if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
            //{
            //    holdTime = 0f;
            //}
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                return true;
            }
        }
#endif

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.Space))
        {
            holdTime += Time.deltaTime;
            if (holdTime >= duration) skipNow = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            holdTime = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }
#endif

        return skipNow;
    }

    void EndReachedOnVideo(VideoPlayer _vp)
    {
        LoadNextScene();
        movieTexture.gameObject.SetActive(false);
    }

    bool m_HasSkipIntroCalled = false;
	IEnumerator SkipIntro(){

		m_HasSkipIntroCalled = true;

		#if UNITY_WEBGL
		movieTexture.Pause();
		audioSrc.Stop ();
		yield return new WaitForSeconds (1f);
		gameObject.SetActive (false);

		#else
		movieTexture.Stop ();
        movieTexture.gameObject.SetActive(false);

		//audioSrc.Stop ();
		yield return new WaitForSeconds (1f);
		#endif

//		DestroyImmediate(GetComponent<Renderer>().material.mainTexture);
//		System.GC.Collect();

		LoadNextScene ();
	}

	void LoadNextScene()
	{
		GetComponent<Renderer> ().enabled = false;
        SkipText.text = "";

        SeveralMonthObj.SetActive (true);
	}
}
