using UnityEngine;

public class CrystalEnd : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) // Stop rotation and movement when hitting the ground
    {
        if (collision.collider.name == "Ground")
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
