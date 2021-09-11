using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Ending()
    {
        SceneManager.LoadScene(2);
    }
    public void Continue()
    {
        m_player.SetActive(true);
        if (m_player.GetComponent<RespawnManager>() != null)
            m_player.GetComponent<RespawnManager>().Respawn();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Ending();
        }
        
    }
}
