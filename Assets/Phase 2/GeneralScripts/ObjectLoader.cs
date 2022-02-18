using UnityEngine;
using System.Collections;

public class ObjectLoader : MonoBehaviour {

	public GameObject Tree9_5;
	private Vector3[] m_Tree9Positions = new Vector3[7];
	private Vector3[] m_Tree9Sizes = new Vector3[7];

	public GameObject BroadLeaf_LOD2;
	private Vector3[]  m_BroadLeaf_LOD2Positions = new Vector3[18];
	private Vector3[]  m_BroadLeaf_LOD2Sizes = new Vector3[18];

	public GameObject tree1a_pre;
	private Vector3[] m_tree1a_prePositions = new Vector3[43];
	private Vector3[] m_tree1a_pre2Sizes = new Vector3[43];

	public GameObject Tree9_2;
	private Vector3[] m_Tree9_2Positions = new Vector3[3];
	private Vector3[] m_Tree9_2Sizes = new Vector3[3];

	public GameObject Broadleaf_Mobile;
	private Vector3[] Broadleaf_MobilePositions = new Vector3[16];
	private Vector3[] Broadleaf_MobileSizes = new Vector3[16];

	public GameObject Broadleaf_Mobile_LOD0;
	private Vector3[] Broadleaf_Mobile_LOD0Positions = new Vector3[4];
	private Vector3[] Broadleaf_Mobile_LOD0Sizes = new Vector3[4];

	public GameObject Broadleaf_Mobile_Billboard;
	private Vector3[] Broadleaf_Mobile_BillboardPositions = new Vector3[6];
	private Vector3[] Broadleaf_Mobile_BillboardSizes = new Vector3[6];

	public GameObject tree2_pre;
	private Vector3[] tree2_prePositions = new Vector3[3];
	private Vector3[] tree2_preSizes = new Vector3[3];

	public GameObject Tree9_4;
	private Vector3[] Tree9_4Positions = new Vector3[2];
	private Vector3[] Tree9_4Sizes = new Vector3[2];

	public GameObject Tree08;
	private Vector3[] Tree08Positions = new Vector3[5];
	private Vector3[] Tree08Sizes = new Vector3[5];

	public GameObject Tree08_2;
	private Vector3[] Tree08_2Positions = new Vector3[3];
	private Vector3[] Tree08_28Sizes = new Vector3[3];

	public GameObject PineTree01;
	private Vector3[] PineTree01Positions = new Vector3[2];
	private Vector3[] PineTree018Sizes = new Vector3[2];

	public GameObject PineTree02;
	private Vector3[] PineTree02Positions = new Vector3[1];
	private Vector3[] PineTree028Sizes = new Vector3[1];

	public GameObject PineTree03;
	private Vector3[] PineTree03Positions = new Vector3[1];
	private Vector3[] PineTree038Sizes = new Vector3[1];

	public GameObject tree03;
	private Vector3[] tree03Positions = new Vector3[2];
	private Vector3[] tree03Sizes = new Vector3[2];

	public GameObject tree05;
	private Vector3[] tree05Positions = new Vector3[1];
	private Vector3[] tree05Sizes = new Vector3[1];

	public GameObject Tree01_2;
	private Vector3[] Tree01_2Positions = new Vector3[1];
	private Vector3[] Tree01_2Sizes = new Vector3[1];

	public GameObject Tree06;
	private Vector3[] Tree06Positions = new Vector3[2];
	private Vector3[] Tree06Sizes = new Vector3[2];

	public GameObject Tree02;
	private Vector3[] Tree02Positions = new Vector3[1];
	private Vector3[] Tree02Sizes = new Vector3[1];

	public GameObject Conifer1;
	private Vector3[] Conifer1Positions = new Vector3[17];
	private Vector3[] Conifer1Sizes = new Vector3[17];

	public GameObject Conifer7;
	private Vector3[] Conifer7Positions = new Vector3[13];
	private Vector3[] Conifer7Sizes = new Vector3[13];

	public GameObject bush03;
	private Vector3[] bush03Positions = new Vector3[5];
	private Vector3[] bush03Sizes = new Vector3[5];

	public GameObject Tree07;
	private Vector3[] Tree07Positions = new Vector3[2];
	private Vector3[] Tree07Sizes = new Vector3[2];

	public GameObject bush05;
	private Vector3[] bush05Positions = new Vector3[3];
	private Vector3[] bush05Sizes = new Vector3[3];

	public GameObject flowers05;
	private Vector3[] flowers05Positions = new Vector3[1];
	private Vector3[] flowers05Sizes = new Vector3[1];

	public GameObject flowers01;
	private Vector3[] flowers01Positions = new Vector3[3];
	private Vector3[] flowers01Sizes = new Vector3[3];

	public GameObject bush02;
	private Vector3[] bush02Positions = new Vector3[1];
	private Vector3[] bush02Sizes = new Vector3[1];

	public GameObject Oduvanchik01;
	private Vector3[] Oduvanchik01Positions = new Vector3[3];
	private Vector3[] Oduvanchik01Sizes = new Vector3[3];

	public GameObject bush01;
	private Vector3[] bush01Positions = new Vector3[1];
	private Vector3[] bush01Sizes = new Vector3[1];

	public GameObject tree02_1;
	private Vector3[] tree02_1Positions = new Vector3[1];
	private Vector3[] tree02_1Sizes = new Vector3[1];

	public GameObject tree04;
	private Vector3[] tree04Positions = new Vector3[3];
	private Vector3[] tree04Sizes = new Vector3[3];

	public GameObject bush06;
	private Vector3[] bush06Positions = new Vector3[2];
	private Vector3[] bush06Sizes = new Vector3[2];

	public GameObject bush01_1;
	private Vector3[] bush01_1Positions = new Vector3[1];
	private Vector3[] bush01_1Sizes = new Vector3[1];

	public GameObject tree01;
	private Vector3[] tree01Positions = new Vector3[1];
	private Vector3[] tree01Sizes = new Vector3[1];

	public GameObject Tree02_2;
	private Vector3[] Tree02_2Positions = new Vector3[1];
	private Vector3[] Tree02_2Sizes = new Vector3[1];

	public GameObject Tree10_4;
	private Vector3[] Tree10_4Positions = new Vector3[1];
	private Vector3[] Tree10_4Sizes = new Vector3[1];

