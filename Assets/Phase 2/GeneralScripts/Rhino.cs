using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

public class Rhino : MonoBehaviour, ISelectObject {

	public Transform 		LitterPicker;
	public Transform		PickerHolister;

	private Animator		m_RhinoAnimator;
	private Animator		m_RhinoLitterAnimator;

	private Ray				ray;
	private RaycastHit 		hit;

	public AudioClip 		RSA2_Walking;
	public AudioClip 		RSA7_Running;

	public AudioClip 		RSA4_OneClickSuccess;
	public AudioClip 		RSA8_OneClickFail;

	public AudioClip 		RSA3_TwoClickSuccess;
	public AudioClip 		RSA10_TwoClickFail;

	public AudioClip 		RSA5_ThreeClickFail;

	public AudioClip 		RSA13_FoundPickerHolster;

	public AudioClip 		RSA15_FoundLitterPicker;

	public AudioClip[] 		RandomSoundClips;

	public Sprite			LitterPickerSprite;
	public Sprite			PickerHolsterSprite;

	private AudioSource		m_RhinoAudioSource;

	private FirstPersonController 	m_RhinoFPS;
	private GameObject				m_RhinoFPSCamera;
	private GameObject				m_Rhino;
	private GameObject				m_RhinoLitter;
	private GameObject				m_RhinoWon;

    private TownController m_TownControlerInstance;

	private bool m_IsCharacter 		= true;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "Rhino";
	public string Name
	{
		get { return m_Name; }
		set { m_Name = value; }
	}

	void Awake()
	{
		m_RhinoFPS 			= GetComponent<FirstPersonController>();
		m_RhinoFPSCamera 	= transform.Find("FirstPersonCharacter").gameObject;
		m_Rhino		 		= transform.Find("Rhino").gameObject;
		m_RhinoLitter 		= transform.Find("RhinoLitterPicker").gameObject;
		m_RhinoWon	 		= transform.Find("RhinoGameWon").gameObject;

		m_RhinoAudioSource 	= GetComponent<AudioSource>();
	}

    void Start()
    {
        m_TownControlerInstance = TownController.GetInstance();
    }

    public void SelectObject() 
	{
		m_RhinoFPS.enabled = true;

		m_RhinoFPSCamera.SetActive(true);
		m_Rhino.SetActive(true);

		m_RhinoLitter.SetActive(false);
		m_RhinoWon.SetActive(false);

		TownController.GetInstance().LockCursorForGame();
		TownController.GetInstance().ShowCursor(true);

		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().enabled = true;
		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().SetMyLocation(transform);
	}

	public void DeSelectObject()
	{
		m_RhinoFPS.enabled = false;

		m_RhinoFPSCamera.SetActive(false);

		m_Rhino.SetActive(true);

		m_RhinoLitter.SetActive(false);
		m_RhinoWon.SetActive(false);
	}

	public void DoLocalAnimation(int animNo)
	{
		TownController.GetInstance().SetHasUIComeUP(true);

		if(animNo == 0)
		{
			DeSelectObject();

			m_Rhino.SetActive(false);
			m_RhinoWon.SetActive(false);

			m_RhinoLitter.SetActive(true);
		}
		else if(animNo == 1)
		{
			DeSelectObject();

			m_Rhino.SetActive(false);
			m_RhinoLitter.SetActive(false);

			m_RhinoWon.SetActive(true);
		}
	}

	public void DeactivateFPS()
	{
		m_RhinoFPS.enabled = false;
	}

	public void AddInventoryItem(string _name)
	{
		if(m_InventoryNameList.Contains(_name)) return;

		if(_name == "Litter Picker")
		{
			m_InventoryItemList.Add(LitterPickerSprite);
		}
		else if(_name == "Litter Picker Holster Belt")
		{
			m_InventoryItemList.Add(PickerHolsterSprite);
		}

		m_InventoryNameList.Add(_name);
	}
	public void RemoveFromInventory(string _name)
	{
	}

	public Sprite m_RuffSackSprite;
	public Sprite RuffSackSprite
	{
		get { return m_RuffSackSprite; }
	}

	private List<Sprite> m_InventoryItemList = new List<Sprite>();
	public List<Sprite> InventoryItemList
	{
		get { return m_InventoryItemList; }
	}

	public void SetInventoryItem(Sprite item)
	{
		m_InventoryItemList.Add(item);
	}

	private List<string> m_InventoryNameList = new List<string>();
	public List<string> InventoryNameList
	{
		get { return m_InventoryNameList; }
	}

	public void SetInventoryItemName(string name)
	{
		m_InventoryNameList.Add(name);
	}

