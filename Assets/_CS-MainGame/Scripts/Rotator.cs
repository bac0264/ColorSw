using UnityEngine;

public class Rotator : MonoBehaviour {
    private bool check = false;
	public float speed = 100f;
    private void Start()
    {   
    }
    // Update is called once per frame
    void Update () {
		transform.Rotate(0f, 0f, speed * Time.deltaTime);
	}
    public void setCheck(bool _check)
    {
        check = _check;
    }
    public bool getCheck()
    {
        return check;
    }
}
