using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public string name;
    public float value;
    public Goal(string goalName, float goalValue)
    {
        name = goalName;
        value = goalValue;
    }
    public float GetDiscontentment(float newValue)
    {
        return newValue * newValue;
    }
    
}
public class Action
{
    public string name;
    public Vector3 spot;
    public List<Goal> goals;
    public Action(string actionName)
    {
        name = actionName;
        goals = new List<Goal>();
    }
    public float GetGoalChange(Goal goal)
    {
        foreach (Goal target in goals)
        {
            if (target.name == goal.name)
            {
                return target.value;
            }
        }
        return 0f;
    }
}
