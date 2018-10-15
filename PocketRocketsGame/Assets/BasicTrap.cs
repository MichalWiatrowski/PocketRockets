using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTrap : MonoBehaviour {

   public float timer = 2.0f;
    private bool trigger = false;
    private bool initTrigger = true;

	// Use this for initialization
	void Start () {
		
	}
	
    private void trapTrigger()
    {

        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            timer = 2.0f;
            trigger = false;
         
        }


        if (initTrigger)
        {
            transform.Translate(0, 2, 0);
            initTrigger = false;
        }
        else if (!trigger)
        {
            transform.Translate(0, -2, 0);
        }
    }

    public void setTrigger()
    {
        trigger = true;
        initTrigger = true;
    }
    public bool getTrigger()
    {
        return trigger;
    }

	// Update is called once per frame
	void Update () {

		if (trigger == true)
        {
            trapTrigger();
        }
	}
}
