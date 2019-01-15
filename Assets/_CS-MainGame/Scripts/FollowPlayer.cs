using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    float widthScreen = 6;
    public Transform player;
    private void Start()
    {
        Camera.main.orthographicSize = widthScreen / Screen.width * Screen.height / 2;
    }
    void Update ()
	{
		if (player.position.y > transform.position.y)
		{
			transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
		}
	}

}
