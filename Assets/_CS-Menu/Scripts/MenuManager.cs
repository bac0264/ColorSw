using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject mainGame;
    public void Sound()
    {

    }

    public void Mode()
    {

    }
    
    public void ProductBy()
    {

    }

    public void NoAds()
    {

    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Rank()
    {

    }

    public void Rate()
    {
        Application.OpenURL("market://details?id=com.zergitas.colorswitch");
    }
}
