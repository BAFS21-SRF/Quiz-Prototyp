using UnityEngine;

public class CanSelect : MonoBehaviour
{

    private GameObject arCamera;
    public GameObject selecetedAnimation;
    private bool isVisable;

    private AudioSource source;
    public bool IsSelected = false;

    private float waitTime = 2.0f;
    private float timer = 0.0f;

    public float minDistanz = 0.8f;
    MeshRenderer meshRenderer;

    void Start()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera");
        meshRenderer = GetComponent<MeshRenderer>();
        Debug.Log("isCameraNull: " + (arCamera == null).ToString());
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (selecetedAnimation != null)
        {
            selecetedAnimation.SetActive(IsSelected);
        }
        if (IsSelected)
        {
            meshRenderer.material.color = Color.Lerp(Color.white, Color.green, 2f);

        }
        else
        {
            meshRenderer.material.color = Color.Lerp(Color.green, Color.white, 2f);
        }
    }

    void FixedUpdate()
    {
        calcCameraToObjectDistance();
    }

    public void Reset()
    {
        IsSelected = false;
        timer = 0.0f;
    }

    private void calcCameraToObjectDistance()
    {
        if (isVisable && !IsSelected)
        {
            var objectPosition = new Vector2 { x = transform.position.x, y = transform.position.z };
            var cameraPosition = new Vector2 { x = arCamera.transform.position.x, y = arCamera.transform.position.z };
            float dist = Vector2.Distance(objectPosition, cameraPosition);
            if (dist < minDistanz)
            {
                timer += Time.fixedDeltaTime;

                if (timer > waitTime)
                {
                    IsSelected = true;
                    source.Play();
                }
            }
            else
            {
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
