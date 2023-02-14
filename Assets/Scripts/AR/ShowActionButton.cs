using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowActionButton : MonoBehaviour
{
    private class PreviousCamera
    {
        public Transform Follow { get; set; }
        public Transform LookAt { get; set; }
        public float FieldOfView { get; set; }
    }

    private PreviousCamera previousCamera;
    public GameObject interaction;
    public Cinemachine.CinemachineVirtualCamera _camera;
    public GameObject focusObject;
    private bool isFocused = false;
    public float fieldOfView = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // interaction = GetComponent<GameObject>();
        interaction.SetActive(false);
        previousCamera = new PreviousCamera();
    }

    private void Update()
    {
        FocusGameObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interaction.SetActive(true);
            SavePreviousCameraSettings();
            isFocused = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interaction.SetActive(false);
            FocusPreviousCameraSettings();
            isFocused = false;
        }   
    }

    private void FocusGameObject()
    {
        if (!isFocused) return;

        if (Input.GetButtonDown("Submit"))
            FocusCameraSettings();
        else if (Input.GetButtonDown("Cancel"))
            FocusPreviousCameraSettings();
    }

    private void SavePreviousCameraSettings()
    {
        previousCamera.Follow = _camera.Follow;
        previousCamera.LookAt = _camera.LookAt;
        previousCamera.FieldOfView = _camera.m_Lens.FieldOfView;
    }
    private void FocusCameraSettings()
    {
        _camera.Follow = focusObject.transform;
        _camera.LookAt = focusObject.transform;
        _camera.m_Lens.FieldOfView = fieldOfView;
    }

    private void FocusPreviousCameraSettings()
    {
        _camera.Follow = previousCamera.Follow;
        _camera.LookAt = previousCamera.LookAt;
        _camera.m_Lens.FieldOfView = previousCamera.FieldOfView;
    }

}
