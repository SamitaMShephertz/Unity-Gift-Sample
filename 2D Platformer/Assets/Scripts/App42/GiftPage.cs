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
using com.shephertz.app42.paas.sdk.csharp.gift;


public class GiftPage : MonoBehaviour {
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
		ShowApp42GiftBoard();
		ShowApp42GiftBoard1();
	}
	void OnGUI()
	{
		GUILayout.BeginArea(new Rect (Screen.width/2 - 150, Screen.height/2 - 250, 800,1000));
		GUI.contentColor = Color.black;	
		GUIStyle myMenustyle = new GUIStyle (GUI.skin.label);
		myMenustyle.fontStyle = FontStyle.BoldAndItalic;			
		myMenustyle.fontSize = 40;

		GUILayout.Label("Gifts Record",myMenustyle, GUILayout.Width(450), GUILayout.Height (50));

		GUILayout.BeginVertical();
		GUILayout.BeginArea(new Rect (10,100,700,600));
		GUILayout.BeginVertical();
		GUI.contentColor = Color.black;	
		GUIStyle myButtonStyle = new GUIStyle (GUI.skin.label);
		myButtonStyle.fontStyle = FontStyle.Bold;			
		myButtonStyle.fontSize = 18;
		GUILayout.Space(10);		
		GUILayout.Label("GiftName",myButtonStyle, GUILayout.Width(150), GUILayout.Height (30));
		GUILayout.Label("Coins",myButtonStyle, GUILayout.Width(150), GUILayout.Height (30));
		GUILayout.Label("MyStery box",myButtonStyle, GUILayout.Width(150), GUILayout.Height (30));

		GUILayout.EndVertical ();
		GUILayout.EndArea ();

			GUILayout.BeginArea (new Rect (50, 250, 220, 320));
			if (GUILayout.Button ( "Main Menu",GUILayout.Width (80), GUILayout.Height (30))) {
				ScoreBoardResponse.playerDetails.Clear();
				Application.LoadLevel("choose");
			}
			GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (150, 250, 220, 320));
			if (GUILayout.Button ( "Exit",GUILayout.Width (80), GUILayout.Height (30))) {
				
				Application.Quit();
			}
			GUILayout.EndArea ();

		GUILayout.BeginArea(new Rect (200,100,700,600));
		GUILayout.BeginVertical();
		GUI.contentColor = Color.black;	

		GUILayout.Space(10);	
		GUILayout.Label("Count",myButtonStyle, GUILayout.Width(150), GUILayout.Height (30));
		GUILayout.BeginArea(new Rect (20,45,700,600));
		GUILayout.Label(""+GiftCallBack.giftCount,myButtonStyle, GUILayout.Width(150), GUILayout.Height (30));
		GUILayout.Label(""+GiftCountCallBack.giftCount,myButtonStyle, GUILayout.Width(150), GUILayout.Height (30));

		GUILayout.EndArea ();

		GUILayout.EndVertical ();
		GUILayout.EndArea ();

		GUILayout.BeginArea(new Rect (00,300,700,600));
		GUIStyle myButtonStyle2 = new GUIStyle (GUI.skin.label);
		myButtonStyle2.fontStyle = FontStyle.Bold;			
		myButtonStyle2.fontSize = 10;
		GUILayout.Space(5);
		GUILayout.Label("Note : On Every 1000 Score you will get 1 Coin as a gift.",myButtonStyle2, GUILayout.Width(500), GUILayout.Height (20));
		GUILayout.Label("           On Every 5000 Score you will get 1 MyStery box as a gift.",myButtonStyle2, GUILayout.Width(500), GUILayout.Height (30));

		GUILayout.EndArea ();

		GUILayout.EndVertical ();
		GUILayout.EndArea ();
		
		
	}//OnGUI End.
	public void ShowApp42GiftBoard()
	{

		GiftService giftService = serviceAPI.BuildGiftService();    
		GiftCallBack callBack = new GiftCallBack ();
		giftService.GetGiftCount(Constant.giftName, App42API.GetLoggedInUser(),  callBack);
	}
	public void ShowApp42GiftBoard1()
	{
		GiftService giftService = serviceAPI.BuildGiftService();  
		GiftCountCallBack callBack = new GiftCountCallBack ();
		giftService.GetGiftCount(Constant.giftName1, App42API.GetLoggedInUser(),  callBack);
	}
}


