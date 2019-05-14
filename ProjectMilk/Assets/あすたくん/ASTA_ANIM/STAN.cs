using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STAN : MonoBehaviour
{
    bool isActive = false;
    [SerializeField] GameObject Stan;
    [SerializeField] float Impulse;

    Vector3 OldVec3;

    private void FixedUpdate()
    {
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");
        OldVec3 = new Vector3(X, 0, Z);
    }
    public void StanStar()
    {
        if (!isActive)
        {
            isActive = true;
            Stan.SetActive(true);
        }
        else
        {
            isActive = false;
            Stan.SetActive(false);
        }
    }

    public void MoveOff()
    {
        gameObject.GetComponent<PlayerController>().enabled = false;
    }

    public void MoveOn()
    {
        gameObject.GetComponent<PlayerController>().enabled = true;
    }

    public void BlowOff()
    {
        gameObject.transform.Translate(-OldVec3 * Impulse);
    }
}
