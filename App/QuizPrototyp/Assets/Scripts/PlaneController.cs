using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;
using UnityEngine.Networking;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    private ARPlaneManager planeManager;
    private ARPlane mainPlane = null;
    private List<Vector2> spawnPoints = new List<Vector2>();
    private bool calcIsDone = true;

    public GameObject fallBackObjectToSpawn;
    public List<GameObject> spwanedObjects = new List<GameObject>();
    public static bool canStart = false;
    public List<CanSelect> Answers = new List<CanSelect>();

    public GameObject TrashCan;

    public Frage frage;

    [SerializeField]
    TMP_Text m_ReasonDisplayText;

    public TMP_Text reasonDisplayText
    {
        get => m_ReasonDisplayText;
        set => m_ReasonDisplayText = value;
    }

    [SerializeField]
    GameObject m_ReasonParent;
    
    public GameObject reasonParent
    {
        get => m_ReasonParent;
        set => m_ReasonParent = value;
    }

  

    // Start is called before the first frame update
    async Task Start()
    {       
        frage = await GetFrage("http://192.168.1.8:8888/api/frage");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canStart && calcIsDone) {         
            m_ReasonDisplayText.text = frage.frageText;
            m_ReasonParent.SetActive(true);
            GameObject.FindGameObjectWithTag("PlaneManager").TryGetComponent<ARPlaneManager>(out planeManager);
                if(planeManager == null){
                Debug.Log($"planeManager ist nulls");
            }
            canStart = false;
            calcIsDone = false;
            calcMainPlane();
            calcSpawnPoints();
            if (spawnPoints.Count > 0)
            {
                int i = 0;
                if(spawnPoints.Count < frage.auswahlmoeglichkeiten.Count){
                    Debug.Log($"Zu wenigs Spawnpunkte {spawnPoints.Count} benötigt {frage.auswahlmoeglichkeiten.Count}");
                }
                foreach (var auswahl in frage.auswahlmoeglichkeiten)
                {
                        GameObject prefabToSpawn = loadPrefabWithAssetId(auswahl.assetId, auswahl.auswahlText);                      
                    
                    Vector3 spawnPoint = new Vector3(spawnPoints[i].x, mainPlane.center.y, spawnPoints[i].y);
                    spwanedObjects.Add(Instantiate(prefabToSpawn, spawnPoint, Quaternion.identity) as GameObject);

                        i++;
                }
            }

            foreach (GameObject gameObject in spwanedObjects)
            {
                CanSelect canSelect = gameObject.GetComponentInChildren<CanSelect>();
                if (canSelect == null)
                {
                    canSelect = gameObject.GetComponent<CanSelect>();
                }

                if (canSelect.IsSelected)
                {
                    Answers.Add(canSelect);
                }
            }
            CanSelect trash = TrashCan.GetComponentInChildren<CanSelect>();

            if (trash.IsSelected)
            {
                spwanedObjects.ForEach(x => x.GetComponentInChildren<CanSelect>().Reset());
                trash.IsSelected = false;
                Answers = new List<CanSelect>();
            }

        }
    }   
    private void calcMainPlane()
    {
        foreach (var plane in planeManager.trackables)
        {
            if ((mainPlane == null
                || ((ARPlane)plane).size.sqrMagnitude >= mainPlane.size.sqrMagnitude))
            {
                if(mainPlane != (ARPlane)plane){
                    mainPlane = (ARPlane)plane; // Biggest Plane
                    Debug.Log("Biggest ARPlane: " + mainPlane.classification.ToString());
                    foreach (Vector2 boundary in mainPlane.boundary){
                      Debug.Log("Boundary X " + boundary.x + " and Y " + boundary.y);  
                    }
                    Debug.Log("Center X " + mainPlane.center.x + " and Y " + mainPlane.center.y + " and Z " + mainPlane.center.z);
                }
            }
        }
    }

    private void calcSpawnPoints()
    {
        Debug.Log($"**********************Calc Spawn Points********************* With {spawnPoints.Count} Spawnpoints and IsMainPlaneNull_ {mainPlane ==null}");        
        var maxX = mainPlane.boundary.Max(value => value.x);
        var minX = mainPlane.boundary.Min(value => value.x);
        var maxY = mainPlane.boundary.Max(value => value.y);
        var minY = (mainPlane.boundary.Min(value => value.y) + maxY) / 2;
        Debug.Log($"minX = {minX}, maxX = {maxX}, minY = {minY}, maxY = {maxY}");
        while (spawnPoints.Count < 4 && mainPlane != null)
        {
            Vector3 spawnpoint = mainPlane.center;
            var x = UnityEngine.Random.Range(maxX, minX);
            var y = UnityEngine.Random.Range(maxY, minY);
            Vector2 newSpawnPoint = new Vector2(x, y);
            //Debug.Log("NewSpawnPoint X " + newSpawnPoint.x + " Y " + newSpawnPoint.y);
            if (isInPlane(newSpawnPoint) && !isTooClose(spawnPoints, newSpawnPoint))
            {
                Debug.Log("Spawn Points X " + newSpawnPoint.x +  " und Y " + newSpawnPoint.y);
                spawnPoints.Add(newSpawnPoint);
            }
        }
    }

    private bool isInPlane(Vector2 newSpawnPoint)
    {
        int max_point = mainPlane.boundary.Length - 1;
        float total_angle = GetAngle(
            mainPlane.boundary[max_point].x, mainPlane.boundary[max_point].y,
            newSpawnPoint.x, newSpawnPoint.y,
            mainPlane.boundary[0].x, mainPlane.boundary[0].y);

        for (int i = 0; i < max_point; i++)
        {
            total_angle += GetAngle(
                mainPlane.boundary[i].x, mainPlane.boundary[i].y,
                newSpawnPoint.x, newSpawnPoint.y,
                mainPlane.boundary[i + 1].x, mainPlane.boundary[i + 1].y);
        }

        return (Math.Abs(total_angle) > 1);
    }

    private float GetAngle(float Ax, float Ay,
    float Bx, float By, float Cx, float Cy)
    {
        // Get the dot product.
        float dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

        // Get the cross product.
        float cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

        // Calculate the angle.
        return (float)Math.Atan2(cross_product, dot_product);
    }

    private float DotProduct(float Ax, float Ay,
    float Bx, float By, float Cx, float Cy)
    {
        // Get the vectors' coordinates.
        float BAx = Ax - Bx;
        float BAy = Ay - By;
        float BCx = Cx - Bx;
        float BCy = Cy - By;

        // Calculate the dot product.
        return (BAx * BCx + BAy * BCy);
    }

    private float CrossProductLength(float Ax, float Ay,
    float Bx, float By, float Cx, float Cy)
    {
        // Get the vectors' coordinates.
        float BAx = Ax - Bx;
        float BAy = Ay - By;
        float BCx = Cx - Bx;
        float BCy = Cy - By;

        // Calculate the Z coordinate of the cross product.
        return (BAx * BCy - BAy * BCx);
    }

    private bool isTooClose(List<Vector2> currentSpawnPoints, Vector2 newSpawnPoint)
    {
        var threshold = 1;

        foreach(Vector2 vec in currentSpawnPoints)
        {
            if (calcDistance(vec, newSpawnPoint) < threshold)
            {
                return true;
            }
        }
        return false;
    }

    private double calcDistance(Vector2 vector1, Vector2 vector2)
    {
        return Math.Sqrt(Math.Pow(Convert.ToDouble(vector2.x) - Convert.ToDouble(vector1.x), 2) + Math.Pow(Convert.ToDouble(vector2.y) - Convert.ToDouble(vector1.y), 2));
    }

    private async Task<Frage> GetFrage(string url){
        var webClient = new System.Net.WebClient();
        string json = await webClient.DownloadStringTaskAsync(new Uri(url));
        return JsonUtility.FromJson<Frage>(json); 
    }
    private GameObject loadPrefabWithAssetId(string AssetId, string name)
    {
        Debug.Log($"loadPrefabWithAssetId, AssetId:{AssetId}, name:{name}");
        GameObject newGameObject = Resources.Load(AssetId) as GameObject;   
        if (newGameObject == null){
            newGameObject = fallBackObjectToSpawn;
        }
        LookAtCamera text = newGameObject.GetComponentInChildren<LookAtCamera>();        
        if (text == null)
        {
            text = newGameObject.GetComponent<LookAtCamera>();
            Debug.Log("text is null");
        }
        text.textMesh.text = name;                   
        
        return newGameObject;
    }

}
