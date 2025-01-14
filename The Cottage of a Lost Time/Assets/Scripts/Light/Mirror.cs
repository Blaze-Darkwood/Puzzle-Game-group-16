using UnityEngine;
using UnityEngine.UIElements;

public class Mirror : MonoBehaviour
{
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

            RaycastHit[] _hits = Physics.RaycastAll(pos, dir, Mathf.Infinity);
            RaycastHit _closestHit = _hits[^1], _furthestHit = _hits[^1], _mirror = _hits[^1];
            float _cDistance = _closestHit.distance, _hDistance = _furthestHit.distance;

            foreach (RaycastHit h in _hits)
            {
                if (h.collider.CompareTag("Target") || h.collider.CompareTag("Mirror"))
                {
                    _mirror = h;
                    break;
                }
                else if (!h.collider.CompareTag("Crystal") && !h.collider.CompareTag("IgnoreLazer"))
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
            RaycastHit hit = _mirror;

            if (!_closestHit.collider.CompareTag("Crystal") && !_closestHit.collider.CompareTag("IgnoreLazer"))
                hit = _closestHit;
            else if (hit.collider != _hits[^1].collider)
                hit = _furthestHit;

            if (hit.collider)
            {
                if (hit.collider.CompareTag("Mirror"))      //Checks to see if a mirror is hit then "reflects" the light by starting a new line
                {
                    mirror = hit.collider.gameObject;
                    Vector3 tempV3 = Vector3.Reflect(dir, hit.normal);
                    hit.collider.gameObject.GetComponent<Mirror>().StartRay(hit.point, tempV3);
                }
                else if (hit.collider.CompareTag("Target"))          //Enter TargetHit code here 0/2
                    Debug.Log("Target hit");
                else if (!hit.collider.CompareTag("Crystal") || !hit.collider.CompareTag("IgnoreLazer"))
                    lr.SetPosition(1, hit.point);
                else if (hit.collider.CompareTag("Crystal"))
                    hit.collider.gameObject.GetComponent<CrystalColor>().ChangeColor();
            }
            else
            {
                if (mirror)
                {
                    /*mirror.GetComponent<Mirror>().*/StopRay();
                    mirror = null;
                }
                lr.SetPosition(1, dir * 100);
            }
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
