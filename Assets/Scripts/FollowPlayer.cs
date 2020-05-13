using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public float forwardOffSet;
    public float rightOffSet;
    public float upOffSet;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + player.forward * forwardOffSet + player.right * rightOffSet + player.up * upOffSet;
        transform.LookAt(player);
    }
}
