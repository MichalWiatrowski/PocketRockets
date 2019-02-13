using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{

    public List<GameObject> players;
    GameObject[] playersSorted = new GameObject[4];
    int playerCount = 4;
    float timer = 0;

    //// Use this for initialization
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    timer += Time.deltaTime;

    //    if (timer > 3)
    //    {
    //        SortPositions();
    //    }
    //}

    //void SortPositions()
    //{
    //    playersSorted = playersSorted.OrderBy(players => players.transform.position.z).ToArray();

    //    for (int x = 0; x < playerCount; x++)
    //    {
    //        playersSorted[x].GetComponent<PlayerStats>().position = playerCount - x;
    //    }

    //    timer = 0;
    //}
}


