using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameplayManager : MonoBehaviour
{
    public  GameObject loseMenu;
    public  GameObject winMenu;
    public  GameObject overlay;
    public  GameObject startMenu;
    public TMP_Text timeDisplay;
    public TMP_Text countdownToGame;
    public Pathfollower teacher;
    public int countdownTimer = 3;
    public int score = 0;
    public int maxScore = 8;
    public int secondsForGame = 20;
    private int secondsLeft;
    private float timeFromBeginning;
    bool countdown = true;
    // Start is called before the first frame update


    private void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (countdown)
        {
            Countdown();
        }
        else
        {
            timeFromBeginning += Time.deltaTime;
            if(timeFromBeginning > 1)
            {
                secondsLeft--;
                timeDisplay.text = secondsLeft.ToString();
                if (secondsLeft <= 0)
                {
                    Lose();
                }
                timeFromBeginning = 0;
            }
        }
    }


    public void Lose()
    {
        Debug.Log("You lose!");
        loseMenu.SetActive(true);
        overlay.SetActive(false);

    }


    public void Score(GameObject ball)
    {
        Destroy(ball);
        score++;
        if(score >= maxScore)
        {
            Win();
        }
    }

    private void Win()
    {
        Debug.Log("You win!");
        winMenu.SetActive(true);
        overlay.SetActive(false);
        
    }

    public void StartAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        overlay.SetActive(true);
        startMenu.SetActive(false);
        countdownToGame.gameObject.SetActive(true);
        countdown = true;

    }

    public void Countdown()
    {

        timeFromBeginning += Time.deltaTime;
        if (timeFromBeginning > 1)
        {
            countdownTimer--;
            timeFromBeginning = 0;
        }
        if(countdownTimer <= 0)
        {
            countdown = false;
            secondsLeft = secondsForGame;
            timeDisplay.text = secondsLeft.ToString();
            timeFromBeginning = 0;
            countdownToGame.gameObject.SetActive(false);
            teacher.StartMoving();
        }
        countdownToGame.text = countdownTimer.ToString();

    }


}
