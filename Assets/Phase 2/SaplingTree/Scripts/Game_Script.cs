using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//sapling tree all chorus sequence sounds like 2 1 4 3 2 1 4 3 4
public class Game_Script : MonoBehaviour {

	public int roundCount;
	public int minVal;
	public int maxVal;

	public GameObject GameOver;
	public GameObject YouWon;
	public GameObject correct;
	public Text winOrOver;
	public GameObject StartBtn;
	public GameObject PlayBtn;

	public List<GameObject> ImgSet;
	public List<GameObject> coloredBtnSet;

	//private int[] shownColorOrde; 

	private int colorCodelength;
	private int PressedColordeBtnCount;

	private int actualRound;

	private bool gameOver;
	private bool correctAns;

	public GameObject GoTxt;

	public GameObject BlackScreen;
	public AudioSource GamePlayAudioSource;
	public AudioClip LG123AudioClip;
	public AudioClip LGGetItUpThereAudioClip;
	public AudioClip LGGreatAudioClip;
	public AudioClip LGWeCanDoThisAudioClip;
	public AudioClip TTCanNotGetTreeUprightAudioClip;
	public AudioClip TTComeOnLittleGorillaAudioClip;
	public AudioClip TTWhatATeamAudioClip;

	public AudioClip LostCoverageAudioClip;
	public Text		 SubtitleTxt;

    private int m_TotalGameRounds = 14;

	void Start () {

		//LoadScenes("SaplingTreeEnd");
        //return;

		actualRound = 0;
		//shownColorOrde = new int[9];

		colorCodelength = -1;
		PressedColordeBtnCount =0;

		gameOver = false;

		if(SaveDataStatic.forTesting)
		{
			LoadScenes("SaplingTreeEnd");
		}

		StartCoroutine(GameBegin());
	}

	IEnumerator GameBegin()
	{
		BlackScreen.SetActive(true);

		yield return StartCoroutine (LostCoverageSubtitle ());

		SubtitleTxt.text = "Come on Little Gorilla...";

		GamePlayAudioSource.PlayOneShot(TTComeOnLittleGorillaAudioClip);
		yield return new WaitForSeconds(TTComeOnLittleGorillaAudioClip.length);


		SubtitleTxt.text = "One. Two.. Three...";
		GamePlayAudioSource.PlayOneShot(LG123AudioClip);
		yield return new WaitForSeconds(LG123AudioClip.length);

		SubtitleTxt.text = "";
		SubtitleTxt.gameObject.SetActive (false);

		yield return new WaitForSeconds(0.5f);

		BlackScreen.SetActive(false);

        m_StartTime = System.DateTime.Now;
	}

    private System.DateTime m_StartTime;
    private System.DateTime m_EndTime;

	IEnumerator LostCoverageSubtitle()
	{
		SubtitleTxt.text = "We apologise but we seem to have temporarily lost pictures. Normal coverage will be returned as soon as possible!";
		GamePlayAudioSource.PlayOneShot(LostCoverageAudioClip);
		yield return new WaitForSeconds(LostCoverageAudioClip.length);

		yield return new WaitForSeconds (2);
		//SubtitleTxt.gameObject.SetActive (false);
	}

	public void StratButton(){
		StartGame();
	}

	private int m_GameRounds = 1;
	private float m_Interval = 1;
	private List<int> m_ShownColorIndexes = new List<int>();
	private List<int> m_SelAnswerIndexes = new List<int>();
	public void StartGame()
	{
		winOrOver.text="";
		StartCoroutine(LightUpImg());
	}

	public void PlayButton(){
		for (int i = 0; i < coloredBtnSet.Count; i++) {
			//coloredBtnSet [i].GetComponent<Button> ().interactable = true;
			coloredBtnSet [i].gameObject.SetActive(true);
		}

		GoTxt.SetActive(true);
	}

	IEnumerator LightUpImg(){

		float r, g, b;
		for(int i = 0; i < m_ShownColorIndexes.Count; i++)
		{
			r = ImgSet[m_ShownColorIndexes[i]].GetComponent<Image> ().color.r;
			g = ImgSet[m_ShownColorIndexes[i]].GetComponent<Image> ().color.g;
			b = ImgSet[m_ShownColorIndexes[i]].GetComponent<Image> ().color.b;

			yield return new WaitForSeconds(m_Interval);
			ImgSet[m_ShownColorIndexes[i]].GetComponent<Image> ().color= new Color (r, g, b,  1f);

			ImgSet[m_ShownColorIndexes[i]].GetComponent<AudioSource>().Play();

			yield return new WaitForSeconds(m_Interval);
			ImgSet[m_ShownColorIndexes[i]].GetComponent<Image> ().color= new Color (r, g, b,  0.5f);
		}

//		m_ShownColorIndexes.Clear();
		m_SelAnswerIndexes.Clear();

		for (int i = m_ShownColorIndexes.Count; i < m_GameRounds; i++) {
			int color = Random.Range (0,4);
            //			int color = m_ChorusArray[i]-1;
            Debug.Log("Color Choosend :  " + color);
			//shownColorOrde[i] =color;

			m_ShownColorIndexes.Add(color);


    		 r = ImgSet[color].GetComponent<Image> ().color.r;
    		 g = ImgSet[color].GetComponent<Image> ().color.g;
    		 b = ImgSet[color].GetComponent<Image> ().color.b;

    			yield return new WaitForSeconds(m_Interval);
    		ImgSet[color].GetComponent<Image> ().color= new Color (r, g, b,  1f);

    			ImgSet[color].GetComponent<AudioSource>().Play();

    			yield return new WaitForSeconds(m_Interval);
    		ImgSet[color].GetComponent<Image> ().color= new Color (r, g, b,  0.5f);
		}

		//PlayBtn.SetActive (true);


		PlayButton();


	}

