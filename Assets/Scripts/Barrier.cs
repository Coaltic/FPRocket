using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public bool scoreRecieved = false;

    public float speed = 15;
    public int barrierScore;
    public GameObject background;
    public GameObject barrier;
    public GameObject[] blocks;

    public Vector3 startPosition;

    void Awake()
    {
        //gameActive = true;
        background = GameObject.Find("Canvas");
        barrier = this.gameObject;
        blocks = new GameObject[barrier.transform.childCount];

        startPosition = background.transform.position + (Vector3.forward * 5);

        for (int i = 0; i < barrier.transform.childCount; i++)
        {
            blocks[i] = barrier.transform.GetChild(i).gameObject;
        }

        Recalculate();
    }

    // Update is called once per frame
    void Update()
    {
        if (barrier.transform.position.z < Camera.main.transform.position.z - barrier.gameObject.transform.localScale.z && GameManager.gameplayActive)
        {
            Recalculate();
        }

        if (GameManager.gameplayActive) barrier.transform.position = barrier.transform.position + (-Vector3.forward * Time.deltaTime * speed);
    }

    public void Recalculate()
    {
        speed = 20;
        barrier.transform.position = startPosition;
        barrierScore = 0;
        scoreRecieved = false;

        foreach (GameObject block in blocks)
        {
            float num = Random.Range(0f, 1f);

            if (num < 0.5f) block.GetComponent<MeshRenderer>().enabled = false;
            else
            {
                block.GetComponent<MeshRenderer>().enabled = true;
                barrierScore += 1;
            }
        }
    }
}
