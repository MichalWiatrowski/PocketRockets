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
       // networkServerUIbuttons.networkServer.readyClients.ForEach(x => x = false);
       
       // for (int i = 0; i < 4; i++)
       // {
         //   networkServerUIbuttons.networkServer.readyClients[i] = 0;
       // }
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
     

       for (int i = 0; i < 4; i++)
        {
            if (networkServerUIbuttons.networkServer.readyClientsTest[i] == 0)
            {
                GameObject.Find("Canvas/preGamePanelRemake/player" + (i + 1) + "Panel").GetComponent<Image>().color = Color.red;
                GameObject.Find("Canvas/preGamePanelRemake/player" + (i + 1) + "Panel").GetComponentInChildren<Text>().text = "Not Ready";
            }
            else if (networkServerUIbuttons.networkServer.readyClientsTest[i] == 1)
            {
                GameObject.Find("Canvas/preGamePanelRemake/player" + (i + 1) + "Panel").GetComponent<Image>().color = Color.green;
                GameObject.Find("Canvas/preGamePanelRemake/player" + (i + 1) + "Panel").GetComponentInChildren<Text>().text = "Ready";
            }
            else if (networkServerUIbuttons.networkServer.readyClientsTest[i] == -1)
            {
                GameObject.Find("Canvas/preGamePanelRemake/player" + (i + 1) + "Panel").GetComponent<Image>().color = Color.gray;
                GameObject.Find("Canvas/preGamePanelRemake/player" + (i + 1) + "Panel").GetComponentInChildren<Text>().text = "Not Connected";
            }
        }
        

      

    }

}
