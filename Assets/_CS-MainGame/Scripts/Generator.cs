using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public List<GameObject> listGameObject = new List<GameObject>();
    public const int distance = 10;
    private void Start()
    {
        StartCoroutine(generate());
    }

    IEnumerator generate()
    {

        yield return null;
    }
}
