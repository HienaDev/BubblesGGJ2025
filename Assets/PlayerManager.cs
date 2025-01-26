using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public string[] playerControls;

    [SerializeField] private int numberOfPlayers = 8;

    private TestButtons buttonsScript;

    [SerializeField] private Transform[] playerSpawnPositions;

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private Color player1;
    [SerializeField] private Color player2;
    [SerializeField] private Color player3;
    [SerializeField] private Color player4;
    [SerializeField] private Color player5;
    [SerializeField] private Color player6;
    [SerializeField] private Color player7;
    [SerializeField] private Color player8;
    private List<Color> playerColors;

    [SerializeField] private RawImage player1RawImage;
    [SerializeField] private RawImage player2RawImage;
    [SerializeField] private RawImage player3RawImage;
    [SerializeField] private RawImage player4RawImage;
    [SerializeField] private RawImage player5RawImage;
    [SerializeField] private RawImage player6RawImage;
    [SerializeField] private RawImage player7RawImage;
    [SerializeField] private RawImage player8RawImage;
    private List<RawImage> playerRawImages;


    [SerializeField] private TextMeshProUGUI player1TextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI player2TextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI player3TextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI player4TextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI player5TextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI player6TextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI player7TextMeshProUGUI;
    [SerializeField] private TextMeshProUGUI player8TextMeshProUGUI;
    public List<TextMeshProUGUI> playerTextMeshProUGUIs;

    private int player1Score = 0;
    private int player2Score = 0;
    private int player3Score = 0;
    private int player4Score = 0;
    private int player5Score = 0;
    private int player6Score = 0;
    private int player7Score = 0;
    private int player8Score = 0;
    public List<int> playerScores;

    [SerializeField] private int totalScore = 50;

    [SerializeField] private GameObject playerSelectScreen;
    [SerializeField] private GameObject playerScore;

    public int nrOfPlayers = 0;

    [Serializable]
    public struct LittleGuy
    {
        public GameObject model;
        public Texture modelPreview;
    }
    [SerializeField] private LittleGuy[] littleGuys;

    private bool gameStarted = false;

    void ShuffleArray<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1); // Get a random index from 0 to i (inclusive)

            // Swap the elements at i and randomIndex
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    public void AddToPlayerScore(int number, int score)
    {
        playerScores[number] += score;
        playerTextMeshProUGUIs[number].text = playerScores[number].ToString();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        buttonsScript = FindAnyObjectByType<TestButtons>();

        playerControls = new string[8];

        playerColors = new List<Color>();

        playerColors.Add(player1);
        playerColors.Add(player2);
        playerColors.Add(player3);
        playerColors.Add(player4);
        playerColors.Add(player5);
        playerColors.Add(player6);
        playerColors.Add(player7);
        playerColors.Add(player8);

        playerRawImages = new List<RawImage>();

        playerRawImages.Add(player1RawImage);
        playerRawImages.Add(player2RawImage);
        playerRawImages.Add(player3RawImage);
        playerRawImages.Add(player4RawImage);
        playerRawImages.Add(player5RawImage);
        playerRawImages.Add(player6RawImage);
        playerRawImages.Add(player7RawImage);
        playerRawImages.Add(player8RawImage);


        playerTextMeshProUGUIs = new List<TextMeshProUGUI>();

        playerTextMeshProUGUIs.Add(player1TextMeshProUGUI);
        playerTextMeshProUGUIs.Add(player2TextMeshProUGUI);
        playerTextMeshProUGUIs.Add(player3TextMeshProUGUI);
        playerTextMeshProUGUIs.Add(player4TextMeshProUGUI);
        playerTextMeshProUGUIs.Add(player5TextMeshProUGUI);
        playerTextMeshProUGUIs.Add(player6TextMeshProUGUI);
        playerTextMeshProUGUIs.Add(player7TextMeshProUGUI);
        playerTextMeshProUGUIs.Add(player8TextMeshProUGUI);

        playerScores = new List<int>();

        playerScores.Add(player1Score);
        playerScores.Add(player2Score);
        playerScores.Add(player3Score);
        playerScores.Add(player4Score);
        playerScores.Add(player5Score);
        playerScores.Add(player6Score);
        playerScores.Add(player7Score);
        playerScores.Add(player8Score);

        playerControls[0] = null;
        playerControls[1] = null;
        playerControls[2] = null;
        playerControls[3] = null;
        playerControls[4] = null;
        playerControls[5] = null;
        playerControls[6] = null;
        playerControls[7] = null;

        ShuffleArray(littleGuys);

    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            if (buttonsScript.keyboard1Pressed)
            {
                int player = CheckAvailablePlayer("keyboard1");

                if (player != -1)
                {
                    playerTextMeshProUGUIs[player].text = totalScore.ToString();
                    playerRawImages[player].texture = littleGuys[player].modelPreview;
                    playerControls[player] = "keyboard1";
                }
            }

            if (buttonsScript.keyboard2Pressed)
            {
                int player = CheckAvailablePlayer("keyboard2");

                if (player != -1)
                {
                    playerTextMeshProUGUIs[player].text = totalScore.ToString();
                    playerRawImages[player].texture = littleGuys[player].modelPreview;
                    playerControls[player] = "keyboard2";
                }
            }

            if (buttonsScript.keyboard3Pressed)
            {
                int player = CheckAvailablePlayer("keyboard3");

                if (player != -1)
                {
                    playerTextMeshProUGUIs[player].text = totalScore.ToString();
                    playerRawImages[player].texture = littleGuys[player].modelPreview;
                    playerControls[player] = "keyboard3";
                }
            }

            if (buttonsScript.controllerSouthPressed)
            {
                int player = CheckAvailablePlayer("controller");

                if (player != -1)
                {
                    playerTextMeshProUGUIs[player].text = totalScore.ToString();
                    playerRawImages[player].texture = littleGuys[player].modelPreview;
                    playerControls[player] = "controller";
                }
            }

            if (buttonsScript.redBuzz1Pressed)
            {
                int player = CheckAvailablePlayer("Buzz1");

                if (player != -1)
                {
                    playerTextMeshProUGUIs[player].text = totalScore.ToString();
                    playerRawImages[player].texture = littleGuys[player].modelPreview;
                    playerControls[player] = "Buzz1";
                }
            }

            if (buttonsScript.redBuzz2Pressed)
            {
                int player = CheckAvailablePlayer("Buzz2");

                if (player != -1)
                {
                    playerTextMeshProUGUIs[player].text = totalScore.ToString();
                    playerRawImages[player].texture = littleGuys[player].modelPreview;
                    playerControls[player] = "Buzz2";
                }
            }

            if (buttonsScript.redBuzz3Pressed)
            {
                int player = CheckAvailablePlayer("Buzz3");

                if (player != -1)
                {
                    playerTextMeshProUGUIs[player].text = totalScore.ToString();
                    playerRawImages[player].texture = littleGuys[player].modelPreview;
                    playerControls[player] = "Buzz3";
                }
            }

            if (buttonsScript.redBuzz4Pressed)
            {
                int player = CheckAvailablePlayer("Buzz4");

                if (player != -1)
                {
                    playerTextMeshProUGUIs[player].text = totalScore.ToString();
                    playerRawImages[player].texture = littleGuys[player].modelPreview;
                    playerControls[player] = "Buzz4";
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                StartGame();
            }
        }
        else
        {
            if(nrOfPlayers == 1 || nrOfPlayers == 0)
            {
                
            }
        }

   
    }

    public IEnumerator GameOverSequence()
    {
        gameStarted = false;
        yield return null;
    }


    public void StartGame()
    {
 
        int playerCount = 0;

        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (playerControls[i] != null)
            {

                playerCount++;

                GameObject bubbleMovementTemp = Instantiate(playerPrefab);
                bubbleMovementTemp.transform.position = playerSpawnPositions[i].transform.position;


                bubbleMovementTemp.GetComponent<BubbleTouchingLogic>().monster = littleGuys[i].model;


                BubbleMovement bubbleMovementScript = bubbleMovementTemp.GetComponent<BubbleMovement>();
                bubbleMovementScript.AddPlayer(i);
                bubbleMovementScript.outline.OutlineColor = playerColors[i];

                AddToPlayerScore(i, 50);

                bubbleMovementScript.score = totalScore;
                bubbleMovementScript.uiScore = playerTextMeshProUGUIs[i];

                // Use switch to activate the appropriate control
                switch (playerControls[i])
                {
                    case "keyboard1":
                        bubbleMovementScript.keyboard1 = true;
                        break;
                    case "keyboard2":
                        bubbleMovementScript.keyboard2 = true;
                        break;
                    case "keyboard3":
                        bubbleMovementScript.keyboard3 = true;
                        break;
                    case "controller":
                        bubbleMovementScript.controller1 = true;
                        break;
                    case "Buzz1":
                        bubbleMovementScript.buzz1 = true;
                        break;
                    case "Buzz2":
                        bubbleMovementScript.buzz2 = true;
                        break;
                    case "Buzz3":
                        bubbleMovementScript.buzz3 = true;
                        break;
                    case "Buzz4":
                        bubbleMovementScript.buzz4 = true;
                        break;
                    default:

                        break;
                }

                bubbleMovementScript.AddPlayer(i);
            }
        }

        if(playerCount > 0)
        {
            gameStarted = true;

            nrOfPlayers = playerCount;

            playerSelectScreen.SetActive(false);
            playerScore.SetActive(true);
        }

    }

    private int CheckAvailablePlayer(string player)
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (playerControls[i] == player)
                return -1;
        }

        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (playerControls[i] == null)
                return i;
        }

        return -1;
    }
}
