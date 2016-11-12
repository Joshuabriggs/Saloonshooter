﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UpgradeMenu : MonoBehaviour {

    List<ShopItem> shopItems = new List<ShopItem>();

    void Start()
    {
        Cursor.visible = true;

        foreach(GameObject _object in GameObject.FindGameObjectsWithTag("ShopButton"))
        {
            if(_object.GetComponent<ShopItem>() != null)
            {
                shopItems.Add(_object.GetComponent<ShopItem>());
            }
        }

        UpdateButtons();

    }

    public void OnNextWave()
    {
        Cursor.visible = false;
        GameState.instance.NextWave();
        SceneManager.UnloadScene("UpgradeMenu");
    }

    void UpdateButtons()
    {
        foreach(ShopItem _item in shopItems)
        {
            _item.CanPlayerAfford();
        }
    }


    //Upgrade Buttons \/\/\/\/

    public void OnMaxHealthIncrease()
    {
        GameState.instance.AddScore(-100);
        GameState.instance.m_maxHealth += 10;
        GameState.instance.UpdateHealthUI();

        UpdateButtons();
    }

    public void OnReloadTimeIncrease()
    {
        GameState.instance.AddScore(-100);
        GameState.instance.reloadTime -= GameState.instance.reloadTime / 20;
        Debug.Log("Reload time: " + GameState.instance.reloadTime);

        GameState.instance.reloadTime = Mathf.Max(GameState.instance.reloadTime, 0f);

        UpdateButtons();

    }

    public void OnTurretCreate()
    {
        GameState.instance.AddScore(-500);

        UpdateButtons();
    }

}
