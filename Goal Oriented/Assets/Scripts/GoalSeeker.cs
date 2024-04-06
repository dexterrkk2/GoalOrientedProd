using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class GoalSeeker : MonoBehaviour
{
    public List<Goal> goals;
    public static List<Action> actions;
    Action changeOverTime;
    const float TICK_LENGTH = 10.0f;
    public NavMeshAgent agent;
    public TextMeshProUGUI intention;
    public TextMeshProUGUI eat;
    public TextMeshProUGUI sleep;
    public TextMeshProUGUI bathroom;
    // Start is called before the first frame update
    void Start()
    {
        actions = new List<Action>();
        goals = new List<Goal> ();
        goals.Add(new Goal("Eat", 4));
        goals.Add(new Goal("Sleep", 3));
        goals.Add(new Goal("Bathroom", 3));
        changeOverTime = new Action("tick");
        changeOverTime.goals.Add(new Goal("Eat",  +4f));
        changeOverTime.goals.Add(new Goal("Sleep",  +4f));
        changeOverTime.goals.Add(new Goal("Bathroom", +4f));
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
    public static void AddAction(Action acton)
    {
        actions.Add(acton);
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
        foreach (Action action in actions)
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
            agent.SetDestination(bestThingToDo.spot);
            intention.text = "I think I will " + bestThingToDo.name;
            foreach (Goal goal in goals)
            {
                goal.value += bestThingToDo.GetGoalChange(goal);
                goal.value = Mathf.Max(goal.value, 0);
            }
            PrintGoals();
        }
        eat.text = goals[0].name + " " + goals[0].value;
        sleep.text = goals[1].name + " " + goals[1].value;
        bathroom.text = goals[2].name + " " + goals[2].value;
    }
}
