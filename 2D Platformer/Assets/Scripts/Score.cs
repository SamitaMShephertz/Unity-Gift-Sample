﻿using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using AssemblyCSharp;

public class Score : MonoBehaviour
{
	public int score = 0;					// The player's score.


	private PlayerControl playerControl;	// Reference to the player control script.
	private int previousScore = 0;			// The score in the previous frame.


	void Awake ()
	{
		// Setting up the reference.
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}


	void Update ()
	{
		// Set the score text.
		guiText.text = "Score: " + score;


	
		// If the score has changed...
		if(previousScore != score)
			// ... play a taunt.
			playerControl.StartCoroutine(playerControl.Taunt());
	
		// Set the previous score to this frame's score.
		previousScore = score;		  

	}

}