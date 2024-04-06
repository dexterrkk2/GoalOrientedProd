using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSeeker : MonoBehaviour
{
    public List<Goal> goals;
    public List<Action> actions;
    Action changeOverTime;
    const float TICK_LENGTH = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        goals = new List<Goal> ();
        goals.Add(new Goal("Eat", 4));
        goals.Add(new Goal("Sleep", 3));
        goals.Add(new Goal("Bathroom", 3));
        actions = new List<Action> ();
        actions.Add(new Action("eat some raw food"));
        actions[0].goals.Add(new Goal("Eat", -2f));
        actions[0].goals.Add(new Goal("Sleep", +2f));
        actions[0].goals.Add(new Goal("Bathroom", +1f));
        actions.Add(new Action("eat a snack"));
        actions[1].goals.Add(new Goal("Eat", -2f));
        actions[1].goals.Add(new Goal("Sleep", -1f));
        actions[1].goals.Add(new Goal("Bathroom", +1f));
        actions.Add(new Action("sleep in the bed"));
        actions[2].goals.Add(new Goal("Eat", +2f));
        actions[2].goals.Add(new Goal("Sleep", -4f));
        actions[2].goals.Add(new Goal("Bathroom", +2f));
        actions.Add(new Action("sleep on the couch"));
        actions[3].goals.Add(new Goal("Eat", -1f));
        actions[3].goals.Add(new Goal("Sleep", -2f));
        actions[3].goals.Add(new Goal("Bathroom", +1f));
        actions.Add(new Action("drink a soda"));
        actions[4].goals.Add(new Goal("Eat", -1f));
        actions[4].goals.Add(new Goal("Sleep", -2f));
        actions[4].goals.Add(new Goal("Bathroom", +3f));
        actions.Add(new Action("visit the bathroom"));
        actions[5].goals.Add(new Goal("Eat", +0f));
        actions[5].goals.Add(new Goal("Sleep", +0f));
        actions[5].goals.Add(new Goal("Bathroom", -4f));
        changeOverTime = new Action("tick");
        changeOverTime.goals.Add(new Goal("Eat",  4f));
        changeOverTime.goals.Add(new Goal("Sleep",  1f));
        changeOverTime.goals.Add(new Goal("Bathroom", 2f));
        Debug.Log("Starting clock. One Hour will pass every " + TICK_LENGTH + " seconds.");
        InvokeRepeating("Tick", 0f, TICK_LENGTH);
        Debug.Log("Hit E to do something.");
    }
    void Tick()
    {
        foreach( Goal goal in goals )
        {
            goal.value += changeOverTime.GetGoalChange(goal);
            goal.value = Mathf.Max(goal.value, 0f);
        }
        PrintGoals();
    }
    void PrintGoals()
    {
        string goalString = "";
        foreach(Goal goal in goals)
        {
            goalString += goal.name + ": " + goal.value + "; ";
        }
        goalString += "Discontentment" + CurrentDiscontentment();
        Debug.Log(goalString);
    }
    float CurrentDiscontentment()
    {
        float total = 0f;
        foreach( Goal goal in goals)
        {
            total += (goal.value * goal.value);
        }
        return total;
    }
    Action ChooseAction(List<Action> actions, List<Goal> goals)
    {
        Action bestAction = null;
        float bestValue = float.PositiveInfinity;
        foreach( Action action in actions )
        {
            float thisValue = Discontentment(action, goals);
            if (thisValue < bestValue)
            {
                bestValue = thisValue;
                bestAction = action;
            }
        }
        return bestAction;
    }
    float Discontentment(Action action, List<Goal> goals)
    {
        float discontentment = 0f;
        foreach( Goal goal in goals)
        {
            float newValue = goal.value + action.GetGoalChange(goal);
            newValue = Mathf.Max(newValue, 0);
            discontentment += goal.GetDiscontentment(newValue);
        }
        return discontentment;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Action bestThingToDo = ChooseAction(actions, goals);
            Debug.Log("I think I will" + bestThingToDo.name);
            foreach (Goal goal in goals)
            {
                goal.value += bestThingToDo.GetGoalChange(goal);
                goal.value = Mathf.Max(goal.value, 0);
            }
            PrintGoals();
        }
    }
}
