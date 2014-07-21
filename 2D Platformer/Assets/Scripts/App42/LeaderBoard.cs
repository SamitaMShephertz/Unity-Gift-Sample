using UnityEngine;
using System;
using System.Collections;
using System.Threading;
using UnityEngine.SocialPlatforms;
using System.Text.RegularExpressions;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;
using System.Collections.Generic;
using AssemblyCSharp;


public class LeaderBoard : MonoBehaviour {
	ServiceAPI serviceAPI;
	Dictionary <string , object> dist = new Dictionary<string, object>();
	private Vector2 scrollPosition = Vector2.zero;
	private bool app42 = true;
	public string scoreFromTxtFld;
	public static Boolean  yourScore   =true;
	public static Boolean  yourGift   =true;
	public static Boolean  block   =true;
	void Start ()
	{
		#if UNITY_EDITOR
		//ServicePointManager.ServerCertificateValidationCallback = Validator;
		#endif
		serviceAPI = new ServiceAPI (Constant.apiKey,Constant.secretKey);
		ShowApp42GlobalLeaderBoard();
	}
	void OnGUI()
	{
		GUILayout.BeginArea(new Rect (Screen.width/2 - 250, Screen.height/2 - 250, 800,1000));
		GUILayout.BeginVertical();
		GUILayout.BeginArea(new Rect (40,150,700,600));
		GUILayout.BeginVertical();
		GUI.contentColor = Color.black;	
		GUIStyle myButtonStyle = new GUIStyle (GUI.skin.label);
		myButtonStyle.fontStyle = FontStyle.Bold;			
		myButtonStyle.fontSize = 18;
		GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(350),GUILayout.Width(540));
		foreach(KeyValuePair<string,double> details in ScoreBoardResponse.GetPlayerDetails()){
			GUILayout.BeginHorizontal();
			GUILayout.Label(details.Key,myButtonStyle, GUILayout.Width(150));
			GUILayout.Space(120);
			GUILayout.Label(details.Value.ToString(),myButtonStyle, GUILayout.Width(150));
			GUILayout.EndHorizontal();
		}
		GUILayout.EndScrollView ();		

		GUILayout.EndVertical();
		GUILayout.EndArea();
		if(block){
		GUILayout.BeginArea (new Rect (170, 400, 220, 320));
		if (GUILayout.Button ( "Restart",GUILayout.Width (80), GUILayout.Height (30))) {
			ScoreBoardResponse.playerDetails.Clear();
			Application.LoadLevel("level");
		}
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (265, 400, 220, 320));
		if (GUILayout.Button ( "Exit",GUILayout.Width (80), GUILayout.Height (30))) {

			Application.Quit();
		}
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (200, 290, 320, 320));
		if(yourScore)
		{
			GUIStyle myButtonStyle1 = new GUIStyle (GUI.skin.label);
			myButtonStyle1.fontStyle = FontStyle.BoldAndItalic;			
			myButtonStyle1.fontSize = 18;	
			GUILayout.Label("Your Score  is " + Remover.savedScore , myButtonStyle1,GUILayout.Width (320), GUILayout.Height (320)); 
		}

		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (100, 320, 320, 320));
		if(yourGift)
		{
			GUIStyle myButtonStyle1 = new GUIStyle (GUI.skin.label);
			myButtonStyle1.fontStyle = FontStyle.BoldAndItalic;			
			myButtonStyle1.fontSize = 18;	
			GUILayout.Label("Congratulations!! your won " + GiftCallBack.giftName , myButtonStyle1,GUILayout.Width (320), GUILayout.Height (320)); 
		}
		
		GUILayout.EndArea ();
	}
		GUILayout.BeginArea (new Rect (210, 360, 220, 320));
		if (GUILayout.Button ( "Main Menu",GUILayout.Width (80), GUILayout.Height (30))) {
			
			Application.LoadLevel("choose");
		}
		GUILayout.EndArea ();


		GUILayout.EndVertical ();
		GUILayout.EndArea ();

		
	}//OnGUI End.
	public void ShowApp42GlobalLeaderBoard()
	{
		yourScore= true;
		ScoreBoardResponse.playerDetails.Clear ();
		ScoreBoardService scoreBoardService = serviceAPI.BuildScoreBoardService();    
		ScoreBoardResponse callBack = new ScoreBoardResponse ();
		scoreBoardService.GetTopNRankers(Constant.gameName, 5, callBack);
	}
}