	public GameObject tree1a;
	private Vector3[] tree1aPositions = new Vector3[1];
	private Vector3[] tree1aSizes = new Vector3[1];

	//Asset Loader
	public GameObject AssetLoader;

	void Awake()
	{
		SaveDataStatic.IsObjectLoadComplete = false;
	}

	// Use this for initialization
	void Start () {
        Debug.LogError("Object Loader eka dan oone na");

		if(SaveDataStatic.StorySequence == "PRepStart") // Pipe repair game lost
		{
			TownController.GetInstance ().LoadingImg.gameObject.transform.parent.gameObject.SetActive(false);
			TownController.GetInstance ().GameCompletePanel.SetActive(true);
			TownController.GetInstance ().GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(false);

			TownController.GetInstance ().SetQuizAnswerCursor();
			return;
		}
		else if(SaveDataStatic.StorySequence == "SaplingTree") // Sampling Tree Lost
		{
			TownController.GetInstance ().LoadingImg.gameObject.transform.parent.gameObject.SetActive(false);
			TownController.GetInstance ().GameCompletePanel.SetActive(true);
			TownController.GetInstance ().GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(false);

			TownController.GetInstance ().SetQuizAnswerCursor();
			return;
		}
		else if(SaveDataStatic.StorySequence == "GrafitiBegin") // Graffiti Lost
		{
			TownController.GetInstance ().LoadingImg.gameObject.transform.parent.gameObject.SetActive(false);
			TownController.GetInstance ().GameCompletePanel.SetActive(true);
			TownController.GetInstance ().GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(false);

			TownController.GetInstance ().SetQuizAnswerCursor();
			return;
		}
	
		Debug.Log ("Start Town 1: " + System.DateTime.Now.ToString ());

		//Tree9_5
		m_Tree9Positions[0] = new Vector3(98, 0, -23);
		m_Tree9Sizes[0] = new Vector3(0.1f, 0.1f, 0.1f);

		m_Tree9Positions[1] = new Vector3(-1093.024f, 0.1020244f, 127.9407f);
		m_Tree9Sizes[1] = new Vector3(3f, 3f, 3f);

		m_Tree9Positions[2] = new Vector3(-1196.799f, 0.1020289f, 89.90344f);
		m_Tree9Sizes[2] = new Vector3(3f, 3, 3);

		m_Tree9Positions[3] = new Vector3(-1196.799f, 0.1020289f, 152.3f);
		m_Tree9Sizes[3] = new Vector3(3f, 3, 3);

		m_Tree9Positions[4] = new Vector3(-1155.5f, 0.1020289f, 136.6f);
		m_Tree9Sizes[4] = new Vector3(3f, 3, 3);

		m_Tree9Positions[5] = new Vector3(-1132.8f, 0.1020289f, 186.3f);
		m_Tree9Sizes[5] = new Vector3(3f, 3, 3);

		m_Tree9Positions[6] = new Vector3(-1293.6f, 0.1020289f, -153.4f);
		m_Tree9Sizes[6] = new Vector3(3f, 3, 3);

		//BroadLeaf_LOD2
		m_BroadLeaf_LOD2Positions[0] = new Vector3(153.45f, 0f, 123.4f);
		m_BroadLeaf_LOD2Sizes[0] = new Vector3(2.5f, 2.5f, 2.5f);

		m_BroadLeaf_LOD2Positions[1] = new Vector3(123.23f, 0f, 84.7f);
		m_BroadLeaf_LOD2Sizes[1] = new Vector3(0.1f, 0.1f, 0.1f);

		m_BroadLeaf_LOD2Positions[2] = new Vector3(-118.05f, -1.05f, 74.6f);
		m_BroadLeaf_LOD2Sizes[2] = new Vector3(0.1f, 1f, 0.75f);

		m_BroadLeaf_LOD2Positions[3] = new Vector3(-149.15f, -1.05f, 74.6f);
		m_BroadLeaf_LOD2Sizes[3] = new Vector3(0.1f, 1f, 0.75f);

		m_BroadLeaf_LOD2Positions[4] = new Vector3(-116f, -1f, -26.55f);
		m_BroadLeaf_LOD2Sizes[4] = new Vector3(0.35f, 0.6f, 0.25f);

		m_BroadLeaf_LOD2Positions[5] = new Vector3(-186.35f, -1f, -33.2f);
		m_BroadLeaf_LOD2Sizes[5] = new Vector3(0.35f, 0.6f, 0.25f);

		m_BroadLeaf_LOD2Positions[6] = new Vector3(-44.65f, -1f, -21.95f);
		m_BroadLeaf_LOD2Sizes[6] = new Vector3(0.35f, 0.6f, 0.25f);

		m_BroadLeaf_LOD2Positions[7] = new Vector3(-7.109985f, 0f, -27.4f);
		m_BroadLeaf_LOD2Sizes[7] = new Vector3(0.15f, 0.25f, 0.15f);

		m_BroadLeaf_LOD2Positions[8] = new Vector3(-15.95001f, 0f, -32.35f);
		m_BroadLeaf_LOD2Sizes[8] = new Vector3(0.1f, 0.2f, 0.1f);

		m_BroadLeaf_LOD2Positions[9] = new Vector3(55.5f, 0f, -29f);
		m_BroadLeaf_LOD2Sizes[9] = new Vector3(0.1f, 0.2f, 0.1f);

		m_BroadLeaf_LOD2Positions[10] = new Vector3(64.34998f, 0f, -24.05f);
		m_BroadLeaf_LOD2Sizes[10] = new Vector3(0.15f, 0.25f, 0.15f);

		m_BroadLeaf_LOD2Positions[11] = new Vector3(135.05f, 0f, -27.8f);
		m_BroadLeaf_LOD2Sizes[11] = new Vector3(0.15f, 0.25f, 0.15f);

		m_BroadLeaf_LOD2Positions[12] = new Vector3(126.2f, 0f, -32.75f);
		m_BroadLeaf_LOD2Sizes[12] = new Vector3(0.1f, 0.2f, 0.1f);

		m_BroadLeaf_LOD2Positions[13] = new Vector3(-246.6f, -5f, -107.35f);
		m_BroadLeaf_LOD2Sizes[13] = new Vector3(0.1f, 2.5f, 1.5f);

		m_BroadLeaf_LOD2Positions[14] = new Vector3(-246.6f, -5f, -131.4f);
		m_BroadLeaf_LOD2Sizes[14] = new Vector3(0.1f, 2f, 1f);

		m_BroadLeaf_LOD2Positions[15] = new Vector3(-259.7f, -1f, -77.95f);
		m_BroadLeaf_LOD2Sizes[15] = new Vector3(0.1f, 0.1f, 0.1f);

		m_BroadLeaf_LOD2Positions[16] = new Vector3(60f, 0f, 400.1f);
		m_BroadLeaf_LOD2Sizes[16] = new Vector3(2.5f, 2.5f, 2.5f);

		m_BroadLeaf_LOD2Positions[17] = new Vector3(-818.5f, 0f, 383f);
		m_BroadLeaf_LOD2Sizes[17] = new Vector3(2.5f, 2.5f, 2.5f);

		//tree1a_pre
		m_tree1a_prePositions[0] = new Vector3(86.65503f, 0f, 91.345f);
		m_tree1a_pre2Sizes[0] = new Vector3(0.1f, 0.1f, 0.1f);

		m_tree1a_prePositions[1] = new Vector3(95.70001f, -0.15f, 97.15f);
		m_tree1a_pre2Sizes[1] = new Vector3(0.25f, 0.25f, 0.25f);

		m_tree1a_prePositions[2] = new Vector3(-36.35001f, -1.05f, 87.3f);
		m_tree1a_pre2Sizes[2] = new Vector3(2f, 2f, 2f);

		m_tree1a_prePositions[3] = new Vector3(-149.15f, -1.05f, 74.6f);
		m_tree1a_pre2Sizes[3] = new Vector3(0.1f, 1f, 0.75f);

		m_tree1a_prePositions[4] = new Vector3(-461.5f, -4f, -139.15f);
		m_tree1a_pre2Sizes[4] = new Vector3(2f, 2f, 3.5f);

		m_tree1a_prePositions[5] = new Vector3(-442.6f, -4f, -146.8f);
		m_tree1a_pre2Sizes[5] = new Vector3(2f, 2f, 2f);

		m_tree1a_prePositions[6] = new Vector3(-406.95f, -4f, -159.6f);
		m_tree1a_pre2Sizes[6] = new Vector3(2f, 2f, 2f);

		m_tree1a_prePositions[7] = new Vector3(-380.6f, -4f, -142.9f);
		m_tree1a_pre2Sizes[7] = new Vector3(2f, 2f, 2f);

		m_tree1a_prePositions[8] = new Vector3(-340.6f, -4f, -142.9f);
		m_tree1a_pre2Sizes[8] = new Vector3(2f, 2f, 2f);

		m_tree1a_prePositions[9] = new Vector3(-353.9f, -4f, -142.9f);
		m_tree1a_pre2Sizes[9] = new Vector3(2f, 2f, 2f);

		m_tree1a_prePositions[10] = new Vector3(-366.5f, -4f, -142.9f);
		m_tree1a_pre2Sizes[10] = new Vector3(2f, 2f, 3f);

		m_tree1a_prePositions[11] = new Vector3(-286.45f, -4f, -142.9f);
		m_tree1a_pre2Sizes[11] = new Vector3(2f, 2, 3f);

		m_tree1a_prePositions[12] = new Vector3(-249.55f, -4f, -142.9f);
		m_tree1a_pre2Sizes[12] = new Vector3(2f, 2f, 3f);

		m_tree1a_prePositions[13] = new Vector3(-249.55f, -4f, -118.4f);
		m_tree1a_pre2Sizes[13] = new Vector3(2f, 2f, 3f);

		m_tree1a_prePositions[14] = new Vector3(-249.55f, -4f, -96.8f);
		m_tree1a_pre2Sizes[14] = new Vector3(2f, 2f, 3f);

		m_tree1a_prePositions[15] = new Vector3(-249.55f, -4f, -82.7f);
		m_tree1a_pre2Sizes[15] = new Vector3(2f, 2f, 3f);

		m_tree1a_prePositions[16] = new Vector3(-451.5f, -4f, -82.7f);
		m_tree1a_pre2Sizes[16] = new Vector3(2f, 2f, 3f);

		m_tree1a_prePositions[17] = new Vector3(-451.5f, -4f, -104f);
		m_tree1a_pre2Sizes[17] = new Vector3(2f, 2f, 3f);

		m_tree1a_prePositions[18] = new Vector3(-464f, -4f, -93.45f);
		m_tree1a_pre2Sizes[18] = new Vector3(4, 4, 4);

		m_tree1a_prePositions[19] = new Vector3(-428.95f, -4f, -149.95f);
		m_tree1a_pre2Sizes[19] = new Vector3(4, 4, 4);

		m_tree1a_prePositions[20] = new Vector3(-395.8f, -4f, -149.95f);
		m_tree1a_pre2Sizes[20] = new Vector3(2, 2, 3);

		m_tree1a_prePositions[21] = new Vector3(-388.05f, -4f, -149.95f);
		m_tree1a_pre2Sizes[21] = new Vector3(2, 2, 3);

		m_tree1a_prePositions[22] = new Vector3(-310.1f, -4f, -149.95f);
		m_tree1a_pre2Sizes[22] = new Vector3(2, 2, 4);

		m_tree1a_prePositions[23] = new Vector3(-310.1f, -4f, -165f);
		m_tree1a_pre2Sizes[23] = new Vector3(2, 2, 4);

		m_tree1a_prePositions[24] = new Vector3(-283.2f, -4f, -165f);
		m_tree1a_pre2Sizes[24] = new Vector3(2, 2, 4);

		m_tree1a_prePositions[25] = new Vector3(-236.5f, -4f, -165f);
		m_tree1a_pre2Sizes[25] = new Vector3(2, 2, 4);

		m_tree1a_prePositions[26] = new Vector3(-255.4f, -4f, -152.2f);
		m_tree1a_pre2Sizes[26] = new Vector3(2, 2, 4);

		m_tree1a_prePositions[27] = new Vector3(-267.5f, -4f, -152.2f);
		m_tree1a_pre2Sizes[27] = new Vector3(2, 2, 2);

		m_tree1a_prePositions[28] = new Vector3(-242.35f, -4f, -90.7f);
		m_tree1a_pre2Sizes[28] = new Vector3(2, 2, 3);

		m_tree1a_prePositions[29] = new Vector3(-296.7f, -4f, -160.4f);
		m_tree1a_pre2Sizes[29] = new Vector3(2, 2, 4);

		m_tree1a_prePositions[30] = new Vector3(-272.4f, -4f, -160.4f);
		m_tree1a_pre2Sizes[30] = new Vector3(2, 2, 4);

		m_tree1a_prePositions[31] = new Vector3(-257.3f, -4f, -160.4f);
		m_tree1a_pre2Sizes[31] = new Vector3(2, 2, 4);

		m_tree1a_prePositions[32] = new Vector3(-324.05f, -4f, -160.4f);
		m_tree1a_pre2Sizes[32] = new Vector3(2, 2, 4);

		m_tree1a_prePositions[33] = new Vector3(-325.65f, -4f, -142.9f);
		m_tree1a_pre2Sizes[33] = new Vector3(2, 2, 3);

		m_tree1a_prePositions[34] = new Vector3(-332f, -4f, -178.5f);
		m_tree1a_pre2Sizes[34] = new Vector3(2, 2, 3);

		m_tree1a_prePositions[35] = new Vector3(-353.3f, -4f, -178.5f);
		m_tree1a_pre2Sizes[35] = new Vector3(2, 2, 3);

		m_tree1a_prePositions[36] = new Vector3(-369.95f, -4f, -178.5f);
		m_tree1a_pre2Sizes[36] = new Vector3(2, 2, 3);

		m_tree1a_prePositions[37] = new Vector3(-415.7f, -4f, -164.7f);
		m_tree1a_pre2Sizes[37] = new Vector3(4, 4, 4);

		m_tree1a_prePositions[38] = new Vector3(-452.4f, -4f, -123.6f);
		m_tree1a_pre2Sizes[38] = new Vector3(4, 4, 4);

		m_tree1a_prePositions[39] = new Vector3(-401f, -4f, 275.5f);
		m_tree1a_pre2Sizes[39] = new Vector3(4, 4, 4);

		m_tree1a_prePositions[40] = new Vector3(-580f, -4f, 301.75f);
		m_tree1a_pre2Sizes[40] = new Vector3(4, 4, 4);

		m_tree1a_prePositions[41] = new Vector3(-1029.605f, 0.1f, -20.33954f);
		m_tree1a_pre2Sizes[41] = new Vector3(1, 1, 1);

		m_tree1a_prePositions[42] = new Vector3(-945.3f, 0.1f, -6.1f);
		m_tree1a_pre2Sizes[42] = new Vector3(3, 3, 3);

		//Tree9_2
		m_Tree9_2Positions[0] = new Vector3(30, 0.1f, 51.85f);
		m_Tree9_2Sizes[0] = new Vector3(2f, 2, 2);

		m_Tree9_2Positions[1] = new Vector3(-26.5f, 0.1f, 263.8f);
		m_Tree9_2Sizes[1] = new Vector3(0.1f, 0.1f, 0.1f);

		m_Tree9_2Positions[2] = new Vector3(-206f, 0.1f, 396f);
		m_Tree9_2Sizes[2] = new Vector3(2f, 2, 2);

		//Broadleaf_Mobile;
		Broadleaf_MobilePositions[0] = new Vector3(66.65503f, 0f, 94.995f);
		Broadleaf_MobileSizes[0] = new Vector3(0.1f, 0.1f, 0.1f);

		Broadleaf_MobilePositions[1] = new Vector3(-299.85f, -1f, -146.48f);
		Broadleaf_MobileSizes[1] = new Vector3(1f, 1f, 1f);

		Broadleaf_MobilePositions[2] = new Vector3(-315.65f, -4f, -146.48f);
		Broadleaf_MobileSizes[2] = new Vector3(1f, 1f, 1f);

		Broadleaf_MobilePositions[3] = new Vector3(-333.55f, -4f, -146.48f);
		Broadleaf_MobileSizes[3] = new Vector3(1f, 1f, 1f);

		Broadleaf_MobilePositions[4] = new Vector3(-597f, -4f, 92f);
		Broadleaf_MobileSizes[4] = new Vector3(4f, 4f, 4f);

		Broadleaf_MobilePositions[5] = new Vector3(-442.5f, -4f, -154.5f);
		Broadleaf_MobileSizes[5] = new Vector3(4f, 4f, 4f);

		Broadleaf_MobilePositions[6] = new Vector3(-166.5f, -4f, 290.1f);
		Broadleaf_MobileSizes[6] = new Vector3(4f, 4f, 4f);

		Broadleaf_MobilePositions[7] = new Vector3(-166.5f, -4f, 290.1f);
		Broadleaf_MobileSizes[7] = new Vector3(4f, 4f, 4f);

		Broadleaf_MobilePositions[8] = new Vector3(-103.65f, -0.75f, 96.15f);
		Broadleaf_MobileSizes[8] = new Vector3(1.5f, 1.5f, 1.5f);

		Broadleaf_MobilePositions[9] = new Vector3(-169f, 0f, 57.7f);
		Broadleaf_MobileSizes[9] = new Vector3(2.5f, 1.5f, 2.5f);

		Broadleaf_MobilePositions[10] = new Vector3(-347.5f, -4f, 300f);
		Broadleaf_MobileSizes[10] = new Vector3(4f, 4, 4f);

		Broadleaf_MobilePositions[11] = new Vector3(-1093.5f, -4f, 361f);
		Broadleaf_MobileSizes[11] = new Vector3(7.5f, 3.5f, 7.5f);

		Broadleaf_MobilePositions[12] = new Vector3(-181f, -4f, 130f);
		Broadleaf_MobileSizes[12] = new Vector3(4f, 4f, 4f);

		Broadleaf_MobilePositions[13] = new Vector3(-630f, -5.5f, 358f);
		Broadleaf_MobileSizes[13] = new Vector3(4f, 4f, 4f);

		Broadleaf_MobilePositions[14] = new Vector3(-928f, -5.5f, 481.5f);
		Broadleaf_MobileSizes[14] = new Vector3(7.5f, 3.5f, 7.5f);

		Broadleaf_MobilePositions[15] = new Vector3(-928f, -5.5f, 481.5f);
		Broadleaf_MobileSizes[15] = new Vector3(7.5f, 3.5f, 7.5f);

		//Broadleaf_Mobile_LOD0
		Broadleaf_Mobile_LOD0Positions[0] = new Vector3(-346.5f, -4f, -154.3f);
		Broadleaf_Mobile_LOD0Sizes[0] = new Vector3(1f, 1f, 1f);

		Broadleaf_Mobile_LOD0Positions[1] = new Vector3(-1081.527f, 0f, -11.68103f);
		Broadleaf_Mobile_LOD0Sizes[1] = new Vector3(3.5f, 3.5f, 3.5f);

		Broadleaf_Mobile_LOD0Positions[2] = new Vector3(-1081.527f, 0f, -11.68103f);
		Broadleaf_Mobile_LOD0Sizes[2] = new Vector3(3.5f, 3.5f, 3.5f);

		Broadleaf_Mobile_LOD0Positions[3] = new Vector3(-1058.5f, 0f, -23f);
		Broadleaf_Mobile_LOD0Sizes[3] = new Vector3(2f, 2f, 2f);

		//Broadleaf_Mobile_Billboard;
		Broadleaf_Mobile_BillboardPositions[0] = new Vector3(-1007.7f, 0.1f, 34.8f);
		Broadleaf_Mobile_BillboardSizes[0] = new Vector3(3f, 3f, 3f);

		Broadleaf_Mobile_BillboardPositions[1] = new Vector3(-1045.8f, 0.1f, 55f);
		Broadleaf_Mobile_BillboardSizes[1] = new Vector3(4f, 4f, 4f);

		Broadleaf_Mobile_BillboardPositions[2] = new Vector3(-1182.9f, 0.1f, 6f);
		Broadleaf_Mobile_BillboardSizes[2] = new Vector3(8f, 5f, 8f);

		Broadleaf_Mobile_BillboardPositions[3] = new Vector3(-1236.4f, 0.1f, -180.7f);
		Broadleaf_Mobile_BillboardSizes[3] = new Vector3(8f, 5f, 8f);

		Broadleaf_Mobile_BillboardPositions[4] = new Vector3(-1137.8f, 0.1f, -194.6f);
		Broadleaf_Mobile_BillboardSizes[4] = new Vector3(8f, 5f, 8f);

		Broadleaf_Mobile_BillboardPositions[5] = new Vector3(-1270.1f, 0.1f, 176.8f);
		Broadleaf_Mobile_BillboardSizes[5] = new Vector3(8f, 5f, 8f);

		//tree2_pre;
		tree2_prePositions[0] = new Vector3(26.845f, 0f, -20.305f);
		tree2_preSizes[0] = new Vector3(1f, 1f, 1f);

		tree2_prePositions[1] = new Vector3(-21f, 0f, 48.6f);
		tree2_preSizes[1] = new Vector3(1f, 1f, 1f);

		tree2_prePositions[2] = new Vector3(-994.9f, 0.1f, 15.4f);
		tree2_preSizes[2] = new Vector3(4f, 4f, 4f);

		//Tree9_4;
		Tree9_4Positions[0] = new Vector3(-202.7924f, 0.1f, 80f);
		Tree9_4Sizes[0] = new Vector3(2f, 2f, 2f);

		Tree9_4Positions[1] = new Vector3(185.5f, 0.1f, 293.4f);
		Tree9_4Sizes[1] = new Vector3(1f, 1f, 1f);

		//Tree08;
		Tree08Positions[0] = new Vector3(10f, 0.1f, 140.7216f);
		Tree08Sizes[0] = new Vector3(10f, 10f, 10f);

		Tree08Positions[1] = new Vector3(-21.5f, 0.1f, 140.7216f);
		Tree08Sizes[1] = new Vector3(10f, 10f, 10f);

		Tree08Positions[2] = new Vector3(163f, 0.1f, 180f);
		Tree08Sizes[2] = new Vector3(10f, 10f, 10f);

		Tree08Positions[3] = new Vector3(163f, -5.1f, 200.7f);
		Tree08Sizes[3] = new Vector3(10f, 10f, 10f);

		Tree08Positions[4] = new Vector3(163f, -5.1f, 231.5f);
		Tree08Sizes[4] = new Vector3(10f, 10f, 10f);

		//Tree08_2;
		Tree08_2Positions[0] = new Vector3(-841.75f, 0f, 331.9f);
		Tree08_28Sizes[0] = new Vector3(10f, 10f, 10f);

		Tree08_2Positions[1] = new Vector3(-858f, 0f, 365f);
		Tree08_28Sizes[1] = new Vector3(10f, 10f, 10f);

		Tree08_2Positions[2] = new Vector3(-880f, 0f, 392.5f);
		Tree08_28Sizes[2] = new Vector3(10f, 10f, 10f);

		//PineTree01;
		PineTree01Positions[0] = new Vector3(-1019.403f, 0.1f, -1019.403f);
		PineTree018Sizes[0] = new Vector3(7.5f, 7.5f, 7.5f);

		PineTree01Positions[1] = new Vector3(-598.9545f, -2.7f, 322.1f);
		PineTree018Sizes[1] = new Vector3(12f, 12f, 12f);

		//PineTree02;
		PineTree02Positions[0] = new Vector3(-617.5206f, 0.1f, 330.1f);
		PineTree028Sizes[0] = new Vector3(10f, 10f, 10f);

		//PineTree03;
		PineTree03Positions[0] = new Vector3(-1020.374f, 0.1f, -19.21356f);
		PineTree038Sizes[0] = new Vector3(5f, 5f, 5f);

		//tree03;
		tree03Positions[0] = new Vector3(-1010.95f, 0.1f, -5.25f);
		tree03Sizes[0] = new Vector3(3.5f, 3.5f, 3.5f);

		tree03Positions[1] = new Vector3(-801f, 0.1f, 311.15f);
		tree03Sizes[1] = new Vector3(3.5f, 3.5f, 3.5f);

		//tree05;
		tree05Positions[0] = new Vector3(-550.6f, 0.1f, 473.12f);
		tree05Sizes[0] = new Vector3(10f, 10f, 10f);

		//Tree01_2;
		Tree01_2Positions[0] = new Vector3(-1061.366f, 0.1f, -59f);
		Tree01_2Sizes[0] = new Vector3(70f, 70f, 70f);

		//Tree06;
		Tree06Positions[0] = new Vector3(-980.2982f, 0.1f, -29.4f);
		Tree06Sizes[0] = new Vector3(40f, 40f, 40f);

		Tree06Positions[1] = new Vector3(-1195.911f, 0.1f, -60.90576f);
		Tree06Sizes[1] = new Vector3(30f, 30f, 30f);

		//Tree02;
		Tree02Positions[0] = new Vector3(-1290.91f, 0.1f, -104.19f);
		Tree02Sizes[0] = new Vector3(40f, 40f, 40f);

		//Conifer1;
		Conifer1Positions[0] = new Vector3(-1035.798f, -0.91f, -12.7f);
		Conifer1Sizes[0] = new Vector3(3.5f, 3.5f, 3.5f);

		Conifer1Positions[1] = new Vector3(-659.9357f, 0.1020411f, -12.69684f);
		Conifer1Sizes[1] = new Vector3(2.5f, 2.5f, 2.5f);

		Conifer1Positions[2] = new Vector3(-656.82f, 0, 462.55f);
		Conifer1Sizes[2] = new Vector3(6f, 6f, 6f);

		Conifer1Positions[3] = new Vector3(-648.495f, 0, 469.7f);
		Conifer1Sizes[3] = new Vector3(5f, 5f, 5f);

		Conifer1Positions[4] = new Vector3(-656.65f, 0, 469.7f);
		Conifer1Sizes[4] = new Vector3(5f, 5f, 5f);

		Conifer1Positions[5] = new Vector3(-652.85f, 0, 475.8f);
		Conifer1Sizes[5] = new Vector3(5f, 5f, 5f);

		Conifer1Positions[6] = new Vector3(-645.45f, 0, 471f);
		Conifer1Sizes[6] = new Vector3(3.5f, 3.5f, 3.5f);

		Conifer1Positions[7] = new Vector3(-1153.2f, 0.1f, 80.7f);
		Conifer1Sizes[7] = new Vector3(20f, 20f, 20f);

		Conifer1Positions[8] = new Vector3(-1110.9f, 0.1f, -68.3f);
		Conifer1Sizes[8] = new Vector3(20f, 20f, 20f);

		Conifer1Positions[9] = new Vector3(-1110.9f, 0.1f, -51.5f);
		Conifer1Sizes[9] = new Vector3(20f, 20f, 20f);

		Conifer1Positions[10] = new Vector3(-1110.9f, 0.1f, -29.1f);
		Conifer1Sizes[10] = new Vector3(20f, 20f, 20f);

		Conifer1Positions[11] = new Vector3(-1128.056f, 0.1f, 37.65903f);
		Conifer1Sizes[11] = new Vector3(20f, 20f, 20f);

		Conifer1Positions[12] = new Vector3(-652.55f, 0f, 464.15f);
		Conifer1Sizes[12] = new Vector3(5f, 5f, 5f);

		Conifer1Positions[13] = new Vector3(-798.6f, 0.1f, 324.15f);
		Conifer1Sizes[13] = new Vector3(7.5f, 7.5f, 7.5f);

		Conifer1Positions[14] = new Vector3(-797.4f, 0f, 3341.4f);
		Conifer1Sizes[14] = new Vector3(7.5f, 7.5f, 7.5f);

		Conifer1Positions[15] = new Vector3(-801.6945f, 0f, 332.35f);
		Conifer1Sizes[15] = new Vector3(7.5f, 7.5f, 7.5f);

		Conifer1Positions[16] = new Vector3(-1037.83f, 0.1f, -16.95f);
		Conifer1Sizes[16] = new Vector3(3.5f, 3.5f, 3.5f);

		//Conifer7;
		Conifer7Positions[0] = new Vector3(-664.8948f, 0.1f, -19.77975f);
		Conifer7Sizes[0] = new Vector3(3.5f, 3.5f, 3.5f);

		Conifer7Positions[1] = new Vector3(-1066.25f, 2.65f, -0.85f);
		Conifer7Sizes[1] = new Vector3(3.5f, 3.5f, 3.5f);

		Conifer7Positions[2] = new Vector3(-690.3f, 0.1f, 116.1f);
		Conifer7Sizes[2] = new Vector3(15f, 15f, 15f);

		Conifer7Positions[3] = new Vector3(-663.2f, 0.1f, 395.9f);
		Conifer7Sizes[3] = new Vector3(15f, 15f, 15f);

		Conifer7Positions[4] = new Vector3(-696.9f, 0.1f, 134.4f);
		Conifer7Sizes[4] = new Vector3(15f, 15f, 15f);

		Conifer7Positions[5] = new Vector3(-678.2f, 0.1f, 101.1f);
		Conifer7Sizes[5] = new Vector3(15f, 15f, 15f);

		Conifer7Positions[6] = new Vector3(-508.76f, 0.1f, 258.5f);
		Conifer7Sizes[6] = new Vector3(15f, 15f, 15f);

		Conifer7Positions[7] = new Vector3(-1041.75f, 0.1f, 630.1f);
		Conifer7Sizes[7] = new Vector3(15f, 15f, 15f);

		Conifer7Positions[8] = new Vector3(-1055.84f, 0.1f, 630.1f);
		Conifer7Sizes[8] = new Vector3(15f, 15f, 15f);

		Conifer7Positions[9] = new Vector3(-1085.8f, 0.1f, 616.5f);
		Conifer7Sizes[9] = new Vector3(15f, 15f, 15f);

		Conifer7Positions[10] = new Vector3(-1064.7f, 0.1f, 625.2f);
		Conifer7Sizes[10] = new Vector3(15f, 15f, 15f);

		Conifer7Positions[11] = new Vector3(-644.6f, 0.1f, 395.9f);
		Conifer7Sizes[11] = new Vector3(15f, 15f, 15f);

		Conifer7Positions[12] = new Vector3(-626.2f, 0.1f, 395.9f);
		Conifer7Sizes[12] = new Vector3(15f, 15f, 15f);

		//bush03;
		bush03Positions[0] = new Vector3(-644.06f, 0.1f, 462.9162f);
		bush03Sizes[0] = new Vector3(4f, 4f, 4f);

		bush03Positions[1] = new Vector3(-4.5f, 0.1f, 497.9f);
		bush03Sizes[1] = new Vector3(40f, 40f, 40f);

		bush03Positions[2] = new Vector3(-143.6f, 0.1f, 482.4f);
		bush03Sizes[2] = new Vector3(40f, 40f, 40f);

		bush03Positions[3] = new Vector3(-1.2f, 0.1f, 277.2f);
		bush03Sizes[3] = new Vector3(40f, 40f, 40f);

		bush03Positions[4] = new Vector3(-37.94861f, 0.1f, 389.5f);
		bush03Sizes[4] = new Vector3(40f, 40f, 40f);

		//Tree07;
		Tree07Positions[0] = new Vector3(-1216f, 0.1f, -99.1f);
		Tree07Sizes[0] = new Vector3(10f, 10f, 10f);

		Tree07Positions[1] = new Vector3(-1164.2f, 0.1f, -38.8f);
		Tree07Sizes[1] = new Vector3(10f, 15f, 10f);

		// bush05;
		bush05Positions[0] = new Vector3(-799.34f, 0.1f, 304.76f);
		bush05Sizes[0] = new Vector3(10f, 10f, 10f);

		bush05Positions[1] = new Vector3(-799.34f, 0.1f, 314.65f);
		bush05Sizes[1] = new Vector3(10f, 10f, 10f);

		bush05Positions[2] = new Vector3(-803.3f, 0.1f, 317.8f);
		bush05Sizes[2] = new Vector3(10f, 10f, 10f);

		//flowers05;
		flowers05Positions[0] = new Vector3(-678.585f, 0.1f, -11.03f);
		flowers05Sizes[0] = new Vector3(5f, 5f, 5f);

		//flowers01;
		flowers01Positions[0] = new Vector3(-674.105f, 1.255f, 467.74f);
		flowers01Sizes[0] = new Vector3(7.5f, 7.5f, 7.5f);

		flowers01Positions[1] = new Vector3(-670.0446f, 0.1020406f, -8.705109f);
		flowers01Sizes[1] = new Vector3(2.5f, 2.5f, 2.5f);

		flowers01Positions[2] = new Vector3(-668.055f, 1.11f, 467.74f);
		flowers01Sizes[2] = new Vector3(7.5f, 7.5f, 7.5f);


		//bush02;
		bush02Positions[0] = new Vector3(-647.93f, 0, 461.02f);
		bush02Sizes[0] = new Vector3(2.5f, 2.5f, 2.5f);

		//Oduvanchik01;
		Oduvanchik01Positions[0] = new Vector3(-684.84f, 0.1f, 464.745f);
		Oduvanchik01Sizes[0] = new Vector3(12.5f, 12.5f, 12.5f);

		Oduvanchik01Positions[1] = new Vector3(-684.84f, 0.1f, 463.805f);
		Oduvanchik01Sizes[1] = new Vector3(12.5f, 12.5f, 12.5f);

		Oduvanchik01Positions[2] = new Vector3(-684.84f, 0.1f, 463.04f);
		Oduvanchik01Sizes[2] = new Vector3(12.5f, 12.5f, 12.5f);

		//bush01;
		bush01Positions[0] = new Vector3(-645.3155f, 0f, 460.82f);
		bush01Sizes[0] = new Vector3(2.5f, 2.5f, 2.5f);

		//tree02;
		tree02_1Positions[0] = new Vector3(-1026.515f, 0.1f, -47.83594f);
		tree02_1Sizes[0] = new Vector3(3.5f, 3.5f, 3.5f);

		//tree04;
		tree04Positions[0] = new Vector3(-1032.135f, 0.95f, -39.21689f);
		tree04Sizes[0] = new Vector3(5f, 5f, 5f);

		tree04Positions[1] = new Vector3(-1114.1f, 0.1f, -110.6f);
		tree04Sizes[1] = new Vector3(15f, 10f, 15f);

		tree04Positions[2] = new Vector3(-1154.1f, 0.1f, -104.6f);
		tree04Sizes[2] = new Vector3(13f, 10f, 13f);

		//bush06;
		bush06Positions[0] = new Vector3(-1258.1f, 0.1f, 64.6f);
		bush06Sizes[0] = new Vector3(60f, 60f, 60f);

		bush06Positions[1] = new Vector3(-1269.9f, 0.1f, -58.7f);
		bush06Sizes[1] = new Vector3(60f, 60f, 60f);

		//bush01_1;
		bush01_1Positions[0] = new Vector3(-1275.62f, 0.1f, -120.64f);
		bush01_1Sizes[0] = new Vector3(30f, 30f, 30f);

		//tree01;
		tree01Positions[0] = new Vector3(-1182.4f, 0.1f, -171.8112f);
		tree01Sizes[0] = new Vector3(4f, 4f, 4f);

		//Tree02_2;
		Tree02_2Positions[0] = new Vector3(173.2829f, 0.1f, 328.5915f);
		Tree02_2Sizes[0] = new Vector3(15f, 15f, 15f);

		//Tree10_4;
		Tree10_4Positions[0] = new Vector3(160.7f, 0.1f, 265.5f);
		Tree10_4Sizes[0] = new Vector3(2f, 2f, 2f);

		//tree1a;
		tree1aPositions[0] = new Vector3(59.1f, 0.1020015f, 275.9f);
		tree1aSizes[0] = new Vector3(3f, 3, 3);

		StartCoroutine(LoadObjects());
	}