	private List<Sprite> m_MedalSpriteList = new List<Sprite>();
	public List<Sprite> MedalSpriteList
	{
		get { return m_MedalSpriteList; }
	}

	public void SetMedalSprite(Sprite medal)
	{
		m_MedalSpriteList.Add(medal);
	}


	public void LitterGunAnimationComplete()
	{
		m_LitterClickCount = 0;

		TownController.GetInstance().SetGameStartPanel(true);
	}

	public void OnRhinoWinAnimationComplete()
	{
		TownController.GetInstance().AfterRhinoWinAnimationIsPlayed();
	}

	private int 			m_LitterClickCount = 0;


    public void CharacterUpdate(bool clickedStatus)
    {
        if (m_TownControlerInstance.LitterCollectGameStart && m_TownControlerInstance.LitterList.Count > 0) // Game Start
        {
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (m_RhinoFPS.IsMoving)
            {
                PlayRandomAudioWhileWandering();
            }

            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.collider.tag == "litter")
                {
                    m_TownControlerInstance.ShowCursor(false);

                    if (clickedStatus)
                    {
                        ItemSelectAudio.GetInstance().PlayAudio(false);

                        m_TownControlerInstance.LitterList.Remove(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);

                        if (m_LitterClickCount == 1)
                        {
                            PlayOneClickSound(false);
                        }
                        else if (m_LitterClickCount == 2)
                        {
                            PlayTwoClickSound(false);
                        }

                        m_LitterClickCount = 0;
                    }

                    if (m_TownControlerInstance.LitterList.Count == 0)
                    {
                        m_TownControlerInstance.OnLitterCollectGameCompletion();
                    }
                }
                else
                {
                    m_TownControlerInstance.ShowCursor(true);

                    if (clickedStatus)
                    {
                        if (m_LitterClickCount + 1 == 1)
                        {
                            PlayOneClickSound(true);
                        }
                        else if (m_LitterClickCount + 1 == 2)
                        {
                            PlayTwoClickSound(true);
                        }
                        else if (m_LitterClickCount + 1 == 3)
                        {
                            PlayThreeClickSound(true);
                        }

                        m_LitterClickCount++;
                    }
                }
            }
            else
            {
                m_TownControlerInstance.ShowCursor(true);
            }
        }
    }

    bool m_IsRandomSoundPlayed = false;
	void PlayRandomAudioWhileWandering()
	{
		if(m_IsRandomSoundPlayed) return;

		int m_RanNo = Random.Range(0, 1000);

		if(m_RanNo < 200)
		{
			m_RhinoAudioSource.PlayOneShot(RandomSoundClips[0]);
		}
		else if(m_RanNo < 450)
		{
			m_RhinoAudioSource.PlayOneShot(RandomSoundClips[1]);
		}
		else if(m_RanNo < 800)
		{
			m_RhinoAudioSource.PlayOneShot(RandomSoundClips[2]);
		}
		else
		{
			m_RhinoAudioSource.PlayOneShot(RandomSoundClips[3]);
		}

		m_IsRandomSoundPlayed = true;
		StartCoroutine(WaitAfterRandomSoundIsPlayed());
	}

	IEnumerator WaitAfterRandomSoundIsPlayed()
	{
		yield return new WaitForSeconds(3);
		m_IsRandomSoundPlayed = false;
	}

	void PlayOneClickSound(bool isFail)
	{
		if(isFail)
		{
			m_RhinoAudioSource.clip = RSA8_OneClickFail;
		}
		else
		{
			m_RhinoAudioSource.clip = RSA4_OneClickSuccess;
		}

		m_RhinoAudioSource.Play();
	}

	void PlayTwoClickSound(bool isFail)
	{
		if(isFail)
		{
			m_RhinoAudioSource.clip = RSA10_TwoClickFail;
		}
		else
		{
			m_RhinoAudioSource.clip = RSA3_TwoClickSuccess;
		}

		m_RhinoAudioSource.Play();
	}

	void PlayThreeClickSound(bool isFail)
	{
		if(isFail)
		{
			m_RhinoAudioSource.clip = RSA5_ThreeClickFail;
		}
		else
		{
			m_RhinoAudioSource.clip = RSA3_TwoClickSuccess;
		}

		m_RhinoAudioSource.Play();
	}

	void PlayLitterPickerFoundSound()
	{
		m_RhinoAudioSource.clip = RSA15_FoundLitterPicker;
		m_RhinoAudioSource.Play();
	}

	void PlayPickerHolsterFoundSound()
	{
		m_RhinoAudioSource.clip = RSA13_FoundPickerHolster;
		m_RhinoAudioSource.Play();
	}
}
