using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    static public GameManager control;
    static public bool gameplayActive;

    public enum State
    {
        InMenu,
        InOptions,
        InGameplay,
        InWinScreen,
        InLoseScreen,
        InCredits
    }

    public State currentState;
    public State previousState;

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

    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case State.InMenu:
                if (gameplayActive)
                {
                    gameplayActive = false;
                    Debug.Log("Gameplay Active: " + gameplayActive);
                }
                break;

            case State.InOptions:
                if (gameplayActive) gameplayActive = false;
                Debug.Log("Gameplay Active: " + gameplayActive);
                break;

            case State.InGameplay:
                if (!gameplayActive)
                {
                    gameplayActive = true;
                    Debug.Log("Gameplay Active: " + gameplayActive);
                }
                break;

            case State.InWinScreen:
                if (gameplayActive) gameplayActive = false;
                Debug.Log("Gameplay Active: " + gameplayActive);
                break;

            case State.InLoseScreen:
                if (gameplayActive)
                {
                    gameplayActive = false;
                    Debug.Log("Gameplay Active: " + gameplayActive);
                }
                break;

            case State.InCredits:
                if (gameplayActive) gameplayActive = false;
                Debug.Log("Gameplay Active: " + gameplayActive);
                break;
        }
    }

    public void SwitchState(State state)
    {
        previousState = currentState;
        currentState = state;
    }
}
