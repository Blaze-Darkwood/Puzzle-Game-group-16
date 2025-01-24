using UnityEngine;

public class TravelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Trigger(true, other);
    }

    private void OnTriggerExit(Collider other)
    {
        Trigger(false, other);
    }

    private void Trigger(bool enter, Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<Player>().TriggerTravel(enter);
    }
}
