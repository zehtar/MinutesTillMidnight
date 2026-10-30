using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraMovement : MonoBehaviour
{
    public Vector3 offset;
    public Transform targetPoint;
    
    public float speed;
    public CinemachineVirtualCamera virtualCam;
    public CinemachineBasicMultiChannelPerlin perlin;
    
    public float zoomSpeed = 0.5f;
    public float maxZoom = 10f;
    
    private float shakeTimer = 0f;

    void Start()
    {
        virtualCam = FindObjectOfType<CinemachineVirtualCamera>();
        perlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (targetPoint == null)
        {
            StartCoroutine(WaitForTarget());
            return;
        }
    }
    
    void Update()
    {
       //UpdateZoom();
        UpdateShake();
    }
    
    void UpdateZoom()
    {
        if (virtualCam.m_Lens.OrthographicSize < maxZoom)
        {
            virtualCam.m_Lens.OrthographicSize += zoomSpeed * Time.deltaTime;
        }
    }

    public void SetTargetZoom(float zoom)
    {
        maxZoom = zoom;
    }
    public void Shake(float duration, float intensity)
    {
        if (perlin == null) return;

        perlin.m_AmplitudeGain = intensity;
        shakeTimer = duration;
    }

    void UpdateShake()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f && perlin != null)
            {
                perlin.m_AmplitudeGain = 0f;
            }
        }
    }
    
    public void Initialize(Transform target)
    {   
        targetPoint = target;
        virtualCam.Follow = targetPoint;
    }
    
    IEnumerator WaitForTarget()
    {
        while (targetPoint == null || virtualCam.Follow == null)
        {
            yield return null; 
        }
    }
}
