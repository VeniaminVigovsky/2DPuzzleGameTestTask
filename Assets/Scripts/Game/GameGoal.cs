using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameGoal
{
    public List<int> GoalIndeces => _goalIndeces;

    public List<int> StartIndeces => _startIndeces;

    [SerializeField] private List<int> _goalIndeces;
    [SerializeField][Tooltip("Должны быть из тех, что в Goal Indeces")] 
    private List<int> _startIndeces;

}
