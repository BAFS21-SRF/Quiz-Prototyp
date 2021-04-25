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
            meshRenderer.material.color = Color.Lerp(Color.green, Color.white, 2f);    
        }
    }

    void FixedUpdate()
    {
        calcCameraToObjectDistance();
    }

    public void Reset(){
        IsSelected = false;
        minDistanz = 1.0f;
        timer = 0.0f;
    }

     private void calcCameraToObjectDistance(){
         if(isVisable){
            float dist = Vector3.Distance(this.transform.position, arCamera.transform.position);
            if(dist < minDistanz){
                 timer += Time.fixedDeltaTime;

                if (timer > waitTime)
                {
                   IsSelected = true;
                }
            }else{
                timer = 0.0f;
            }
         }        
    }

    void OnBecameVisible()
    {
        isVisable = true;
    }

    void OnBecameInvisible()
    {
        timer = 0.0f;
        isVisable = false;
    }
}
