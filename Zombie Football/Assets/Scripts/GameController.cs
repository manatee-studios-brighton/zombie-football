using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] goals = new GameObject[2];
    [SerializeField] private GameObject scoreTextObject;
    
    private readonly GoalChecker[] _goalCheckers = new GoalChecker[2];
    private readonly int[] _scores = { 0, 0 };
    private TextMeshProUGUI _scoreMesh;

    void Start()
    {
        int i = 0;
        foreach (var goal in goals)
        {
            _goalCheckers[i++] = goal.GetComponent<GoalChecker>();
        }

        _scoreMesh = scoreTextObject.GetComponent<TextMeshProUGUI>();
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
    }

    private void UpdateScoreText()
    {
        _scoreMesh.text = _scores[0] + " - " + _scores[1];
    }
}


