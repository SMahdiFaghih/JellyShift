using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController playerController;

    public float restartDelay;

    private void Start()
    {
        gameManager = GameManager.instance;
        playerController = GetComponent<PlayerController>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.transform.parent != null && collisionInfo.collider.transform.parent.tag == "Obstacle")
        {
            playerController.enabled = false;
            Invoke("Restart", restartDelay);
        }
        else if (collisionInfo.collider.transform.tag == "Diamond")
        {
            gameManager.CollectDiamond(gameObject);
            Destroy(collisionInfo.collider.gameObject);
        }
        else if (collisionInfo.collider.tag == "Ground")
        {
            playerController.isJumping = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag  == "Falling")
        {
            playerController.enabled = false;
            Invoke("Restart", restartDelay);
        }
        else if (collider.tag == "LevelComplete")
        {
            gameManager.LevelComplete();
        }
        else if (collider.tag == "TurnLeft")
        {
            gameManager.TurnLeft(collider);
        }
        else if (collider.tag == "TurnRight")
        {
            gameManager.TurnRight(collider);
        }
    }

    private void Restart()
    {
        gameManager.Restart();
    }
}
