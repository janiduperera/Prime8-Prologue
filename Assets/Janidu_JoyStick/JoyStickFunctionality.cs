using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickFunctionality : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_IOS || UNITY_ANDROID
        gameObject.SetActive(true);
#else
        gameObject.SetActive(false);
#endif
    }
}
