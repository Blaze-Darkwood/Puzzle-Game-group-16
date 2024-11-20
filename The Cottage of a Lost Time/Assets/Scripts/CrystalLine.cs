using UnityEngine;

public class CrystalLine : MonoBehaviour
{
    [SerializeField] private int axis;
    [SerializeField] private Rigidbody crystal;
    
    private static readonly bool[] constraints = new bool[2];

    private void OnTriggerStay(Collider other) // Unfreeze axis
    {
        if (other.name == "Crystal")//other.CompareTag("Crystal"))
        {
            constraints[axis] = true;
            UpdateFreeze();
        }
    }

    private void OnTriggerExit(Collider other) // Freeze axis
    {
        if (other.name == "Crystal")//other.CompareTag("Crystal"))
        {
            constraints[axis] = false;
            UpdateFreeze();
        }
    }

    private void UpdateFreeze() // Confirm freezes
    {
        RigidbodyConstraints _strain = RigidbodyConstraints.FreezePosition;

        if (!constraints[0] && constraints[1])
            _strain = RigidbodyConstraints.FreezePositionX;
        else if (constraints[0] && !constraints[1])
            _strain = RigidbodyConstraints.FreezePositionZ;
        else if (constraints[0] && constraints[1])
            _strain = RigidbodyConstraints.None;

        crystal.constraints = _strain | RigidbodyConstraints.FreezeRotation;
    }
}
