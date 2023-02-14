using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject menu;

    private void Awake()
    {
        // menu = GameObject.Find("Canvas").GetComponent<Canvas>();
        menu.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Fim");
        menu.SetActive(true);
    }
}
