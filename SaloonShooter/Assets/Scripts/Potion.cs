using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour {

    private GameObject m_player;
    private int m_relativeposition;
    private int m_heal;
    private float m_xVal;
    private int m_beerNum;
    private Transform m_transform;
    private bool m_close;


	// Use this for initialization
	void Start () {

        m_player = GameObject.FindGameObjectWithTag("Player");
        m_transform = transform;
        m_close = false;

        if(gameObject.tag == "SmallBeer")
        {
            m_heal = 10;
        }
        if (gameObject.tag == "MediumBeer")
        {
            m_heal = 25;
        }
        if (gameObject.tag == "LargeBeer")
        {
            m_heal = 75;
        }
        if (gameObject.tag == "OversizedBeer")
        {
            m_heal = 150;
        }

        m_xVal = m_transform.position.x;
        if(m_xVal == 7)
        {
            m_beerNum = 0;
        }
        if (m_xVal == 0)
        {
            m_beerNum = 1;
        }
        if (m_xVal == -7)
        {
            m_beerNum = 2;
        }

    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log(GameState.instance.m_beerNumber);

        if(m_player.transform.position.x >= m_transform.position.x -1 && m_player.transform.position.x <= m_transform.position.x + 1)
        {
            m_close = true;
            
            if(Input.GetKeyUp(KeyCode.E))
            {
                GameState.instance.UpdateHealth(m_heal);
                GameState.instance.m_beerNumber--;
                GameState.instance.m_beerStorage[m_beerNum] = 0;
                m_player.GetComponent<PlayerControll>().m_drunkspin++;
                Destroy(gameObject);
                m_close=false;
            }
        }
        else
        {
            m_close = false;
        }
	
	}

    void OnGUI()
    {
        if (m_close == true)
        {
            GUI.Label(new Rect(Screen.width / 2-50, Screen.height / 2+40, 100, 20), "Press 'E' to drink");
        }
    }
}
