using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneController : MonoBehaviour
{
    public ARPlaneManager planeManager;
    private ARPlane mainPlane = null;
    private List<Vector2> spawnPoints = new List<Vector2>();

    public GameObject objectToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Fixed Updated");
        calcMainPlane();
        calcSpawnPoints();

        if (spawnPoints.Count > 0)
        {
            Instantiate(objectToSpawn, (Vector3)spawnPoints[0], new Quaternion(0, 0, 0, 0));
        }

        Debug.Log(mainPlane.classification.ToString());
    }

    private void calcMainPlane()
    {
        Debug.Log("Calc Main Plane");
        foreach (var plane in planeManager.trackables)
        {
            Debug.Log("ARPlane Class: " + ((ARPlane)plane).classification.ToString());
            Debug.Log("ARPlane sqrMagnitude: " + ((ARPlane)plane).size.sqrMagnitude.ToString());

            if (((ARPlane)plane).classification == PlaneClassification.Floor
                && mainPlane != null
                && ((ARPlane)plane).size.sqrMagnitude > mainPlane.size.sqrMagnitude)
            {
                mainPlane = (ARPlane)plane; // Biggest Plane 
            }
        }


        Debug.Log("Biggest ARPlane: " + ((ARPlane)plane).classification.ToString());
    }

    private void calcSpawnPoints()
    {
        Debug.Log("Calc Spawn Points");
        while (spawnPoints.Count <= 4)
        {
            float randomAngle = Random.Range(0f, 6.28319f);
            Vector2 newSpawnPoint = new Vector2(Mathf.Cos(random), Mathf.Sin(random));
            if (isInPlane(newSpawnPoint) && !isTooClose(spawnPoints, newSpawnPoint))
            {
                Debug.Log("Spawn Points X " + newSpawnPoint.x +  " und Y " newSpawnPoint.y);
                spawnPoints.Add(newSpawnPoint);
            }
        }
    }

    private bool isInPlane(Vector2 newSpawnPoint)
    {
        Debug.Log("is Plane");
        // Get the angle between the point and the
        // first and last vertices.
        int max_point = mainPlane.boundary.Length - 1;
        float total_angle = GetAngle(
            mainPlane.boundary[max_point].x, mainPlane.boundary[max_point].y,
            newSpawnPoint.x, newSpawnPoint.y,
            mainPlane.boundary[0].x, mainPlane.boundary[0].y);

        // Add the angles from the point
        // to each other pair of vertices.
        for (int i = 0; i < max_point; i++)
        {
            total_angle += GetAngle(
                mainPlane.boundary[i].x, mainPlane.boundary[i].y,
                newSpawnPoint.x, newSpawnPoint.y,
                mainPlane.boundary[i + 1].x, mainPlane.boundary[i + 1].y);
        }

        // The total angle should be 2 * PI or -2 * PI if
        // the point is in the polygon and close to zero
        // if the point is outside the polygon.
        // The following statement was changed. See the comments.
        //return (Math.Abs(total_angle) > 0.000001);
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
        Debug.Log("is Too Close");
        var threshold = 1;

        foreach(Vector2 vec in currentSpawnPoints)
        {
            if (calcDistance(vec, newSpawnPoint) > threshold)
            {
                return false;
            }
        }
        return true;
    }

    private double calcDistance(Vector2 vector1, Vector2 vector2)
    {
        return Math.Sqrt(Math.Pow(Convert.ToDouble(vector2.x) - Convert.ToDouble(vector1.x), 2) + Math.Pow(Convert.ToDouble(vector2.y) - Convert.ToDouble(vector1.y), 2));
    }

}
