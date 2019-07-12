using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STAN : MonoBehaviour {

    bool isActive = false;
    [SerializeField] GameObject Stan;
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
        gameObject.GetComponent<PlayerControl>().enabled = false;
    }

    public void MoveOn()
    {
        gameObject.GetComponent<PlayerControl>().enabled = true;
    }
}