	IEnumerator LightUpImg2(int color){

		float r = ImgSet[color].GetComponent<Image> ().color.r;
		float g = ImgSet[color].GetComponent<Image> ().color.g;
		float b = ImgSet[color].GetComponent<Image> ().color.b;
		//yield return new WaitForSeconds(0f);
		ImgSet[color].GetComponent<Image> ().color= new Color (r, g, b,  1f);

		yield return new WaitForSeconds(0.2f);
		ImgSet[color].GetComponent<Image> ().color= new Color (r, g, b,  0.5f);
		PressedColordeBtnCount++;
	}
	public void PressedColordeBtn(int c){

		StartCoroutine(PressedAnswerButton(c));
	}

	public AudioSource GameWonAudioSource;
	public AudioSource GameOverAudioSource;

	IEnumerator PressedAnswerButton(int c)
	{
		float r = ImgSet[c].GetComponent<Image> ().color.r;
		float g = ImgSet[c].GetComponent<Image> ().color.g;
		float b = ImgSet[c].GetComponent<Image> ().color.b;
	
		ImgSet[c].GetComponent<Image> ().color= new Color (r, g, b,  1f);
		ImgSet[c].GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(0.2f);
		ImgSet[c].GetComponent<Image> ().color= new Color (r, g, b,  0.5f);

		m_SelAnswerIndexes.Add(c);

		bool m_IsInSequence = true;

		if(m_SelAnswerIndexes.Count == m_ShownColorIndexes.Count)
		{
			for(int i = 0; i < m_SelAnswerIndexes.Count; i++)
			{
				if(m_SelAnswerIndexes[i] != m_ShownColorIndexes[i])
				{
					m_IsInSequence = false;
					break;
				}
			}
		}
		else
		{
			yield break;
		}

		if(m_IsInSequence)
		{
			if(m_GameRounds < m_TotalGameRounds)
			{
				for (int i = 0; i < coloredBtnSet.Count; i++) {
					coloredBtnSet [i].gameObject.SetActive(false);
				}
				GoTxt.SetActive(false);
				yield return new WaitForSeconds(1.1f);
				//m_GameRounds++;
				m_GameRounds = m_ShownColorIndexes.Count + 1;
				m_Interval = m_Interval - (1f/ m_TotalGameRounds)/*0.12f*/;
				StartGame();
			}
			else // Has Finished all the rounds.
			{
				YouWon.SetActive (true);
                m_EndTime = System.DateTime.Now;
                System.TimeSpan m_TimeDiff = m_EndTime - m_StartTime;
                SaveDataStatic.SaplingTreeFinishTime = m_TimeDiff.ToString(@"mm\:ss");

                GameWonAudioSource.Play();

				yield return new WaitForSeconds(GameWonAudioSource.clip.length);

				yield return new WaitForSeconds(1);

				BlackScreen.SetActive(true);

				SubtitleTxt.gameObject.SetActive (true);
				SubtitleTxt.text = "Oh…Erghh…come on Tiny, get it up there!";

				GamePlayAudioSource.PlayOneShot(LGGetItUpThereAudioClip);
				yield return new WaitForSeconds(LGGetItUpThereAudioClip.length);

				SubtitleTxt.text = "Ahhh…";

				GamePlayAudioSource.PlayOneShot(TTCanNotGetTreeUprightAudioClip);
				yield return new WaitForSeconds(TTCanNotGetTreeUprightAudioClip.length);

				SubtitleTxt.text = "We can do this!";

				GamePlayAudioSource.PlayOneShot(LGWeCanDoThisAudioClip);
				yield return new WaitForSeconds(LGWeCanDoThisAudioClip.length);

				SubtitleTxt.text = "";

				yield return new WaitForSeconds(1f);

				SubtitleTxt.text = "Ahhh…what a team!";

				GamePlayAudioSource.PlayOneShot(TTWhatATeamAudioClip);
				yield return new WaitForSeconds(TTWhatATeamAudioClip.length);

				SubtitleTxt.text = "Great!";

				GamePlayAudioSource.PlayOneShot(LGGreatAudioClip);
				yield return new WaitForSeconds(LGGreatAudioClip.length);

				SubtitleTxt.text = "";

				LoadScenes("SaplingTreeEnd");
			}
		}
		else
		{
			GameOver.SetActive (true);

			GameOverAudioSource.Play();

			yield return new WaitForSeconds(GameOverAudioSource.clip.length);

			LoadScenes("SaplingTree");
		}
	}

	public GameObject LoadTxt;

	void LoadScenes(string _gameStatus)
	{
		SaveDataStatic.StorySequence = _gameStatus;

//		//TODO : Remvoe this 
//		SaveDataStatic.StorySequence = "SaplingTreeEnd";
		LoadTxt.SetActive(true);
		SceneManager.LoadSceneAsync("Town");
	}

}
