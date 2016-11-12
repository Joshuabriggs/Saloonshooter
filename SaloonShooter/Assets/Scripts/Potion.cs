using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour {

    private GameObject m_player;
    private int m_relativeposition;
    private Transform m_transform;
    private bool m_close;


	// Use this for initialization
	void Start () {

        m_player = GameObject.FindGameObjectWithTag("Player");
        m_transform = transform;
        m_close = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(m_player.transform.position.x >= m_transform.position.x -1 && m_player.transform.position.x <= m_transform.position.x + 1)
        {
            m_close = true;
            
            if(Input.GetKey(KeyCode.H))
            {
                GameState.instance.UpdateHealth(-25f);
                Destroy(gameObject);
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
            GUI.Label(new Rect(10, 40, 100, 20), "Press 'H' to drink");
        }
    }
}
