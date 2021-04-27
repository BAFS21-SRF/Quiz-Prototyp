using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TitleFrage : MonoBehaviour
{
    // Start is called before the first frame update
     [SerializeField]
     private TextMeshProUGUI  textMeshPro;

     public string title;

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = title;
    }
}
