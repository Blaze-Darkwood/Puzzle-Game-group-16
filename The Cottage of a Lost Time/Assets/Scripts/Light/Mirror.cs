using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private LayerMask mirrorMask;
    [SerializeField] private CrystalColor cColor;
    
    private Vector3 pos;
    private Vector3 dir;
    private LineRenderer lr;
    private bool isOpen;
    private GameObject mirror;
    private Color colorIn;

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
                Color _colorOut = GetColor(_hitObjs[1]);
                mirror.GetComponent<Mirror>().StartRay(_hit.point, reflect, _colorOut);
            }

            lr.SetPosition(1, _hit.point);
        }
    }

    public void StartRay(Vector3 _pos, Vector3 _dir, Color _c)
    {
        isOpen = true;
        pos = _pos;
        dir = _dir;
        colorIn = _c;
        lr.material.color = cColor.ChangeColor(colorIn);
    }

    private Color GetColor(RaycastHit _hit)
    {
        return _hit.collider.GetComponent<CrystalColor>().ChangeColor(colorIn);
    }
}
