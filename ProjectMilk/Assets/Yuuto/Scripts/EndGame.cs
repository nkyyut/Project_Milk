using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    /// <summary>
    /// アプリケーションの終了
    /// </summary>
    public void endGame()
    {
        Debug.Log("EndGame_OK");
        UnityEngine.Application.Quit();
    }
}
