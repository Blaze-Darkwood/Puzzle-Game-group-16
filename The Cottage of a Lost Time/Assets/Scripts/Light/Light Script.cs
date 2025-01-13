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

    private void Update()
    {
        RaycastHit[] _hits = Physics.RaycastAll(laserOrigin.position, dir, Mathf.Infinity);
        RaycastHit _closestHit = _hits[^1], _furthestHit = _hits[^1];
        float _cDistance = _closestHit.distance, _hDistance = _furthestHit.distance;

        foreach (RaycastHit h in _hits)
        {
            if (!h.collider.CompareTag("Crystal") && !h.collider.CompareTag("IgnoreLazer"))
            {
                if (h.distance < _cDistance)
                {
                    _closestHit = h;
                    _cDistance = h.distance;
                }
                else if (h.distance < _hDistance)
                {
                    _furthestHit = h;
                    _hDistance = h.distance;
                }
            }
        }

        if (!_closestHit.collider.CompareTag("Crystal") && !_closestHit.collider.CompareTag("IgnoreLazer"))
            hit = _closestHit;
        else
            hit = _furthestHit;

        if (hit.collider)
        {
            if (hit.collider.CompareTag("Mirror"))          //Checks to see if a mirror is hit then "reflects" the light by starting a new line
            {
                mirror = hit.collider.gameObject;
                Vector3 tempV3 = Vector3.Reflect(dir, hit.normal);
                hit.collider.gameObject.GetComponent<Mirror>().StartRay(hit.point, tempV3);
            }
            lr.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Target"))          //Enter TargetHit code here 0/2
                Debug.Log("Target hit");
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
