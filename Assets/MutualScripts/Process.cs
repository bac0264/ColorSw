using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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

    public GamePlay mode;
    

    public enum GamePlay{
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
        // resetup barrier default
        defaultBarrier.transform.DOMoveY(barriderDefaultOut.y, 0.3f);
        // scale up
        Tween fade = mainManager.transform.DOScale(0, 0.3f);
       // container.DOScale(0, 0.2f);
        Camera.main.transform.DOMoveY(0, 0.5f);

        yield return fade.WaitForCompletion();

        _setupBarrier();
        // hide maingame and container
        mainManager.SetActive(false);
        mainManager.transform.localScale = scaleDefault;
        container.gameObject.SetActive(false);
        // container.localScale = scaleDefault;


        // active menu game
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
        menuManager.GetComponent<Animator>().Play("Out");
        yield return new WaitForSeconds(0.9f);
        menuManager.SetActive(false);
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
        _moveCamera();
        loseManager.SetActive(false);
        yield return new WaitForSeconds(0.1f);

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
        _moveCamera();
        mainManager.SetActive(false);
        _setupBarrier();
        deleteContainer();
        yield return new WaitForSeconds(0.1f);
        container.gameObject.SetActive(false);
        loseManager.GetComponent<LoseManager>().updateScore();  
        loseManager.SetActive(true);
    }

    public void _backFromLose()
    {
        mode = GamePlay.Home_2;
        status();
    }
    IEnumerator timetoBack_2()
    {
        _moveCamera();
        loseManager.SetActive(false);
        menuManager.SetActive(true);
        deleteContainer();
        menuManager.GetComponent<Animator>().Play("In");
        yield return null;
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
}
