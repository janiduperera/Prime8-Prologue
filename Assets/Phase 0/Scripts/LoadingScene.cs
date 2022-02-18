using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingAnimation());
    }

    IEnumerator LoadingAnimation()
    {
        float x = 0;
        Text m_LoadingTxt = LoadingPanel.transform.GetChild(0).Find("Text").gameObject.GetComponent<Text>();
        m_LoadingTxt.text = "Loading";
        string m_Dot = "";
        while (x < 6)
        //while(!m_LeaderboardLoaded)
        {
            if (m_Dot.Length >= 3)
            {
                m_Dot = "";
            }
            m_Dot += ".";
            x += 1f;
            m_LoadingTxt.text = "Loading" + m_Dot;
            yield return new WaitForSeconds(1f);
        }


        SceneManager.LoadSceneAsync("welcome", LoadSceneMode.Additive);
       // LoadingPanel.GetComponent<Animator>().enabled = true;
    }
}
