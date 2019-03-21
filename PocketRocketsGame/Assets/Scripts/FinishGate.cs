using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGate : MonoBehaviour {

    int count = 1;
    public Transform winText;
    public AudioClip winningClip;
    private AudioSource winningSource;

    void Awake()
    {
        winningSource = GetComponent<AudioSource>();
    }

   void OnTriggerEnter(Collider collided)
    {
       // check to see if trap is colliding with a vehicle
        if (collided.CompareTag("Player"))
        {
            //winningSource.PlayOneShot(winningClip, 0.1f);
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
            StartCoroutine(RestartGame());
        }
        count++;

        int test = networkServerUIbuttons.networkServer.getPlayerAmount();
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2);
        networkServerUIbuttons.networkServer.restartGame();
    }
}
