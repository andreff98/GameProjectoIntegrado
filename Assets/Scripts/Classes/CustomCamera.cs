using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera
{
    // How long the object should shake for.
    public float shakeDuration = 0f;
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPos;
    private Transform camera;

    public CustomCamera()
	{
        
    }

    public CustomCamera(Transform _camera)
    {
        originalPos = _camera.localPosition;
        camera = _camera as Transform;
    }

    public void ShakeCamera()
    {
        if (shakeDuration > 0)
        {
            // camera.localPosition = originalPos + System.Random.insideUnitSphere * shakeAmount;
            camera.localPosition = Vector3.Lerp(camera.localPosition, originalPos + Random.insideUnitSphere * shakeAmount, Time.deltaTime * 3);
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camera.localPosition = originalPos;
        }
    }
}