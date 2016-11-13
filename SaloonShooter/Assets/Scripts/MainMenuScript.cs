using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

    [SerializeField] GameObject m_player;
    bool m_gameBegin = false;

    [SerializeField] GameObject m_menuCam;
    [SerializeField] GameObject m_gameThings;
    [SerializeField] GameObject m_waypoint;

    Transform m_goto;

	// Use this for initialization
	void Start () {
        m_goto = m_waypoint.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(m_gameBegin)
        {
            m_menuCam.transform.position = Vector3.MoveTowards(m_menuCam.transform.position, new Vector3(m_goto.position.x, m_goto.position.y + 5f, m_goto.position.z), 70f * Time.deltaTime);
            Quaternion wanted_rotation = Quaternion.LookRotation(-m_goto.transform.position);
            m_menuCam.transform.rotation = Quaternion.RotateTowards(m_menuCam.transform.rotation, wanted_rotation, Time.deltaTime * 200);
        }

	}

    public void StartGame()
    {
        m_gameBegin = true;
        StartCoroutine(DisableDelay());
        StartCoroutine(WaypointChange());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaypointChange()
    {
        yield return new WaitForSeconds(0.9f);
        m_goto = m_player.transform;
    }

    IEnumerator DisableDelay()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        m_gameThings.SetActive(true);
    }

}
