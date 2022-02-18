using UnityEngine;
using System.Collections;

public class Soundmanager2 : MonoBehaviour {
	public AudioSource efxSource;
	public AudioSource musicSource;
	public static Soundmanager2 instance = null;
	
	public float lowPitchRange = .95f;
	public float highPitchRange = 1.05f;
	
	// Use this for initialization
	void Awake () {
		efxSource.volume = 1.0f;
		if(instance == null){
			instance = this;
		}else if(instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

    public IEnumerator playwater(AudioClip clip)
    {
        yield return new WaitForSeconds(3);
        efxSource.clip = clip;
        efxSource.Play();
    }

	public void playSingle(AudioClip clip){
		efxSource.clip = clip;
		efxSource.Play();
     //   Debug.Log(efxSource.clip.name);
	}
	
	public void playSingle1(AudioClip clip){
		efxSource.clip = clip;
		efxSource.Play();
		efxSource.loop = true;
	}
	public void playMusic(float delayTime){
		musicSource.PlayDelayed (delayTime);
	}
	
	public AudioSource getEfxSource(){
		return this.efxSource;
	}
	
	public AudioSource getMusicSource(){
		return this.musicSource;
	}
	
	// Random Sound Playing
	public void RandomizeSfx(params AudioClip[] clips){
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(lowPitchRange,highPitchRange);
		
		efxSource.pitch = randomPitch;
		efxSource.clip = clips[randomIndex];
		efxSource.Play();
		
	}

}
