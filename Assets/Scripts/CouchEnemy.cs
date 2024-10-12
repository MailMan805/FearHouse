using UnityEngine;

public class TriggerExample : MonoBehavior
{
    private void onTriggerEnter(Collider other)
    {
        Debug.Log("Object entered the trigger: " + other.name);

    }

    private void onTriggerstay(Collider other)
    {
        Debug.Log("Object staying in the trigger: " + other.name);


    }
    private void onTriggerExit(Collider other)
    {
        Debug.Log("Object exited the trigger:" + other.name);


    }
}
    