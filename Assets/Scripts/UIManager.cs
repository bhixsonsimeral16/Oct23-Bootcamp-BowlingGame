using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform frameHolder;
    [SerializeField] GameObject messageUIStrike, messageUISpare, gameOverUI;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float messageTime = 2f;
    [SerializeField] private GameObject mobileCanvas;

    private FrameUI[] frames;

    private void Start()
    {
        ResetFrameUIs();

        messageUISpare.SetActive(false);
        messageUIStrike.SetActive(false);
        gameOverUI.SetActive(false);
        mobileCanvas.SetActive(false);
        
        frames = frameHolder.GetComponentsInChildren<FrameUI>();

#if UNITY_ANDROID || UNITY_IOS
        mobileCanvas.SetActive(true);
#endif
    }

    public void ResetFrameUIs()
    {
        frames = new FrameUI[frameHolder.childCount];

        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = frameHolder.GetChild(i).GetComponent<FrameUI>();
            frames[i].SetFrameNumber(i + 1);
        }
    }

    public void SetFrameValue(int frameNumber, int throwNumber, int score)
    {
        frames[frameNumber - 1].UpdateScore(throwNumber, score);
    }

    public void SetFrameTotal(int frameNumber, int total)
    {
        frames[frameNumber].UpdateTotal(total);
    }

    public void ShowStrikeMessage()
    {
        messageUIStrike.SetActive(true);
        Invoke(nameof(HideStrikeMessage), messageTime);
    }

    void HideStrikeMessage()
    {
        messageUIStrike.SetActive(false);
    }

    public void ShowSpareMessage()
    {
        messageUISpare.SetActive(true);
        Invoke(nameof(HideSpareMessage), messageTime);
    }

    void HideSpareMessage()
    {
        messageUISpare.SetActive(false);
    }

    public void ShowGameOverUI(int finalScore)
    {
        scoreText.text = finalScore.ToString();
        gameOverUI.SetActive(true);
    }
}
