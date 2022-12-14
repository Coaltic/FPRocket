using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static public GameManager control;
    static public bool gameplayActive;
    public GameObject ship;
    public GameObject barrier;

    public int currentScore;
    public int highScore;

    public enum GameState
    {
        InMenu,
        InOptions,
        InGameplay,
        InReplay,
        InWinScreen,
        InLoseScreen,
        InCredits
    }

    public GameState currentState;
    public GameState previousState;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case GameState.InMenu:
                if (gameplayActive) gameplayActive = false;
                if (ship.activeSelf == false) 
                {
                    PlayerPrefs.SetInt("highScore", highScore);
                    barrier.SetActive(false);
                    ship.transform.position = ship.GetComponent<Ship>().shipStartPosition;
                    ship.GetComponent<Rigidbody>().isKinematic = true;
                    ship.SetActive(true);
                    currentScore = 0;
                }
                break;

            case GameState.InOptions:
                if (gameplayActive) gameplayActive = false;
                Debug.Log("Gameplay Active: " + gameplayActive);
                break;

            case GameState.InGameplay:
                if (!gameplayActive)
                {
                    barrier.GetComponent<Barrier>().Recalculate();
                    barrier.SetActive(true);
                    gameplayActive = true;
                }
                if (currentScore > highScore) highScore = currentScore;
                PlayerPrefs.SetInt("highScore", highScore);
                break;

            case GameState.InReplay:
                if (!gameplayActive)
                {
                    barrier.transform.position += Vector3.forward * 35;
                    ship.SetActive(true);
                    gameplayActive = true;
                }
                PlayerPrefs.SetInt("highScore", highScore);
                break;

            case GameState.InWinScreen:
                if (gameplayActive) gameplayActive = false;
                Debug.Log("Gameplay Active: " + gameplayActive);
                PlayerPrefs.SetInt("highScore", highScore);
                break;

            case GameState.InLoseScreen:
                if (gameplayActive) gameplayActive = false;
                if (ship.activeSelf) ship.SetActive(false);

                if (currentScore > highScore) highScore = currentScore;
                PlayerPrefs.SetInt("highScore", highScore);
                break;

            case GameState.InCredits:
                if (gameplayActive) gameplayActive = false;
                Debug.Log("Gameplay Active: " + gameplayActive);
                break;
        }
    }

    public void SwitchState(GameState state)
    {
        previousState = currentState;
        currentState = state;
    }
}
