using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Toilet : MonoBehaviour
{
    Action useBathroom;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadAction", 0);
    }
    void LoadAction()
    {
        useBathroom= new Action("visit the bathroom");
        useBathroom.goals.Add(new Goal("Eat", +0f));
        useBathroom.goals.Add(new Goal("Sleep", +0f));
        useBathroom.goals.Add(new Goal("Bathroom", -4f));
        useBathroom.spot = transform.position;
        GoalSeeker.AddAction(useBathroom);
    }
}
