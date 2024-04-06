using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couch : MonoBehaviour
{
    Action couch;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadAction", 1);
    }
    private void LoadAction() 
    {
        couch = new Action("Sleep on the couch");
        couch.goals.Add(new Goal("Eat", -1f));
        couch.goals.Add(new Goal("Sleep", -2f));
        couch.goals.Add(new Goal("Bathroom", +1f));
        couch.spot = transform.position;
        Debug.Log(GoalSeeker.actions);
        GoalSeeker.AddAction(couch);
    }
}
