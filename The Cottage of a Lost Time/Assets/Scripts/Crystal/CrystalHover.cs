using UnityEngine;

public class CrystalHover : MonoBehaviour
{
    [SerializeField] private float step = .1f;

    private float startY, minY = -.5f, maxY = .5f;
    private bool goingUp = false;

    private void Start()
    {
        startY = transform.position.y;
        minY = startY + minY;
        maxY = startY + maxY;
    }

    private void Update()
    {
        if (goingUp)
        {

        }
    }
}
