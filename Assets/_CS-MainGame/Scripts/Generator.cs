using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public List<GameObject> listGameObject = new List<GameObject>();
    public GameObject coin;
    public GameObject changeColor;
    public Transform container;
    public float distance = 10; // distance between player and pref to generate
    public float distanceCC = 4; // distance between changeColor and pref to generate

    // function call generate
    public void generateObject(GameObject obj, Vector3 pos)
    {
        GameObject pref = _getRandomGameObject();
        StartCoroutine(generate(obj, pos, pref));
    }

    // get object is randomed
    public GameObject _getRandomGameObject()
    {
        int rd = Random.Range(0, listGameObject.Count);
        return listGameObject[rd];
    }
    // generate barriers with coin and color changer
    IEnumerator generate(GameObject obj, Vector3 pos, GameObject pref)
    {
        if (obj.GetComponent<Rotator>() != null)
        {
            if (obj.GetComponent<Rotator>().getCheck())
            {
                obj.GetComponent<Collider2D>().enabled = false;
                GameObject _obj = Instantiate(pref, pos, Quaternion.identity);
                _obj.transform.SetParent(container);
                if (randomCoin())
                {
                    GameObject _coin = Instantiate(coin, _obj.transform.position, Quaternion.identity);
                    _coin.transform.SetParent(container);
                }
                if (randomChangeColor())
                {
                    Vector3 _pos = _obj.transform.position;
                    _pos.y -= distanceCC;
                    GameObject _changeColor = Instantiate(changeColor, _pos, Quaternion.identity);
                    _changeColor.transform.SetParent(container);
                }
            }
        }
        yield return null;
    }
    // rate to generate coin 1/4
    bool randomCoin()
    {
        int random = Random.Range(0, 4);
        if (random == 3) return true;
        return false;
    }
    // rate to gernate color changer 1/3
    bool randomChangeColor()
    {
        int random = Random.Range(0, 3);
        if (random == 2) return true;
        return false;
    }

}
