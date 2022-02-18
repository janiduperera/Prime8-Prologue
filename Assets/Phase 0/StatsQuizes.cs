using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsQuizes : MonoBehaviour {

    public KitchenGamePlay kitchenGamePlay;
    public Texture2D handTexure;
    public GameObject cursor4;
    public GameObject panda;
    public GameObject bobBird;
    public GameObject quizDes;
    public GameObject cursor;

    public Text quizText;
    public string []quizStr;
    public GameObject[] answers;
	public AudioClip[] quesclips;
    public AudioClip[] ansclips;
    private Animator bobAnim;
    private bool toggle = false;
	private AudioSource audioSrc;
	// Use this for initialization
	void Start () {
        bobAnim = bobBird.GetComponent<Animator>();
		audioSrc = GetComponent<AudioSource> ();
	}

    private int m_QuesitonIndex = 0;

    public void ShowQuiz1(int index) // Question 3 is called
    {
        kitchenGamePlay.pointerDot.GetComponent<Image>().enabled = false;
        kitchenGamePlay.cursor.GetComponent<Image>().enabled = false;

        m_QuesitonIndex = index;
        cursor.SetActive(false);
#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
#else
        kitchenGamePlay.JoyStickSettings(false);
#endif
        bobAnim.SetBool("popup", true);
        bobAnim.SetBool("exit", false);
        StartCoroutine(PopUpQuiz());
        StartCoroutine(ReadQuiz1(index));
       // StartCoroutine(ShowAnswer(index));
        panda.GetComponent<MonoBehaviour>().enabled = false;

        kitchenGamePlay.HasQuizStarted = true;
    }

	public void ShowQuiz(int index){ // Question 1 and 2 is called

        m_QuesitonIndex = index;

        kitchenGamePlay.pointerDot.GetComponent<Image>().enabled = false;
        kitchenGamePlay.cursor.GetComponent<Image>().enabled = false;

        cursor.SetActive(false);
#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
#else
        kitchenGamePlay.JoyStickSettings(false);
#endif
        bobAnim.SetBool("popup", true);
		bobAnim.SetBool("exit",false);
		StartCoroutine(PopUpQuiz());
		StartCoroutine(ReadQuiz(index));
        //StartCoroutine(ShowAnswer(index));
        panda.GetComponent<MonoBehaviour>().enabled = false;

        kitchenGamePlay.HasQuizStarted = true;
	}

    int m_TogSelected;
    public void BatteryQuestionToggleSelected(int _toggleSelected)
    {
        m_TogSelected = _toggleSelected;
    }

    public void HideQuiz(int index){

        if(index == 1) // 2nd Question
        {
            if(m_TogSelected == 1 || m_TogSelected == 2)
            {
                return;
            }
        }

        cursor.SetActive(true);
#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#else
        kitchenGamePlay.JoyStickSettings(true);
#endif
        bobAnim.SetBool("popup", false);
		bobAnim.SetBool("exit",true);
		quizDes.SetActive(false);
		audioSrc.Stop ();
        answers[index].SetActive(false);
        panda.GetComponent<MonoBehaviour>().enabled = true;
        answers[3].SetActive(false);

        kitchenGamePlay.HasQuizStarted = false;

        kitchenGamePlay.pointerDot.GetComponent<Image>().enabled = true;
        kitchenGamePlay.cursor.GetComponent<Image>().enabled = true;
    }

    IEnumerator ShowAnswer(int index)
    {
        #if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(handTexure, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
#endif
        panda.GetComponent<MonoBehaviour>().enabled = false;
        //yield return new WaitForSeconds(quesclips[index].length);
        yield return null;
        answers[index].SetActive(true);
        audioSrc.clip = ansclips[index];
        audioSrc.Play();
        answers[3].SetActive(true);
    }

    IEnumerator PopUpQuiz()
    {
        yield return new WaitForSeconds(1f);
        quizDes.SetActive(true);
        answers[3].SetActive(true);
    }
    IEnumerator ReadQuiz1(int index)
    {
        quizText.text = "";
        audioSrc.clip = quesclips[index];
        audioSrc.Play();
        // yield return new WaitForSeconds(5);
        //for (int i = 0; i < quizStr[index].Length; i++)
        //{
        //    quizText.text = quizText.text + quizStr[index][i];
        //    yield return new WaitForSeconds((quesclips[index].length / quizStr[index].Length));// - 0.008f);
        //}
        yield return StartCoroutine(DisplayTyping(quizStr[index], quesclips[index].length));

        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(ShowAnswer(index));
    }
    IEnumerator ReadQuiz(int index)
    {
        yield return new WaitForSeconds(1);
        quizText.text = "";
		audioSrc.clip = quesclips [index];
		audioSrc.Play ();
        //yield return new WaitForSeconds(0);
        //for (int i = 0; i < quizStr[index].Length; i++)
        //{
        //    quizText.text = quizText.text + quizStr[index][i];
        //    yield return new WaitForSeconds((quesclips[index].length / quizStr[index].Length));// - 0.008f);
        //}

        yield return StartCoroutine(DisplayTyping(quizStr[index], quesclips[index].length));

        yield return new WaitForSeconds(0.5f);
        yield return  StartCoroutine(ShowAnswer(index));
    }

    IEnumerator DisplayTyping(string sub, float displayFor)
    {
        float timer = 0;
        bool hurry = false;
        quizText.text = "";

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
            onDis -= quizText.text.Length;
            for (int i = 0; i < onDis; i++)
            {
                quizText.text += sub[0];
                sub = sub.Remove(0, 1);
                if (sub.Length <= 0)
                    break;
            }

            yield return null;
        }

        //if hurried, post the rest of the phrase immediately and set scroll to max
        if (sub.Length > 0)
            quizText.text += sub;
        yield return null;//give UI time to update before fixing scroll position
    
    }
}
