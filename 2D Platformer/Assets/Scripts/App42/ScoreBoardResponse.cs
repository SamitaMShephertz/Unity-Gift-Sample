using System;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class ScoreBoardResponse : App42CallBack
	{
		public static Dictionary<string, double> playerDetails = new Dictionary<string, double>();
		public void OnSuccess (object obj)
		{
			if (obj is Game) {
				Game gameObj = (Game)obj;
			//	result = gameObj.ToString ();
				Debug.Log ("GameName : " + gameObj.ToString ());
				if (gameObj.GetScoreList () != null) {
					IList<Game.Score> scoreList = gameObj.GetScoreList ();
					for (int i = 0; i < scoreList.Count; i++) {
						Debug.Log ("UserName is  : " + scoreList [i].GetUserName ());
		
						playerDetails.Add(scoreList [i].GetUserName (), scoreList [i].GetValue ());
						Debug.Log ("Value is  : " + scoreList [i].GetValue ());
					}
				}
			} 
			LeaderBoard.yourScore = false;
		}
		
		public void OnException (Exception e)
		{
		//	result = e.ToString ();
			Debug.Log ("EXCEPTION : " + e);
	
		}
		
		public static Dictionary<string,double> GetPlayerDetails ()
		{
			return playerDetails;
		}
		
	}
      
}

