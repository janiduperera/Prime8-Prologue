using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class JoyStickController : MonoBehaviour
{
#if UNITY_IOS || UNITY_ANDROID
    public FixedJoystick FixedJoystick;
    public FixedTouchField FixedTouchField;
    public FixedButton FixedButton; // Normally used for Jump button

    private FirstPersonController m_FPS;

    // Start is called before the first frame update
    void Start()
    {
        m_FPS = GetComponent<FirstPersonController>();
        m_FPS.m_MouseLook.lockCursor = false;
        m_FPS.m_MouseLook.XSensitivity = 0.2f;
        m_FPS.m_MouseLook.YSensitivity = 0.2f;

        if (SceneManager.GetActiveScene().name == "Town")
        {
            FixedJoystick = TownController.GetInstance().JoyStick.gameObject.transform.Find("Fixed Joystick").gameObject.GetComponent<FixedJoystick>();
            FixedTouchField = TownController.GetInstance().JoyStick.gameObject.transform.Find("MouseLookJoyStick").gameObject.GetComponent<FixedTouchField>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_FPS.RunAxis = FixedJoystick.Direction;
        //m_FPS.JumpAxis = FixedButton.Pressed;
        m_FPS.m_MouseLook.LookAxis = FixedTouchField.TouchDist;
    }
#endif
}
