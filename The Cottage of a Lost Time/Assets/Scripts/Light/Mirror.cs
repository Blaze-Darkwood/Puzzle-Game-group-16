using UnityEngine;

public class Mirror : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 dir;
    private LineRenderer lr;
    private bool isOpen;

    private GameObject mirror;

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
            RaycastHit hit;
            if (Physics.Raycast(pos, dir, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Mirror"))      //Checks to see if a mirror is hit then "reflects" the light by starting a new line
                {
                    mirror = hit.collider.gameObject;
                    Vector3 tempV3 = Vector3.Reflect(dir, hit.normal);
                    hit.collider.gameObject.GetComponent<Mirror>().StartRay(hit.point, tempV3);
                }
                if (hit.collider.CompareTag("Target"))          //Enter TargetHit code here 0/2
                    Debug.Log("Target hit");
                lr.SetPosition(1, hit.point);
            }
            else
            {
                if (mirror)
                {
                    mirror.GetComponent<Mirror>().StopRay();
                    mirror = null;
                }
                lr.SetPosition(1, dir * 100);
            }
        }
        else if (mirror)
        {
            mirror.GetComponent<Mirror>().StopRay();
        }

        //if (Input.GetKey(KeyCode.P))
        //{
        //    StopRay();
        //    mirror = null;
        //    Debug.Log("test");
        //}
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
