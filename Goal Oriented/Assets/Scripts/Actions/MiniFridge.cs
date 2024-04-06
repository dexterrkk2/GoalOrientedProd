using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MiniFridge : MonoBehaviour
{
    Action miniFridge;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadAction", 1);
    }
    void LoadAction()
    {
        miniFridge = new Action("drink a soda");
        miniFridge.goals.Add(new Goal("Eat", -1f));
        miniFridge.goals.Add(new Goal("Sleep", -2f));
        miniFridge.goals.Add(new Goal("Bathroom", +3f));
        miniFridge.spot = transform.position;
        GoalSeeker.AddAction(miniFridge);
    }
}
