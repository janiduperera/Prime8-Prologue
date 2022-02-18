using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class InventryOnclick : MonoBehaviour {
    public GameObject Medal;
    public GameObject inventryPanel;
	Sprite buttonSprite;
	public GameObject cursor2;
	public GameObject cursor3;
	public GameObject cursor4;
	public GameObject fridgeGlow;
	private Image image;
    public GameObject panda;
	//public Button[] buttons;
	public Sprite key;
	public  bool GetKey = false;
	bool isClicked;
	bool tookBattery = false;
	bool tookBag = false;
	public  bool putSerial = false;
	bool  bagNbattery = false;
	public  bool bintocursor = false;
	public  bool putSalard = false;
	public  bool saladOk = false;
    public  bool getSalard = false;
    public  bool ClickedBagNbatry = false;
    public  bool getstamp = false;
    public  bool putLetter = false;
    public  bool MedalOk = false;

    public Toggle[] togglearray;
    public Image[] exclamationImages;
    public AudioClip excalmationSound;
    public AudioSource audio;
	public Texture2D handTex;
	public Texture2D BatterTexForCursor;
	public Texture2D BagTexForCursor;
	public Image[] buttons;

	public Sprite Serial;
	public Sprite UiSprite;
	public Sprite UiSprite2;
	public Sprite bag;
	public Sprite battery;
	public Sprite bagNbatry;
	public Sprite bin;
	public Sprite salard;
    public Sprite stamp;
    public Sprite letterCase;

    public KitchenGamePlay kitchenScript;
    public inventory2 inventory2;


	//Button b;
	// Use this for initializationandler
	void Start () {
        exclamationImages[0].GetComponent<Image>().enabled = false;
        exclamationImages[1].GetComponent<Image>().enabled = false;
        audio.Stop();
        //exclamationImages[0].SetActive(false);       

	}
	
	// Update is called once per frame
    void Update()
    {
        if (kitchenScript.putBagNbattery == true)
        {
            //fridgeGlow.SetActive(true);
            cursor2.SetActive(false);
            
        }
        if (kitchenScript.putbin)
        {

            foreach (Image b in buttons)
            {
                if (b.sprite == bin)
                {
                    b.sprite = UiSprite;
                    break;

                }
                cursor3.SetActive(false);
            }
            if (kitchenScript.putBagNbattery == true && kitchenScript.putbin)
            {
                putSalard = true;
                bintocursor = false;

            }
            if (kitchenScript.putsalardTobin)
            {
              //  cursor4.SetActive(false);
                saladOk = true;
            }
        }
    }
    public void OnPointerClick(Toggle toggle)
    {

        if (toggle.name == "Toggle (1)")
        {
            if (toggle.GetComponent<Toggle>().isOn == true)
            {
                audio.clip = excalmationSound;
                audio.Play();
                exclamationImages[1].GetComponent<Image>().enabled = false;
                exclamationImages[0].GetComponent<Image>().enabled = true;
            }

        }
        if (toggle.name == "Toggle (2)")
        {
            if (toggle.GetComponent<Toggle>().isOn == true)
            {
                audio.clip = excalmationSound;
                audio.Play();

                exclamationImages[0].GetComponent<Image>().enabled = false;
                exclamationImages[1].GetComponent<Image>().enabled = true;
            }

        }

        if (toggle.name == "Toggle")
        {
            if (toggle.GetComponent<Toggle>().isOn == true)
            {
                exclamationImages[0].GetComponent<Image>().enabled = false;
                exclamationImages[1].GetComponent<Image>().enabled = false;
            }
        }
       
        else
        {

        }

    }

	public void OnClicked(Image button){

        if (tookBattery == true)
        {
            if (button.sprite.name == bag.name && button.gameObject.transform.GetChild(0).GetComponent<Button>().interactable)
            {
                if (kitchenScript.putBagNbattery == false)
                {
                    button.sprite = bagNbatry;
#if !(UNITY_IOS || UNITY_ANDROID)
                    Cursor.SetCursor(handTex,Vector2.zero,CursorMode.Auto);
#else
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
#endif


                    foreach (Image b in buttons)
                    {
                        if (b.sprite.name == battery.name)
                        {
#if !(UNITY_IOS || UNITY_ANDROID)
                                Cursor.visible = true;
                                Cursor.lockState = CursorLockMode.None;
#endif
                            b.sprite = UiSprite;
                            break;
                        }
                    }

                    if (GetKey == true)
                    {

                    }
                    bagNbattery = true;
                }
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                //tookBattery = false;

            }
            tookBattery = false;
            return;
        }

        if (tookBag == true)
        {
            if (button.sprite.name == battery.name && button.gameObject.transform.GetChild(0).GetComponent<Button>().interactable)
            {
                if (kitchenScript.putBagNbattery == false)
                {
                    button.sprite = bagNbatry;
#if !(UNITY_IOS || UNITY_ANDROID)
                    Cursor.SetCursor (handTex, Vector2.zero, CursorMode.Auto);
#else
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
#endif


                    foreach (Image b in buttons)
                    {
                        if (b.sprite.name == bag.name)
                        {
#if !(UNITY_IOS || UNITY_ANDROID)
                            Cursor.visible = true;
                            Cursor.lockState = CursorLockMode.None;
#endif
                            b.sprite = UiSprite;
                            break;
                        }
                    }

                    if (GetKey == true)
                    {

                    }
                    bagNbattery = true;
                }
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                //tookBag = false;

            }
            tookBag = false;
            return;
        }

        if (button.name == "ButtonMedal" && Medal.activeSelf)
        {
            MedalOk = true;
            panda.GetComponent<MonoBehaviour>().enabled = true;
            Medal.SetActive(false);
        }

        if (button.sprite.name == stamp.name)
        {
#if !(UNITY_IOS || UNITY_ANDROID)
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
            kitchenScript.JoyStickSettings(true);
            inventryPanel.SetActive(false);
            panda.GetComponent<MonoBehaviour>().enabled = true;
            button.sprite = UiSprite;
            cursor4.SetActive(true);
            ShowCursor2(stamp);
            getstamp = true;
        }

		if (button.sprite.name == bin.name && button.gameObject.transform.GetChild(0).GetComponent<Button>().interactable) {
#if !(UNITY_IOS || UNITY_ANDROID)
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
            kitchenScript.JoyStickSettings(true);
            inventryPanel.SetActive(false);
            inventory2.panelHideFlag = false;
            panda.GetComponent<MonoBehaviour>().enabled = true;
			//if(KitchenGamePlay.unlockedDoor){
            button.sprite = UiSprite;
			ShowCursor2(bin);
			bintocursor = true;			
			//}
			if(kitchenScript.putbin){
				//button.interactable = false;
			}
		}


        if (button.sprite.name == bagNbatry.name && button.gameObject.transform.GetChild(0).GetComponent<Button>().interactable)
        {
#if !(UNITY_IOS || UNITY_ANDROID)
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
            kitchenScript.JoyStickSettings(true);
            inventryPanel.SetActive(false);
            inventory2.panelHideFlag = false;
            panda.GetComponent<MonoBehaviour>().enabled = true;
            cursor4.SetActive(true);
            ShowCursor2(bagNbatry);
            ClickedBagNbatry = true;
            button.sprite = UiSprite;
        }

        if (button.sprite.name == letterCase.name)
        {
#if !(UNITY_IOS || UNITY_ANDROID)
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
            kitchenScript.JoyStickSettings(true);
            inventryPanel.SetActive(false);
            inventory2.panelHideFlag = false;
            panda.GetComponent<MonoBehaviour>().enabled = true;
            cursor4.SetActive(true);
            ShowCursor2(letterCase);
            putLetter = true;
            button.sprite = UiSprite;
        }

		if (button.sprite.name == key.name && button.gameObject.transform.GetChild(0).GetComponent<Button>().interactable) {

#if !(UNITY_IOS || UNITY_ANDROID)
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
            kitchenScript.JoyStickSettings(true);
            inventryPanel.SetActive(false);
            inventory2.panelHideFlag = false;
            panda.GetComponent<MonoBehaviour>().enabled = true;
            GetKey = true;
            button.sprite = UiSprite;
			ShowCursor2(key);
			//}
			//button.interactable = false;
			//if(KitchenGamePlay.UnlockDoor()){
				//button.image.sprite.name = "xxx";
			//}

		}
	
		if (button.sprite.name == salard.name && button.gameObject.transform.GetChild(0).GetComponent<Button>().interactable) {
#if !(UNITY_IOS || UNITY_ANDROID)
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
            kitchenScript.JoyStickSettings(true);
            inventryPanel.SetActive(false);
            inventory2.panelHideFlag = false;
            panda.GetComponent<MonoBehaviour>().enabled = true;
            cursor4.SetActive(true);
				ShowCursor2(salard);
                getSalard = true;
                button.sprite = UiSprite;
		
		}

		
		
       
        //else Cursor.SetCursor(handTex,Vector2.zero,CursorMode.Auto);

        if (button.sprite.name == bag.name && button.gameObject.transform.GetChild(0).GetComponent<Button>().interactable) {
			
			if(bagNbattery == true){
				//button.interactable =false;
				//button.image.sprite = UiSprite;
			}
			else if(bagNbattery == false){
			//Cursor.SetCursor(battery.texture ,Vector2.zero,CursorMode.Auto);
				Cursor.SetCursor(BagTexForCursor , new Vector2(BagTexForCursor.width / 2, BagTexForCursor.height / 2), CursorMode.Auto);
#if !(UNITY_IOS || UNITY_ANDROID)
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
#endif
			tookBag = true;
			

			//button.interactable = false;
			}
		}

		if (button.sprite.name == battery.name && button.gameObject.transform.GetChild(0).GetComponent<Button>().interactable) {

			if(bagNbattery == true){
				//button.interactable =false;
				//button.image.sprite = UiSprite;
			}
			else if(bagNbattery == false){
				//Cursor.SetCursor(battery.texture ,Vector2.zero,CursorMode.Auto);
				Cursor.SetCursor(BatterTexForCursor, new Vector2(BatterTexForCursor.width / 2, BatterTexForCursor.height / 2), CursorMode.Auto);
#if !(UNITY_IOS || UNITY_ANDROID)
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
#endif
				tookBattery = true;


				//button.interactable = false;
			}
		}

		if (button.sprite.name == Serial.name && button.gameObject.transform.GetChild(0).GetComponent<Button>().interactable) {
            //	ShowCursor(Serial);
#if !(UNITY_IOS || UNITY_ANDROID)
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
            kitchenScript.JoyStickSettings(true);
            inventryPanel.SetActive(false);
            inventory2.panelHideFlag = false;
            panda.GetComponent<MonoBehaviour>().enabled = true;
            cursor4.SetActive(true);
			button.sprite = UiSprite;
            ShowCursor2(Serial);
			//button.interactable = false;
			putSerial = true;

			//Debug.Log("ff");
		}

        Soundmanager2.instance.playSingle(kitchenScript.ItemPickAudio);
        //SendMessageUpwards("PlaySourceClip", kitchenScript.ItemPickAudio, SendMessageOptions.DontRequireReceiver);
        // (button.name);	
    }
	public  bool PutSerial(){
		return putSerial;
	}
	public  bool getKey(){
		return GetKey;
	}
//	public static bool bagNBattery(){
//	
//		return bagNbattery;
//	}

	//void ShowCursor(Sprite spr){
	//	if (cursor2 != null) {
	//		cursor2.GetComponent<Image> ().sprite = spr;
	//		cursor2.SetActive (true);
	//	}
	

	//	}
	//void ShowCursor1(Sprite spr){
		//if (cursor3 != null) {
		//	cursor3.GetComponent<Image> ().sprite = spr;
		//	cursor3.SetActive (true);
		//}

    //}

	void ShowCursor2(Sprite spr){
		if (cursor4 != null) {
            kitchenScript.pointerDot.SetActive(false);
			cursor4.GetComponent<Image> ().sprite = spr;
			cursor4.SetActive (true);
		}
		
	}

	public Sprite DefaultInvButtonHolder;
	public Text InventoryItemNameTxt;
	string m_ParentSpriteName;
	public void OnPointerEnter(Transform _parentBtn)
	{
		if (_parentBtn.gameObject.GetComponent<Image> ().sprite == DefaultInvButtonHolder)
			return;

		m_ParentSpriteName = _parentBtn.gameObject.GetComponent<Image> ().sprite.name;

		if (_parentBtn.gameObject.name.Contains ("Medal")) {

			if (m_ParentSpriteName == "Recycle") {
				InventoryItemNameTxt.text = "Amanda : Recycling Medal";
			}
			else if (m_ParentSpriteName == "AntiPolu") {
				InventoryItemNameTxt.text = "Caterpillar : Exercise & Diet";
			}
			else if (m_ParentSpriteName == "CleanEnv") {
				InventoryItemNameTxt.text = "Sam Snake : Air Pollution & Energy Use";
			}
			else if (m_ParentSpriteName == "Conservation") {
				InventoryItemNameTxt.text = "Little Gorilla : Wildlife & Rainforest";
			}
			else if (m_ParentSpriteName == "LeaveItClean") {
				InventoryItemNameTxt.text = "Rhino : Litter & Packaging";
			}
			else if (m_ParentSpriteName == "SeaOcean") {
				InventoryItemNameTxt.text = "Wanda Whale : Healthy Seas & Oceans";
			}
			else if (m_ParentSpriteName == "Tree") {
				InventoryItemNameTxt.text = "Tiny Tiger : Conversation & Biodiversity";
			}
			else if (m_ParentSpriteName == "WaterCons") {
				InventoryItemNameTxt.text = "Hugi Hippo : Water Conservation";
			}

		} else {
			InventoryItemNameTxt.text = m_ParentSpriteName;
		}


		InventoryItemNameTxt.gameObject.transform.SetParent (_parentBtn);
		InventoryItemNameTxt.gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (98, 33, 0);
	}

	public void OnPointerExit()
	{
		InventoryItemNameTxt.text = "";
		InventoryItemNameTxt.gameObject.transform.SetParent (null);
	}
}