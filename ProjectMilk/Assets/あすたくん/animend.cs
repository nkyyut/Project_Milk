using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animend : MonoBehaviour {

    // Scene上に存在するアスタくんをアタッチしてください
    [SerializeField] AnimCon animcon;

    private void end()
    {
        animcon.isEnd = true;
    }
}
