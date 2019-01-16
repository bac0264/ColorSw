using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Process : MonoBehaviour
{
    public static Process instance;

    Vector3 playerDefaultPos = new Vector3(0, -1.1f, 0);
    Vector3 scaleDefault = new Vector3(1, 1, 1);
    Vector3 barriderDefaultIn = new Vector3(0, 3.3f, 0);
    Vector3 barriderDefaultOut = new Vector3(0, 8f, 0);

    public GameObject menuManager;
    public GameObject mainManager;
    public GameObject loseManager;
    public Transform container;
    public GameObject player;
    public GameObject defaultBarrier;
    public GameObject panel;

    public GamePlay mode;


    public enum GamePlay {
        PlayGame,
        Home,
        Replay,
        Lose,
        Home_2
    }
    private void Start()
    {
        if (instance == null) instance = this;
        mode = GamePlay.Home;
    }
    public void status()
    {
        switch (mode)
        {
            case GamePlay.PlayGame:
                StartCoroutine(timetoPlay());
                player.GetComponent<Player>().setupPlayer(playerDefaultPos);
                break;
            case GamePlay.Home:
                StartCoroutine(timetoBack());
                player.GetComponent<Player>().setupPlayer(playerDefaultPos);
                break;
            case GamePlay.Replay:
                StartCoroutine(replay());
                player.GetComponent<Player>().setupPlayer(playerDefaultPos);
                break;
            case GamePlay.Lose:
                StartCoroutine(lose());
                break;
            case GamePlay.Home_2:
                StartCoroutine(timetoBack_2());
                player.GetComponent<Player>().setupPlayer(playerDefaultPos);
                break;
            default: break;
        }
    }
    public void Back()
    {
        mode = GamePlay.Home;
        status();
    }
    IEnumerator timetoBack()
    {
        GameObject pause_1 = GameObject.FindGameObjectWithTag("Pause_1");
        GameObject pause_2 = GameObject.FindGameObjectWithTag("Pause_2");
        Time.timeScale = 1;
        StartCoroutine(panelFadeIn(0.5f));
        pause_1.transform.DOScale(0, 0.2f);
        pause_2.transform.DOScale(0, 0.2f);
        // resetup barrier default
        // scale up
        // Tween fade = mainManager.transform.DOScale(0, 0.3f);
        // container.DOScale(0, 0.2f);
        defaultBarrier.transform.DOMoveY(barriderDefaultOut.y, 0.3f);
        yield return new WaitForSeconds(0.5f);

        _setupBarrier();
        // hide maingame and container
        Camera.main.transform.DOMoveY(0, 0.5f);
        mainManager.SetActive(false);
        mainManager.transform.localScale = scaleDefault;
        container.gameObject.SetActive(false);
        // container.localScale = scaleDefault;


        // active menu game
        StartCoroutine(panelFadeOut(0.2f));
        menuManager.SetActive(true);
        deleteContainer();
        menuManager.GetComponent<Animator>().Play("In");
    }

    void deleteContainer()
    {
        if (container.childCount > 0)
        {
            for (int i = 0; i < container.childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }
    }
    public void Play()
    {
        mode = GamePlay.PlayGame;
        status();
    }
    IEnumerator timetoPlay()
    {
        StartCoroutine(panelFadeIn(0.8f));
        menuManager.GetComponent<Animator>().Play("Out");
        yield return new WaitForSeconds(0.8f);
        menuManager.SetActive(false);
        StartCoroutine(panelFadeOut(0.3f));
        mainManager.SetActive(true);
        container.gameObject.SetActive(true);
        //player.GetComponent<Player>().tutorialIn();
        defaultBarrier.SetActive(true);
        defaultBarrier.transform.DOMoveY(barriderDefaultIn.y, 0.3f);
    }

    public void _replay()
    {
        Debug.Log("replay");
        mode = GamePlay.Replay;

        status();
    }
    IEnumerator replay()
    {
        StartCoroutine(panelFadeIn(0.5f));
        yield return new WaitForSeconds(0.5f);
        _moveCamera();
        loseManager.SetActive(false);
        StartCoroutine(panelFadeOut(0.3f));
        //
        mainManager.SetActive(true);
        container.gameObject.SetActive(true);
        // player.GetComponent<Player>().tutorialIn();
        defaultBarrier.SetActive(true);
        defaultBarrier.transform.DOMoveY(barriderDefaultIn.y, 0.3f);
    }

    public void _lose()
    {
        mode = GamePlay.Lose;
        status();
    }
    IEnumerator lose()
    {
        StartCoroutine(panelFadeIn(0.5f));
        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
        _moveCamera();
        mainManager.SetActive(false);
        _setupBarrier();
        deleteContainer();
        container.gameObject.SetActive(false);
        loseManager.GetComponent<LoseManager>().updateScore();
        StartCoroutine(panelFadeOut(0.3f));
        loseManager.SetActive(true);
    }

    public void _backFromLose()
    {
        mode = GamePlay.Home_2;
        status();
    }
    IEnumerator timetoBack_2()
    {
        StartCoroutine(panelFadeIn(0.5f));
        yield return new WaitForSeconds(0.5f);
        _moveCamera();
        loseManager.SetActive(false);
        StartCoroutine(panelFadeOut(0.3f));
        menuManager.SetActive(true);
        deleteContainer();
        menuManager.GetComponent<Animator>().Play("In");
    }
    void _setupBarrier()
    {
        defaultBarrier.GetComponent<Collider2D>().enabled = true;
        defaultBarrier.SetActive(false);
        defaultBarrier.transform.position = barriderDefaultOut;
    }

    void _activeMenu()
    {
        menuManager.SetActive(true);
        deleteContainer();
        menuManager.GetComponent<Animator>().Play("In");
    }
    void _moveCamera()
    {
        Camera.main.transform.DOMoveY(0, 0);
    }

    IEnumerator panelFadeIn(float durantion)
    {
        Transform _panel = panel.transform.GetChild(0);
        _panel.gameObject.SetActive(true);
        Tween fadeIn = _panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), durantion);
        yield return fadeIn.WaitForCompletion();
    }
    IEnumerator panelFadeIn(float durantion, float a)
    {
        Transform _panel = panel.transform.GetChild(0);
        _panel.gameObject.SetActive(true);
        Tween fadeIn = _panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, a), durantion);
        yield return fadeIn.WaitForCompletion();
    }
    IEnumerator panelFadeOut(float durantion)
    {
        Transform _panel = panel.transform.GetChild(0);
        Tween fadeOut = _panel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), durantion);
        yield return fadeOut.WaitForCompletion();
        _panel.gameObject.SetActive(false);
    }

    public void _Pause()
    {
        StartCoroutine(_pause(0.2f));
    }
    IEnumerator _pause(float durantion)
    {
        GameObject pause_1 = GameObject.FindGameObjectWithTag("Pause_1");
        GameObject pause_2 = GameObject.FindGameObjectWithTag("Pause_2");
        StartCoroutine(panelFadeIn(durantion, 160f/255));
        pause_1.transform.DOScale(1, durantion);
        pause_2.transform.DOScale(1, durantion);
        yield return new WaitForSeconds(durantion);
        Time.timeScale = 0;
    }
    public void _Continue()
    {
        StartCoroutine(_continue(0.2f));
    }
    IEnumerator _continue(float durantion)
    {
        Time.timeScale = 1;
        GameObject pause_1 = GameObject.FindGameObjectWithTag("Pause_1");
        GameObject pause_2 = GameObject.FindGameObjectWithTag("Pause_2");
        pause_1.transform.DOScale(0, durantion);
        pause_2.transform.DOScale(0, durantion);
        StartCoroutine(panelFadeOut(durantion));
        yield return new WaitForSeconds(durantion);
    }
    
}
