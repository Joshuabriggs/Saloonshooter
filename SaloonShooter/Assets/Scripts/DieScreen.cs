using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DieScreen : MonoBehaviour {

	public void RestartToMenu()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
