using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameMangent : MonoBehaviour {
	public Text[] btnTxt = new Text[0];
	public Text nowNum;
	public Text timeTxt;

	public string[] numArr = new string[16];
	private string[] ansArr = new string[16];

	private CanvasGroup btnArea;
	private int currentP;
	private float timer;
	private bool isPlay;

	public Text[] graTxt = new Text[5];
	private float[] graArr = new float[6];
	public GameObject graPanel;

	public Text startCount;
	private int startInt = 5;
	public GameObject startAnimation;

	// Use this for initialization
	void Start () {
		for(int i=0; i<numArr.Length; i++)
		{
			ansArr[i] = numArr[i];
		}
		btnArea = GameObject.Find ("CenterArea").GetComponent<CanvasGroup> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlay)
		{
			timer += Time.deltaTime;
			timeTxt.text = string.Format("{0:0.0}", timer);
		}
	}

	public void ClickStartBTN()  //**********************
	{
		startInt = 5;
		InvokeRepeating ("StartCount", 0, 1);
	}

	public void CheckAns(Text ans)
	{
		if(ans.text == ansArr[currentP])
		{
			currentP ++;
			nowNum.text = currentP.ToString();
		}

		if(currentP == ansArr.Length)
		{
			isPlay = false;
			btnArea.interactable = false;
			EndGame();
		}
	}

	//-----------------------------------
	void NumRange()
	{
		int count = Random.Range (12, 25);
		int posX;
		int posY;
		string temp;

		for(int i =0; i< count; i++)
		{
			posX = Random.Range (0, 16);
			posY = Random.Range (0, 16);
			temp = numArr[posX];
			numArr[posX] = numArr[posY];
			numArr[posY] = temp;
		}
	}

	void PutNumTxt()
	{
		for(int i = 0; i<btnTxt.Length; i++)
		{
			btnTxt[i].text = numArr[i];
		}
	}

	void TextReSet()
	{
		currentP = 0;
		timer = 0f;
		isPlay = true;

		nowNum.text = currentP.ToString ();
	}

	//----------------------------------------------	
	void AddGrade()
	{
		for(int i =0; i< graArr.Length; i++)
		{
			if(graArr[i] == 0)
			{
				graArr[i] = timer;
				break;
			}
		}

		for (int i = 0; i< graArr.Length; i++)
			print (graArr [i]);
	}

	void Ranking()
	{
		float temp;

		for(int i = 0; i < graArr.Length; i++)
		{
			for(int j = i+1 ; j < graArr.Length; j++)
			{
				if(graArr[i] > graArr[j] && graArr[j] != 0)
				{
					temp = graArr[i];
					graArr[i] = graArr[j];
					graArr[j] = temp;
				}
			}
		}

		graArr [graArr.Length - 1] = 0;
	}

	void SetGraTxt()
	{
		for(int i = 0; i < graTxt.Length; i++)
		{
			if(graArr[i] != 0)
				graTxt[i].text = "(" + (i+1) + ") --------- " + string.Format("{0:0.0}", graArr[i]);
			else
				graTxt[i].text = "";
		}
	}

	void EndGame()
	{
		graPanel.SetActive(true);
		AddGrade ();
		Ranking ();
		SetGraTxt ();
	}

	//---------------------------------------
	void StartCount()
	{
		startInt --;
		startCount.text = startInt.ToString ();
		if (startInt == 0)
		{
			CancelInvoke("StartCount");
			startAnimation.SetActive(false);
			NumRange ();
			PutNumTxt ();
			TextReSet ();
		}
	}
}
