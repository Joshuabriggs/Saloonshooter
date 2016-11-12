using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour {

	
    void Start()
    {
        Cursor.visible = true;
    }

    public void OnNextWave()
    {
        Cursor.visible = false;
        GameState.instance.NextWave();
        SceneManager.UnloadScene("UpgradeMenu");
    }

    void UpdateButtons()
    {

    }


    //Upgrade Buttons \/\/\/\/

    public void OnMaxHealthIncrease()
    {
        GameState.instance.AddScore(-100);
        GameState.instance.m_maxHealth += 10;
        GameState.instance.UpdateHealthUI();
    }

    public void OnReloadTimeIncrease()
    {
        GameState.instance.AddScore(-100);
        GameState.instance.reloadTime -= GameState.instance.reloadTime / 20;
        Debug.Log("Reload time: " + GameState.instance.reloadTime);

        GameState.instance.reloadTime = Mathf.Max(GameState.instance.reloadTime, 0f);

    }

    public void OnTurretCreate()
    {
        GameState.instance.AddScore(-500);
    }

}
