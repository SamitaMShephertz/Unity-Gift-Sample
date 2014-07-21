
	using UnityEngine;
	using UnityEngine.SocialPlatforms;
	using System.Collections;
	using System.Collections.Generic;
	using System.Net;
	using System;
	using com.shephertz.app42.paas.sdk.csharp;
	using com.shephertz.app42.paas.sdk.csharp.user;
	using AssemblyCSharp;
using System.Text.RegularExpressions;
using com.shephertz.app42.paas.sdk.csharp.gift;

	public class Login: MonoBehaviour 
	{
		ServiceAPI serviceAPI;
		UserService userService; // Initializing User Service.
	GiftService giftService = null;
		UserResponse callBack = new UserResponse ();
		public  string userTxtFld;
		public  string passwordTxtFld;
		public  string emailIdTxtFld;
		public static string userNameTxtFld;
		public static string exceptionMessage;
		public static Boolean  mainmenu   =false;
	public string path = "";
		void Start ()
		{
			#if UNITY_EDITOR
			//ServicePointManager.ServerCertificateValidationCallback = Validator;
			#endif
				serviceAPI = new ServiceAPI (Constant.apiKey,Constant.secretKey);
		path = Application.dataPath + "/StreamingAssets";
		giftService = serviceAPI.BuildGiftService();
		giftService.CreateGift(Constant.giftName,path+"/coin.jpg" , "Coins" ,"Increase 1000 score","Game",new GiftCallBack());
		giftService.CreateGift(Constant.giftName1,path+"/box.jpg" , "MyStery box" ,"Increase 2000 score","Game",new GiftCallBack ());
		//path = ;
		}

		void OnGUI()
		{
		GUILayout.BeginArea(new Rect (Screen.width/2 - 250, Screen.height/2 - 250, 800,1000));
		GUILayout.BeginVertical();
		GUI.contentColor = Color.white;	
				GUIStyle myButtonStyle = new GUIStyle (GUI.skin.label);
				myButtonStyle.fontStyle = FontStyle.Bold;			
				myButtonStyle.fontSize = 18;	

		GUIStyle boxStyle = new GUIStyle (GUI.skin.box);
		boxStyle.fontStyle = FontStyle.Bold;			
		boxStyle.fontSize = 18;	
		GUIStyle mytextStyle = new GUIStyle (GUI.skin.textField);
		mytextStyle.fontStyle = FontStyle.Bold;			
		mytextStyle.fontSize = 18;	

		GUILayout.BeginArea (new Rect (120, 130, 320, 320));
		GUILayout.Box ( "Login Screen", boxStyle,GUILayout.Width (320), GUILayout.Height (280));
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (125, 195, 320, 320));
		GUILayout.Label ("User Name",myButtonStyle,GUILayout.Width (250), GUILayout.Height (40)); 
		GUILayout.Label ("Password",myButtonStyle,GUILayout.Width (250), GUILayout.Height (40)); 
		GUILayout.Label ( "Email Id",myButtonStyle,GUILayout.Width (250), GUILayout.Height (40));
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (225, 190, 320, 320));
		userTxtFld = GUILayout.TextField( userTxtFld, 25,mytextStyle ,GUILayout.Width (200), GUILayout.Height (40));

		passwordTxtFld= GUILayout.PasswordField( passwordTxtFld ,"*"[0], 25,mytextStyle ,GUILayout.Width (200), GUILayout.Height (40));

		emailIdTxtFld= GUILayout.TextField( emailIdTxtFld , 25,mytextStyle ,GUILayout.Width (200), GUILayout.Height (40));

		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (145, 340, 320, 320));
		if (GUILayout.Button ("Login",GUILayout.Width (100), GUILayout.Height (30))) {
			App42API.SetLoggedInUser(userTxtFld);		
			userService = serviceAPI.BuildUserService (); // Initializing UserService.
			userService.Authenticate(userTxtFld, passwordTxtFld, callBack );

		}
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (305, 340, 320, 320));

		if (GUILayout.Button ( "Register",GUILayout.Width (100), GUILayout.Height (30))) {
			App42API.SetLoggedInUser(userTxtFld);
			userService = serviceAPI.BuildUserService (); // Initializing UserService.
			userService.CreateUser(userTxtFld, passwordTxtFld,emailIdTxtFld, callBack );
			
		}
		GUILayout.EndArea ();
		GUILayout.BeginArea (new Rect (130, 410, 320, 320));
		if(mainmenu)
		{
			GUIStyle myButtonStyle1 = new GUIStyle (GUI.skin.label);
			myButtonStyle1.fontStyle = FontStyle.Bold;			
			myButtonStyle1.fontSize = 18;	
			GUILayout.Label(exceptionMessage , myButtonStyle1,GUILayout.Width (320), GUILayout.Height (320)); 
			
		}
		GUILayout.EndArea ();
		GUILayout.EndVertical ();
		GUILayout.EndArea ();

		}
			
}

