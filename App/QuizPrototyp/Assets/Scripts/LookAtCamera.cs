using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
     public TextMesh textMesh;

    // Start is called before the first frame update

    void Update()
    {        
        transform.rotation = Quaternion.LookRotation(transform.position - GameObject.FindGameObjectWithTag("MainCamera").transform.position);
    }
    
}
