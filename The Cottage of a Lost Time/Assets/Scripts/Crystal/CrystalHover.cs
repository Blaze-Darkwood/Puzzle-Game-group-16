using System.Collections.Generic;
using UnityEngine;

public class CrystalHover : MonoBehaviour
{
    [SerializeField] private Transform[] checkpoints;
    [SerializeField] private float speed;

    private int targetIndex = 0;
    private int previousIndex = 1;

    private float timeToCheck;
    private float elapsedTime = .0f;

    private void Update()
    {
        if (Time.timeScale > .0f)
        {
            elapsedTime += Time.deltaTime;
            float elapsedPercentage = elapsedTime / timeToCheck;
            elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
            Vector3 newPos = Vector3.Lerp(C(previousIndex).localPosition,
                C(targetIndex).localPosition, elapsedPercentage);
            Quaternion newRot = Quaternion.Lerp(C(previousIndex).rotation,
                C(targetIndex).rotation, elapsedPercentage);
            transform.SetLocalPositionAndRotation(newPos, newRot);

            if (elapsedPercentage >= 1)
                TargetNextCheckpoint();
        }
    }

    private void TargetNextCheckpoint() // Return next checkpoint index and time this checkpoint will take
    {
        previousIndex = targetIndex;
        targetIndex = targetIndex == 0 ? 1 : 0;
        elapsedTime = .0f;
        float distanceToCheck = Vector3.Distance(C(previousIndex).position, C(targetIndex).position);
        timeToCheck = distanceToCheck / speed;
    }

    private Transform C(int check) // Return the transform scripts asks for
    {
        return checkpoints[check];
    }
}
