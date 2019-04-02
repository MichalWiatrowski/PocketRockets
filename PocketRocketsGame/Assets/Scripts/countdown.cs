using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class countdown : MonoBehaviour {

    public List<Transform> players;
    public Text countText;
    PlayerStats[] stats = new PlayerStats[4];

    public AudioClip startClip;
    public GameObject Camera;

    void Start()
    {
        StartCoroutine(CountdownStart());
    }

    IEnumerator CountdownStart()
    {

        Camera.transform.GetComponent<AudioSource>().PlayOneShot(startClip);
        for (int i = 0; i < 4; i++)
        {
            if (networkServerUIbuttons.networkServer.playersConnected[i] == 1)
            {
                stats[i] = players[i].GetComponent<PlayerStats>();
                stats[i].setSpeed(0);
                stats[i].setCurrentLane(i);
                stats[i].setSwitchLeft(false);
                stats[i].setSwitchStateL(0);
                stats[i].setSwitchRight(false);
                stats[i].setSwitchStateR(0);
            }
        }
        networkServerUIbuttons.networkServer.sendSwitchStateL();
        networkServerUIbuttons.networkServer.sendSwitchStateR();

        Color newColour = countText.color;

        countText.gameObject.SetActive(true);
        countText.text = "3";

        for (float i = 0; i < 1; i += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            newColour.a = 1.0f - i;
            countText.color = newColour;
        }


        countText.text = "2";

        for (float i = 0; i < 1; i += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            newColour.a = 1.0f - i;
            countText.color = newColour;
        }

        countText.text = "1";

        for (float i = 0; i < 1; i += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            newColour.a = 1.0f - i;
            countText.color = newColour;
        }

        countText.text = "Go!";

        for (float i = 0; i < 1; i += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
            newColour.a = 1.0f - i;
            countText.color = newColour;
        }

        countText.gameObject.SetActive(false);

       

        for (int i = 0; i < 4; i++)
        {
            if (networkServerUIbuttons.networkServer.playersConnected[i] == 1)
            {
                stats[i].resetSpeed();
                stats[i].setSwitchRight(true);
                stats[i].setSwitchStateR(1);
            }
        }
        networkServerUIbuttons.networkServer.sendSwitchStateL();
        networkServerUIbuttons.networkServer.sendSwitchStateR();

    }
}
