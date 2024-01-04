using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameplayManager : MonoBehaviour
{
    public  GameObject loseMenu;
    public TMP_Text timeDisplay;
    public int score = 0;
    public int maxScore = 8;
    public int secondsForGame = 20;
    private int secondsLeft;
    private float timeFromBeginning;
    // Start is called before the first frame update


    private void Start()
    {
        secondsLeft = secondsForGame;
        timeDisplay.text = secondsLeft.ToString();
        timeFromBeginning = 0;
    }


    // Update is called once per frame
    void Update()
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


    public void Lose()
    {
        Debug.Log("You lose!");
        loseMenu.SetActive(true);
        // wait for 3 seconds
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }





}
