using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonChange : MonoBehaviour {
	public static ButtonChange instance = null;
	public Sprite speakerOn;
	public Sprite speakerOff;
	private Sprite img = null;
	private AudioSource music;
	public static bool musicOn = true;
	private GameObject mainmenu;
	private AudioSource mainmenumusic;

	void Awake(){
		mainmenu = GameObject.Find ("MainMenu");
		if (mainmenu)
			mainmenumusic = mainmenu.GetComponent<AudioSource> ();
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}
	
	public void OnClick(){
		musicOn = !musicOn;
		if (musicOn) {
			if (mainmenu)
			mainmenumusic.Play ();
			if(SoundManager.instance)
				SoundManager.instance.getMusicSource().Play();
		} else {
			if (mainmenu)
			mainmenumusic.Pause ();
			if(SoundManager.instance)
				SoundManager.instance.getMusicSource().Pause();
		}
		img = musicOn ? speakerOn : speakerOff;
		this.GetComponent<Image> ().sprite = img;
	}
	
	public void SpeakerClicked(){
		musicOn = !musicOn;
		if (musicOn) {
			if(SoundManager.instance)
				SoundManager.instance.getMusicSource().Play();
		} else {
			if(SoundManager.instance)
				SoundManager.instance.getMusicSource().Pause();
		}
		img = musicOn ? speakerOn : speakerOff;
		this.GetComponent<Image> ().sprite = img;
	}
}
