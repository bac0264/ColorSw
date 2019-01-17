using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{

    public CoinManager coinManager;
    public Generator generator;
    public ScoreManager scoreManager;
    public float jumpForce = 10f;
    public List<Sprite> spriteList = new List<Sprite>();

    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public string currentColor;
    public int _currentColor = -1;
    public Color colorCyan;
    public Color colorYellow;
    public Color colorMagenta;
    public Color colorPink;

    public GameObject tutorial;
    public GameObject effect;
    const int addCoin = 1;
    const int I_distanceTutorial = -3;
    const int O_distanceTutorial = -6;
    const int addScore = 1;

    public int currentID = 0;
    public int score = 0;
    public int temp = 0;

    public object EventSystemManager { get; private set; }

    void Start()
    {
        SetRandomColor();
        _setupSprite();
    }

    void _setupSprite()
    {
        SaveLoad.instance.loadingID(ref currentID, "id");
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[currentID];
    }
    IEnumerator _out()
    {
        Tween t_Out = tutorial.transform.DOMoveY(O_distanceTutorial, 0.5f);
        yield return t_Out.WaitForCompletion();
        tutorial.SetActive(false);
    }
    public void tutorialOut()
    {
        StartCoroutine(_out());
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            else
            {
                temp++;
                if (temp == 1)
                {
                    gameObject.GetComponent<Collider2D>().isTrigger = true;
                    //tutorialOut();

                }
                rb.velocity = Vector2.up * jumpForce;
            }
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    return;
                }
                else
                {
                    temp++;
                    if (temp == 1)
                    {
                        gameObject.GetComponent<Collider2D>().isTrigger = true;
                        //tutorialOut();
                    }
                    rb.velocity = Vector2.up * jumpForce;
                }

                //  if (gameStatus == GameStatus.READY) gameStatus = GameStatus.PLAYING;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
            }
        }
        // else if (Input.GetMouseButtonUp(0))
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Coin")
        {
            coinManager.addCoin(addCoin);
            coinManager.UpdateCoin();
            coinManager.savingCoin();
            Destroy(col.gameObject);
        }
        if (col.tag == "ColorChanger")
        {
            SetRandomColor();
            Destroy(col.gameObject);
            return;
        }

        if (col.tag != currentColor && col.tag != "Coin" && col.tag != "Check")
        {
            Debug.Log("GAME OVER!");
            StartCoroutine(timetoLose());
        }
        if (col.tag == "Check")
        {
            col.GetComponent<CheckObject>().setCheck(true);
            Vector3 pos = transform.position;
            pos.y += generator.distance;
            generator.generateObject(col.gameObject, pos);
        }
        if(col.tag == "CheckBarrier")
        {
            score += addScore;
            scoreManager.setScore(score);
            scoreManager.setHighScore(scoreManager.getScore());
            scoreManager.scoreDisplay();
        }

    }
    IEnumerator timetoLose()
    {
        Color color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g
            , gameObject.GetComponent<SpriteRenderer>().color.b, gameObject.GetComponent<SpriteRenderer>().color.a);
        rb.bodyType = RigidbodyType2D.Static;
        gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(color.r,color.g,color.b, 0),0);
        Destroy(Instantiate(effect, gameObject.transform.position, Quaternion.identity),1.5f);
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<SpriteRenderer>().DOColor(new Color(color.r, color.g, color.b, 1), 0);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Process.instance._lose();
        gameObject.SetActive(false);
    }
    void SetRandomColor()
    {
        int index = Random.Range(0, 4);
        if (_currentColor == -1)
        {
            _currentColor = index;
        }
        else
        {
            if (_currentColor == index)
            {
                while (_currentColor == index)
                {
                    index = Random.Range(0, 4);
                }
            }
        }
        _currentColor = index;
        switch (index)
        {
            case 0:
                currentColor = "Cyan";
                sr.color = colorCyan;
                break;
            case 1:
                currentColor = "Yellow";
                sr.color = colorYellow;
                break;
            case 2:
                currentColor = "Magenta";
                sr.color = colorMagenta;
                break;
            case 3:
                currentColor = "Pink";
                sr.color = colorPink;
                break;
        }
    }

    public void setupPlayer(Vector3 pos)
    {
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        temp = 0;
        score = 0;
        scoreManager.setScore(score);
        transform.position = pos;
        scoreManager.scoreDisplay();
    }
}
