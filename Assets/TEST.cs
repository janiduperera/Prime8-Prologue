using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TEST : MonoBehaviour
{
    public string SceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        SaveDataStatic.MissionList.Clear();
        SaveDataStatic.MissionList.Add("Complete recycling competition", "null");
        SaveDataStatic.MissionList.Add("Collect Litter!", "null");
        SaveDataStatic.MissionList.Add("Repair pipe and clear that drain!", "null");
        SaveDataStatic.MissionList.Add("Help Tiny Tiger with Sapling Tree!", "null");
        SaveDataStatic.MissionList.Add("Stop the smoking car!", "null");
        SaveDataStatic.MissionList.Add("Help Postman to clear Graffiti!", "null");

        if (SceneToLoad == "Town")
        {
            PlayerPrefs.SetFloat("GameTime", 0);
            SetCharacterPositions();
            SaveDataStatic.Score = 100;
            SaveDataStatic.MissionList["Complete recycling competition"] = "100| ";
        }
        SceneManager.LoadSceneAsync(SceneToLoad);

        //string s = "00:12:34";
        //System.DateTime d = System.Convert.ToDateTime(s);
        //Debug.Log(d);
    }

    void SetCharacterPositions()
    {
        SaveDataStatic.StorySequence = "Litter";
        //SaveDataStatic.StorySequence = "LitterEnd";
        //SaveDataStatic.StorySequence = "PRepStart";
        //SaveDataStatic.StorySequence = "PRepEnd";
        //SaveDataStatic.StorySequence = "Drain";
        //SaveDataStatic.StorySequence = "DrainFinish";
        //SaveDataStatic.StorySequence = "SaplingTreeVideoStart";
        //SaveDataStatic.StorySequence = "SaplingTree";
        //SaveDataStatic.StorySequence = "SaplingTreeEnd";
        //SaveDataStatic.StorySequence = "SmokeCarBegin";
        //SaveDataStatic.StorySequence = "SmokeCarEnd";
        //SaveDataStatic.StorySequence = "PostBox";
        //SaveDataStatic.StorySequence = "GrafitiBegin";
        //SaveDataStatic.StorySequence = "GrafitiEnd";
        //SaveDataStatic.StorySequence = "SeaLife";

    }
}
