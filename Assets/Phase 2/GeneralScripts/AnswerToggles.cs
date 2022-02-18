using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerToggles : MonoBehaviour {

	public Toggle[]	Answer1Toggle;

	public Toggle[]	Answer2Toggle;

	// Use this for initialization
	void Start () {
	
	}
	
	public bool IsAllAnswersSelected()
	{
		bool answer1 = false;
		if(Answer1Toggle.Length > 0)
		{
			foreach(Toggle t1 in Answer1Toggle)
			{
				if(t1.isOn)
				{
					answer1 = true;
					break;
				}
			}
		}
		else
		{
			answer1 = true;
		}

		bool answer2 = false;
		if(Answer2Toggle.Length > 0)
		{
			foreach(Toggle t2 in Answer2Toggle)
			{
				if(t2.isOn)
				{
					answer2 = true;
					break;
				}
			}
		}
		else
		{
			answer2 = true;
		}

		if(answer1 && answer2) return true;
		else return false;
	}
}
