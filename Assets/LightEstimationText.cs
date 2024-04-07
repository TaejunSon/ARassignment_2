using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using static System.Net.Mime.MediaTypeNames;

public class LightEstimationText : MonoBehaviour
{
    [SerializeField]
    private ARCameraManager arCameraManager;

    [SerializeField]
    private UnityEngine.UI.Text brightnessValue;

    [SerializeField]
    private UnityEngine.UI.Text colorCorrectionValue;

    private Light currentLight;

    private void Awake()
    {
        currentLight = GetComponent<Light>();
    }

    private void OnEnable()
    {
        arCameraManager.frameReceived += FrameUpdated;
    }

    private void OnDisable()
    {
        arCameraManager.frameReceived -= FrameUpdated;
    }
    private void FrameUpdated(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue) // ±¤¿øÀÇ Æò±Õ ¹à±â
        {
            brightnessValue.text = $"Brightness: {args.lightEstimation.averageBrightness.Value}";
            currentLight.intensity = args.lightEstimation.averageBrightness.Value;
        }

        if (args.lightEstimation.colorCorrection.HasValue) // »ö»ó Á¤º¸(RGBA)
        {
            colorCorrectionValue.text = $"Color: {args.lightEstimation.colorCorrection.Value}";
            currentLight.color = args.lightEstimation.colorCorrection.Value;
        }
    }
}