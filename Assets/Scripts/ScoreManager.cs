using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int totalScore = 0;
    bool isSpare = false;
    bool isStrike = false;
    public int[] frameScores = new int[10];
    List<int> frameScoreList = new List<int>();

    public int currentThrow { get; private set; } = 1;
    public int currentFrame { get; private set; } = 1;

    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        ResetScore();
    }

    public void SetFrameScore(int score)
    {
        uiManager.SetFrameValue(currentFrame, currentThrow, score);

        // Ball 1
        if (currentThrow == 1)
        {
            frameScores[currentFrame - 1] += score;
            frameScoreList.Add(score);

            // if the previous frame was a spare, add the score to the previous frame
            if (isSpare)
            {
                frameScores[currentFrame - 2] += score;
                isSpare = false;
            }
            else
            {
                // If the previous frame was a strike, add the score to the previous frame
                if (frameScoreList.Count >= 2 && frameScoreList[frameScoreList.Count - 2] == 10)
                {
                    frameScores[currentFrame - 2] += frameScoreList[currentFrame - 1];
                }

                // If the frame before the previous throw was a strike, add the current throw score to 2 frames ago
                if (frameScoreList.Count >= 3 && frameScoreList[frameScoreList.Count - 3] == 10)
                {
                    frameScores[currentFrame - 3] += frameScoreList[currentFrame - 1];
                }
            }

            if (score == 10)
            {
                Debug.Log("Strike!");
                // Last frame, if the first throw is a strike, go to next throw
                if (currentFrame == 10)
                {
                    currentThrow++;
                }

                // If the first throw is a strike, go to next frame
                else
                {
                    isStrike = true;

                    currentFrame++;
                    gameManager.soundManager.PlaySound("strike");
                    uiManager.ShowStrikeMessage();
                }

                gameManager.ResetAllPins();
            }

            // If the score is less than 10 on first throw, go to next throw
            else
            {
                currentThrow++;
            }

            return;
        }

        // Ball 2
        else if (currentThrow == 2)
        {
            frameScores[currentFrame - 1] += score;
            frameScoreList.Add(score);

            // If the frame before the previous throw was a strike, add the current throw score to 2 frames ago
            if (frameScoreList.Count >= 3 && frameScoreList[frameScoreList.Count - 3] == 10)
            {
                frameScores[currentFrame - 3] += frameScoreList[currentFrame - 1];
            }

            // If the score is 10 on second throw, go to next frame
            if (frameScores[currentFrame - 1] == 10)
            {
                // Last frame, if the second throw is a spare go to next throw
                if (currentFrame == 10)
                {
                    currentThrow++;
                }

                else
                {
                    isSpare = true;
                    currentFrame++;
                    currentThrow = 1;
                    gameManager.soundManager.PlaySound("spare");
                    uiManager.ShowSpareMessage();
                }
            }

            // Final frame, 2 strikes in a row
            else if (frameScores[currentFrame - 1] == 20 && currentFrame == 10)
            {
                currentThrow++;
            }
            
            else
            {
                if(currentFrame == 10)
                {
                    // If the score is less than 10 on second throw, end game
                    currentThrow = 0;
                    currentFrame = 0;

                }
                else
                {
                    currentFrame++;
                    currentThrow = 1;
                }
            }
            gameManager.ResetAllPins();
            return;
        }

        // Ball 3
        else if (currentThrow == 3 && currentFrame == 10)
        {
            frameScores[currentFrame - 1] += score;
            currentThrow = 0;
            currentFrame = 0;
            gameManager.ResetAllPins();
            return;
        }
    }

    public int GetTotalScore()
    {
        totalScore = 0;
        foreach (int score in frameScores)
        {
            totalScore += score;
        }
        return totalScore;
    }

    public void ResetScore()
    {
        totalScore = 0;
        isSpare = false;
        isStrike = false;
        currentThrow = 1;
        currentFrame = 1;
        frameScores = new int[10];
        frameScoreList = new List<int>();
    }

    public int[] GetFrameSores()
    {
        return frameScores;
    }
}
