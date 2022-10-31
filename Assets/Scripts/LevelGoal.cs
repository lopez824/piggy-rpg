using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    public UIHandler uiHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            uiHandler.startMessage.SetText("Hooray! You reached the boat!!");
    }
}
