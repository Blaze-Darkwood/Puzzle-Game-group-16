using UnityEngine;

public class CrystalLine : MonoBehaviour
{
    [SerializeField] private int axis;
    [SerializeField] private Rigidbody crystal;
    
    private static readonly bool[] constraints = new bool[2];

    private void OnTriggerEnter(Collider other)
    {
        constraints[axis] = true;
        UpdateFreeze();
    }

    private void OnTriggerExit(Collider other)
    {
        constraints[axis] = false;
        UpdateFreeze();
    }

    private void UpdateFreeze()
    {
        RigidbodyConstraints strain = RigidbodyConstraints.None;

        if (!constraints[0] && constraints[1])
            strain = RigidbodyConstraints.FreezePositionX;
        else if (constraints[0] && !constraints[1])
            strain = RigidbodyConstraints.FreezePositionZ;

        crystal.constraints = strain | RigidbodyConstraints.FreezeRotation;
    }
}
