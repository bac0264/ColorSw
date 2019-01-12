using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class Player : MonoBehaviour
{

    public CoinManager coinManager;
    public Generator generator;
    public ScoreManager scoreManager;
    public float jumpForce = 10f;


    public Rigidbody2D rb;
    public SpriteRenderer sr;

    public string currentColor;

    public Color colorCyan;
    public Color colorYellow;
    public Color colorMagenta;
    public Color colorPink;

    public GameObject tutorial;

    const int addCoin = 1;
    const int I_distanceTutorial = -3;
    const int O_distanceTutorial = -6;
    const int addScore = 1;

    int score = 0;
    public int temp = 0;
    void Start()
    {
        SetRandomColor();
    }
    public void tutorialIn()
    {
        tutorial.SetActive(true);
        tutorial.transform.DOMoveY(I_distanceTutorial, 0f);
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
            temp++;
            if (temp == 1)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = true;
                tutorialOut();
            }
            rb.velocity = Vector2.up * jumpForce;
        }
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
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
        if (col.tag == "Check")
        {
            score += addScore;
            scoreManager.setScore(score);
            scoreManager.setHighScore(addScore);
            scoreManager.scoreDisplay();
            col.GetComponent<Rotator>().setCheck(true);
            Vector3 pos = transform.position;
            pos.y += generator.distance;
            generator.generateObject(col.gameObject, pos);
        }

    }

    void SetRandomColor()
    {
        int index = Random.Range(0, 4);

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
}
