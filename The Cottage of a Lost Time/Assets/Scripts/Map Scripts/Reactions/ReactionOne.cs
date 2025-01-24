using UnityEngine;

public class ReactionOne : MonoBehaviour, ITargetReaction
{
    private GameObject target;

    private void Start()
    {
        target = GameObject.Find("WallPuzzle1");
    }

    public void React()
    {
        Destroy(target);
    }
}
