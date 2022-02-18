using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System.Net;
using com.shephertz.app42.paas.sdk.csharp.storage;
using System;
using MiniJSON;
using System.Linq;

public class App42Leaderboard : App42CallBack
{
    string m_GameName = "Prologue";
    string m_ApiKey = "86b948581b34f63a0d09a9a21d698209f043d98eb3d2b10ce1b1b7f5c724db34";
    string m_SecretKey = "e318dd8b33a95ec2339ba040f4e2d0f415162b660bda9a0dd163e0b39ab2d7d8";

    //AssemblyCSharp.ScoreBoardResponse m_ResponseCallBack = new AssemblyCSharp.ScoreBoardResponse();
    ServiceAPI m_Sp = null;
    ScoreBoardService m_ScoreBoardService = null; // Initializing ScoreBoard Service.

    StorageService m_StorageService = null;

   // AssemblyCSharp.UserRankingResponse m_UserRankCallBack = new AssemblyCSharp.UserRankingResponse();

    string m_DBName = "Prime8DB";
    string m_CollectionName = "PrologueLBCollection";


    //List<string> m_Scores = new List<string>();
    Dictionary<string, string> m_Scores = new Dictionary<string, string>();
    Dictionary<string, string> m_HallOfFameDic = new Dictionary<string, string>();

#if UNITY_EDITOR
    public static bool Validator(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    { 
        return true; 
    }
#endif

    public void Initialize()
    {
        App42API.Initialize(m_ApiKey, m_SecretKey);
    }

    private string m_Type = "";
    private MainMenu m_MainMenuObj;
    private PilotEnd m_PilotEndObj;
  
    public void SaveScore(string _userName, string _email, int _score, string _time, MainMenu _mainMenu = null, PilotEnd _pilotEnd = null)
    {
        m_MainMenuObj = _mainMenu;
        m_PilotEndObj = _pilotEnd;
        m_Type = "SaveScore";

        Dictionary<string, object> jsonDoc = new Dictionary<string, object>(); 
        jsonDoc.Add("UserName", _userName);
        jsonDoc.Add("Email", _email);
        jsonDoc.Add("Score", _score);
        jsonDoc.Add("Time", _time);
        jsonDoc.Add("Avatar", SaveDataStatic.ChoosenAvatar);

        // App42API.SetDbName(m_DBName);
       

        m_ScoreBoardService = m_Sp.BuildScoreBoardService(); // Initializing ScoreBoard Service.

        m_ScoreBoardService.AddJSONObject(m_CollectionName, jsonDoc);

        m_ScoreBoardService.SaveUserScore(m_GameName, _userName, _score, this);
    }

    public void GetLeaderboard(MainMenu _mainMenu = null, PilotEnd _pilotEnd = null)
    {
        m_MainMenuObj = _mainMenu;
        m_PilotEndObj = _pilotEnd;

        m_Type = "GetScore";

        int max = 50; 
        //string key1 = "UserName";
        //string value1 = "Janidu"; 
        //Query query = QueryBuilder.Build(key1, value1, Operator.EQUALS); // Build query q1 for key1 equal to name and value1 equal to John  


        m_ScoreBoardService = m_Sp.BuildScoreBoardService();
        //App42Log.SetDebug(true);        //Print output in your editor console  

        m_ScoreBoardService.GetTopNRankers(m_GameName, max, this);   
    }

    public void GetStorageData(string _type = "GetStorage", PilotEnd _pilotEnd = null)
    {
        m_PilotEndObj = _pilotEnd;

        m_Type = _type;
        m_StorageService = m_Sp.BuildStorageService();
        m_StorageService.FindAllDocuments(m_DBName, m_CollectionName, this);
    }

    public void GetPrime8HallOfFame(MainMenu _mainMenu = null)
    {
        m_MainMenuObj = _mainMenu;

        m_Type = "GetHallOfFame30Days";

        int max = 8;

        //Get previous month first and last day.
        //DateTime today = SaveDataStatic.GetTime(DateTime.Today.ToString());
        //TODO: Update This when going live
        DateTime today = DateTime.Today;//.AddDays(30);
        DateTime month = new DateTime(today.Year, today.Month, 1);
        DateTime first = month.AddMonths(-1);
        DateTime last = month.AddDays(-1);

        //DateTime m_EndDate = DateTime.Now;
        //DateTime m_StartDate = m_EndDate.AddDays(-30);

        Debug.Log("start Date :  " + first + " and last " + last);

        m_ScoreBoardService = m_Sp.BuildScoreBoardService();
        m_ScoreBoardService.GetTopNRankers(m_GameName, first, last, max, this);
    }

    private void GetStorageForHallOfFame()
    {
        m_Type = "HallOfFameStorage";
        m_StorageService = m_Sp.BuildStorageService();
        m_StorageService.FindAllDocuments(m_DBName, m_CollectionName, this);
    }

    #region App42CallBack Implementation
    public void OnSuccess(object response)
    {
       if(m_Type == "SaveScore")
        {
            if (m_PilotEndObj)
            {
                m_PilotEndObj.LoadingPanel.SetActive(false);

                m_PilotEndObj.BringInMessagePanel("Thank you for posting your score with us. Be sure to follow your progress on the leaderboard!");
                m_PilotEndObj.ShareButton.SetActive(true);
                m_PilotEndObj.InputSection.SetActive(false);
                m_PilotEndObj.EmailInputSection.SetActive(false);
            }

            if (PlayerPrefs.GetInt("IsFirstTimeSharing", 0) == 0)
                PlayerPrefs.SetInt("IsFirstTimeSharing", 1);
        }
       else if(m_Type == "GetScore")
        {

            Game gameObj = (Game)response;

            Debug.Log("GameName : " + gameObj.GetName());

            if (gameObj.GetScoreList() != null)
            {
                IList<Game.Score> scoreList = gameObj.GetScoreList();

                m_Scores.Clear();

                for (int i = 0; i < scoreList.Count; i++)
                {
                    //string s = scoreList[i].GetUserName(), scoreList[i].GetValue() + "", (i + 1) + ""
                    //SaveDataStatic.GlobalLeaderBoardValues.Add(new string[] { scoreList [i].GetUserName (),
                    //    scoreList [i].GetValue () + "", (i + 1) + ""
                    //});
                    m_Scores.Add(scoreList[i].GetScoreId(), "UserID=" + i + "|UserNickName=" + scoreList[i].GetUserName() + "|UserFinalScore=" + scoreList[i].GetValue() + "|");
                    //Debug.Log(i + " -> User Name :  " + scoreList[i].GetUserName());
                 //   Debug.Log(scoreList[i].GetScoreId());
                }

                GetStorageData();
            }
            else
            {
                if(m_MainMenuObj)
                {
                    m_MainMenuObj.MessagePanel.SetActive(true);
                    m_MainMenuObj.LbLoadingPanel.SetActive(false);
                }
            }

        }
       else if(m_Type == "GetHallOfFame30Days")
        {
            Game m_HallOfFameObj = (Game)response;

            Debug.Log("GameName : " + m_HallOfFameObj.GetName());

            if (m_HallOfFameObj.GetScoreList() != null)
            {
                IList<Game.Score> m_HallOfFameList = m_HallOfFameObj.GetScoreList();

                m_HallOfFameDic.Clear();



                for (int i = 0; i < m_HallOfFameList.Count; i++)
                {
                    m_HallOfFameDic.Add(m_HallOfFameList[i].GetScoreId(), "UserID=" + i + "|UserNickName=" + m_HallOfFameList[i].GetUserName() + "|UserFinalScore=" + m_HallOfFameList[i].GetValue() + "|");

                }

                GetStorageForHallOfFame();


            }
            else
            {
                if (m_MainMenuObj)
                {
                    m_MainMenuObj.MessagePanel.SetActive(true);
                    m_MainMenuObj.LbLoadingPanel.SetActive(false);
                }
            }

        }
       else if(m_Type == "HallOfFameStorage")
        {
            Storage storage1 = (Storage)response;
            IList<Storage.JSONDocument> jsonDocList1 = storage1.GetJsonDocList();

            Dictionary<string, string> m_TempScoreIdAndTemp1 = new Dictionary<string, string>();

            for (int i = 0; i < jsonDocList1.Count; i++)
            {
                //  Debug.Log("jsonDoc is " + jsonDocList[i].GetJsonDoc());
                //PlayerInfo pInfo = PlayerInfo.CreateFromJSON(jsonDocList[i].GetJsonDoc());

                IDictionary dict = (IDictionary)Json.Deserialize(jsonDocList1[i].GetJsonDoc());

                m_TempScoreIdAndTemp1.Add((string)dict["_$scoreId"], (string)dict["Time"] + "`" + dict["Avatar"].ToString());
            }

            List<string> m_TempHallOfFamceScoreboardList = new List<string>();

            foreach (KeyValuePair<string, string> pair in m_HallOfFameDic)
            {
                string _time;
                if (m_TempScoreIdAndTemp1.TryGetValue(pair.Key, out _time))
                {
                    m_TempHallOfFamceScoreboardList.Add(pair.Value + ("UserGameTime=" + _time.Split('`')[0] + "|AvatarNo=" + _time.Split('`')[1]));
                }
            }

            SaveDataStatic.HallOfFameList.Clear();

            foreach (string str in m_TempHallOfFamceScoreboardList)
            {
                LBScoreObject obj = new LBScoreObject();
                obj.NickName = str.Split('|')[1].Split('=')[1];
                obj.Score = Convert.ToInt32(str.Split('|')[2].Split('=')[1]);

                obj.AvatarImageName = str.Split('|')[4].Split('=')[1];

                string m_TimeInString = str.Split('|')[3].Split('=')[1];
                obj.TimeInString = m_TimeInString;

                int m_TimeInSeconds = (Convert.ToInt32(m_TimeInString.Split('`')[0].Split(':')[0]) * 3600) + (Convert.ToInt32(m_TimeInString.Split('`')[0].Split(':')[1]) * 60) + Convert.ToInt32(m_TimeInString.Split('`')[0].Split(':')[2]);
                obj.TimeInSeconds = 1000000 - m_TimeInSeconds; // Since the following is order by descending. Need to do this to time. 

                SaveDataStatic.HallOfFameList.Add(obj);
            }

            var sortResult = SaveDataStatic.HallOfFameList.OrderByDescending(a => a.Score).ThenByDescending(a => a.TimeInSeconds).ToList();

            SaveDataStatic.HallOfFameList.Clear();
            foreach (LBScoreObject obj in sortResult)
            {
                SaveDataStatic.HallOfFameList.Add(obj);
            }


            if (m_MainMenuObj)
            {
                m_MainMenuObj.OpenHallOfFame();
            }
        }
        else if(m_Type == "GetStorage")
        {
            Storage storage = (Storage)response; 
            IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();

            Dictionary<string, string> m_TempScoreIdAndTemp = new Dictionary<string, string>();

            for (int i = 0; i < jsonDocList.Count; i++) 
            { 
                //Debug.Log("jsonDoc is " + jsonDocList[i].GetJsonDoc());
                //PlayerInfo pInfo = PlayerInfo.CreateFromJSON(jsonDocList[i].GetJsonDoc());

                IDictionary dict = (IDictionary)Json.Deserialize(jsonDocList[i].GetJsonDoc());

                m_TempScoreIdAndTemp.Add((string)dict["_$scoreId"], (string)dict["Time"]+"`"+dict["Avatar"].ToString());
            }

            List<string> m_TempScoreboardList = new List<string>();
            foreach (KeyValuePair<string, string> pair in m_Scores)
            {
                string _time; 
                if(m_TempScoreIdAndTemp.TryGetValue(pair.Key, out _time))
                {
                    m_TempScoreboardList.Add(pair.Value + ("UserGameTime=" + _time.Split('`')[0] + "|AvatarNo=" + _time.Split('`')[1]));
                }
            }

            SaveDataStatic.LeaderboardList.Clear();

            foreach (string str in m_TempScoreboardList)
            {
                LBScoreObject obj = new LBScoreObject();
                obj.NickName = str.Split('|')[1].Split('=')[1];
                obj.Score = Convert.ToInt32(str.Split('|')[2].Split('=')[1]);

                obj.AvatarImageName = str.Split('|')[4].Split('=')[1];

                string m_TimeInString = str.Split('|')[3].Split('=')[1];
                obj.TimeInString = m_TimeInString;
               
                int m_TimeInSeconds = (Convert.ToInt32(m_TimeInString.Split('`')[0].Split(':')[0]) * 3600) + (Convert.ToInt32(m_TimeInString.Split('`')[0].Split(':')[1]) * 60) + Convert.ToInt32(m_TimeInString.Split('`')[0].Split(':')[2]);
                obj.TimeInSeconds = 1000000 - m_TimeInSeconds; // Since the following is order by descending. Need to do this to time. 
                Debug.Log(obj.NickName);
                Debug.Log(obj.Score);
                Debug.Log(obj.AvatarImageName);
                Debug.Log(obj.TimeInString);
                Debug.Log(obj.TimeInSeconds);
                SaveDataStatic.LeaderboardList.Add(obj);
            }
            /*
            LBScoreObject obj1 = new LBScoreObject();
            obj1.NickName = "Si";
            obj1.Score = 550;
            obj1.AvatarImageName = "1";
            string st = "00:45:25";
            obj1.TimeInString = st;
            int m_TimeInSeconds1 = (Convert.ToInt32(st.Split('`')[0].Split(':')[0]) * 3600) + (Convert.ToInt32(st.Split('`')[0].Split(':')[1]) * 60) + Convert.ToInt32(st.Split('`')[0].Split(':')[2]);
            obj1.TimeInSeconds = 1000000 - m_TimeInSeconds1; // Since the following is order by descending. Need to do this to time.  
            SaveDataStatic.LeaderboardList.Add(obj1);

            LBScoreObject obj2 = new LBScoreObject();
            obj2.NickName = "Jo Jo B";
            obj2.Score = 500;
            obj2.AvatarImageName = "4";
            st = "00:45:25";
            obj2.TimeInString = st;
            m_TimeInSeconds1 = (Convert.ToInt32(st.Split('`')[0].Split(':')[0]) * 3600) + (Convert.ToInt32(st.Split('`')[0].Split(':')[1]) * 60) + Convert.ToInt32(st.Split('`')[0].Split(':')[2]);
            obj2.TimeInSeconds = 1000000 - m_TimeInSeconds1; // Since the following is order by descending. Need to do this to time.  
            SaveDataStatic.LeaderboardList.Add(obj2);

            LBScoreObject obj3 = new LBScoreObject();
            obj3.NickName = "Big Al";
            obj3.Score = 500;
            obj3.AvatarImageName = "2";
            st = "00:45:31";
            obj3.TimeInString = st;
            m_TimeInSeconds1 = (Convert.ToInt32(st.Split('`')[0].Split(':')[0]) * 3600) + (Convert.ToInt32(st.Split('`')[0].Split(':')[1]) * 60) + Convert.ToInt32(st.Split('`')[0].Split(':')[2]);
            obj3.TimeInSeconds = 1000000 - m_TimeInSeconds1; // Since the following is order by descending. Need to do this to time.  
            SaveDataStatic.LeaderboardList.Add(obj3);

            LBScoreObject obj4 = new LBScoreObject();
            obj4.NickName = "Suzie C";
            obj4.Score = 450;
            obj4.AvatarImageName = "6";
            st = "00:44:40";
            obj4.TimeInString = st;
            m_TimeInSeconds1 = (Convert.ToInt32(st.Split('`')[0].Split(':')[0]) * 3600) + (Convert.ToInt32(st.Split('`')[0].Split(':')[1]) * 60) + Convert.ToInt32(st.Split('`')[0].Split(':')[2]);
            obj4.TimeInSeconds = 1000000 - m_TimeInSeconds1; // Since the following is order by descending. Need to do this to time.  
            SaveDataStatic.LeaderboardList.Add(obj4);

            LBScoreObject obj5 = new LBScoreObject();
            obj5.NickName = "Pat Rat";
            obj5.Score = 450;
            obj5.AvatarImageName = "3";
            st = "00:44:51";
            obj5.TimeInString = st;
            m_TimeInSeconds1 = (Convert.ToInt32(st.Split('`')[0].Split(':')[0]) * 3600) + (Convert.ToInt32(st.Split('`')[0].Split(':')[1]) * 60) + Convert.ToInt32(st.Split('`')[0].Split(':')[2]);
            obj5.TimeInSeconds = 1000000 - m_TimeInSeconds1; // Since the following is order by descending. Need to do this to time.  
            SaveDataStatic.LeaderboardList.Add(obj5);

            LBScoreObject obj6 = new LBScoreObject();
            obj6.NickName = "Hami Zon";
            obj6.Score = 350;
            obj6.AvatarImageName = "4";
            st = "00:40:02";
            obj6.TimeInString = st;
            m_TimeInSeconds1 = (Convert.ToInt32(st.Split('`')[0].Split(':')[0]) * 3600) + (Convert.ToInt32(st.Split('`')[0].Split(':')[1]) * 60) + Convert.ToInt32(st.Split('`')[0].Split(':')[2]);
            obj6.TimeInSeconds = 1000000 - m_TimeInSeconds1; // Since the following is order by descending. Need to do this to time.  
            SaveDataStatic.LeaderboardList.Add(obj6);
            */

            var sortResult = SaveDataStatic.LeaderboardList.OrderByDescending(a => a.Score).ThenByDescending(a => a.TimeInSeconds).ToList();

            SaveDataStatic.LeaderboardList.Clear();
            foreach(LBScoreObject obj in sortResult)
            {
                SaveDataStatic.LeaderboardList.Add(obj);
            }


            if (m_MainMenuObj)
            {
                m_MainMenuObj.OpenLB();
            }
        }
       else // Check NickName
        {
            Storage storageForCheckingUniqueNickName = (Storage)response;
            IList<Storage.JSONDocument> jsonDocListForNickName = storageForCheckingUniqueNickName.GetJsonDocList();


            string m_MyNickName = PlayerPrefs.GetString("Prime8Player", "");
            m_MyNickName = m_MyNickName.Trim();
            bool m_AllreadyPresent = false;
            if (jsonDocListForNickName.Count > 0)
            {
                for (int i = 0; i < jsonDocListForNickName.Count; i++)
                {
                    IDictionary dict1 = (IDictionary)Json.Deserialize(jsonDocListForNickName[i].GetJsonDoc());

                    if (m_MyNickName == (string)dict1["UserName"])
                    {
                        m_AllreadyPresent = true;
                        break;
                    }
                }
            }
           
            if (m_PilotEndObj)
            {
                m_PilotEndObj.AfterCheckNickNameFinish(m_AllreadyPresent);
            }
        }
    }

    public void OnException(Exception ex)
    {
        Debug.Log("Exception ex : " + ex.ToString() + " m_Type : " + m_Type);
        if (m_Type == "SaveScore" || m_Type == "CheckNickName")
        {
            if(m_PilotEndObj)
            {
                m_PilotEndObj.LoadingPanel.SetActive(false);

                m_PilotEndObj.BringInMessagePanel("Your internet connection is not working. Please check your connection.");
                m_PilotEndObj.ShareButton.SetActive(true);
                m_PilotEndObj.InputSection.SetActive(false);
                m_PilotEndObj.EmailInputSection.SetActive(false);
            }
        }
        else if(m_Type == "GetHallOfFame30Days")
        {
            if (m_MainMenuObj)
            {
                //m_MainMenuObj.MessagePanel.SetActive(true);
                m_MainMenuObj.BringInMessagePanel("Coming Soon!");
                m_MainMenuObj.LbLoadingPanel.SetActive(false);
            }
        }
        else
        {
            if (m_MainMenuObj)
            {
                m_MainMenuObj.BringInMessagePanel();
                //m_MainMenuObj.MessagePanel.SetActive(true);
                m_MainMenuObj.LbLoadingPanel.SetActive(false);
            }
        }

    }
    #endregion App42CallBack Implementation

    #region Singleton
    private static App42Leaderboard m_Instance;

    private App42Leaderboard() 
    {
#if UNITY_EDITOR
        ServicePointManager.ServerCertificateValidationCallback = Validator;
#endif

        m_Sp = new ServiceAPI(m_ApiKey, m_SecretKey);

        App42API.SetDbName(m_DBName);
    }

    public static App42Leaderboard GetInstace()
    {
        if (m_Instance == null)
            m_Instance = new App42Leaderboard();
        return m_Instance;
    }
    #endregion Singleton
}

public class LBScoreObject
{
    public string NickName;
    public int Score;
    public int TimeInSeconds;
    public string TimeInString;
    public string AvatarImageName;
}
