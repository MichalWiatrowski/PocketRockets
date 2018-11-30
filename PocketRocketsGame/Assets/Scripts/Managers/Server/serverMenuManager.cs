using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class serverMenuManager : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject preGamePanel;
    public AudioClip menuMusic;

    private AudioSource menuSource;


    void Awake() {

        menuSource = GetComponent<AudioSource>();

    }

    // Use this for initialization
    void Start()
    {
        menuSource.PlayOneShot(menuMusic);
        mainMenuPanel.SetActive(true);
        preGamePanel.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
      
       
    }

    public void hostServer()
    {
        networkServerUIbuttons.networkServer.hostServer();

        mainMenuPanel.SetActive(false);
        preGamePanel.SetActive(true);
    }

    public void startGame()
    {
        networkServerUIbuttons.networkServer.hostGame();
       
    }
}
