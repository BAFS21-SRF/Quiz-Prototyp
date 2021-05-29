using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DisableTrackedVisuals : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Disables spawned planes and ARPlaneManager")]
    bool m_DisablePlaneRendering;

    public bool disablePlaneRendering
    {
        get => m_DisablePlaneRendering;
        set => m_DisablePlaneRendering = value;
    }

    [SerializeField]
    ARPlaneManager m_PlaneManager;

    public ARPlaneManager planeManager
    {
        get => m_PlaneManager;
        set => m_PlaneManager = value;
    }

    void OnEnable()
    {
        PlaceTrashOnPlane.onPlacedObject += OnPlacedObject;
    }

    void OnDisable()
    {
        PlaceTrashOnPlane.onPlacedObject -= OnPlacedObject;
    }

    void OnPlacedObject()
    {
        if (m_DisablePlaneRendering)
        {
            m_PlaneManager.SetTrackablesActive(false);
            m_PlaneManager.enabled = false;
        }
    }
}
