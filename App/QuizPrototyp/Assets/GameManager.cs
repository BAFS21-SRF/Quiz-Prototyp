using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string guidId;
    public async Task NextScene(){
        
       Debug.Log("NextScene called");
       GameStart gameStart = await GetRequest<GameStart>("http://192.168.1.8:8888/api/GameStart");
       if(gameStart == null){
           Debug.Log("gameStart null");
       }
       guidId = gameStart.guid;
          Debug.Log(guidId);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCount > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void Quit(){
         Application.Quit();
    }

    private async Task<T> GetRequest<T>(string url){
        var webClient = new System.Net.WebClient();
        string json = await webClient.DownloadStringTaskAsync(new Uri(url));
        return JsonUtility.FromJson<T>(json); 
    }
}
