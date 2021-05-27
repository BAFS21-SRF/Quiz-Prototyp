using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ApiController : MonoBehaviour
{
    private string baseUrl = $"http://192.168.1.8:8888";


    public void GetAssetFromServer(string assetId, UnityAction<GameObject> callback)
    {
        Debug.Log("GetAssetFromServer called");
        StartCoroutine(GetAssetFromServerRoutine(assetId, callback));
    }

    public void StartApiCall<T>(string url, UnityAction<T> callback)
    {
        StartCoroutine(ApiCall<T>(url, callback));
    }

    private IEnumerator ApiCall<T>(string url, UnityAction<T> callback)
    {
        url = baseUrl + "/api" + url;
        using (UnityWebRequest www = UnityWebRequest.Get(new Uri(url)))
        {
            yield return www.SendWebRequest();


            callback(JsonUtility.FromJson<T>(www.downloadHandler.text));
        }
    }

    private IEnumerator GetAssetFromServerRoutine(string assetId, UnityAction<GameObject> callback)
    {
        Debug.Log("GetAssetFromServerRoutine called");

        string os = "";
#if UNITY_ANDROID
        os = "Android";
#else
        os = "IOS";
#endif

        string bundleURL = $"{baseUrl}/assets/{os}/{assetId}";
        Debug.Log("Requesting bundle at " + bundleURL);

        //request asset bundle
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            if (bundle != null)
            {
                string rootAssetPath = bundle.GetAllAssetNames()[0];
                GameObject arObject = bundle.LoadAsset(rootAssetPath) as GameObject;
                bundle.Unload(false);
                callback(arObject);
            }
            else
            {
                Debug.Log("Not a valid asset bundle");
            }
        }
    }
}
