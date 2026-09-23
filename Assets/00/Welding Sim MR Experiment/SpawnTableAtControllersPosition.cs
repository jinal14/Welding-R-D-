using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class SpawnTableAtControllersPosition : MonoBehaviour
{
    public float yHelper;
    public Vector3 rightControllersPosition,leftControllerPosition;
    private Quaternion rightControllerRotation, leftControllerRotation;
    public GameObject tablePrefab;
    public GameObject boxPrefab,sirsBox;
    public Transform boxPosition;

    public GameObject udpGameObject;

    public TextMeshProUGUI debugText;


    private void Start()
    {
        rightControllersPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        
        leftControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        
        
    }

    private void Update()
    {
        rightControllersPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        leftControllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

        rightControllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        leftControllerRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            SpawnTable();
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            SpawnBox();
        }
    }

    void SpawnTable()
    {         
        if (rightControllersPosition != null) 
        { 
            Instantiate(tablePrefab, new Vector3(rightControllersPosition.x,rightControllersPosition.y-yHelper,rightControllersPosition.z), Quaternion.identity);//new Quaternion(0.00641f,0.02004f,-0.00319f,1)
            debugText.text = rightControllerRotation.ToString();
            udpGameObject.SetActive(true);
        }
    }

    void SpawnBox()
    {
        if (leftControllerPosition != null)
        {
            Instantiate(sirsBox, new Vector3(leftControllerPosition.x,leftControllerPosition.y-yHelper,leftControllerPosition.z), Quaternion.identity);
        }
        /*boxPosition = GameObject.FindWithTag("partSeven").transform;
        if(boxPosition) 
            Instantiate(boxPrefab, boxPosition.position, Quaternion.identity); */
    }
}
