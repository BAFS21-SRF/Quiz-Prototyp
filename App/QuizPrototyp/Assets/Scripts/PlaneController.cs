using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;
using System.Threading.Tasks;

public class PlaneController : MonoBehaviour
{
    private List<Vector2> spawnPoints = new List<Vector2>();
    private ARPlaneManager planeManager;
    private ARPlane mainPlane = null;
    public static bool canStart = false;  
    private bool calcIsDone = false;

    public GameController gameController;
    public int minDistanceToOtherObjects = 1;
    public int countOfSpawnPoints = 4;

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
            calcIsDone = true; // Since the calculation can take longer and is otherwise started more times
            CalcMainPlane();
            CalcSpawnPoints();
            Debug.Log($"Spawnpoints Count: {spawnPoints.Count}");
            await gameController.Init(spawnPoints, mainPlane.center.y);
        }
    }

    private void CalcMainPlane()
    {
        foreach (var plane in planeManager.trackables)
        {
            if (mainPlane == null || plane.size.sqrMagnitude >= mainPlane.size.sqrMagnitude)
            {
                if(mainPlane != plane){
                    mainPlane = plane; // Biggest Plane
                    Debug.Log("Biggest ARPlane: " + mainPlane.classification.ToString());
                    Debug.Log("Center X " + mainPlane.center.x + " and Y " + mainPlane.center.y + " and Z " + mainPlane.center.z);
                }
            }
        }
    }

    private void CalcSpawnPoints()
    {
        Debug.Log($"**********************Calc Spawn Points********************* With {spawnPoints.Count} Spawnpoints and IsMainPlaneNull_ {mainPlane ==null}");        
        var maxX = mainPlane.boundary.Max(value => value.x);
        var minX = mainPlane.boundary.Min(value => value.x);
        var maxY = mainPlane.boundary.Max(value => value.y);
        var minY = (mainPlane.boundary.Min(value => value.y) + maxY) / 2;
        
        var trashPosition = PlaceTrashOnPlane.spawnedObject.GetComponentInChildren<CanSelect>().transform.position;
        var trashList = new List<Vector2> { new Vector2 { x = trashPosition.x, y = trashPosition.z } };
        Debug.Log($"TrahsPosition X = {trashPosition.x} and Y = {trashList[0].y}");

        Debug.Log($"minX = {minX}, maxX = {maxX}, minY = {minY}, maxY = {maxY}");
        while (spawnPoints.Count < countOfSpawnPoints && mainPlane != null)
        {
            Vector3 spawnpoint = mainPlane.center;
            var x = UnityEngine.Random.Range(maxX, minX);
            var y = UnityEngine.Random.Range(maxY, minY);
            Vector2 newSpawnPoint = new Vector2(x, y);
            if (IsInPlane(newSpawnPoint) && !IsTooClose(spawnPoints, newSpawnPoint))
            {
                Debug.Log("Spawn Points X " + newSpawnPoint.x +  " und Y " + newSpawnPoint.y);
                spawnPoints.Add(newSpawnPoint);
            }
        }
    }

    private bool IsInPlane(Vector2 newSpawnPoint)
    {
        int max_point = mainPlane.boundary.Length - 1;
        float total_angle = Polygon.GetAngle(
            mainPlane.boundary[max_point].x, mainPlane.boundary[max_point].y,
            newSpawnPoint.x, newSpawnPoint.y,
            mainPlane.boundary[0].x, mainPlane.boundary[0].y);

        for (int i = 0; i < max_point; i++)
        {
            total_angle += Polygon.GetAngle(
                mainPlane.boundary[i].x, mainPlane.boundary[i].y,
                newSpawnPoint.x, newSpawnPoint.y,
                mainPlane.boundary[i + 1].x, mainPlane.boundary[i + 1].y);
        }

        return (Math.Abs(total_angle) > 1);
    }

    private bool IsTooClose(List<Vector2> currentSpawnPoints, Vector2 newSpawnPoint)
    {
        foreach(Vector2 vec in currentSpawnPoints)
        {
            if (CalcDistance(vec, newSpawnPoint) < minDistanceToOtherObjects)
            {
                return true;
            }
        }
        return false;
    }

    private double CalcDistance(Vector2 vector1, Vector2 vector2)
    {
        return Math.Sqrt(Math.Pow(Convert.ToDouble(vector2.x) - Convert.ToDouble(vector1.x), 2) + Math.Pow(Convert.ToDouble(vector2.y) - Convert.ToDouble(vector1.y), 2));
    }


}
