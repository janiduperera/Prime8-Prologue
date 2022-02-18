using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.Video;

public class MoviePlayer : MonoBehaviour {

	#if UNITY_WEBGL
//	public WebGLMovieTexture[] Movies;
	private bool m_StartMovie = false;
	private WebGLMovieTexture	m_MovieTexture;
	#else
	private VideoPlayer	    m_MovieTexture;
	#endif
	private AudioSource		m_AudioSource;

	// Use this for initialization
	void Awake () {

		transform.localScale = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x,
			Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y-Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, 1);
	

		m_AudioSource = GetComponent<AudioSource>();

        m_MovieTexture = GetComponent<VideoPlayer>();

#if !UNITY_WEBGL
        m_MovieTexture.enabled = true;
        m_MovieTexture.loopPointReached += EndReachedOnVideo;

        //Set Audio Output to AudioSource
        m_MovieTexture.audioOutputMode = VideoAudioOutputMode.AudioSource;          //Assign the Audio from Video to AudioSource to be played         m_MovieTexture.EnableAudioTrack(0, true);         m_MovieTexture.SetTargetAudioSource(0, m_AudioSource);         m_MovieTexture.controlledAudioTrackCount = 1;
#else
        m_MovieTexture.enabled = false;
#endif
    }

    public void PlayMovie(){

		if (TownController.GetInstance ().MapCamera.enabled)
        {
            TownController.GetInstance().ControlMapCamera(false);
            //TownController.GetInstance().MapCamera.enabled = false;
        }


		if(InventoryPanel.GetInstance().IsInventoryActive())
		{
			InventoryPanel.GetInstance().UnSetInventory();
		}

		TownController.GetInstance ().TownControllerAudio.mute = true;
		TownController.GetInstance ().PandaAdHocAudio.mute = true;

        TownController.GetInstance().JoyStickSettings(false);

		if(!SaveDataStatic.forTesting)
		{
#if UNITY_WEBGL
			m_StartMovie = false;
#endif

			TownController.GetInstance().HideCursor();

			if(SaveDataStatic.StorySequence == "Litter")
			{
#if UNITY_WEBGL

				if (SaveDataStatic.PlayTheSeagullVideo == false) {
					m_MovieTexture = new WebGLMovieTexture("StreamingAssets/LitterDrop.mp4");
					GetComponent<Renderer>().material.mainTexture = m_MovieTexture;

				} else {

					Destroy(GetComponent<Renderer>().material.mainTexture);
					System.GC.Collect();
					m_MovieTexture = new WebGLMovieTexture("StreamingAssets/SeagullStealing.mp4");
					GetComponent<Renderer>().material.mainTexture = m_MovieTexture;
				}
#else
				if (SaveDataStatic.PlayTheSeagullVideo == false) {
                    m_MovieTexture.url = Application.streamingAssetsPath + "/LitterDrop.mp4";
				} else {
                    m_MovieTexture.url = Application.streamingAssetsPath + "/SeagullStealing.mp4";
                }
                m_MovieTexture.Play();
#endif
            }
			else if(SaveDataStatic.StorySequence == "Drain")
			{


#if UNITY_WEBGL
				Destroy(GetComponent<Renderer>().material.mainTexture);
				System.GC.Collect();
				m_MovieTexture = new WebGLMovieTexture("StreamingAssets/HippoWaterJump.mp4");
				GetComponent<Renderer>().material.mainTexture = m_MovieTexture;
#else
                m_MovieTexture.url = Application.streamingAssetsPath + "/HippoWaterJump.mp4";
                m_MovieTexture.Play();
#endif

            }
			//else if(SaveDataStatic.StorySequence == "SaplingTree")
            else if(SaveDataStatic.StorySequence == "SaplingTreeVideoStart")
			{


#if UNITY_WEBGL
				Destroy(GetComponent<Renderer>().material.mainTexture);
				System.GC.Collect();
				m_MovieTexture = new WebGLMovieTexture("StreamingAssets/TigerSaplingTree.mp4");
				GetComponent<Renderer>().material.mainTexture = m_MovieTexture;
#else
                m_MovieTexture.url = Application.streamingAssetsPath + "/TigerSaplingTree.mp4";
                m_MovieTexture.Play();
#endif

            }	
			else if(SaveDataStatic.StorySequence == "Graffiti")
			{


#if UNITY_WEBGL
				Destroy(GetComponent<Renderer>().material.mainTexture);
				System.GC.Collect();
				m_MovieTexture = new WebGLMovieTexture("StreamingAssets/PostManHat.mp4");
				GetComponent<Renderer>().material.mainTexture = m_MovieTexture;
#else
                m_MovieTexture.url = Application.streamingAssetsPath + "/PostManHat.mp4";
                m_MovieTexture.Play();
#endif

            }
			else if(SaveDataStatic.StorySequence == "SmokeCarBegin")
			{


#if UNITY_WEBGL
				Destroy(GetComponent<Renderer>().material.mainTexture);
				System.GC.Collect();
				m_MovieTexture = new WebGLMovieTexture("StreamingAssets/SmokeCar.mp4");
				GetComponent<Renderer>().material.mainTexture = m_MovieTexture;
#else
                m_MovieTexture.url = Application.streamingAssetsPath + "/SmokeCar.mp4";
                m_MovieTexture.Play();
#endif

            }	
			else if(SaveDataStatic.StorySequence == "SmokeCarEnd")
			{


#if UNITY_WEBGL
				Destroy(GetComponent<Renderer>().material.mainTexture);
				System.GC.Collect();
				m_MovieTexture = new WebGLMovieTexture("StreamingAssets/CarKey.mp4");
				GetComponent<Renderer>().material.mainTexture = m_MovieTexture;
#else
                m_MovieTexture.url = Application.streamingAssetsPath + "/CarKey.mp4";
                m_MovieTexture.Play();
#endif

            }	
			else if(SaveDataStatic.StorySequence == "GrafitiEnd")
			{


#if UNITY_WEBGL
				Destroy(GetComponent<Renderer>().material.mainTexture);
				System.GC.Collect();
				m_MovieTexture = new WebGLMovieTexture("StreamingAssets/PandaLetterPostAndThoughtBuble.mp4");
				GetComponent<Renderer>().material.mainTexture = m_MovieTexture;
#else
                m_MovieTexture.url = Application.streamingAssetsPath + "/PandaLetterPostAndThoughtBuble.mp4";
                m_MovieTexture.Play();
#endif


            }

#if UNITY_WEBGL
			//yield return null;

			m_StartMovie = true;
	
#else
			//m_MovieTexture = GetComponent<Renderer>().material.mainTexture as MovieTexture;
			//m_AudioSource.clip = m_MovieTexture.audioClip;

			//m_AudioSource.Play();
			//m_MovieTexture.Play();

			//yield return new WaitForSeconds (m_MovieTexture.duration);

			//TownController.GetInstance ().TownControllerAudio.mute = false;
			//TownController.GetInstance ().PandaAdHocAudio.mute = false;

			//TownController.GetInstance ().MapCamera.enabled = true;

			//TownController.GetInstance().OnMoviePlayCompletion();

			//gameObject.SetActive(false);
#endif


		}
		else
		{
			//yield return new WaitForSeconds(1f);
			TownController.GetInstance().OnMoviePlayCompletion();

			gameObject.SetActive(false);
		}
	}

    void EndReachedOnVideo(VideoPlayer _vp)
    {
        TownController.GetInstance().TownControllerAudio.mute = false;
        TownController.GetInstance().PandaAdHocAudio.mute = false;

        TownController.GetInstance().ControlMapCamera(true);
        TownController.GetInstance().JoyStickSettings(true);
        TownController.GetInstance().OnMoviePlayCompletion();
       

        gameObject.SetActive(false);
    }

#if UNITY_WEBGL
	void Update()
	{
		
		if(m_StartMovie)
		{
			m_MovieTexture.Update ();

			if (Mathf.Approximately (m_MovieTexture.time, m_MovieTexture.duration)) {

				m_StartMovie = false;
				Destroy(GetComponent<Renderer>().material.mainTexture);

				TownController.GetInstance ().TownControllerAudio.mute = false;
				TownController.GetInstance ().PandaAdHocAudio.mute = false;

				TownController.GetInstance().OnMoviePlayCompletion();

				gameObject.SetActive(false);
				System.GC.Collect();
			}
		}

    }
#endif

}
