using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvel : MonoBehaviour
{
    public RuntimeAnimatorController animatorController;

    // Start is called before the first frame update
    void Start()
    {
        // GameObject[] plataforms = GameObject.Find("Field").GetComponentsInChildren<GameObject>();
        /*
        Transform[] plataforms = GameObject.Find("Field").GetComponentsInChildren<Transform>();

        foreach (Transform t in plataforms)
        {
            if (t.name == "Field") continue;

            t.gameObject.AddComponent<Animator>();
            t.gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController;
        }
        */
        transform.position = new Vector3(1038.59277f, 786.049988f, -191.487335f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider == null) return;

        if (hit.gameObject.CompareTag("red"))
        {
            GameObject.Destroy(hit.gameObject, 1 * Time.deltaTime);

            Invoke("LoadScene",.5f);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
