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
    private List<Vector2> spawnPoints = new List<Vector2>();
    private ARPlaneManager planeManager;
    private ARPlane mainPlane = null;
    public static bool canStart = false;  
    private bool calcIsDone = false;

    public GameController gameController;

    async Task FixedUpdate()
    {
         await Setup();
    }

    private async Task Setup(){        
        if (canStart && !calcIsDone) {           
            GameObject.FindGameObjectWithTag("PlaneManager").TryGetComponent<ARPlaneManager>(out planeManager);
                if(planeManager == null){
                Debug.Log($"planeManager ist nulls");
            }
            canStart = false;
            calcMainPlane();
            calcSpawnPoints();
            await gameController.Init(spawnPoints, mainPlane.center.y);
            calcIsDone = true;
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

        var trashList = new List<Vector2> { new Vector2 { x = gameController.TrashCan.transform.position.x, y = gameController.TrashCan.transform.position.z } };
        Debug.Log($"TrahsPosition X = {trashList[0].x} and Y = {trashList[0].y}");

        Debug.Log($"minX = {minX}, maxX = {maxX}, minY = {minY}, maxY = {maxY}");
        while (spawnPoints.Count < 4 && mainPlane != null)
        {
            Vector3 spawnpoint = mainPlane.center;
            var x = UnityEngine.Random.Range(maxX, minX);
            var y = UnityEngine.Random.Range(maxY, minY);
            Vector2 newSpawnPoint = new Vector2(x, y);
            if (isInPlane(newSpawnPoint) && !isTooClose(trashList, newSpawnPoint) && !isTooClose(spawnPoints, newSpawnPoint))
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


}
