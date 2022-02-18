using UnityEngine;
using System.Collections;

public class AssetLoader : MonoBehaviour {

    public GameObject[] Props;
	//public GameObject[] RoadCol;

	//public GameObject[] TreeLines;

	//public GameObject[] AreaColliders;

	public GameObject[] TrafficCrossWayPoints;

    public GameObject DuckMale, DuckFemale, Crow;

	//public GameObject[] LampPosts;

	//public GameObject[] TrafficLights;

	//public GameObject[] TownObjects;

	public GameObject[] Characters;
	public GameObject[] InstantiatedCharacters;
	public string[] CharacterNames;



	// Use this for initialization
	void Start () {

		m_Instane = this;
		StartCoroutine(LoadAssets());
	}
	
	IEnumerator LoadAssets()
	{
        //yield return null;
		yield return new WaitForEndOfFrame ();
		GameObject go;

        //Props
        for(int i = 0; i < Props.Length; i++)
        {
            go = (GameObject)Instantiate(Props[i]);
        }

        //Traffic Cross Way Points
        TrafficCrossings[] m_TempTrafficCrossing = new TrafficCrossings[4];
		for(int i = 0; i < TrafficCrossWayPoints.Length; i++)
		{
			go = (GameObject)Instantiate(TrafficCrossWayPoints[i]);

			if (i < 4) {
				m_TempTrafficCrossing [i] = go.GetComponent < TrafficCrossings> ();
			} else {
				for (int t = 0; t < m_TempTrafficCrossing.Length; t++) {
					m_TempTrafficCrossing [t].TrafficCars [i - 4] = go.GetComponent<TrafficCar> ();
				}
			}

			//yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

        //Ducks
        go = (GameObject)Instantiate(DuckMale);
     //   yield return new WaitForSeconds(0.1f);
        go = (GameObject)Instantiate(DuckFemale);
     //   yield return new WaitForSeconds(0.1f);

    
        //Crows
        for(int i = 0; i < 10; i++)
        {
            go = (GameObject)Instantiate(Crow, new Vector3(Random.Range(-1264f, 164f), 22, Random.Range(-65f, 622f)), Quaternion.identity);
      //      yield return new WaitForSeconds(0.1f);
        }
      
		//Characters
		for(int i = 0; i < Characters.Length; i++)
		{
			InstantiatedCharacters[i] = (GameObject)Instantiate(Characters[i]);
			InstantiatedCharacters [i].name = CharacterNames [i];

			//yield return new WaitForSeconds(0.1f);

			FillLoadImage ();
		}

		TownController.GetInstance ().StartAfterObjectsLoaded ();
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

	public GameObject GetCharacter(int _index)
	{
		return InstantiatedCharacters [_index];
	}

	#region Singleton
	private static AssetLoader m_Instane;
	private AssetLoader(){}
	public static AssetLoader GetInstance()
	{
		return m_Instane;
	}
	#endregion Singleton
}
