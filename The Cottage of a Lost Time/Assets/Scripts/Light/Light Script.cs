using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField] private GameObject laserOrigin;

    private RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.Log(hit.distance);
        }
    }
}
