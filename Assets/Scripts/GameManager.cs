using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] List<Pin> pins;
    [SerializeField] UIManager uiManager;
    [SerializeField] Camera mainCamera, closeUpCamera;

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

        mainCamera.enabled = true;
        closeUpCamera.enabled = false;

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
        Invoke(nameof(NextThrow), 2f);
    }

    void NextThrow()
    {

        int fallenPins = CalculateFallenPins();
        scoreManager.SetFrameScore(fallenPins);

        if(scoreManager.currentFrame == 0)
        {
            Debug.Log("Game over!");
            Debug.Log($"Total score: {scoreManager.GetTotalScore()}");
            uiManager.ShowGameOverUI(scoreManager.GetTotalScore());
        }

        int frameTotal = 0;
        for (int i = 0; i < scoreManager.currentFrame - 1; i++)
        {
            frameTotal += scoreManager.frameScores[i];
            uiManager.SetFrameTotal(i, frameTotal);
        }

        SwitchCamera();
        playerController.StartThrow();
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

    public void SwitchCamera()
    {
        mainCamera.enabled = !mainCamera.enabled;
        closeUpCamera.enabled = !closeUpCamera.enabled;
    }
}
