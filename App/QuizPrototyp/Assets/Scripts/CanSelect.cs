using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSelect : MonoBehaviour
{

    private GameObject arCamera;
    private bool isVisable;
    public bool IsSelected = false;

    private float waitTime = 2.0f;
    private float timer = 0.0f;

    public float minDistanz = 1.0f;
     public float addDistanz = 0.1f;
    private float timerVisable = 0.0f;
    MeshRenderer meshRenderer;

    void Start(){
         arCamera = GameObject.FindGameObjectWithTag("MainCamera");
         meshRenderer = GetComponent<MeshRenderer>();
         Debug.Log("isCameraNull: " + (arCamera == null).ToString());
    }

  

    void Update()
    {
        if(IsSelected){
            meshRenderer.material.color = Color.Lerp(Color.white, Color.green, 2f);         
        }else{
            meshRenderer.material.color = Color.white;
        }

        if(isVisable){
             timerVisable += Time.deltaTime;
             if(timerVisable > 2){
                minDistanz += 0.1f;
                timerVisable -= 2;
             }
        }
    }

    void FixedUpdate()
    {
        calcCameraToObjectDistance();
    }

    public void Reset(){
        IsSelected = false;
        minDistanz = 1.0f;
        timerVisable = 0.0f;
    }

     private void calcCameraToObjectDistance(){
         if(isVisable){
            float dist = Vector3.Distance(this.transform.position, arCamera.transform.position);
            if(dist < minDistanz){
                Debug.Log($"{this.name} hat eine Distanz von {dist} zur Kamera");
                 timer += Time.fixedDeltaTime;

                if (timer > waitTime)
                {
                   IsSelected = true;
                   Debug.Log($"Selected: IsSelected");
                }
            }else{
                timer = 0.0f;
            }
         }        
    }

    void OnBecameVisible()
    {
        Debug.Log("IsVisable");
        isVisable = true;
    }

    void OnBecameInvisible()
    {
        timerVisable = 0.0f;
         Debug.Log("IsNotVisable");
        isVisable = false;
    }
}