using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    [SerializeField] int price;
    Button m_button;

    void Start()
    {
        m_button = GetComponent<Button>();
    }

    public void CanPlayerAfford()
    {
        if (GameState.instance.m_score >= price)
        {
            m_button.interactable = true;
        }
        else
        {
            m_button.interactable = false;
        }
    }

}
