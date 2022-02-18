using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GameFinish : MonoBehaviour {

	public GameObject	WinSection;
	public GameObject	LostSection;
	public GameObject	StartMenuPanel;

	public void SetGameFinishPanel(bool _hasWon)
	{
		TownController.GetInstance().StatScreen.alpha = 0;

		if(_hasWon)
		{
			WinSection.SetActive(true);
			LostSection.SetActive(false);
		}
		else
		{
			WinSection.SetActive(false);
			LostSection.SetActive(true);
		}
	}

	public void OnYesTryAgainClicked()
	{
		gameObject.SetActive(false);

        if (SaveDataStatic.StorySequence == "Litter")
        {
            TownController.GetInstance().AfterLitterCollectTryAgainPressed();
        }
        else if (SaveDataStatic.StorySequence == "PRepStart")
        { // Pipe repair game lost, so want to try again.
            TownController.GetInstance().SceneLoad("PipeRepair");
        }
        else if (SaveDataStatic.StorySequence == "SaplingTree")
        { // Sapling Tree game lost, so want to try again.
            TownController.GetInstance().SceneLoad("SaplingTree");
        }
        else if (SaveDataStatic.StorySequence == "GrafitiBegin")
        { // Graffiti Game lost, so want to try again.
            TownController.GetInstance().SceneLoad("GraftiRemove");
        }
        else if (SaveDataStatic.StorySequence == "SmokeCarBegin")
        { //Smoke car game lost, so want to try again
            TownController.GetInstance().SmokingCar.GetComponent<SmokingCar>().PlaySmokingCarGameAgain();
        }
    }

	public void OnNoTryAgainClicked()
	{
		gameObject.SetActive(false);
		SaveDataStatic.IsMiniGameSkiped = true;

		if(SaveDataStatic.StorySequence == "Litter")
		{
			TownController.GetInstance().AfterRhinoWinMsgBoxDoneBtnPressed();
            if (SaveDataStatic.MissionList["Collect Litter!"] == "null")
            {
                TownController.GetInstance().SetScore(50);
                SaveDataStatic.MissionList["Collect Litter!"] = "50";
            }
        }
		else if(SaveDataStatic.StorySequence == "PRepStart") // Pipe Repair game Lost, move on with next sequience. 
		{
			SaveDataStatic.StorySequence = "PRepEnd";
            if (SaveDataStatic.MissionList["Repair pipe and clear that drain!"] == "null")
            {
                TownController.GetInstance().SetScore(50);
                SaveDataStatic.MissionList["Repair pipe and clear that drain!"] = "50";
            }

            TownController.GetInstance().SceneLoad("Town");
		}
		else if(SaveDataStatic.StorySequence == "SaplingTree") // Sapling tree game Lost, move on with next sequience. 
		{
			SaveDataStatic.StorySequence = "SaplingTreeEnd";
            if (SaveDataStatic.MissionList["Help Tiny Tiger with Sapling Tree!"] == "null")
            {
                TownController.GetInstance().SetScore(50);
                SaveDataStatic.MissionList["Help Tiny Tiger with Sapling Tree!"] = "50";
            }
            TownController.GetInstance().SceneLoad("Town");
		}
		else if(SaveDataStatic.StorySequence == "GrafitiBegin") // Graffiti game Lost, move on with next sequience. 
		{
            SaveDataStatic.WasPostBoxCleaned = true;
            SaveDataStatic.StorySequence = "GrafitiEnd";
            if (SaveDataStatic.MissionList["Help Postman to clear Graffiti!"] == "null")
            {
                TownController.GetInstance().SetScore(50);
                SaveDataStatic.MissionList["Help Postman to clear Graffiti!"] = "50";
            }
           
            TownController.GetInstance().SceneLoad("Town");
		}
		else if (SaveDataStatic.StorySequence == "SmokeCarBegin") { //Smoke car game lost, so want to try again

            SaveDataStatic.StorySequence = "SmokeCarEnd";
            if (SaveDataStatic.MissionList["Stop the smoking car!"] == "null")
            {
                TownController.GetInstance().SetScore(50);
                SaveDataStatic.MissionList["Stop the smoking car!"] = "50";
            }

            TownController.GetInstance().SceneLoad("Town");
		}
	}

	public void OnDoneClicked()
	{
		gameObject.SetActive(false);

		if(SaveDataStatic.StorySequence == "Litter")
		{
			TownController.GetInstance().AfterRhinoWinMsgBoxDoneBtnPressed();
            if (SaveDataStatic.MissionList["Collect Litter!"] == "null")
            {
                TownController.GetInstance().SetScore(100);
            }
            SaveDataStatic.MissionList["Collect Litter!"] = "100|"+TownController.GetInstance().LitterGameTime;
        }
		else if(SaveDataStatic.StorySequence == "PRepEnd") // Pipe Repair game is done, move on with next sequience. 
		{
			TownController.GetInstance().AfterHippoWinMsgBoxDoneBtnPressed();
            if (SaveDataStatic.MissionList["Repair pipe and clear that drain!"] == "null")
            {
                TownController.GetInstance().SetScore(100);
            }
            SaveDataStatic.MissionList["Repair pipe and clear that drain!"] = "100|" + SaveDataStatic.PipeRepairFinishTime;
        }
		else if(SaveDataStatic.StorySequence == "SaplingTreeEnd") // Sapling tree game is done, move on with next sequience. 
		{
			TownController.GetInstance().GetActiveISelObj().DeSelectObject();
		
			TownController.GetInstance().OnMoviePlayCompletion();
            if (SaveDataStatic.MissionList["Help Tiny Tiger with Sapling Tree!"] == "null")
            {
                TownController.GetInstance().SetScore(100);
            }
            SaveDataStatic.MissionList["Help Tiny Tiger with Sapling Tree!"] = "100|"+SaveDataStatic.SaplingTreeFinishTime;
        }
		else if(SaveDataStatic.StorySequence == "GrafitiEnd") // Graffiti game is done, move on with next sequience. 
		{
			Quiz.GetInstance().ShowQuez(5);
            if (SaveDataStatic.MissionList["Help Postman to clear Graffiti!"] == "null")
            {
                TownController.GetInstance().SetScore(100);
                SaveDataStatic.MissionList["Help Postman to clear Graffiti!"] = "100|" + SaveDataStatic.GraffityFinishTime;
            }
            SaveDataStatic.WasPostBoxCleaned = true;
        }
	}
}
