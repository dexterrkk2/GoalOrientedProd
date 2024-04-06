using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    Action fridge;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadAction", 1);
    }
    void LoadAction()
    {
        fridge = new Action("Eat some raw food");
        fridge.goals.Add(new Goal("Eat", -3f));
        fridge.goals.Add(new Goal("Sleep", +2f));
        fridge.goals.Add(new Goal("Bathroom", +1f));
        fridge.spot = transform.position;
        GoalSeeker.AddAction(fridge);
    }
}
