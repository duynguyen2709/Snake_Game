using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
	public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
        SpawnFood.ate = true;
    }
    public static void EndGame()
    {
       
        SceneManager.LoadScene(2);
    }
}
