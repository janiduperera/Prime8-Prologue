using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RewardStars : MonoBehaviour {

	public GameObject Medal;
	public GameObject[] Stars;

	public AudioClip  StarAudioClip;
	public AudioClip  Prime8ChorusClip;
	public AudioClip  Prime8SecondChorusClip;

	private AudioSource m_RewardAudioSource;

	void Awake()
	{
		m_RewardAudioSource = GetComponent<AudioSource>();
	}

	void OnEnable()
	{
		StartCoroutine(ShowStarsOneByOne());
	}

	IEnumerator ShowStarsOneByOne()
	{
        TownController.GetInstance().JoyStickSettings(false);
        TownController.GetInstance().ControlMapCamera(false);

        m_RewardAudioSource.clip = StarAudioClip;
		m_RewardAudioSource.Play();

		foreach(GameObject star in Stars)
		{
			star.SetActive(true);

			yield return new WaitForSeconds(1f);

		}

		while(m_RewardAudioSource.isPlaying) yield return null;

		m_RewardAudioSource.clip = Prime8ChorusClip;
		m_RewardAudioSource.Play();

		while(m_RewardAudioSource.isPlaying) yield return null;

		if(Prime8SecondChorusClip != null)
		{
			m_RewardAudioSource.clip = Prime8SecondChorusClip;
			m_RewardAudioSource.Play();

			while(m_RewardAudioSource.isPlaying) yield return null;
		}

		TownController.GetInstance().HasQuizStarted = false;

		if(SaveDataStatic.StorySequence == "Litter")
		{
			//Medal.SetActive(false);
			Destroy (Medal);

			if(SaveDataStatic.IsMiniGameSkiped)
			{
				SaveDataStatic.IsMiniGameSkiped = false;
			}
			else
			{
			}
            //TownController.GetInstance().TimerTxt.text = "";
            TownController.GetInstance().SetTimerUI(false);

            TownController.GetInstance().BackToAmandaPanda("", null);
            TownController.GetInstance().JoyStickSettings(true);
            TownController.GetInstance().ControlMapCamera(true);

        }
		else if(SaveDataStatic.StorySequence == "Drain")
		{
			//Medal.SetActive(false);
			Destroy (Medal);

			if(SaveDataStatic.IsMiniGameSkiped)
			{
				SaveDataStatic.IsMiniGameSkiped = false;
			}
			else
			{
			}
            //TownController.GetInstance().TimerTxt.text = "";
            TownController.GetInstance().SetTimerUI(false);

            TownController.GetInstance().BackToAmandaPanda("", null);
            TownController.GetInstance().JoyStickSettings(true);
            TownController.GetInstance().ControlMapCamera(true);
        }
		else if(SaveDataStatic.StorySequence == "SaplingTreeEnd")
		{
			if(Medal.name == "Tree Protector medal")
			{
				//Medal.SetActive(false);
				Destroy (Medal);
				Quiz.GetInstance().ShowQuez(7); // Show Conservation Medal
				yield break;
			}

			//Medal.SetActive(false);
			Destroy (Medal);

			if(SaveDataStatic.IsMiniGameSkiped)
			{
				SaveDataStatic.IsMiniGameSkiped = false;
			}
			else
			{
			}
            //TownController.GetInstance().TimerTxt.text = "";
            TownController.GetInstance().SetTimerUI(false);

            TownController.GetInstance().BackToAmandaPanda("", null);
		}
		else if(SaveDataStatic.StorySequence == "SmokeCarEnd")
		{
			//Medal.SetActive(false);
			Destroy (Medal);

            //TownController.GetInstance().TimerTxt.text = "";
            TownController.GetInstance().SetTimerUI(false);

            TownController.GetInstance().BackToAmandaPanda("", null);
            TownController.GetInstance().JoyStickSettings(true);
            TownController.GetInstance().ControlMapCamera(true);
        }
		else if(SaveDataStatic.StorySequence == "GrafitiEnd")
		{
			//Medal.SetActive(false);
			Destroy (Medal);

			if(SaveDataStatic.IsMiniGameSkiped)
			{
				SaveDataStatic.IsMiniGameSkiped = false;
				//TownController.GetInstance().SetScore(0);
			}
			else
			{
				//TownController.GetInstance().SetScore(12);
			}
            //TownController.GetInstance().TimerTxt.text = "";
            TownController.GetInstance().SetTimerUI(false);

            TownController.GetInstance().BackToAmandaPanda("", null);
            TownController.GetInstance().JoyStickSettings(true);
            TownController.GetInstance().ControlMapCamera(true);
        }
		else if(SaveDataStatic.StorySequence == "SeaLife")
		{
            TownController.GetInstance().SceneLoad("PilotEnd");
            //Destroy (Medal);


            //TownController.GetInstance().SetTimerUI(false);

            //TownController.GetInstance().BackToAmandaPanda("", null);
            //TownController.GetInstance().JoyStickSettings(true);
            //TownController.GetInstance().ControlMapCamera(true);
        }
    }

}
