using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class countdown : MonoBehaviour {

    public List<Transform> players;
    public Text countText;
    PlayerStats[] stats = new PlayerStats[4];

    void Start()
    {
        StartCoroutine(CountdownStart());
    }

    IEnumerator CountdownStart()
    {
        for (int x = 0; x < networkServerUIbuttons.networkServer.getPlayerAmount(); x++)
        {
            stats[x] = players[x].GetComponentInParent<PlayerStats>();
            stats[x].speed = 0;
        }

        countText.gameObject.SetActive(true);
        countText.text = "3";

        yield return new WaitForSeconds(1);

        countText.text = "2";

        yield return new WaitForSeconds(1);

        countText.text = "1";

        yield return new WaitForSeconds(1);

        countText.text = "Go!";

        yield return new WaitForSeconds(1);

        countText.gameObject.SetActive(false);

        for (int x = 0; x < networkServerUIbuttons.networkServer.getPlayerAmount(); x++)
        {
            stats[x].speed = stats[x].getDefaultSpeed();
        }
        
    }
}
