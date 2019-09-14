using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class MainGame : MonoBehaviour
{
    public int score = -1;
    public Text score_t;
    public Text sentence_t;
    public int time =-1;
    const int size = 5;
    string[][] sentence  = new string[size][];
    public Text[] card ;
    public GameObject[] card_x;
    public GameObject ProgressBar;
    public GameObject slot;
    public float progressValue = 1 ;
    public GameObject Hand;
    public GameObject panelLose;
    public GameObject click;
    
    int sentence_num;
    
    void Start(){
        sentence[0] = new string[] {"what is your name?","Как тебя зовут?","name?","is","what","your"};
        sentence[1] = new string[] {"my name is Alexander","Моё имя александр","my","name","is","Alexander"};
        sentence[2] = new string[] {"it is rainy today?","Сегодня дождливая погода?","is","it","rainy","today?"};
        sentence[3] = new string[] {"are you kidding me?","Ты что, издеваешься?","you","me?","kidding","are"};
        sentence[4] = new string[] {"he try to escape","Он пытается сбежать","escape","he","to","try"};
        

        // InvokeRepeating("Progress",0,10);
        // List<string> slova = new List<string>();
            
        //     using (StreamReader sr = new StreamReader(Path.Combine(Application.streamingAssetsPath,"slova.txt")))
        //     {
        //         while (!sr.EndOfStream)
        //             slova.Add(sr.ReadLine());
        //     }
        //     for (int i=0;i<size;i++){
        //         string[] name = slova[i].Split('.');
        //         sentence[i] = new string[6];
        //         for (int j=0;j<6;j++)
        //         sentence[i][j] = name[j];
        //     }
            // string[] name = slova[0].Split('.');
            // string[] value = slova[1].Split('.');
            // Debug.Log(name[0]);
            // Debug.Log(name[1]);
            // Debug.Log(name[2]);
            // Debug.Log(name[3]);
            // Debug.Log(name[4]);
            // Debug.Log(name[5]);
        
        nextLevel();
    }
    int k =0;
    void Update(){
        
        
    }
    IEnumerator Progress()
	{
		yield return new WaitForSeconds(0.1f);
        ProgressBar.GetComponent<Image>().fillAmount -= Time.deltaTime;
        progressValue = ProgressBar.GetComponent<Image>().fillAmount;
        if (progressValue >0){
            StartCoroutine(Progress());
        }else{
            endGameCheck();
        }
	}
    public void endGameCheck(){
        float[] x = new float[4];
        int[] x_name = new int[4]{0,1,2,3};
        bool flag = false;
        for (int i=0;i<4;i++){
            if (card[i].gameObject.transform.position.y < -2f){
                flag = true;
            }
            Debug.Log(card[i].gameObject.transform.position.y);
        }
        Debug.Log(flag);
        if (flag){
            panelLose.active = true;
            AudioLose();
        }else{
            Debug.Log("Strat obrobotka");
            for (int i=0;i<4;i++){
                x[i] = card_x[i].transform.localPosition.x;
            }
            float tempf;
            int temp;
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    if (x[i] > x[j])
                    {
                        tempf = x[i];
                        x[i] = x[j];
                        x[j] = tempf;
                        temp = x_name[i];
                        x_name[i] = x_name[j];
                        x_name[j] = temp;
                    }
                }
            }
            for (int i =0;i<4;i++){
                Debug.Log(x_name[i]);
            }
            
            string text_end = "";
            for (int i =0;i<4;i++){
                if (i != 3)
                text_end += card[x_name[i]].text+" ";
                else text_end += card[x_name[i]].text;
            }
            Debug.Log(text_end);
            if (text_end == sentence[sentence_num][0]) nextLevel();
            else {
                panelLose.active = true;
                AudioLose();
            }
        }
        

        
    }
    void AudioLose() {
        Debug.Log("Audio");
		gameObject.GetComponent<AudioSource>().Play();
	}
    
    public void nextLevel(){
        
        score++;
        score_t.text = "Score "+score;
        // Debug.Log("Score"+score);
        sentence_num = Random.Range(0,size);
        
        for (int i=0;i<4;i++){
            card[i].text = sentence[sentence_num][i+2];
            card_x[i].transform.SetParent(Hand.transform); // = new Vector3(card_x[i].transform.position.x, -4.192132f,card_x[i].transform.position.z);
        }
        sentence_t.text = sentence[sentence_num][1];
        ProgressBar.GetComponent<Image>().fillAmount = 1;
        StartCoroutine(Progress());
    }
}
