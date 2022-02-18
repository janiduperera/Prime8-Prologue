using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmokingCar : MonoBehaviour {

	public GameObject Exauster;
	public Transform[] HidingAreas;
	public Transform  SnakeTransform;
	public Transform  SnakeCameraTransform;
	public GameObject SmokingCarCam;
	public GameObject ClothThatBlock;

	private List<ParticleSystem> m_SmokeParticle = new List<ParticleSystem>();

	private Animator m_CarWindowAnimator;

	private Vector3 m_SamSnakeGameStartPos;

	private AudioSource	m_SmokingCarAudioSource;
	public AudioClip	SneakingAudioClip;
	public AudioClip	BlastAudioClip;
	public AudioClip	LaughterAudioClip;

	private AudioSource	m_CarEngineAudioSource;

	void Awake()
	{
		m_CarWindowAnimator = GetComponent<Animator>();

		m_SmokingCarAudioSource = GetComponent<AudioSource>();
		m_CarEngineAudioSource = transform.Find("car").gameObject.GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {

		name = "Smoking Car";
		TownController.GetInstance ().SmokingCar = gameObject;
		TownController.GetInstance ().CatepillarCloth = ClothThatBlock;
		TownController.GetInstance ().Exauster = Exauster;

		//int i = 0;
		foreach(Transform child in Exauster.transform)
		{
			m_SmokeParticle.Add(child.gameObject.GetComponent<ParticleSystem>());

			child.gameObject.GetComponent<ParticleSystem>().Stop();
//			if(i < 2)
//			{
//				child.gameObject.GetComponent<ParticleSystem>().Play();
//			}
//			else 
//			{
//				child.gameObject.GetComponent<ParticleSystem>().Stop();
//			}
//
//			i++;
		}

		for(int r = 0; r < HidingAreas.Length; r++)
		{
			HidingAreas[r] = (Transform)Instantiate(HidingAreas[r]);
			HidingAreas[r].gameObject.GetComponent<HidingArea>().enabled = false;
			HidingAreas[r].gameObject.GetComponent<Collider>().enabled = false;
		}


	}

	public void StartSmokeInCar()
	{
		foreach(Transform child in Exauster.transform)
		{
			m_SmokeParticle.Add(child.gameObject.GetComponent<ParticleSystem>());

			child.gameObject.GetComponent<ParticleSystem>().Play();
		}
	}

    private System.DateTime m_StartTime;
    private System.DateTime m_EndTime;
    public void StartSmokingCarGame()
	{
        TownController.GetInstance().DidSmokingCarStarted = true;
		m_CarWindowAnimator.SetBool("lookAt", true);

		for(int i = 0; i < HidingAreas.Length; i++)
		{
			HidingAreas[i].gameObject.GetComponent<HidingArea>().enabled = true;
			HidingAreas[i].gameObject.GetComponent<Collider>().enabled = true;
		}

		if (SnakeTransform == null) {
			SnakeTransform = TownController.GetInstance ().SamSnake.transform;
			SnakeCameraTransform = SnakeTransform.GetChild (0);
		}

		m_SamSnakeGameStartPos = SnakeTransform.position;

		SmokingCarCam.SetActive(true);

		TownController.GetInstance().BackGroundAudio.Stop();

		m_SmokingCarAudioSource.clip = SneakingAudioClip;
		m_SmokingCarAudioSource.loop = true;
		m_SmokingCarAudioSource.Play();

		m_CarEngineAudioSource.Play();

        m_StartTime = System.DateTime.Now;
        TownController.GetInstance().StatScreen.alpha = 0;
    }
	
	public void WhenLookAtSamsDirection()
	{
		if (SnakeTransform == null) {
			SnakeTransform = TownController.GetInstance ().SamSnake.transform;
			SnakeCameraTransform = SnakeTransform.GetChild (0);
		}

		if(Vector3.Distance(transform.position, SnakeTransform.position) > 140f)
		{
			return;
		}

		bool m_IsWithInRange = false;
		for(int i = 0; i < HidingAreas.Length; i++)
		{
			if(HidingAreas[i].gameObject.GetComponent<HidingArea>().IsHiding)
			{
				m_IsWithInRange = true;
				break;
			}
		}

		if(!m_IsWithInRange)
		{
			//Debug.Log("Is Not Within Range");
			//Increase the volume of car 
			//Make the player go back to original position
			SnakeTransform.position = m_SamSnakeGameStartPos;
			TownController.GetInstance().SetSubtitleText("Driver saw you! Need to start again!", /*3*/1, null);


            TownController.GetInstance().DidSmokingCarStarted = false;
            //		StartCoroutine(PlayBlastAudio());

            //
            TownController.GetInstance().GetActiveISelObj().DeactivateFPS();

			TownController.GetInstance().SetHasUIComeUP(true);

			TownController.GetInstance().GameCompletePanel.SetActive(true);
			TownController.GetInstance().GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(false);

           // TownController.GetInstance().UnLockCursorFromGame();
            TownController.GetInstance().SetQuizAnswerCursor();
            //
        }
		else
		{
			//Debug.Log("Within Range");

		}

	}

	public void PlaySmokingCarGameAgain()
	{
        TownController.GetInstance().DidSmokingCarStarted = true;

        TownController.GetInstance().GetActiveISelObj().SelectObject ();
		TownController.GetInstance().SetHasUIComeUP(false);
		TownController.GetInstance().LockCursorForGame();
        TownController.GetInstance().StatScreen.alpha = 0;
        //TownController.GetInstance().HideCursor();
        StartCoroutine(PlayBlastAudio());
	}

	IEnumerator PlayBlastAudio()
	{
		m_SmokingCarAudioSource.loop = false;
		m_SmokingCarAudioSource.clip = BlastAudioClip;
		m_SmokingCarAudioSource.Play();

		yield return new WaitForSeconds(BlastAudioClip.length);

		m_SmokingCarAudioSource.clip = SneakingAudioClip;
		m_SmokingCarAudioSource.loop = true;
		m_SmokingCarAudioSource.Play();

        m_StartTime = System.DateTime.Now;
    }

	public void OnAnimationEnd()
	{
		m_CarWindowAnimator.SetBool("lookAt", false);
		StartCoroutine(StartTheAnimationAgain());
	}

	IEnumerator StartTheAnimationAgain()
	{
		yield return new WaitForSeconds(0.5f);

		m_CarWindowAnimator.SetBool("lookAt", true);
	}

	public void StopTheSmokingCar()
	{
        TownController.GetInstance().DidSmokingCarStarted = false;
        SmokingCarCam.SetActive(false);

		for(int i = 0; i < HidingAreas.Length; i++)
		{
			HidingAreas[i].gameObject.GetComponent<HidingArea>().enabled = false;
		}

		foreach(Transform child in Exauster.transform)
		{
			child.gameObject.SetActive(false);
		}

		ClothThatBlock.SetActive(true);

		m_SmokingCarAudioSource.loop = false;
		m_SmokingCarAudioSource.Stop();

		m_CarEngineAudioSource.Stop();

        m_EndTime = System.DateTime.Now;
        System.TimeSpan m_TimeDiff = m_EndTime - m_StartTime;
        SaveDataStatic.SmokeCarTookTime = m_TimeDiff.ToString(@"hh\:mm\:ss");
        TownController.GetInstance().SetScore(100);
        SaveDataStatic.MissionList["Stop the smoking car!"] = "100|" + SaveDataStatic.SmokeCarTookTime;

        TownController.GetInstance().StatScreen.alpha = 1;
    }

	public AudioSource GetSmokingCarAudioSource()
	{
		return m_SmokingCarAudioSource;
	}
}
