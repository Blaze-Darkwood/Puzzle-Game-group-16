using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField] private Transform laserOrigin;
    private Vector3 dir;
    private LineRenderer lr;
    private GameObject mirror;

    private RaycastHit hit;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        dir = laserOrigin.forward;
        lr.positionCount = 2;
        lr.SetPosition(0, laserOrigin.position);
    }

    void Update()
    {
        if (Physics.Raycast(laserOrigin.position, dir, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Mirror"))
            {
                mirror = hit.collider.gameObject;
                Vector3 tempV3 = Vector3.Reflect(dir, hit.normal);
                hit.collider.gameObject.GetComponent<Mirror>().StartRay(hit.point, tempV3);
            }
            else
            lr.SetPosition(1, hit.point);
        }
        else
        {
            if (mirror)
            {
                mirror.GetComponent<Mirror>().StopRay();
                mirror = null;
            }
            lr.SetPosition(1, dir * 200);
        }
    }
}
