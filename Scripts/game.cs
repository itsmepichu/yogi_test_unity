using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour {


    [SerializeField]
    private int 
        playerScore = 0;

    [SerializeField]
    private GameObject 
        worldController,
        canvasBG,
        canvasPlayButton,
        canvasCurrentScore,
        canvasHighScore;

    private Text
        canvasHighScoreTextComponent,
        canvasCurrentScoreTextComponent;


    // Use this for initialization
    void Start () {
        canvasBG = GameObject.Find("_UI_BG");
        canvasPlayButton = GameObject.Find("_UI_BTN_PLAY");
        canvasCurrentScore = GameObject.Find("_UI_CURRENT_SCORE");
        canvasHighScore = GameObject.Find("_UI_HIGH_SCORE");
        canvasHighScoreTextComponent = canvasHighScore.GetComponent<Text>();
        canvasCurrentScoreTextComponent = canvasCurrentScore.GetComponent<Text>();

        canvasCurrentScore.SetActive(false);
        canvasHighScoreTextComponent.text = "HIGH SCORE: " + getHighScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Start Game by Initiating the world controller
    /// </summary>
    public void startGame()
    {
        playerScore = 0;

        canvasBG.SetActive(false);
        canvasPlayButton.SetActive(false);
        canvasCurrentScore.SetActive(false);
        canvasHighScore.SetActive(false);
        Instantiate(worldController, Vector3.zero, Quaternion.identity);
    }

    public void gameOver()
    {
        // Destroy GameObjects
        GameObject[] terrainObjects = GameObject.FindGameObjectsWithTag("terrain");
        foreach(GameObject obj in terrainObjects)
            Destroy(obj);

        GameObject[] obstaclesObjects = GameObject.FindGameObjectsWithTag("obstacle");
        foreach (GameObject obj in obstaclesObjects)
            Destroy(obj);

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in playerObjects)
            Destroy(obj);///

        GameObject[] worldControllers = GameObject.FindGameObjectsWithTag("world_controller");
        foreach (GameObject obj in worldControllers)
            Destroy(obj);

        // Handle Player Score
        int highScore = getHighScore();
        string currentScoreText = "CURRENT SCORE: "+playerScore;
        string highScoreText = "HIGH SCORE: "+highScore;
        if(playerScore > highScore)
        {
            currentScoreText = "NEW HIGH SCORE: " + playerScore;
            highScoreText = "PREVIOUS HIGH SCORE: " + highScore;
            setHighScore(playerScore);
        }

        canvasBG.SetActive(true);

        canvasPlayButton.GetComponentInChildren<Text>().text = "Play Again";
        canvasPlayButton.SetActive(true);

        canvasHighScore.SetActive(true);
        canvasHighScoreTextComponent.text = highScoreText;

        canvasCurrentScoreTextComponent.text = currentScoreText;
        canvasCurrentScore.SetActive(true);

    }

    public void updateScore(int score)
    {
        playerScore += score;
    }

    public int getHighScore()
    {
        if(PlayerPrefs.HasKey("_endless_runner_test_highscore")) {
            return PlayerPrefs.GetInt("_endless_runner_test_highscore");
        } else
        {
            return 0;
        }
    }

    public void setHighScore(int highScore)
    {
        PlayerPrefs.SetInt("_endless_runner_test_highscore", highScore);
    }
}
