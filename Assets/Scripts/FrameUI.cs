using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameUI : MonoBehaviour
{
    [SerializeField] TMP_Text txtFrame, txtThrow1, txtThrow2, txtThrow3, txtTotal;
    [SerializeField] bool isLastFrame;
    int frameScore = 0;

    public void UpdateScore(int throwNumber, int score)
    {
        frameScore += score;
        if (!isLastFrame)
        {
            if (throwNumber == 1)
            {
                if (frameScore == 10)
                {
                    txtThrow1.text = "X";
                    txtThrow2.text = "";
                }
                else
                {
                    txtThrow1.text = score.ToString();
                }
            }
            else if (throwNumber == 2)
            {
                if (frameScore == 10)
                {
                    txtThrow2.text = "/";
                }
                else
                {
                    txtThrow2.text = score.ToString();
                    txtFrame.text = frameScore.ToString();
                }
            }
        }

        // Final Frame
        else
        {
            if (throwNumber == 1)
            {
                if (frameScore == 10)
                {
                    txtThrow1.text = "X";
                }
                else
                {
                    txtThrow1.text = score.ToString();
                }
            }
            else if (throwNumber == 2)
            {
                if (frameScore == 20)
                {
                    txtThrow2.text = "X";
                }
                else if (frameScore == 10)
                {
                    txtThrow2.text = "/";
                }
                else
                {
                    frameScore += score;
                    txtThrow2.text = score.ToString();
                }
            }
            else if (throwNumber == 3)
            {
                // Three strikes in a row
                if (frameScore == 30)
                {
                    txtThrow3.text = "X";
                }
                else if (frameScore == 20)
                {
                    // First throw was a strike, this makes the third throw a spare
                    if (txtThrow1.text == "X")
                    {
                        txtThrow3.text = "/";
                    }
                    // First throw was not a strike, which means the second throw was a spare. 
                    // This makes the third throw a strike
                    else
                    {
                        txtThrow3.text = "X";
                    }
                }
                // Total score not a multiple of 10, so no spare or strike
                else
                {
                    txtThrow3.text = score.ToString();
                }
                txtFrame.text = frameScore.ToString();
            }
        }
    }

    public void UpdateTotal(int total)
    {
        txtTotal.text = total.ToString();
    }

    public void SetFrameNumber(int frameNumber)
    {
        txtFrame.text = frameNumber.ToString();
        txtThrow1.text = "";
        txtThrow2.text = "";
        txtTotal.text = "";
        if (isLastFrame)
        {
            txtThrow3.text = "";
        }
    }
}
