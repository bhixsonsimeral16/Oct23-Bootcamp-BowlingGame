using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform frameHolder;
    [SerializeField] GameObject messageUIStrike, messageUISpare, gameOverUI;
    [SerializeField] TMP_Text scoreText;

    private FrameUI[] frames;

    private void Start()
    {
        messageUISpare.SetActive(false);
        messageUIStrike.SetActive(false);
        gameOverUI.SetActive(false);
        
        frames = frameHolder.GetComponentsInChildren<FrameUI>();
    }
}
