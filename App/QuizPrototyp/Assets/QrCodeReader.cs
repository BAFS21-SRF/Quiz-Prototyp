using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ZXing;

public class QrCodeReader : MonoBehaviour
{
    public Camera cam;
    private BarcodeReader barCodeReader;

    public string qrCodeText = string.Empty;
    public bool canScan = false;

    FrameCapturer m_pixelCapturer;

    // Use this for initialization
    void Start()
    {
        barCodeReader = new BarcodeReader();
        barCodeReader.Options.PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE };
        m_pixelCapturer = cam.GetComponentInChildren<FrameCapturer>();
        if (m_pixelCapturer == null)
        {
            m_pixelCapturer = cam.GetComponent<FrameCapturer>();
        }

        StartCoroutine(lookForQrCode(setText));

    }

    private void setText(string text)
    {
        qrCodeText = text;
    }

    IEnumerator lookForQrCode(UnityAction<string> callback)
    {
        while (true)
        {
            if (canScan)
            {
                yield return new WaitForEndOfFrame();
                if (m_pixelCapturer == null)
                {
                    Debug.LogError($"m_pixelCapturer ist null");

                }
                Resolution currentResolution = Screen.currentResolution;


                Color32[] framebuffer = m_pixelCapturer.m_lastCapturedColors;
                /*Debug.Log($"framebuffer Length: {framebuffer.Length}");
                Debug.Log($"currentResolution width: {currentResolution.width}");
                 Debug.Log($"currentResolution height: {currentResolution.height}");*/

                var data = barCodeReader.Decode(framebuffer, currentResolution.width, currentResolution.height / 2);

                if (data != null)
                {
                    callback(data.Text);
                    Debug.Log(data);
                    Debug.Log("QR: " + data.Text);
                }

                // skip 1 frame each time 
                // solves GetPixels() blocks for ReadPixels() to complete
                // https://medium.com/google-developers/real-time-image-capture-in-unity-458de1364a4c
                m_pixelCapturer.m_shouldCaptureOnNextFrame = true;
            }

        }
    }
}
