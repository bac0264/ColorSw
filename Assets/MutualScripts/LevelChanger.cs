using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class LevelChanger : MonoBehaviour
{
    public static LevelChanger instance;
    public bool FadeIn;
    public bool FadeOut;
    public bool check;
    public Image im;
    public string sceneName;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        im = gameObject.transform.GetChild(0).GetComponent<Image>();
    }
    private void Update()
    {
        if (check == true)
        {
            if (FadeIn == true)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                StartCoroutine(_FadeIn());
            }
            if (FadeOut == true)
            {
                if (im != null)
                {
                    StartCoroutine(_FadeOut());
                }
            }
        }
    }
    IEnumerator _FadeOut()
    {
        FadeOut = false;
        Tween fadeOut = im.DOColor(new Color(0f, 0f, 0f, 0f), 0.4f);
        yield return fadeOut.WaitForCompletion();
        transform.GetChild(0).gameObject.SetActive(false);
    }
    IEnumerator _FadeIn()
    {
        FadeIn = false;
        Tween fadeIn = im.DOColor(new Color(0f, 0f, 0f, 1f), 0.4f);
        yield return fadeIn.WaitForCompletion();
        SceneManager.LoadScene(sceneName.ToString());
    }
    public void FadeInfc(string _sceneName)
    {
        if (instance != null)
        {
            check = true;
            FadeIn = true;
            sceneName = _sceneName;
        }
    }
    public void nextScene(string _sceneName)
    {
        if (instance != null)
        {
            FadeInfc(_sceneName);
        }
    }
    public void FadeOutfc()
    {
        if (instance != null)
        {
            if (check == true)
            {
                FadeOut = true;
                // Fade.instance.check = false;
            }
        }
    }
}
