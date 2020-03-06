using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {
    //attached to the end messages.
    /// <summary>
    /// Runs a timer, once the timer runs out, the scene resets
    /// </summary>
    float m_fTimer;
    float maxTime = 5;
	// Use this for initialization
	void Start () {
        m_fTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        m_fTimer += Time.deltaTime;

        if (m_fTimer >= maxTime)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }
}
