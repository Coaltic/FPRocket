using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    //public bool gameActive = false;
    public float speed = 15;
    public GameObject background;
    public GameObject barrier;
    public GameObject[] blocks;

    void Awake()
    {
        //gameActive = true;
        background = GameObject.Find("Canvas");
        barrier = this.gameObject;
        blocks = new GameObject[barrier.transform.childCount];

        for (int i = 0; i < barrier.transform.childCount; i++)
        {
            blocks[i] = barrier.transform.GetChild(i).gameObject;
        }

        foreach (GameObject block in blocks)
        {
            float num = Random.Range(0f, 1f);
            if (num < 0.5f) block.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (barrier.transform.position.z < Camera.main.transform.position.z - barrier.gameObject.transform.localScale.z && GameManager.gameplayActive)
        {
            barrier.transform.position = background.transform.position + (Vector3.forward * 5);
            Recalculate();
        }

        if (GameManager.gameplayActive) barrier.transform.position = barrier.transform.position + (-Vector3.forward * Time.deltaTime * speed);
    }

    public void Recalculate()
    {
        foreach (GameObject block in blocks)
        {
            float num = Random.Range(0f, 1f);

            if (num < 0.5f) block.SetActive(false);
            else block.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
