using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TerminalCode : MonoBehaviour
{
    [SerializeField]
    private Sprite[] digits;
    [SerializeField]
    private Image[] characters;

    private string codeSequence;

    public int number;
    public GameObject input;
    public TextMeshPro Texttext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void InsertCode()
    {
        Debug.Log("Hello");
        // input = gameObject.GetComponent<GameObject>();
       //  var x = input.GetComponent<Text>();
        Texttext.text = "Hello";
        // input.GetComponentInChildren<Text>().text = number.ToString();
    }

    public void ResetCode()
    {
        
    }
}
