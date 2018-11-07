using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class serverMenuManager : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject preGamePanel;
    

    // Use this for initialization
    void Start()
    {
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
