using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;
    public Canvas PauseMenu;
    public GameObject player;
    public GameObject levelCompletedUI;
    public Text levelNumber;
    public float turnTime = 1f;
    private PlayerController playerController;

    private readonly int[] turnLeftXState = new int[] { -2, 2, 2, -2 };
    private readonly int[] turnLeftZState = new int[] { 2, 2, -2, -2 };
    private readonly int[] turnRightXState = new int[] { 2, 2, -2, -2 };
    private readonly int[] turnRightZState = new int[] { 2, -2, -2, 2 };

    public Text collectedDiamondsUI;

    void Awake()
    {
        instance = this;
        levelNumber.text = SceneManager.GetActiveScene().name;
        print(levelNumber.text);
        if(SceneManager.GetActiveScene().name == "Level 1")
        {
            PlayerPrefs.SetString("Collected Diamonds", "0");
            collectedDiamondsUI.text = "0";
        }   
    }

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        collectedDiamondsUI.text = PlayerPrefs.GetString("Collected Diamonds");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                PauseMenu.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                PauseMenu.gameObject.SetActive(false);
            }
        }
    }

    public void TurnLeft(Collider collider)
    {
        playerController.rb.isKinematic = true;
        playerController.enabled = false;
        int state = (int)player.transform.eulerAngles.y / 90;
        LeanTween.rotateY(player, player.transform.eulerAngles.y - 90, turnTime);
        LeanTween.moveX(player, collider.transform.position.x + turnLeftXState[state], turnTime);
        LeanTween.moveZ(player, collider.transform.position.z + turnLeftZState[state], turnTime);
        Invoke("SetRigidbodySettingsAfterTurn", 1);
        print("Turn left");
    }

    public void TurnRight(Collider collider)
    {
        playerController.rb.isKinematic = true;
        playerController.enabled = false;
        int state = (int)player.transform.eulerAngles.y / 90;
        LeanTween.rotateY(player, player.transform.eulerAngles.y + 90, turnTime);
        LeanTween.moveX(player, collider.transform.position.x + turnRightXState[state], turnTime);
        LeanTween.moveZ(player, collider.transform.position.z + turnRightZState[state], turnTime);
        Invoke("SetRigidbodySettingsAfterTurn", 1);
        print("Turn Right");
    }

    public void SetRigidbodySettingsAfterTurn()
    {
        playerController.enabled = true;
        playerController.rb.isKinematic = false;
    }

    public void LevelComplete(GameObject levelCompleted)
    {
        AudioSource levelCompletedSound = levelCompleted.GetComponent<AudioSource>();
        levelCompletedSound.Play();

        playerController.enabled = false;
        PlayerPrefs.SetString("Collected Diamonds", collectedDiamondsUI.text);
        levelCompletedUI.SetActive(true);
    }

    public void CollectDiamond(GameObject player)
    {
        AudioSource collectDiamondSound = player.GetComponent<AudioSource>();
        collectDiamondSound.Play();

        int currentDiamonds = Convert.ToInt32(collectedDiamondsUI.text);
        currentDiamonds ++;
        collectedDiamondsUI.text = currentDiamonds.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
