using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public Canvas canvas;
    public Button playButton;
    public Vector3 playButtonStartPosition;
    public Vector3 playButtonEndPosition;

    void Awake()
    {
        canvas = this.gameObject.transform.GetChild(0).GetComponent<Canvas>();
        playButton = canvas.transform.GetChild(0).GetComponent<Button>();
    }

    private void Start()
    {
        playButtonStartPosition = playButton.transform.position;
        playButton.transform.position = playButtonStartPosition - (Vector3.up * playButton.gameObject.GetComponent<RectTransform>().sizeDelta.x);
        playButtonEndPosition = playButton.transform.position;
        playButton.transform.DOMove(playButtonStartPosition + (Vector3.up * playButton.gameObject.GetComponent<RectTransform>().sizeDelta.x), 2.0f, true);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlay()
    {
        playButton.transform.DOMove(playButtonEndPosition, 2.0f, true).OnComplete(OnCompleted);
        
    }

    public void OnCompleted()
    {
        GameManager.control.SwitchState(GameManager.State.InGameplay);
    }
}