	int m_RunTwise = 0;
	void FillLoadImage()
	{
		if (TownController.GetInstance ().LoadingImgTop.fillAmount >= 1) {
			TownController.GetInstance ().LoadingImg.fillAmount = TownController.GetInstance ().LoadingImg.fillAmount + (2f / 72f);
		} else {
			TownController.GetInstance ().LoadingImgTop.fillAmount = TownController.GetInstance ().LoadingImgTop.fillAmount + (2f / 72f);
		}

		TownController.GetInstance ().LoadingPercentage = TownController.GetInstance ().LoadingPercentage + (1f / 72f);
		if ((int)(TownController.GetInstance ().LoadingPercentage * 100) > 100)
			TownController.GetInstance ().LoadingTxt.text = "100%";
		else
			TownController.GetInstance ().LoadingTxt.text = ((int)(TownController.GetInstance ().LoadingPercentage * 100)) + "%";
	}

	IEnumerator LoadObjects()
	{
		GameObject go;

		for(int i = 0; i < m_BroadLeaf_LOD2Positions.Length; i++)
		{
			go = (GameObject)Instantiate(BroadLeaf_LOD2);
			go.transform.position = m_BroadLeaf_LOD2Positions[i];
			go.transform.localScale = m_BroadLeaf_LOD2Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}


		for(int i = 0; i < m_tree1a_prePositions.Length; i++)
		{
			go = (GameObject)Instantiate(tree1a_pre);
			go.transform.position = m_tree1a_prePositions[i];
			go.transform.localScale = m_tree1a_pre2Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < m_Tree9_2Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree9_2);
			go.transform.position = m_Tree9_2Positions[i];
			go.transform.localScale = m_Tree9_2Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}


