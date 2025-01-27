using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField] private Transform laserOrigin;
    [SerializeField] private LayerMask mirrorMask;
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
        RaycastHit[] _hitObjs = new RaycastHit[2];

        Physics.Raycast(laserOrigin.position, dir, out _hitObjs[0], Mathf.Infinity, mirrorMask);
        Physics.Raycast(laserOrigin.position, dir, out _hitObjs[1], Mathf.Infinity);
        hit = _hitObjs[1];

        if (!_hitObjs[1].collider.CompareTag("Crystal") && !_hitObjs[1].collider.CompareTag("IgnoreLazer"))
        {
            Collider _coll = hit.collider;
            GameObject _hitObj = _coll.gameObject;
            mirror = null;

            if (_coll.CompareTag("Target"))
                _hitObj.GetComponent<Target>().HitTarget(new Color(0, .9176470588f, 1, 0));
        }
        else if (_hitObjs[0].collider)
        {
            hit = _hitObjs[0];
            mirror = hit.collider.gameObject;
            Vector3 reflect = Vector3.Reflect(dir, hit.normal);
            mirror.GetComponent<Mirror>().StartRay(hit.point, reflect, Color.white);
        }

        lr.SetPosition(1, hit.point);
    }
}
