using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class Clever : MonoBehaviour
{
    public Text sentence_t;
    const int size = 10;
    public Text[] button;
    string[][] sentence  = new string[size][];
    public GameObject panelLose;
    int sentence_num;
    public GameObject click;
    void AudioKlick() {
		click.GetComponent<AudioSource>().Play();
	}
    void AudioLose() {
        Debug.Log("Audio");
		gameObject.GetComponent<AudioSource>().Play();
	}
    void Start(){
        sentence[0] = new string[] {"Hush","Странный","Тишина","Удар","Каша"};
        sentence[1] = new string[] {"Thrush","Делать","Дятел","Дрозд","Кукушка"};
        sentence[2] = new string[] {"Cerulean","Лазурный","Цветной","Обычный","Радужный"};
        sentence[3] = new string[] {"Betrayal","Батлер","Батл","Правительство","Предательство"};
        sentence[4] = new string[] {"Demure","Целомудренный","Мудрый","Мастер","Многодумающий"};
        sentence[5] = new string[] {"Pure","Цистый","Грязный","Аккуратный","Опрятный"};
        sentence[6] = new string[] {"Sempiternal ","Бесконечный","Вечный","Невозможный","Реальный"};
        sentence[7] = new string[] {"Halcyon","Чистый","Безмятежный","Свежий","Невероятный"};
        sentence[8] = new string[] {"Chimes","Веретено","Долото","Куранты","Колокол"};
        sentence[9] = new string[] {"Eloquence","Удовольствие","Красноречие","Неизбежность","Равенство"};
        
        // List<string> slova = new List<string>();
        
        //     using (StreamReader sr = new StreamReader(Path.Combine(Application.streamingAssetsPath,"slova2.txt")))
        //     {
        //         while (!sr.EndOfStream)
        //             slova.Add(sr.ReadLine());
        //     }
        //     for (int i=0;i<size;i++){
        //         string[] name = slova[i].Split('.');
        //         sentence[i] = new string[5];
        //         for (int j=0;j<5;j++)
        //         sentence[i][j] = name[j];
        //     }
            // Debug.Log(sentence[0][1]);
            // Debug.Log(sentence[0][2]);
            // Debug.Log(sentence[0][3]);
            // Debug.Log(sentence[0][4]);
        
        nextLevel();
    }
    public void endGameCheck(int a){
        AudioKlick();
        string[] but = new string[10]{"Тишина","Дрозд","Лазурный","Предательство","Целомудренный","Цистый","Вечный","Безмятежный","Куранты","Красноречие"};
        //Debug.Log(button[a].text);
        if (button[a].text == but[sentence_num])
            nextLevel();
            else {
                AudioLose();
                panelLose.active = true;
            }
    }
    /*public void endGameCheck(){
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
            else panelLose.active = true;
        }
        

        
    }*/
    public void nextLevel(){

        sentence_num = Random.Range(0,size);
        int[] k = new int[4]{0,0,0,0};
        
        for (int i=0;i<4;i++){
            // bool flag = true;
            // while (flag){
            //     a = Random.Range(0,4);
            //     Debug.Log(a);
            //     int s = 0;
            //     if (k[a] != 0){
            //             flag=false;
            //             k[a]++;
            //     }else s++;
            //     if (s == 4) flag = false;
                
                
                
            // }
            button[i].text = sentence[sentence_num][i+1];
        }
        
        sentence_t.text = sentence[sentence_num][0];
    }
}
