using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Snap : MonoBehaviour
{
    public RectTransform panel; // to hold the scrollView
    public List<Button> btnn; // 
    public RectTransform center; // Center to compare the distance for each iten 
    public bool startSnap;
    public float[] distance; // All Item's center
    private bool dragging = false;
    public int bttnDistance;
    private int minButtonNum;
    private bool checkStart;
    // Use this for initialization
    public void _setupStart(int current)
    {
        checkStart = false;
        distance = new float[btnn.Count];
        // distance between  
        minButtonNum = current;
    }
    private void Update()
    {
        // set bttnDistance
        if (bttnDistance == 0 && btnn.Count >= 2)
        {
            bttnDistance = (int)Mathf.Abs(btnn[btnn.Count - 1].GetComponent<RectTransform>().anchoredPosition.x - btnn[btnn.Count - 2].GetComponent<RectTransform>().anchoredPosition.x);
            if (bttnDistance != 0)
                _initPos((minButtonNum) * (-bttnDistance));
        }
        if (checkStart)
        {
            for (int i = 0; i < btnn.Count; i++)
            {
                distance[i] = (int)Mathf.Abs(center.transform.position.x - btnn[i].transform.position.x);
            }
            float minDistance = Mathf.Min(distance);

            for (int a = 0; a < btnn.Count; a++)
            {
                if (minDistance == distance[a])
                {
                    minButtonNum = a;
                    break;
                }
            }
            if (!dragging)
            {
                LerpToImage(minButtonNum * (-bttnDistance));
            }
        }
    }

    public void LerpToImage(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, 0.1f);
        //  float newX = position;
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;
    }
    public void _initPos(int position)
    {
        float newX = position;
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;
        checkStart = true;
    }
    public void StartDrag()
    {
        dragging = true;
    }
    public void EndDrag()
    {
        dragging = false;
    }
    public int getMinButtonNum()
    {
        return minButtonNum;
    }
    public bool getDrag()
    {
        return dragging;
    }
}
