using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class serverMenuManager : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject preGamePanel;
    public GameObject preGamePanelRemake;


    public GameObject player1panel;
    public GameObject player2panel;
    public GameObject player3panel;
    public GameObject player4panel;


    public AudioClip menuMusic;

    private AudioSource menuSource;


    void Awake() {

        menuSource = GetComponent<AudioSource>();
        
    }

    // Use this for initialization
    void Start()
    {
        menuSource.PlayOneShot(menuMusic);
        if (networkServerUIbuttons.networkServer.getPlayerAmount() > 0)
        {
            preGamePanelRemake.SetActive(true);
            mainMenuPanel.SetActive(false);
        }
        else
        {
            mainMenuPanel.SetActive(true);
            preGamePanelRemake.SetActive(false);
        }
        networkServerUIbuttons.networkServer.readyClients.ForEach(x => x = false);
        
    }
	
	

    public void hostServer()
    {
        networkServerUIbuttons.networkServer.hostServer();

        mainMenuPanel.SetActive(false);
        //preGamePanel.SetActive(true);
        preGamePanelRemake.SetActive(true);
    }

    public void startGame()
    {
        networkServerUIbuttons.networkServer.hostGame();
       
    }


    // Update is called once per frame
    void Update()
    {
       
        if (networkServerUIbuttons.networkServer.getReadyClient()[0] == false)
        {
            player1panel.GetComponent<Image>().color = Color.red;
            player1panel.GetComponentInChildren<Text>().text = "Not Ready";
        }
        else
        {
            player1panel.GetComponent<Image>().color = Color.green;
            player1panel.GetComponentInChildren<Text>().text = "Ready";
        }

        ///////////////////////
        if (networkServerUIbuttons.networkServer.getReadyClient()[1] == false)
        {
            player2panel.GetComponent<Image>().color = Color.red;
            player2panel.GetComponentInChildren<Text>().text = "Not Ready";
        }
        else
        {
            player2panel.GetComponent<Image>().color = Color.green;
            player2panel.GetComponentInChildren<Text>().text = "Ready";
        }
        ////////////////////////
        if (networkServerUIbuttons.networkServer.getReadyClient()[2] == false)
        {
            player3panel.GetComponent<Image>().color = Color.red;
            player3panel.GetComponentInChildren<Text>().text = "Not Ready";
        }
        else
        {
            player3panel.GetComponent<Image>().color = Color.green;
            player3panel.GetComponentInChildren<Text>().text = "Ready";
        }
        ///////////////////////
        if (networkServerUIbuttons.networkServer.getReadyClient()[3] == false)
        {
            player4panel.GetComponent<Image>().color = Color.red;
            player4panel.GetComponentInChildren<Text>().text = "Not Ready";
        }
        else
        {
            player4panel.GetComponent<Image>().color = Color.green;
            player4panel.GetComponentInChildren<Text>().text = "Ready";
        }















    }

}
