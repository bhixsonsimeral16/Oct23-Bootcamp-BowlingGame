using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] List<Pin> pins;

    bool isGameActive = false;
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void StartGame()
    {
        isGameActive = true;

        // Get the first throw!
        playerController.StartThrow();
    }

    void RestartGame()
    {
        Debug.Log("Restarting game");
        
        isGameActive = false;
        scoreManager.ResetScore();
        ResetAllPins();
        StartGame();
    }

    public void SetNextThrow()
    {
        Invoke("NextThrow", 3f);
    }

    void NextThrow()
    {
        if(scoreManager.currentFrame == 0)
        {
            Debug.Log("Game over!");
            Debug.Log($"Total score: {scoreManager.GetTotalScore()}");
        }
        else
        {
            Debug.Log($"Frame {scoreManager.currentFrame} - Throw {scoreManager.currentThrow}");
            scoreManager.SetFrameScore(CalculateFallenPins());
            Debug.Log($"Current Score: {scoreManager.GetTotalScore()}");

            playerController.StartThrow();
        }
    }

    public int CalculateFallenPins()
    {
        int fallenPins = 0;
        foreach (Pin pin in pins)
        {
            if (pin.isFallen && pin.gameObject.activeSelf)
            {
                fallenPins++;
                pin.gameObject.SetActive(false);
            }
        }

        Debug.Log("Fallen pins: " + fallenPins);
        return fallenPins;
    }

    public void ResetAllPins()
    {
        foreach (Pin pin in pins)
        {
            pin.ResetPin();
        }
    }
}
