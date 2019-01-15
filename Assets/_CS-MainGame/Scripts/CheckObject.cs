using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObject : MonoBehaviour
{
    private bool check = false;
    public void setCheck(bool _check)
    {
        check = _check;
    }
    public bool getCheck()
    {
        return check;
    }
}
