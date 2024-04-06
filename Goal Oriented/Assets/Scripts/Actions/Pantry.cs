using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pantry : MonoBehaviour
{
    Action pantry;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadAction", 1);
    }
    void LoadAction()
    {
        pantry = new Action("eat a snack");
        pantry.goals.Add(new Goal("Eat", -2f));
        pantry.goals.Add(new Goal("Sleep", -1f));
        pantry.goals.Add(new Goal("Bathroom", +1f));
        pantry.spot = transform.position;
        GoalSeeker.AddAction(pantry);
    }
}
