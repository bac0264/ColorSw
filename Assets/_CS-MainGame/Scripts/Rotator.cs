using UnityEngine;

public class Rotator : MonoBehaviour {
	//public float speed = 100f;

    // Update is called once per frame
    public Vector3 spinStrength = new Vector3(0, 0, 1);
    public bool active = false;

    void Update()
    {
        if (active)
        {
            transform.Rotate(spinStrength);
        }
    }
}
