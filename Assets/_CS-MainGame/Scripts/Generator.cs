using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public List<GameObject> listGameObject = new List<GameObject>();
    public const int distance = 10;

    IEnumerator generate(GameObject obj, Vector3 pos, GameObject pref)
    {
        if (obj.GetComponent<Rotator>().getCheck())
        {
            Instantiate(pref, pos, Quaternion.identity);
        }
        yield return null;
    }


}
