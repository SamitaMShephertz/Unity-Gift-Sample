
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


public class Choose : MonoBehaviour {
	ServiceAPI serviceAPI;
	Dictionary <string , object> dist = new Dictionary<string, object>();
	private Vector2 scrollPosition = Vector2.zero;
	private bool app42 = true;
	public string scoreFromTxtFld;
	public static Boolean  yourScore   =true;
	public static Boolean  yourGift   =true;
	void Start ()
	{
		#if UNITY_EDITOR
		//ServicePointManager.ServerCertificateValidationCallback = Validator;
		#endif
		serviceAPI = new ServiceAPI (Constant.apiKey,Constant.secretKey);

	}
	void OnGUI()
	{
		GUILayout.BeginArea(new Rect (Screen.width/2 - 50, Screen.height/2 - 50, 800,1000));
		GUILayout.BeginVertical();

		GUIStyle myButtonStyle = new GUIStyle (GUI.skin.button);
		myButtonStyle.fontStyle = FontStyle.BoldAndItalic;			
		myButtonStyle.fontSize = 42;
		Texture textImage = new Texture ();
		if (GUILayout.Button ( "Play",myButtonStyle, GUILayout.Width (170), GUILayout.Height (50))) {
			ScoreBoardResponse.playerDetails.Clear();
			LeaderBoard.yourScore=true;
			LeaderBoard.yourGift=true;	
			Application.LoadLevel("level");
		}
		GUILayout.Space(5);
		GUIStyle myButtonStyle1 = new GUIStyle (GUI.skin.button);
		myButtonStyle1.fontStyle = FontStyle.BoldAndItalic;			
		myButtonStyle1.fontSize = 22;
		GUILayout.Space(15);
		if (GUILayout.Button ( "LeaderBoard",myButtonStyle1,GUILayout.Width (170), GUILayout.Height (30))) {
			LeaderBoard.yourGift= false;
			LeaderBoard.yourScore= false;
			LeaderBoard.block= false;
			Application.LoadLevel("scoreBoard");
		}
		GUILayout.Space(15);
		if (GUILayout.Button ( "Gifts",myButtonStyle1,GUILayout.Width (170), GUILayout.Height (30))) {
			
			Application.LoadLevel("gift");
		}


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


