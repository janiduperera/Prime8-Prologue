using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SaveDataStatic {

	//TODO : Make this false in Production
	public static bool forTesting = false;
    public static bool forSubsOnlyTesting = false;

	public static bool IsObjectLoadComplete = false;

  	public static string StorySequence = "Litter"; // Produciton
    //public static string StorySequence = "LitterEnd";
 //   public static string StorySequence = "SmokeCarBegin";
    // public static string StorySequence = "DrainFinish";

   // public static Vector3 PandaPosition = new Vector3(-924, 2.375f, 22.9f); // Sampling Tree Pos
    //TODO : remove this in production
 //   public static Vector3 PandaPosition = new Vector3(-664f, 2.375f, 454); // Post Box Pos
    //public static Vector3 PandaPosition = new Vector3(-344f, 2.375f, -28); // Drain

    public static Vector3 PandaPosition = new Vector3(70f, 2.375f, 34.25f); // Production

	public static Vector3 RhinoPosition = new Vector3(-385.41f, 2.375f, -25.4f);
	public static Vector3 HippoPosition = new Vector3(-380.5f, 2.375f, -27f);
	//public static Vector3 HippoPosition = new Vector3(-640f, 2.375f, -8.78f); // Drain

	public static Vector3 GorillaPosition = new Vector3(-1271.31f, 3.22f, -138.97f);
	public static Vector3 TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
	public static Vector3 SnakePosition = new Vector3(-966.1f, 2.375f, 282.9f);
	public static Vector3 CaterpillarPosition = new Vector3(-1229.45f, 2.375f, 515.75f);

	public static float	PandaYRotation = 230;
	public static float HippoYRotation = 0;
	public static float RhinoYRotation = 0;

	public static float	GorillaYRotation = 30;
	public static float TigerYRotation = 180;
	public static float SnakeYRotation = 180;
	public static float CaterpillarYRotation = 0;

    public static void ResetCharacterPositionAndRotation()
    {
        PandaPosition = new Vector3(70f, 2.375f, 34.25f); // Production

        HippoPosition = new Vector3(-380.5f, 2.375f, -27f);
        RhinoPosition = new Vector3(-385.41f, 2.375f, -25.4f);
        GorillaPosition = new Vector3(-1271.31f, 3.22f, -138.97f);
        SnakePosition = new Vector3(-966.1f, 2.375f, 282.9f);
        TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
        CaterpillarPosition = new Vector3(-1229.45f, 2.375f, 515.75f);

        ResetCharacterRotation();
    }

    public static void ResetCharacterRotation()
    {
        PandaYRotation = 230;
        HippoYRotation = 0;
        RhinoYRotation = 0;

        GorillaYRotation = 30;
        SnakeYRotation = 180;
        TigerYRotation = 180;
        CaterpillarYRotation = 0;
    }

    public static List<string> PandaInvItemNameList = new List<string>();
	public static List<string> RhinoInvItemNameList = new List<string>();
	public static List<string> HippoInvItemNameList = new List<string>();

	public static List<string> TigerInvItemNameList = new List<string>();
	public static List<string> GorillaInvItemNameList = new List<string>();
	public static List<string> SnakeInvItemNameList = new List<string>();
	public static List<string> CaterpillarItemNameList = new List<string>();

	//public static Vector3 RhinoLitterGameStartPos = new Vector3(-416, 2.375f, -25);
	public static bool WasPostBoxCleaned = false;
	public static bool PlayTheSeagullVideo = false;

	public static List<string> AwardedMedalList = new List<string>();
    public static void AddToAwardedMedalList(string _medalName)
    {
        if(AwardedMedalList.Contains(_medalName) == false)
        {
            AwardedMedalList.Add(_medalName);
        }
    }

    public static bool IsMiniGameSkiped = false;
	public static int  Score;
    public static string GameTime;

    public static Dictionary<string, string> MissionList = new Dictionary<string, string>();
    public static string PipeRepairFinishTime;
    public static string SaplingTreeFinishTime;
    public static string GraffityFinishTime;
    public static string SmokeCarTookTime;


    public static string URL = "http://192.168.64.2/Prologue/";
    public static List<LBScoreObject> LeaderboardList = new List<LBScoreObject>();
    public static List<LBScoreObject> HallOfFameList = new List<LBScoreObject>();

    public static string SceneName = "";

    public static int ChoosenAvatar = 0; // default avatar choosen is Panda

    public static System.DateTime GetTime(string country)
    {
        string id;
        switch (country)
        {
            case "UK":
                id = "GMT Standard Time";
                break;
            case "Germany":
            case "France":
                id = "Central European Standard Time";
                break;
            default:
                id = "UTC";
                break;
        }

        System.TimeZoneInfo timeZone = System.TimeZoneInfo.FindSystemTimeZoneById(id);
        return System.TimeZoneInfo.ConvertTime(System.DateTime.Now, System.TimeZoneInfo.Local, timeZone);
    }
}
