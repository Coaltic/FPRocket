using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public float adSkipTimer;
    private const float adSkipTimerConst = 2.0f;

    public bool replay = false;
    public bool enteredMenu = true;
    public Canvas canvas;

    public Button playButton;
    private Vector3 playButtonStartPosition;
    public Vector3 playButtonEndPosition;

    public Button retryButton;
    private Vector3 retryButtonStartPosition;
    private Vector3 retryButtonEndPosition;

    public Button noThanksButton;

    public Text scoreText;
    public Text fpsText;

    void Awake()
    {
        canvas = this.gameObject.transform.GetChild(0).GetComponent<Canvas>();
        adSkipTimer = adSkipTimerConst;
    }

    private void Start()
    {
        playButtonStartPosition = playButton.transform.position + (Vector3.up * playButton.gameObject.GetComponent<RectTransform>().sizeDelta.x);
        playButton.transform.position -= (Vector3.up * playButton.gameObject.GetComponent<RectTransform>().sizeDelta.x);
        playButtonEndPosition = playButton.transform.position;
        playButton.transform.DOMove(playButtonStartPosition, 2.0f, true);

        retryButtonStartPosition = retryButton.transform.position - (Vector3.up * retryButton.gameObject.GetComponent<RectTransform>().sizeDelta.x);
        retryButton.transform.position += (Vector3.up * retryButton.gameObject.GetComponent<RectTransform>().sizeDelta.x);
        retryButtonEndPosition = retryButton.transform.position;

        noThanksButton.transform.position = playButtonEndPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //fpsText.text = "FPS: " + (int)(1.0f / Time.deltaTime);
        switch (GameManager.control.currentState)
        {
            case GameManager.GameState.InMenu:
                if (!enteredMenu) return;
                playButton.transform.DOMove(playButtonStartPosition, 2.0f, true);
                enteredMenu = false;
                scoreText.text = "HIGH SCORE: \n" + GameManager.control.highScore;
                break;

            case GameManager.GameState.InOptions:
                
                break;

            case GameManager.GameState.InGameplay:
                scoreText.text = "SCORE: \n" + GameManager.control.currentScore;
                break;

            case GameManager.GameState.InReplay:
                scoreText.text = "SCORE: \n" + GameManager.control.currentScore;
                break;

            case GameManager.GameState.InWinScreen:
                
                break;

            case GameManager.GameState.InLoseScreen:
                //if (!replay) 
                    retryButton.transform.DOMove(retryButtonStartPosition, 2.0f, true);;
                //replay = true;

                adSkipTimer -= Time.deltaTime;
                if (adSkipTimer <= 0.0f) noThanksButton.transform.DOMove(playButtonStartPosition, 2.0f, true);
                break;

            case GameManager.GameState.InCredits:
                
                break;
        }
    }

    public void OnClickPlay()
    {
        playButton.transform.DOMove(playButtonEndPosition, 2.0f, true).OnComplete(OnCompletePlay);
        ResetTimer();
        
    }

    public void OnClickRetry()
    {
        GameManager.control.SwitchState(GameManager.GameState.InReplay);
        noThanksButton.transform.DOMove(playButtonEndPosition, 2.0f, true);
        retryButton.transform.DOMove(retryButtonEndPosition, 2.0f, true);
        ResetTimer();
        
    }

    public void OnClickNoThanks()
    {
        if (GameManager.control.currentScore > GameManager.control.highScore) GameManager.control.highScore = GameManager.control.currentScore;
        GameManager.control.SwitchState(GameManager.GameState.InMenu);
        retryButton.transform.DOMove(retryButtonEndPosition, 2.0f, true);
        noThanksButton.transform.DOMove(playButtonEndPosition, 2.0f, true);
        enteredMenu = true;
        ResetTimer();
    }

    public void OnCompletePlay()
    {
        GameManager.control.SwitchState(GameManager.GameState.InGameplay);
        
    }

    public void OnCompleteLose()
    {
        enteredMenu = true;
        GameManager.control.SwitchState(GameManager.GameState.InMenu);
    }

    public void ResetTimer()
    {
        adSkipTimer = adSkipTimerConst;
    }

}
