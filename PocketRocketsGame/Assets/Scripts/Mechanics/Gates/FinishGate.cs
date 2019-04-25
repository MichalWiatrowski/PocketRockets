using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishGate : MonoBehaviour {

    public GameObject winnerScreen;
    int count = 1;
    public Transform winText;
    public AudioClip winningClip;
    private AudioSource winningSource;
    public GameObject sceneManager;
    void Awake()
    {
        winningSource = GetComponent<AudioSource>();
    }

   void OnTriggerEnter(Collider collided)
    {
       // check to see if trap is colliding with a vehicle
        if (collided.CompareTag("Player"))
        {
            winningSource.PlayOneShot(winningClip, 0.1f);
            FindWinner(collided);
        }
    }

    void FindWinner(Collider player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        if (count == 1)
        {
            stats.setWinner(true);

            winText.gameObject.SetActive(true);
        }
        else if (count == networkServerUIbuttons.networkServer.getPlayerAmount())
        {
            sceneManager.GetComponent<serverGameManager>().gameStarted = false;
            winnerScreen.transform.localScale = new Vector3(1, 1, 1);
            winnerScreen.gameObject.SetActive(true);
            //StartCoroutine(RestartGame());
        }

       
            winnerScreen.transform.GetChild(0).GetChild(count - 1).GetChild(0).GetComponent<Text>().text = stats.getPlayerName();
            winnerScreen.transform.GetChild(0).GetChild(count - 1).GetChild(1).GetComponent<Text>().text = "" + count;
            winnerScreen.transform.GetChild(0).GetChild(count - 1).GetChild(2).GetComponent<Text>().text = "" + stats.getPlayerTime();
       
        



        count++;

        int test = networkServerUIbuttons.networkServer.getPlayerAmount();


       
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2);
        networkServerUIbuttons.networkServer.restartGame();
    }
}
