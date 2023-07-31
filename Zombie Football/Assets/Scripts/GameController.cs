using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] goals = new GameObject[2];
    [SerializeField] private GameObject scoreTextObject;
    [SerializeField] private GameObject timeTextObject;
    
    private readonly GoalChecker[] _goalCheckers = new GoalChecker[2];
    private readonly int[] _scores = { 0, 0 };
    private TextMeshProUGUI _scoreMesh;
    private TextMeshProUGUI _timeMesh;

    private float _time = 0f;

    void Start()
    {
        int i = 0;
        foreach (var goal in goals)
        {
            _goalCheckers[i++] = goal.GetComponent<GoalChecker>();
        }

        _scoreMesh = scoreTextObject.GetComponent<TextMeshProUGUI>();
        _timeMesh = timeTextObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < _goalCheckers.Length; i++)
        {
            if (_goalCheckers[i].goalScored)
            {
                _scores[1 - i]++;
                UpdateScoreText();
                _goalCheckers[i].goalScored = false;
            }
        }
        
        UpdateTimeText();
    }

    private void UpdateScoreText()
    {
        _scoreMesh.text = _scores[0] + " - " + _scores[1];
    }

    private void UpdateTimeText()
    {
        _timeMesh.text = Mathf.Floor(_time / 60) + ":" + (Math.Floor(_time % 60)<10 ? "0" : "") + Math.Floor(_time % 60);
    }
}