		for(int i = 0; i < Broadleaf_MobilePositions.Length; i++)
		{
			go = (GameObject)Instantiate(Broadleaf_Mobile);
			go.transform.position = Broadleaf_MobilePositions[i];
			go.transform.localScale = Broadleaf_MobileSizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Broadleaf_Mobile_LOD0Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Broadleaf_Mobile_LOD0);
			go.transform.position = Broadleaf_Mobile_LOD0Positions[i];
			go.transform.localScale = Broadleaf_Mobile_LOD0Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Broadleaf_Mobile_BillboardPositions.Length; i++)
		{
			go = (GameObject)Instantiate(Broadleaf_Mobile_Billboard);
			go.transform.position = Broadleaf_Mobile_BillboardPositions[i];
			go.transform.localScale = Broadleaf_Mobile_BillboardSizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < tree2_prePositions.Length; i++)
		{
			go = (GameObject)Instantiate(tree2_pre);
			go.transform.position = tree2_prePositions[i];
			go.transform.localScale = tree2_preSizes[i];

			yield return new WaitForSeconds(0.1f);
			FillLoadImage ();
		}


		for(int i = 0; i < Tree9_4Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree9_4);
			go.transform.position = Tree9_4Positions[i];
			go.transform.localScale = Tree9_4Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Tree08Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree08);
			go.transform.position = Tree08Positions[i];
			go.transform.localScale = Tree08Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}


		for(int i = 0; i < Tree08_2Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree08_2);
			go.transform.position = Tree08_2Positions[i];
			go.transform.localScale = Tree08_28Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < PineTree01Positions.Length; i++)
		{
			go = (GameObject)Instantiate(PineTree01);
			go.transform.position = PineTree01Positions[i];
			go.transform.localScale = PineTree018Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < PineTree02Positions.Length; i++)
		{
			go = (GameObject)Instantiate(PineTree02);
			go.transform.position = PineTree02Positions[i];
			go.transform.localScale = PineTree028Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < PineTree03Positions.Length; i++)
		{
			go = (GameObject)Instantiate(PineTree03);
			go.transform.position = PineTree03Positions[i];
			go.transform.localScale = PineTree038Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < tree03Positions.Length; i++)
		{
			go = (GameObject)Instantiate(tree03);
			go.transform.position = tree03Positions[i];
			go.transform.localScale = tree03Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < tree05Positions.Length; i++)
		{
			go = (GameObject)Instantiate(tree05);
			go.transform.position = tree05Positions[i];
			go.transform.localScale = tree05Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Tree01_2Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree01_2);
			go.transform.position = Tree01_2Positions[i];
			go.transform.localScale = Tree01_2Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Tree06Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree06);
			go.transform.position = Tree06Positions[i];
			go.transform.localScale = Tree06Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Tree02Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree02);
			go.transform.position = Tree02Positions[i];
			go.transform.localScale = Tree02Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Conifer1Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Conifer1);
			go.transform.position = Conifer1Positions[i];
			go.transform.localScale = Conifer1Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Conifer7Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Conifer7);
			go.transform.position = Conifer7Positions[i];
			go.transform.localScale = Conifer7Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < bush03Positions.Length; i++)
		{
			go = (GameObject)Instantiate(bush03);
			go.transform.position = bush03Positions[i];
			go.transform.localScale = bush03Sizes[i];

			yield return new WaitForSeconds(0.1f);
			FillLoadImage ();
		}

		for(int i = 0; i < Tree07Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree07);
			go.transform.position = Tree07Positions[i];
			go.transform.localScale = Tree07Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}


		for(int i = 0; i < bush05Positions.Length; i++)
		{
			go = (GameObject)Instantiate(bush05);
			go.transform.position = bush05Positions[i];
			go.transform.localScale = bush05Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < flowers05Positions.Length; i++)
		{
			go = (GameObject)Instantiate(flowers05);
			go.transform.position = flowers05Positions[i];
			go.transform.localScale = flowers05Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < flowers01Positions.Length; i++)
		{
			go = (GameObject)Instantiate(flowers01);
			go.transform.position = flowers01Positions[i];
			go.transform.localScale = flowers01Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < bush02Positions.Length; i++)
		{
			go = (GameObject)Instantiate(bush02);
			go.transform.position = bush02Positions[i];
			go.transform.localScale = bush02Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Oduvanchik01Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Oduvanchik01);
			go.transform.position = Oduvanchik01Positions[i];
			go.transform.localScale = Oduvanchik01Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < bush01Positions.Length; i++)
		{
			go = (GameObject)Instantiate(bush01);
			go.transform.position = bush01Positions[i];
			go.transform.localScale = bush01Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < tree02_1Positions.Length; i++)
		{
			go = (GameObject)Instantiate(tree02_1);
			go.transform.position = tree02_1Positions[i];
			go.transform.localScale = tree02_1Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < tree04Positions.Length; i++)
		{
			go = (GameObject)Instantiate(tree04);
			go.transform.position = tree04Positions[i];
			go.transform.localScale = tree04Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < bush06Positions.Length; i++)
		{
			go = (GameObject)Instantiate(bush06);
			go.transform.position = bush06Positions[i];
			go.transform.localScale = bush06Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < bush01_1Positions.Length; i++)
		{
			go = (GameObject)Instantiate(bush01_1);
			go.transform.position = bush01_1Positions[i];
			go.transform.localScale = bush01_1Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < tree01Positions.Length; i++)
		{
			go = (GameObject)Instantiate(tree01);
			go.transform.position = tree01Positions[i];
			go.transform.localScale = tree01Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}


		for(int i = 0; i < m_Tree9Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree9_5);
			go.transform.position = m_Tree9Positions[i];
			go.transform.localScale = m_Tree9Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < tree1aPositions.Length; i++)
		{
			go = (GameObject)Instantiate(tree1a);
			go.transform.position = tree1aPositions[i];
			go.transform.localScale = tree1aSizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Tree10_4Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree10_4);
			go.transform.position = Tree10_4Positions[i];
			go.transform.localScale = Tree10_4Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		for(int i = 0; i < Tree02_2Positions.Length; i++)
		{
			go = (GameObject)Instantiate(Tree02_2);
			go.transform.position = Tree02_2Positions[i];
			go.transform.localScale = Tree02_2Sizes[i];

			yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		Instantiate (AssetLoader);
	}
}
