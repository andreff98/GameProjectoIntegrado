using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CircusDetection : MonoBehaviour
{
    private ParticleSystem explosion;
    private AudioSource explosionSound;

    public GameObject efeitosVisuais;
    // public GameObject menu;
    public GameObject menu;

    private void Awake()
    {
        explosion = efeitosVisuais.GetComponent<ParticleSystem>();
        explosionSound = GetComponent<AudioSource>();
        explosion.Stop();
        // menu = GetComponent<GameObject>();
        // explosionSound.Stop();
    }

    #region setup
    //mesh properties
    Mesh mesh;
    public Vector3[] polygonPoints;
    public int[] polygonTriangles;

    //polygon properties
    public bool isFilled;
    public int polygonSides;
    public float polygonRadius;
    public float centerRadius;

    //Codigo meu
    MeshCollider _MeshCollider;
    private bool isMeshColliderCreated = false;

    void CreateMeshCollider()
    {
        if (isMeshColliderCreated) return;

        _MeshCollider =  this.gameObject.AddComponent<MeshCollider>();
        _MeshCollider.convex = true;
        _MeshCollider.isTrigger = true;
        isMeshColliderCreated = true;
    }

    void Start()
    {
        mesh = new Mesh();
        mesh.name = "teste";
        _MeshCollider = new MeshCollider();
        this.GetComponent<MeshFilter>().mesh = mesh;
        // this.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void Update()
    {
        if (isFilled)
        {
            DrawFilled(polygonSides, polygonRadius);
        }
        else
        {
            DrawHollow(polygonSides, polygonRadius, centerRadius);
        }
    }
    #endregion

    #region Alghorits
    void DrawFilled(int sides, float radius)
    {
        polygonPoints = GetCircumferencePoints(sides, radius).ToArray();
        polygonTriangles = DrawFilledTriangles(polygonPoints);
        mesh.Clear();
        mesh.vertices = polygonPoints;
        mesh.triangles = polygonTriangles;
        CreateMeshCollider();
    }

    void DrawHollow(int sides, float outerRadius, float innerRadius)
    {
        List<Vector3> pointsList = new List<Vector3>();
        List<Vector3> outerPoints = GetCircumferencePoints(sides, outerRadius);
        pointsList.AddRange(outerPoints);
        List<Vector3> innerPoints = GetCircumferencePoints(sides, innerRadius);
        pointsList.AddRange(innerPoints);

        polygonPoints = pointsList.ToArray();

        polygonTriangles = DrawHollowTriangles(polygonPoints);
        mesh.Clear();
        mesh.vertices = polygonPoints;
        mesh.triangles = polygonTriangles;
        CreateMeshCollider();
    }

    int[] DrawHollowTriangles(Vector3[] points)
    {
        int sides = points.Length / 2;
        int triangleAmount = sides * 2;

        List<int> newTriangles = new List<int>();
        for (int i = 0; i < sides; i++)
        {
            int outerIndex = i;
            int innerIndex = i + sides;

            //first triangle starting at outer edge i
            newTriangles.Add(outerIndex);
            newTriangles.Add(innerIndex);
            newTriangles.Add((i + 1) % sides);

            //second triangle starting at outer edge i
            newTriangles.Add(outerIndex);
            newTriangles.Add(sides + ((sides + i - 1) % sides));
            newTriangles.Add(outerIndex + sides);
        }
        return newTriangles.ToArray();
    }

    List<Vector3> GetCircumferencePoints(int sides, float radius)
    {
        List<Vector3> points = new List<Vector3>();
        float circumferenceProgressPerStep = (float)1 / sides;
        float TAU = 2 * Mathf.PI;
        float radianProgressPerStep = circumferenceProgressPerStep * TAU;

        for (int i = 0; i < sides; i++)
        {
            float currentRadian = radianProgressPerStep * i;
            points.Add(new Vector3(Mathf.Cos(currentRadian) * radius, Mathf.Sin(currentRadian) * radius, 0));
        }
        return points;
    }

    int[] DrawFilledTriangles(Vector3[] points)
    {
        int triangleAmount = points.Length - 2;
        List<int> newTriangles = new List<int>();
        for (int i = 0; i < triangleAmount; i++)
        {
            newTriangles.Add(0);
            newTriangles.Add(i + 2);
            newTriangles.Add(i + 1);
        }
        return newTriangles.ToArray();
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(this.gameObject.name);
            explosion.Play();
            explosionSound.Play();
            CustomCamera camera = new CustomCamera(Camera.main.GetComponent<Transform>());
            camera.shakeDuration = 5f;
            camera.ShakeCamera();
            // GameObject dead = GameObject.Find("Dead").GetComponent<GameObject>();
            // menu.enabled = true;
            menu.SetActive(true);

            Invoke("PauseGame", 1);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
