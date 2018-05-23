using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMgr : MonoBehaviour {

    public void StartGame()
    {
        Debug.Log("NewScene");
        SceneManager.LoadScene("GameScene");
    }

	public void Exit()
    {
        Application.Quit();
    }
}
