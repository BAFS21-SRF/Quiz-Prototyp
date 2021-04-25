using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string guidId;
    bool getNextScene;
    public void NextScene(){
        getNextScene = true;
       
    }

    async Task Update(){
        if(getNextScene){
            getNextScene=false;
            Debug.Log("NextScene called");
            GameStart gameStart = await GetRequest<GameStart>("http://192.168.1.8:8888/api/gamestart");
            if(gameStart == null){
                Debug.Log("gameStart null");
            }
            guidId = gameStart.guid;
            Debug.Log(guidId);
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);            
        }
    }



    public void Quit(){
         Application.Quit();
    }

    private async Task<T> GetRequest<T>(string url){
         Debug.Log(url);
        var webClient = new System.Net.WebClient();
        string json = await webClient.DownloadStringTaskAsync(new Uri(url));
        Debug.Log(json);
        return JsonUtility.FromJson<T>(json); 
    }
}
