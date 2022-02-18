using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Quiz : MonoBehaviour {

	public GameObject[]	QuizObjects;

	public Animator		BobBirdAnim;

	private GameObject	m_QuizDescriptionSecObj;
	private GameObject	m_QuizAndAnswerSecObj;
	private GameObject	m_SubmitBtnObj;
	private Text 		m_QuizDescriptionTxt;
	private AudioSource	m_AudioSource;
	private AudioSource m_WrongAnsAudioSource;

	private int 		m_QuizNo;
	private string[] 	m_Quez;

	public AudioClip[]	QuizAudioClips;
	public AudioClip[]	Q1AnswerAudioClips;
	public AudioClip[]	Q2AnswerAudioClips;
	public AudioClip[]	Q3AnswerAudioClips;
	public AudioClip[]	Q4AnswerAudioClips;
	public AudioClip[]	Q5AnswerAudioClips;
	public AudioClip[]	Q6AnswerAudioClips;

	public AudioClip	WrongAnswerAudioClip;

	public GameObject[]	Medals;

	private Coroutine	m_PopUpQuizCoroutine = null;

	#region Singleton
	private static Quiz m_Instane;
	private Quiz(){}
	public static Quiz GetInstance()
	{
		return m_Instane;
	}
	#endregion Singleton

	void Awake()
	{
		m_Instane = this;
	}

	void Start () {
	
		m_Quez = new string[6];

		m_Quez[0] = "Litter is still sadly a huge problem across the UK. " +
			"There are many theories on why some people don’t care about what " +
			"they drop on the floor but the fact remains that it can be seen on roadsides, " +
			"in parks and gardens, on beaches, in the sea and rivers…in fact almost anywhere. " +
			"Not only does it look terrible but it can block drains and harm wildlife amongst many things!";

		m_Quez[1] = "Water is a very precious commodity and therefore " +
			"any water leaks need to be repaired swiftly and efficiently! " +
			"Did you know that even a tap that is left to drip once every 5 " +
			"seconds will effectively waste 1 litre of water every day; that is " +
			"7 litres of water in one week! So you can imagine what a huge leak like this will waste!";

		m_Quez[2] = "There are many reasons for drains and drainage channels in our streets." +
			" Usually they are there to make sure the roads and pavements are kept clear of " +
			"surface water when it rains. In 2012 there were many floods across the UK when we " +
			"experienced high levels of rainfall. Some of this flooding was due to blocked surface " +
			"drainage and may well have been prevented if the drains had been kept clear!";

		m_Quez[3] = "Many new trees need a helping hand with support in form of a stake to give added " +
			"strength as they grow in case of heavy winds. Sometimes you will also see them surrounded by " +
			"wire mesh to give added protection – and in some cases from potential animal damage.";

		m_Quez[4] = "Since legislation (new rules) in 1998 the car industry has made great efforts to improve " +
			"and reduce emissions from car exhausts with the target of 140g CO2/km being set. In 2009 this target " +
			"figure was reduced to 130g CO2/km. This is anticipated to reduce to 95g CO2/km by 2020.";

		m_Quez[5] = "Whilst graffiti is considered by most as a form of criminal damage, it is officially described " +
			"as ‘any inscription, marking, writing, painting or drawing, illicitly scratched, scribbled, drawn, cut, " +
			"carved, posted, pasted, sprayed or painted on any surface’. The word illicitly means \"not allowed by custom "+
			"or law\".\r The cost of removing graffiti can run into hundreds of pounds so it is very very sad when we see some!";
		
		m_AudioSource = GetComponent<AudioSource>();
		m_WrongAnsAudioSource = transform.Find("WrongAnsAudio").gameObject.GetComponent<AudioSource>();
	}
	
	public void ShowQuez(int _quizNo)
	{
		TownController.GetInstance().HasQuizStarted = true;
		GameObject m_Med;
        TownController.GetInstance().JoyStickSettings(false);
        TownController.GetInstance().ControlMapCamera(false);

		if(_quizNo == 7)
		{
			//Medals[_quizNo].SetActive(true);
			m_Med = (GameObject)Instantiate (Medals [_quizNo]);
			m_Med.name = GetMedalName(_quizNo);

			// Conservation
			SaveDataStatic.AddToAwardedMedalList("Conservation");
		}
		else if(_quizNo != 6)
		{
			m_QuizNo = _quizNo;

			BobBirdAnim.SetBool("popup", true);
			BobBirdAnim.SetBool("exit", false);

			TownController.GetInstance().BackGroundAudio.volume = 0.5f;

			TownController.GetInstance().SetQuizAnswerCursor();

			m_PopUpQuizCoroutine = StartCoroutine(PopUpQuiz());
		}
		else
		{
			//Medals[_quizNo].SetActive(true);
			m_Med = (GameObject)Instantiate (Medals [_quizNo]);
			m_Med.name = GetMedalName(_quizNo);

			// Sea Life and Ocean
			SaveDataStatic.AddToAwardedMedalList("SeaLifeAndOcean");
		}
	}

	string GetMedalName(int _index)
	{
		string m_MedName = "";

		switch (_index) {

		case 0:
			m_MedName = "Prime8_medal_LeaveItClean";
			break;
		case 2:
			m_MedName = "Water Conserver Medal";
			break;
		case 3:
			m_MedName = "Tree Protector medal";
			break;
		case 4:
			m_MedName = "Anti Pollution Medal";
			break;
		case 5:
			m_MedName = "Clean Environment Medal";
			break;
		case 6:
			m_MedName = "Sea-life and Oceans Medal";
			break;
		case 7:
			m_MedName = "Conservation Medal";
			break;
		}

		return m_MedName;
	}

	IEnumerator PopUpQuiz()
	{
		yield return new WaitForSeconds(1f);

		QuizObjects[m_QuizNo].SetActive(true);

		m_QuizDescriptionSecObj = QuizObjects[m_QuizNo].transform.Find("QDes").gameObject;
		m_QuizAndAnswerSecObj = QuizObjects[m_QuizNo].transform.Find("QnA").gameObject;
		m_SubmitBtnObj = QuizObjects[m_QuizNo].transform.Find("SubmitBtn").gameObject;

		m_QuizDescriptionTxt = m_QuizDescriptionSecObj.transform.Find("QusDesTxt").gameObject.GetComponent<Text>();

		m_QuizDescriptionTxt.text = "";
		m_AudioSource.clip = QuizAudioClips[m_QuizNo];
		m_AudioSource.Play();

        //float m_QuizReadSeconds = 0;
        //for (int i = 0; i < m_Quez[m_QuizNo].Length; i++)
        //{
        //    m_QuizDescriptionTxt.text = m_QuizDescriptionTxt.text + m_Quez[m_QuizNo][i];
        //    yield return new WaitForSeconds((QuizAudioClips[m_QuizNo].length / m_Quez[m_QuizNo].Length));
        //}

        yield return StartCoroutine(DisplayTyping(m_Quez[m_QuizNo], QuizAudioClips[m_QuizNo].length));

        while (m_AudioSource.isPlaying)
            yield return null;

        yield return new WaitForSeconds(2);

		m_QuizAndAnswerSecObj.SetActive(true);
		m_SubmitBtnObj.SetActive(true);

		AudioClip[] m_AnswerClips = GetAudioClips();

		int m_AnsAudioNo = 0;
		foreach(AudioClip au in m_AnswerClips)
		{
			m_AudioSource.clip = au;
			m_AudioSource.Play();

			yield return new WaitForSeconds(au.length);

			m_AnsAudioNo++;

			if(m_AnsAudioNo % 2 == 0 && m_AnsAudioNo != m_AnswerClips.Length - 1) // short pause between the two questions but not in synopsis
			{
				yield return new WaitForSeconds(1);
			}
		}

//		TownController.GetInstance().SetQuizAnswerCursor();
	}

    IEnumerator DisplayTyping(string sub, float displayFor)
    {
        float timer = 0;
        bool hurry = false;
        m_QuizDescriptionTxt.text = "";

        float defaultDelayBetweenLetters = 0.1f; //how long to wait before displaying the next letter

        //  displayArea.transform.Find("AutherImg").gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(_autherImageLocation);
        float timeBetweenLetters = (displayFor + .001f) / ((float)sub.Length);
        float bonusPadding = 0; //any displayFor time that's left after typing has finished gets added to the timePadding

        if (timeBetweenLetters > defaultDelayBetweenLetters)
        {
            timeBetweenLetters = defaultDelayBetweenLetters;
            bonusPadding = displayFor - timeBetweenLetters * sub.Length;
            if (bonusPadding < 0)//for fringe cases
                bonusPadding = 0;
        }

        while (sub.Length > 0 && !hurry)
        {
            timer += Time.deltaTime;
            float onDis = Mathf.Round(timer / timeBetweenLetters);
            onDis -= m_QuizDescriptionTxt.text.Length;
            for (int i = 0; i < onDis; i++)
            {
                m_QuizDescriptionTxt.text += sub[0];
                sub = sub.Remove(0, 1);
                if (sub.Length <= 0)
                    break;
            }

            yield return null;
        }

        //if hurried, post the rest of the phrase immediately and set scroll to max
        if (sub.Length > 0)
            m_QuizDescriptionTxt.text += sub;
        yield return null;//give UI time to update before fixing scroll position

    }

    AudioClip[] GetAudioClips()
	{
		if(m_QuizNo == 0)
		{
			return Q1AnswerAudioClips;
		}
		else if(m_QuizNo == 1)
		{
			return Q2AnswerAudioClips;
		}
		else if(m_QuizNo == 2)
		{
			return Q3AnswerAudioClips;
		}
		else if(m_QuizNo == 3)
		{
			return Q4AnswerAudioClips;
		}
		else if(m_QuizNo == 4)
		{
			return Q5AnswerAudioClips;
		}
		else if(m_QuizNo == 5)
		{
			return Q6AnswerAudioClips;
		}
		else
		{
			return Q1AnswerAudioClips;
		}
	}

	public void SumbitButtonPressed()
	{
	//	if(m_AudioSource.isPlaying) return;

		if(m_QuizAndAnswerSecObj.GetComponent<AnswerToggles>().IsAllAnswersSelected())
		{
			BobBirdAnim.SetBool("popup", false);
			BobBirdAnim.SetBool("exit", true);

			QuizObjects[m_QuizNo].SetActive(false);

			if(m_PopUpQuizCoroutine != null)
				StopCoroutine(m_PopUpQuizCoroutine);

			m_AudioSource.Stop();

			//TownController.GetInstance().AfterSumbitButtonPressed();

			if(SaveDataStatic.StorySequence == "PRepEnd") // Pipe Repair complete
			{
                TownController.GetInstance().AfterSumbitButtonPressed();
                TownController.GetInstance().HasQuizStarted = false;
				TownController.GetInstance().AfterPipReparingComplete();
			}
			else
			{
				//Medals[m_QuizNo].SetActive(true);
				GameObject m_Med = (GameObject)Instantiate (Medals [m_QuizNo]);
				m_Med.name = GetMedalName(m_QuizNo);

				switch(m_QuizNo)
				{
				case 0: // Leave It Clean
					SaveDataStatic.AddToAwardedMedalList("LeaveItClean");
					break;
				case 2: // Water Conservation
					SaveDataStatic.AddToAwardedMedalList("WaterConservation");
					break;
				case 3: // Tree Protector
					SaveDataStatic.AddToAwardedMedalList("TreeProtector");
					break;
				case 4: // Anti Polution
					SaveDataStatic.AddToAwardedMedalList("AntiPolution");
					break;
				case 5: // Clean Environment
					SaveDataStatic.AddToAwardedMedalList("CleanEnvironment");
					break;
				}
			}
		}
		else
		{
			Debug.Log("Not Selected Yet");
		}
	}

	public void OnCorrectAnsRequiredTogSelected(Toggle _toggleBtn)
	{
        if (_toggleBtn.isOn)
        {
            m_WrongAnsAudioSource.clip = WrongAnswerAudioClip;
            m_WrongAnsAudioSource.Play();

            //_toggleBtn.isOn = false;

            _toggleBtn.gameObject.transform.Find("WrongAnsImg").gameObject.GetComponent<Image>().enabled = true;

            StartCoroutine(HideWrongAnswerImageAfterDelay(_toggleBtn.gameObject.transform.Find("WrongAnsImg")));
        }
        else
        {

        }
    }

	IEnumerator HideWrongAnswerImageAfterDelay(Transform _wrongAnsImgTra)
	{
		yield return new WaitForSeconds(2);

		_wrongAnsImgTra.gameObject.GetComponent<Image>().enabled = false;
	}
}
