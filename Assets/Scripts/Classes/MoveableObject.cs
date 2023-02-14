using System;
using UnityEditor;
using UnityEngine;

public class MoveableObject
{
    [NonSerialized] public bool canPickUP = false;
    [NonSerialized] public bool hasItem = false;
    [NonSerialized] public GameObject objectToPickUp;

    public string label { get; set; }
    public Vector3 LastPosition { get; set; }
    public Transform LastParent { get; set; }
    public MoveableObject()
	{
        label = "teste";
    }
}

public class LastMoveableObject
{
    public GameObject LastParent { get; set; }  
    public Vector3 LastPosition { get; set; }
}

