using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string guidId;

    private ApiController apiController;
    bool getNextScene;
    public void NextScene(){
        getNextScene = true;
       
    }

    async Task Update(){
        if(getNextScene){
            getNextScene=false;
            apiController = new ApiController();
            Debug.Log("NextScene called");
            GameStart gameStart = await apiController.GetRequest<GameStart>("/gamestart", true);
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
}
