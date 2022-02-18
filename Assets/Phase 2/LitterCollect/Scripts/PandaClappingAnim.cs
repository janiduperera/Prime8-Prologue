using UnityEngine;
using System.Collections;

public class PandaClappingAnim : MonoBehaviour {


	public void PandaClappingComplete()
	{
		transform.parent.gameObject.GetComponent<AudioSource>().loop = false;
		transform.parent.gameObject.GetComponent<AudioSource>().Stop();

		if(SaveDataStatic.StorySequence == "Litter") // Litter Collect Complete
		{
			if(!SaveDataStatic.forTesting)
			{
				Quiz.GetInstance().ShowQuez(0);
			}
			else
			{
                TownController.GetInstance().SetTimerUI(false);

                TownController.GetInstance().BackToAmandaPanda("", null);
            }
		}
		else if(SaveDataStatic.StorySequence == "SaplingTreeEnd") // Sapling Tree Complete
		{
			if(!SaveDataStatic.forTesting)
			{
				Quiz.GetInstance().ShowQuez(3);
			}
			else
			{
                //TownController.GetInstance().TimerTxt.text = "";
                TownController.GetInstance().SetTimerUI(false);

                TownController.GetInstance().BackToAmandaPanda("", null);

            }
		}
	}
}
