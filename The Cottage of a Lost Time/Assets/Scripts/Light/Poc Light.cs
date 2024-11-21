using UnityEngine;

public class PocLight : MonoBehaviour
{
    [SerializeField] private GameObject blockade;
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject redLight;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (blockade.activeInHierarchy == true)
                blockade.SetActive(false);
            else blockade.SetActive(true);
            if (laser.activeInHierarchy == true)
                laser.SetActive(false);
            else laser.SetActive(true);
            if (redLight.activeInHierarchy == true)
                redLight.SetActive(false);
            else redLight.SetActive(true);
        }
    }
}
