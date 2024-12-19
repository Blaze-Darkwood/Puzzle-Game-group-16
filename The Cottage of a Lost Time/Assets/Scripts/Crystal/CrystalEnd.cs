using UnityEngine;

public class CrystalEnd : MonoBehaviour
{
    [SerializeField] private BoxCollider coll;

    private void OnCollisionEnter(Collision collision) // Stop rotation and movement when hitting the ground
    {
        if (collision.collider.CompareTag("Ground"))
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnTriggerEnter(Collider other) // Make trigger to allow player to pass
    {
        if (other.CompareTag("Player"))
            coll.isTrigger = true;
    }

    private void OnTriggerExit(Collider other) // Disable trigger to stop crystal from passing
    {
        if (other.CompareTag("Player"))
            coll.isTrigger = false;
    }
}
