using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Process : MonoBehaviour
{
    Vector3 playerDefaultPos = new Vector3(0, -1.1f, 0);
    Vector3 scaleDefault = new Vector3(1, 1, 1);
    public GameObject menuManager;
    public GameObject mainManager;
    public Transform container;
    public GameObject player;
    public void Back()
    {
        StartCoroutine(timetoIn());
    }
    IEnumerator timetoIn()
    {
        // scale up
        Tween fade = mainManager.transform.DOScale(0, 0.5f);
       // container.DOScale(0, 0.2f);
        Camera.main.transform.DOMoveY(0, 0.5f);
        yield return fade.WaitForCompletion();

        // hide maingame and container
        mainManager.SetActive(false);
        mainManager.transform.localScale = scaleDefault;
        container.gameObject.SetActive(false);
       // container.localScale = scaleDefault;
        player.transform.position = playerDefaultPos;

        // active menu game
        menuManager.SetActive(true);
        if (container.childCount > 0)
        {
            for (int i = 0; i < container.childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }
        menuManager.GetComponent<Animator>().Play("In");
        player.GetComponent<Player>().tutorialIn();
        player.GetComponent<Collider2D>().isTrigger = false;
        player.GetComponent<Player>().temp = 0;
    }
    public void Play()
    {
        StartCoroutine(timetoOut());
    }
    IEnumerator timetoOut()
    {
        menuManager.GetComponent<Animator>().Play("Out");
        yield return new WaitForSeconds(0.7f);
        menuManager.SetActive(false);
        mainManager.SetActive(true);
        container.gameObject.SetActive(true);
        player.GetComponent<Player>().tutorialIn();
    }
}
