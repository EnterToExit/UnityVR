using UnityEngine;
using UnityEngine.XR;

public class HmdInfoManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"Is Device Active: {XRSettings.isDeviceActive}");
        Debug.Log($"Device Name is: {XRSettings.loadedDeviceName}");
    }
}