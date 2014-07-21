using UnityEngine;
using System.Collections;
using com.shephertz.app42.paas.sdk.csharp.game;
using com.shephertz.app42.paas.sdk.csharp;
using AssemblyCSharp;
using com.shephertz.app42.paas.sdk.csharp.gift;
using System.Collections.Generic;

public class Remover : MonoBehaviour
{
	public GameObject splash;
	public static int savedScore;
	ServiceAPI serviceAPI;
	void Start ()
	{
		#if UNITY_EDITOR
		//ServicePointManager.ServerCertificateValidationCallback = Validator;
		#endif
		serviceAPI = new ServiceAPI (Constant.apiKey,Constant.secretKey);
	//	serviceAPI.SetBaseURL ("http://", "192.168.1.33", 8082);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			// .. stop the Health Bar following the player
			if(GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
			{
				GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
			}

			// ... instantiate the splash where the player falls in.
			Instantiate(splash, col.transform.position, transform.rotation);
			// ... destroy the player.
			Destroy (col.gameObject);
			// ... reload the level.
			StartCoroutine("ReloadGame");
		}
		else
		{
			// ... instantiate the splash where the enemy falls in.
			Instantiate(splash, col.transform.position, transform.rotation);

			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		//Application.LoadLevel(Application.loadedLevel);
		//Debug.Log (GameObject.Find ("Score").GetComponent<Score>().score);
		savedScore = GameObject.Find ("Score").GetComponent<Score> ().score;
		SaveScore (savedScore);
		LeaderBoard.yourGift = false;
		Application.LoadLevel("scoreBoard");
	}
	void SaveScore (int score)
	{
		LeaderBoard.block = true;
		GiftService giftService = serviceAPI.BuildGiftService ();
		ScoreBoardService scoreBoardService = serviceAPI.BuildScoreBoardService (); 
		SaveScoreCallBack callBack = new SaveScoreCallBack ();
		GiftCallBack giftCallBack = new GiftCallBack ();
		IList<string> recipientsList = new List<string>();  
		recipientsList.Add(App42API.GetLoggedInUser ());  
		if (score > 5000) {
			score= score+2000;
			LeaderBoard.yourGift = true;
			giftService.DistributeGifts(Constant.giftName1,recipientsList,1,giftCallBack);
				scoreBoardService.SaveUserScore (Constant.gameName, App42API.GetLoggedInUser (), score, callBack);
				}
		else if(score<5000 && score>1000){
			score= score+1000;			
			LeaderBoard.yourGift = true;
			giftService.DistributeGifts(Constant.giftName,recipientsList,1,giftCallBack);
			scoreBoardService.SaveUserScore (Constant.gameName, App42API.GetLoggedInUser (), score, callBack);
		}
		else{	
			LeaderBoard.yourGift=false;
			scoreBoardService.SaveUserScore (Constant.gameName, App42API.GetLoggedInUser (), score, callBack);
		}
		savedScore=score;
}
}
