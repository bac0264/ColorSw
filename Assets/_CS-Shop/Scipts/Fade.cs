using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (LevelChanger.instance != null)
        {
            LevelChanger.instance.FadeOutfc();
        }
    }
}
