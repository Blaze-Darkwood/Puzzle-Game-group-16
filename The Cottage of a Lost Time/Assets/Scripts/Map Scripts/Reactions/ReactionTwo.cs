using UnityEngine;

public class ReactionTwo : MonoBehaviour, ITargetReaction
{
    private GameObject target;

    private void Start()
    {
        target = GameObject.Find("WallPuzzle2");
    }

    public void React()
    {
        target.SetActive(false);
    }
}
