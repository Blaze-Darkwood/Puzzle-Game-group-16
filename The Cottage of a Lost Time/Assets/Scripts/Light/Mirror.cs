using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private LayerMask mirrorMask;
    
    private Vector3 pos;
    private Vector3 dir;
    private LineRenderer lr;
    private bool isOpen;
    private GameObject mirror;
    private float timer = 1f;

    void Start()
    {
        isOpen = false;
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (isOpen)
        {
            lr.positionCount = 2;
            lr.SetPosition(0, pos);
            RaycastHit[] _hitObjs = new RaycastHit[2];

            Physics.Raycast(pos, dir, out _hitObjs[0], Mathf.Infinity, mirrorMask);
            Physics.Raycast(pos, dir, out _hitObjs[1], Mathf.Infinity);
            RaycastHit _hit = _hitObjs[1];

            if (!_hitObjs[1].collider.CompareTag("Crystal") && !_hitObjs[1].collider.CompareTag("IgnoreLazer"))
            {
                Collider _coll = _hit.collider;
                GameObject _hitObj = _coll.gameObject;
                mirror = null;

                if (_coll.CompareTag("Target"))
                    _hitObj.GetComponent<Target>().HitTarget(new Color(0, .9176470588f, 1, 0));
            }
            else if (_hitObjs[0].collider)
            {
                _hit = _hitObjs[0];
                mirror = _hit.collider.gameObject;
                Vector3 reflect = Vector3.Reflect(dir, _hit.normal);
                mirror.GetComponent<Mirror>().StartRay(_hit.point, reflect);
            }
            /*else if (_hitObjs[1].collider.CompareTag("Crystal"))
                _hitObjs[1].collider.GetComponent<CrystalColor>().ChangeColor();*/

            lr.SetPosition(1, _hit.point);
        }
        else if (mirror)
        {
            /*mirror.GetComponent<Mirror>().*/StopRay();
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            StopRay();
            timer = 1f;
        }
    }
    public void StartRay(Vector3 _pos, Vector3 _dir)
    {
        isOpen = true;
        pos = _pos;
        dir = _dir;
    }
    public void StopRay()                                       //Should stop ray, doesn't work idk why
    {
        isOpen = false;
        lr.positionCount = 0;
    }
}
