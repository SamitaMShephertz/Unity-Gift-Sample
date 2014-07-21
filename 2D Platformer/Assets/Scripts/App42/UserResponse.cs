using System;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine;
using System.Collections;

using System.Collections.Generic;
namespace AssemblyCSharp
{
	public class UserResponse : App42CallBack
	{
		private string result = "";


		public void OnSuccess(object user)
		{
			try
			{
				// ... and then reload the level.
				Application.LoadLevel("choose");
				if (user is User)
				{
					User userObj = (User)user;
					result = userObj.ToString();
					Debug.Log ("UserName : " + userObj.GetUserName());
					Debug.Log ("EmailId : " + userObj.GetEmail());
				}
			}
			catch (App42Exception exception)
			{
				result =exception.ToString();
				Debug.Log ("App42Exception : "+ exception);

			}
		}
		
		public void OnException(Exception exception)
		{
			result = exception.ToString();
			Debug.Log ("Exception : " + exception);
			App42Exception ex = (App42Exception)exception;

			if (ex.GetAppErrorCode () == 2005) {

				Login.exceptionMessage ="User with emailId  already exists." ;
						}
			if (ex.GetAppErrorCode () == 2001) {
				
				Login.exceptionMessage ="Username already exists" ;
			}
			if (ex.GetAppErrorCode () == 2002) {
				
				Login.exceptionMessage ="UserName/Password did not match." ;
			}
			Login.mainmenu = true;
			Debug.Log (Login.exceptionMessage);


		}
		



	}
}

