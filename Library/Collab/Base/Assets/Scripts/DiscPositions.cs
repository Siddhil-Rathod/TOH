﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiscPositions : MonoBehaviour
{
    public static DiscPositions Instance;
    public List<Disc>[] poles = new List<Disc>[3] { new List<Disc>(), new List<Disc>(), new List<Disc>() };
    public int lastDisc=0, lastPole=0;
    public Transform[] discs=new Transform[5];
    public TextMeshProUGUI movesText;
    public GameObject win;
    float time = 1f;
    private void Start()
    {
        Instance = this;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
        {
            if(lastDisc!=0)
            {
                discs[lastDisc-1].position = new Vector3(3*(lastPole-1), 1.05f, 0);
                lastDisc = 0;
                lastPole = 0;

                //Updating the move value
                Disc.Instance.moves--;
                movesText.text = "Moves : " + Disc.Instance.moves.ToString("00");
            }
            else
                Debug.Log("Here we are");
        }

        if (Instance.poles[2].Count == 1)
        {
            time -= Time.deltaTime;
            if (time <= 0f)
            {
                win.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
