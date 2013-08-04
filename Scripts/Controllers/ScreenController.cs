using UnityEngine;
using System.Collections;

public class ScreenController : MonoBehaviour
{
    public Camera MainCamera;
    public float MinZoomFOV;
    public float MaxZoomFOV;

	// Use this for initialization
	void Start ()
    {
        var camera = transform.Find("MainCamera");
	    MainCamera = camera.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    /// <summary>
    /// Zooms the camera
    /// </summary>
    /// <param name="dir">Direction of zoom. If positive - zooms in, if negative - zooms out.</param>
    /// <param name="MinZoomFOV">Minimum zoom in distance.</param>
    /// <param name="MaxZoomFOV">Maximum zoom out distance.</param>
    /// <param name="ZoomSpeed">Zoom speed.</param>
    public void Zoom(float dir, float ZoomSpeed)
    {
        if (dir != 0)
        {
            MainCamera.orthographicSize -= (ZoomSpeed) * dir;

            if (MainCamera.orthographicSize < MinZoomFOV)
            {
                MainCamera.orthographicSize = MinZoomFOV;
            }

            if (MainCamera.orthographicSize > MaxZoomFOV)
            {
                MainCamera.orthographicSize = MaxZoomFOV;
            }
        }
    }

    /// <summary>
    /// Moves the camera across the map
    /// </summary>
    /// <param name="x">X movement</param>
    /// <param name="y">Y movement</param>
    public void ScrollScreen(float x, float y)
    {
        float alpha = MainCamera.transform.rotation.y;
        float betha = ((MainCamera.transform.rotation.y * (180 / Mathf.PI)) + 90f) * (Mathf.PI / 180);

        float xOffset = Mathf.Sin(alpha) * y + Mathf.Sin(betha) * x;
        float zOffset = Mathf.Cos(alpha) * y + Mathf.Cos(betha) * x;

        //if(MainCamera.transform.position.x+xOffset>Floor.localScale.x)
        //{
        //    xOffset = xOffset - (MainCamera.transform.position.x + xOffset - Floor.localScale.x);
        //}
        //if (MainCamera.transform.position.z + zOffset > Floor.localScale.z)
        //{
        //    zOffset = zOffset - (MainCamera.transform.position.z + zOffset - Floor.localScale.x);
        //}

        //if (MainCamera.transform.position.x + xOffset < -Floor.localScale.x)
        //{
        //    xOffset = xOffset - (Floor.localScale.x - MainCamera.transform.position.x + xOffset);
        //}
        //if (MainCamera.transform.position.z + zOffset > Floor.localScale.z)
        //{
        //    zOffset = zOffset - (Floor.localScale.z - MainCamera.transform.position.z + zOffset);
        //}

        Vector3 mainPosition = new Vector3(
            xOffset
            , 0
            , zOffset);
        MainCamera.transform.position -= mainPosition;
    }


    //public void RotateScreen(float y)
    //{
    //    float anglToRad = (y * RotationSpeed) * (Mathf.PI / 180);
    //    //if(MainCamera.transform.position.x+xOffset>Floor.localScale.x)
    //    //{
    //    //    xOffset = xOffset - (MainCamera.transform.position.x + xOffset - Floor.localScale.x);
    //    //}
    //    //if (MainCamera.transform.position.z + zOffset > Floor.localScale.z)
    //    //{
    //    //    zOffset = zOffset - (MainCamera.transform.position.z + zOffset - Floor.localScale.x);
    //    //}

    //    //if (MainCamera.transform.position.x + xOffset < -Floor.localScale.x)
    //    //{
    //    //    xOffset = xOffset - (Floor.localScale.x - MainCamera.transform.position.x + xOffset);
    //    //}
    //    //if (MainCamera.transform.position.z + zOffset > Floor.localScale.z)
    //    //{
    //    //    zOffset = zOffset - (Floor.localScale.z - MainCamera.transform.position.z + zOffset);
    //    //}

    //    float rx = MainCamera.transform.rotation.x;
    //    float rz = MainCamera.transform.rotation.z;
    //    float rw = MainCamera.transform.rotation.w;

    //    MainCamera.transform.rotation.Set(rx, anglToRad, rz, rw);
    //}
}