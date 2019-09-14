using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


	[ExecuteInEditMode]
	[SerializeField]
	[System.Serializable]	
public class Type {
	public LevelType levelType;
	public ScoreType scoreType;

}
	[ExecuteInEditMode]
	[System.Serializable]	
public class LevelType {
	public bool levelType = false;
	public bool useOneText = false;
	public Text LeftText,RightText;
	public Text oneText;
}
	[ExecuteInEditMode]
	[System.Serializable]	
public class ScoreType {
	public bool scoreType = false;
	public Text scoreText;
}
	[ExecuteInEditMode]
	[SerializeField]
	[System.Serializable]
public class Setting {
	[Header("Shape Setting")]
	public  GameObject[] ShapeOut;
	public  GameObject[] ShapeProgresBar;
	public Sprite spriteProgressBar;
	public Color BackColorBar = new Color (1,1,1,1),colorProgressBar = new Color (1,1,1,1);
	[Range(0f,1f)]
	public float fillAmountBar = 0.5f;
	public Sprite spriteShape;
	public Color colorShape = new Color (1,1,1,1);
	[Header("Text Setting")]
	public GameObject[] textobj;
	public Font textFont;
	public Color textColor = new Color(0,0,0,1);
	

}
	[ExecuteInEditMode]
	[System.Serializable]
public class Mode {
		public bool CountCheck = false;
		[Range(0.01f, 0.1f)]
		public float countMax = 0.01f;
		public bool useBackProgress = true;
		[Range(0.01f, 0.1f)]
		public float ProgressMinuseSpeed = 0.01f;
}
	[ExecuteInEditMode]
	[System.Serializable]
public class DopeVariable {
		public float score =0;
		public int ProgressBarDiv = 1;
		public int level = 1;
}
	[ExecuteInEditMode]
	[SerializeField]

public class ScorePlug : MonoBehaviour {
	
	public bool start = false;
	public bool setobject = true;
	public bool RedactorPlugin = false;
		[Range(0f, 1f)]
		private  float progressMax =0;
		public Type typeProgressBar;
		public Setting setting;
		public Mode mode;
		public DopeVariable dopeVariable;
		
	 void Inicializetion(){
		 if (typeProgressBar.levelType.levelType) {
			 if (!typeProgressBar.levelType.useOneText) {
				typeProgressBar.levelType.LeftText.text = "1";
				typeProgressBar.levelType.RightText.text = "2";
			 }else typeProgressBar.levelType.oneText.text = "1";
		 }
		 if (typeProgressBar.scoreType.scoreType){
			 typeProgressBar.scoreType.scoreText.text = "0";
		 }
		dopeVariable.level = 1;
		setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount = 0;
	}
	void IncShape() {
		//Sprite Inicializetion
		for (int i =0;i < setting.ShapeOut.Length;i++) setting.ShapeOut[i].GetComponent<Image>().sprite = setting.spriteShape;
		for (int i =0;i < setting.ShapeProgresBar.Length;i++) setting.ShapeProgresBar[i].GetComponent<Image>().sprite = setting.spriteProgressBar;
		//Color Inicializetion
		for (int i =0;i < setting.ShapeOut.Length;i++) setting.ShapeOut[i].GetComponent<Image>().color = setting.colorShape;
		setting.ShapeProgresBar[0].GetComponent<Image>().color = setting.BackColorBar; // Задний цвет
		setting.ShapeProgresBar[1].GetComponent<Image>().color = setting.colorProgressBar; //Передний цвет
		setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount = setting.fillAmountBar; //Ползунок в настройке

		//Text Color
		for (int i =0;i < setting.textobj.Length;i++) setting.textobj[i].GetComponent<Text>().color = setting.textColor;
		//Text Font
		for (int i =0;i < setting.textobj.Length;i++) setting.textobj[i].GetComponent<Text>().font = setting.textFont; 
		//Lock
		progressMax = 0;
	}
    void Awake(){
		if (!RedactorPlugin ) {
			IncShape();
			Inicializetion();
			
		} 
		
    }
	bool Progress = true;
	void Update(){
		if (setobject){
			 RedactorPlugin = false;
			 start = false;
		}else{
			if (start){	
				if(Progress) ProgressPlus(); else ProgressMinuse();
				RedactorPlugin = false;
			}else {
				RedactorPlugin = true;
			}
			if (RedactorPlugin)  IncShape();
			
		}
		if (typeProgressBar.scoreType.scoreType) typeProgressBar.scoreType.scoreText.text = ""+dopeVariable.score;
		
	}
	void ProgressPlus() {
		float progressOnline;
		progressOnline = setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount;
			if (progressOnline < progressMax) {
				switch (mode.CountCheck) {
					case true : CountTrue(mode.countMax); break;	
					case false : TimeTrue(); break;
				}
			}
			if (progressOnline >= 1) {
				Progress = false;
				dopeVariable.level++; //++Level
				if (typeProgressBar.levelType.levelType) {
					if (!typeProgressBar.levelType.useOneText) {
						typeProgressBar.levelType.RightText.text = ""+(dopeVariable.level+1);
					}else typeProgressBar.levelType.oneText.text = ""+dopeVariable.level;
				} 
				
			}
	}
	void ProgressMinuse() {
		float progressOnline;
		progressOnline = setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount;
		if (!mode.useBackProgress){
			Progress = true;
			setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount = 0;
		}
		progressMax = 0;
		if (progressOnline > 0) {
				StartCoroutine(ResetProgress());
		}else {
			Progress = true;
			 if (typeProgressBar.levelType.levelType) {
					if (!typeProgressBar.levelType.useOneText) {
						typeProgressBar.levelType.LeftText.text = ""+dopeVariable.level;
					}else typeProgressBar.levelType.oneText.text = ""+dopeVariable.level;
				}
		}
	}
	IEnumerator ResetProgress() {
		yield return 0;//new WaitForSeconds(0.01f);
		setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount -= mode.ProgressMinuseSpeed;
	}
	void TimeTrue() {
		float test;
		if (setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount < 1) {
			test = Time.deltaTime;
			setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount += test;
		}else{
			setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount = 0;	
		}
	}
	void CountTrue(float max) {
		if (setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount < 1) {
			setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount += max;
		}else{
			setting.ShapeProgresBar[1].GetComponent<Image>().fillAmount = 0;	
		}
	}
	
	public void testKlick() {
		progressMax += 1f/dopeVariable.ProgressBarDiv;
		
		if(progressMax > 1) progressMax = 1;
	}
	

}
