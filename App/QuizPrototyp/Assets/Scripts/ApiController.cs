using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ApiController : MonoBehaviour
{
    private string baseUrl = $"http://192.168.1.8:8888/api";
      public async Task<T> GetRequest<T>(string url, bool loggen = false){
        url = baseUrl + url;
        if(loggen){
            Debug.Log(url);
        }
        var webClient = new System.Net.WebClient();
        string json = await webClient.DownloadStringTaskAsync(new Uri(url));
        if(loggen){
            Debug.Log(json);
        }
        return JsonUtility.FromJson<T>(json); 
    }
}
