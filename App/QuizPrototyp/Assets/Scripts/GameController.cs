using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using System;
using UnityEngine.Events;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject fallBackObjectToSpawn;
    public GameObject qrCodeReader;
    public Camera arCamera;

    private GameObject TrashCan;
    private List<Vector2> spawnPoints = new List<Vector2>();
    private float mainPlaneY;
    private List<GameObject> spwanedObjects = new List<GameObject>();
    private List<CanSelect> Answers = new List<CanSelect>();
    private Frage frage = null;
    private int antwortCount = 1;

    private string currentQrCodeText = string.Empty;

    private int frageWert = 100;

    private int score = 0;
    [SerializeField]
    TMP_Text m_ReasonDisplayText;
    public TMP_Text reasonDisplayText
    {
        get => m_ReasonDisplayText;
        set => m_ReasonDisplayText = value;
    }
    [SerializeField]
    TMP_Text m_scoreText;
    public TMP_Text scoreText
    {
        get => m_scoreText;
        set => m_scoreText = value;
    }

    [SerializeField]
    GameObject m_ScoreBox;
    public GameObject scoreBox
    {
        get => m_ScoreBox;
        set => m_ScoreBox = value;
    }

    [SerializeField]
    GameObject m_ReasonParent;
    public GameObject reasonParent
    {
        get => m_ReasonParent;
        set => m_ReasonParent = value;
    }
    private ApiController apiController;


    void Start()
    {
        apiController = (new GameObject("ApiController")).AddComponent<ApiController>();
        apiController.GetAssetFromServer("fallback", OnAssetLoadedFromServer);
        StartCoroutine(waitForQrCode(GotQrCode));
    }

    public void Init(List<Vector2> spawnPoints, float mainPlaneY)
    {
        TrashCan = PlaceTrashOnPlane.spawnedObject;
        this.mainPlaneY = mainPlaneY;
        this.spawnPoints = spawnPoints;
        SpwanFrage(String.Empty);
    }

    void FixedUpdate()
    {
        if (spawnPoints.Count >= 4 && frage != null)
        {
            m_ReasonParent.SetActive(true);
            scoreBox.SetActive(true);
            PlayGame();
        }
    }

    private void SpwanFrage(string qrCode)
    {
        currentQrCodeText = qrCode;
        Debug.Log($"Spawn Frage mit QRCode: {qrCode}");

        DespawnFrage();
        apiController.StartApiCall<Frage>($"/frage?guid={GameManager.guidId}", nextFrage);
    }

    private void DespawnFrage()
    {
        Answers = new List<CanSelect>();
        foreach (var toDestroy in spwanedObjects)
        {
            Destroy(toDestroy);
        }
        spwanedObjects = new List<GameObject>();
    }


    private void nextFrage(Frage frage)
    {
        this.frage = frage;
        m_ReasonDisplayText.text = frage.frageText;
        antwortCount = GetAntwortCount(frage.auswahlmoeglichkeiten);
        int i = UnityEngine.Random.Range(0, spawnPoints.Count);
        if (spawnPoints.Count < frage.auswahlmoeglichkeiten.Count)
        {
            Debug.Log($"Zu wenigs Spawnpunkte {spawnPoints.Count} benötigt {frage.auswahlmoeglichkeiten.Count}");
        }
        foreach (var auswahl in frage.auswahlmoeglichkeiten)
        {
            GameObject prefabToSpawn = loadPrefabWithAssetId(auswahl.assetId, auswahl.auswahlText);
            Vector3 spawnPoint = new Vector3(spawnPoints[i % spawnPoints.Count].x, mainPlaneY, spawnPoints[i % spawnPoints.Count].y);
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint, Quaternion.identity);
            spawnedObject.transform.LookAt(new Vector3(arCamera.transform.position.x, mainPlaneY, arCamera.transform.position.z));
            spwanedObjects.Add(spawnedObject);
            i++;
        }
    }

    private int GetAntwortCount(List<Auswahlmoeglichkeiten> auswahlmoeglichkeiten)
    {
        int count = auswahlmoeglichkeiten.Where(x => x.order > 0).Count();
        Debug.Log($"GetAntwortCount: {count}");
        return count;
    }

    private void PlayGame()
    {
        foreach (GameObject gameObject in spwanedObjects)
        {
            CanSelect canSelect = gameObject.GetComponentInChildren<CanSelect>();
            if (canSelect == null)
            {
                canSelect = gameObject.GetComponent<CanSelect>();
            }
            if (canSelect.IsSelected && !Answers.Contains(canSelect))
            {
                Answers.Add(canSelect);
                Debug.Log("Added to awnser");
            }
        }
        CheckReset();
        CheckForNextFrage();
    }

    private void CheckReset()
    {
        CanSelect trash = TrashCan.GetComponentInChildren<CanSelect>();

        if (trash.IsSelected)
        {
            foreach (GameObject gameObject in spwanedObjects)
            {
                CanSelect canSelect = gameObject.GetComponentInChildren<CanSelect>();
                if (canSelect == null)
                {
                    canSelect = gameObject.GetComponent<CanSelect>();
                }
                canSelect.Reset();

            }
            trash.Reset();
            Answers = new List<CanSelect>();
        }
    }

    private void CheckForNextFrage()
    {
        if (Answers.Count() == antwortCount)
        {
            Debug.Log("SpawnFrage vom NextFrage");
            score += checkAwnser();
            Debug.Log($"Score: {score}");
            scoreText.text = $"Score: {score}";
            // ToDo despawn Objects and load new frage from qrcode
            DespawnFrage();
            m_ReasonDisplayText.text = "Please scan QR Code";
        }
    }

    private void GotQrCode(string text)
    {
        SpwanFrage(text);
    }

    IEnumerator waitForQrCode(UnityAction<string> callback)
    {
        while (true)
        {
            string qrCode = currentQrCodeText;
            QrCodeReader teset = qrCodeReader.GetComponent<QrCodeReader>();
            do
            {
                qrCode = teset.qrCodeText;
                yield return new WaitForSeconds(1);
            } while (qrCode == currentQrCodeText);

            callback(qrCode);
        }
    }

    private int checkAwnser()
    {
        int value = frageWert;
        try
        {
            List<Auswahlmoeglichkeiten> resultList = frage.auswahlmoeglichkeiten.OrderBy(x => x.order).ToList();
            int k = 0;
            for (int i = 0; i < resultList.Count; i++)
            {
                if (resultList[i].order == 0)
                {
                    continue;
                }

                LookAtCamera textLookAtCamera = Answers[k].GetComponentInChildren<LookAtCamera>();
                string text = textLookAtCamera.textMesh.text;
                if (resultList[i].auswahlText != text)
                {
                    value -= 100 / antwortCount;
                }
                k++;

            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

        return value;
    }

    private GameObject loadPrefabWithAssetId(string AssetId, string name)
    {
        Debug.Log($"loadPrefabWithAssetId, AssetId:{AssetId}, name:{name}");
        GameObject newGameObject = Resources.Load(AssetId) as GameObject;
        if (newGameObject == null)
        {
            newGameObject = fallBackObjectToSpawn;
            Debug.Log("newGameObject set");
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

    private void OnAssetLoadedFromServer(GameObject asset)
    {
        fallBackObjectToSpawn = asset;
        Debug.Log("Fallback Object has been changed");
    }
}