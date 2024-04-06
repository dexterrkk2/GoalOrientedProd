using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Bed : MonoBehaviour
{
    Action sleep;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadAction", 1);
    }
    void LoadAction()
    {
        sleep = new Action("sleep in the bed");
        sleep.goals.Add(new Goal("Eat", +2f));
        sleep.goals.Add(new Goal("Sleep", -4f));
        sleep.goals.Add(new Goal("Bathroom", +2f));
        sleep.spot = transform.position;
        GoalSeeker.AddAction(sleep);
    }
}
