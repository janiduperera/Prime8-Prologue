using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class RecycleMedalStars : MonoBehaviour {

	public GameObject Medal;
	public GameObject[] Stars;

	public AudioClip  StarAudioClip;
	public AudioClip  Prime8ChorusClip;
	public AudioClip  Prime8SecondChorusClip;

	private AudioSource m_RewardAudioSource;

	public GameObject panda;

    public KitchenGamePlay kitchenGamePlay;
    public InventryOnclick InventryOnclick;

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
        kitchenGamePlay.pointerDot.GetComponent<Image>().enabled = false;

        panda.GetComponent<MonoBehaviour>().enabled = false;
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

		Medal.SetActive(false);

		panda.transform.GetChild(0).gameObject.SetActive(true);
		panda.GetComponent<MonoBehaviour>().enabled = true;
        kitchenGamePlay.pointerDot.GetComponent<Image>().enabled = true;

        InventryOnclick.MedalOk = true;

#if UNITY_IOS || UNITY_ANDROID
        kitchenGamePlay.JoyStick.gameObject.SetActive(true);
#endif
    }


}
