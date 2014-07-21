using System;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp.gift;
namespace AssemblyCSharp
{
	public class GiftCountCallBack : App42CallBack
	{
		public static string  giftName = "";
		public static int  giftCount=0;
		
		public void OnSuccess (object obj)
		{

			if (obj is App42Response) {
				App42Response resposne = (App42Response)obj;
				//	result = gameObj.ToString ();
				Debug.Log ("Gift Response : " + resposne.GetTotalRecords ());
				giftCount=resposne.GetTotalRecords ();
				
				
			}
		}
		
		public void OnException (Exception e)
		{
			//	result = e.ToString ();
			Debug.Log ("EXCEPTION : " + e);
			
		}
		
	}
	
}




