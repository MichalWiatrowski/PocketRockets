using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FontEditer : MonoBehaviour {

    private Text[] GetText;

    // Use this for initialization
    void Awake () {
        GetText = Text.FindObjectsOfType<Text>();

        foreach (Text go in GetText)
        {
            go.font = Resources.Load<Font>("Fonts/JazzCreateBubble");
            go.fontSize = 35;
            go.color = Color.magenta;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
