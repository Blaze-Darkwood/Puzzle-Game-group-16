using UnityEngine;

public class CrystalLine : MonoBehaviour
{
    [SerializeField] private int axis;
    [SerializeField] private Rigidbody crystal;
    
    private static readonly bool[] constraints = new bool[2];

    private void OnTriggerEnter(Collider other) // Unfreeze axis
    {
        constraints[axis] = true;
        UpdateFreeze();
    }

    private void OnTriggerExit(Collider other) // Freeze axis
    {
        constraints[axis] = false;
        UpdateFreeze();
    }

    private void UpdateFreeze() // Confirm freezes
    {
        RigidbodyConstraints _strain = RigidbodyConstraints.None;

        if (!constraints[0] && constraints[1])
            _strain = RigidbodyConstraints.FreezePositionX;
        else if (constraints[0] && !constraints[1])
            _strain = RigidbodyConstraints.FreezePositionZ;

        crystal.constraints = _strain | RigidbodyConstraints.FreezeRotation;
    }
}
