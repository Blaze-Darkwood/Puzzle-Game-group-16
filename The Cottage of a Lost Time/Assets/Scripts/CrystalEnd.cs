using UnityEngine;

public class CrystalEnd : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Ground")
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
