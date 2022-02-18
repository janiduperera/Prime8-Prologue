using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotTaker : MonoBehaviour
{
    int index = 1;
    // Start is called before the first frame update
    void Start()
    {

#if !UNITY_EDITOR
        Destroy(gameObject);
#else
        DontDestroyOnLoad(gameObject);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ScreenCapture.CaptureScreenshot("Screenshot_"+index+".png");
            index++;
        }
    }
}
