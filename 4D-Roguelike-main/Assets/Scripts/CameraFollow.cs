
using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class CameraFollow : MonoBehaviour
{
    public Camera cam;
    
    [Header("CameraMove")]
    public Transform theTarget;
    public static CameraFollow Instance;
    //public Transform targetPlr, targetXr, targetXl, targetYr, targetYl, targetZr, targetZl, targetWr, targetWl;
    public string cameraTarget;
    public float smoothSpeed = 1.69f; public Vector3 zoomOffset;


    [Header("HP background color")]
    public Color camPlrColorHP = Color.white;
    public float duration = 3.0F;
    
    MapDrawer gridSize;


    void Start() {
        //theTarget = targetPlr;
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
    }

    void FixedUpdate() { 
        //MoveCamera(); // the old look far 
        transform.position = Vector3.Lerp(transform.position, theTarget.position + zoomOffset, smoothSpeed);
        //cam.backgroundColor = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(Time.time, duration) / duration);
        //cam.backgroundColor = 
    } //UnityEngine.Color


    /*public void MoveCamera() {

        switch (cameraTarget) {
            default: break;
            case "targetXr": transform.position = Vector3.Lerp(transform.position, targetXr.position + zoomOffset, smoothSpeed); break;
            case "targetXl": transform.position = Vector3.Lerp(transform.position, targetXl.position + zoomOffset, smoothSpeed); break;

        }
        gridSize.UpdateScreen();
    }

    public void LookFarXr() { cameraTarget = "targetXr"; }
    public void LookFarXl() { cameraTarget = "targetXl"; }
    public void LookFarYr() { theTarget = targetYr; }
    public void LookFarYl() { theTarget = targetYl; }
    public void LookFarZr() { theTarget = targetXr; }
    public void LookFarZl() { theTarget = targetXl; }
    public void LookFarWr() { theTarget = targetYr; }
    public void LookFarWl() { theTarget = targetYl; }

    //public void LookFar(Vector3 newFocus) { theTarget = newFocus; }
    public void LookBack() { theTarget = targetPlr; }*/

}