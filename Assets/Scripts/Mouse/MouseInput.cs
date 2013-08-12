using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour
{
    private RaycastHit _hit;
    private bool _isHit;
    private float _length = 1000;
    //public Transform Marker;
    public Camera MainCamera;
    private bool _isScrolling = false;
    private Vector3 _mousePosition;
    public float ScrollingSpeed;
    public float ZoomSpeed = 20f;
    public float MinZoomFOV = 10f;
	public float MaxZoomFOV = 40f;
	
    public PlayerController PlayerController;
    public ScreenController ScreenController;
	
    public int ScreenEdgeDelta;
    public float ScreenScrollSpeed;
	public Transform Floor;
    private Ray _ray;

	// Update is called once per frame

    void Start()
    {
        //Marker = transform.Find("SelectionMarker");
        //if (!Marker)
        //{
        //    Debug.LogError("Unity.Start()" + name + "has no cursor!");
        //    enabled = false;
        //}
		
		Floor = GameObject.Find("InvisibleFloor").transform;
		if (!Floor)
        {
            Debug.LogError("Unity.Start()" + name + "has no invisible floor!");
            enabled = false;
        }
		
		ScreenController = GetComponent<ScreenController>();
		if (!ScreenController)
        {
            Debug.LogError("Unity.Start()" + name + "has no screen controller!");
            enabled = false;
        }
		
        
    }

    #region Processing

	void Update()
	{
	    // Tracking cursor
        _ray = MainCamera.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(_ray, out _hit, _length))
		{
			
            if (_hit.collider.name != "SelectionMarker")
            {
				if(_hit.collider.name == "Plane")
				{
	                Debug.Log(_hit.collider.name);
	                Vector3 pos = new Vector3(_hit.point.x, _hit.point.y + 20, _hit.point.z);
	
                    //Marker.position = pos;
                    //Marker.renderer.enabled = true;
				}
                
            }
			_isHit = true;
            PlayerController.Aim(_hit.point);

        }
        else
        {

            //Marker.renderer.enabled = false;
            _isHit = false;

        }
		
		//ProcessMouseOver();
	    
	    ProcessLeftMouseButton();
        ProcessRightMouseButton();
		ProcessMiddleMouseButton();
        //ProcessMouseOnScreenEdge();
        OnMouseScroll();
        //ProcessnLeftMouse();

        Debug.DrawRay(_ray.origin, _ray.direction * _length);
        
        //Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(hit.transform.position, (hit.collider as CapsuleCollider).radius);
	}

    private void ProcessMouseOnScreenEdge()
    {
        float x = 0;
        float y = 0;
        if (Input.mousePosition.x >= Screen.width - ScreenEdgeDelta) x = ScreenScrollSpeed;
        if (Input.mousePosition.x <= ScreenEdgeDelta) x = -ScreenScrollSpeed;

        if (Input.mousePosition.y >= Screen.height - ScreenEdgeDelta) y = ScreenScrollSpeed;
        if (Input.mousePosition.y <= ScreenEdgeDelta) y = -ScreenScrollSpeed;

        ScreenController.ScrollScreen(x, y);
        // Move the camera

        //transform.position += mRightDirection * Time.deltaTime * mSpeed;

    }

    void OnGUI()
    {
        //ProcessLeftMouseButton();
        //ProcessRightMouseButton();
        //OnMouseScroll();
    }

    protected void ProcessLeftMouseButton()
    {
        if (Input.GetMouseButton(0))
		{
        	OnLeftMouseDrag();
		}

        if (Input.GetButtonDown("Fire1"))
        {
        }

        if (Input.GetButtonUp("Fire1"))
        {
            OnMouseLeftClick();
        }
    }

    

    protected void ProcessRightMouseButton()
    {
        if (Input.GetMouseButton(1))
		{
			OnRightMouseDrag();
		}

        if (Input.GetButtonDown("Fire2"))
        {
        }

        if (Input.GetButtonUp("Fire2"))
        {
            OnMouseRightClick();
        }

    }

    private void ProcessMiddleMouseButton()
    {
        if (Input.GetMouseButton(2))
        {
            OnMiddleMouseDrag();
        }

        if (Input.GetButtonDown("Fire3"))
        {
        }

        if (Input.GetButtonUp("Fire3"))
        {
        }
    }

    

    #endregion Processing

    #region Event Handlers

    private void OnMouseScroll()
    {
        ScreenController.Zoom(Input.GetAxis("Mouse ScrollWheel"), -ZoomSpeed);
    }
	
    protected void OnMouseLeftClick()	
    {
        if (_hit.collider is Collider)
        {
			ActorController controller = _hit.collider.GetComponent<ActorController>();
            if(controller) PlayerController.Select(controller);
        }
        else
        {
            PlayerController.Select(null);
        }
    }

    private void OnMouseRightClick()
    {
        if (_hit.collider == null) return;

        if (_hit.collider is Collider)
        {
            if (_hit.collider.name == "InvisibleFloor")
            {
                PlayerController.MoveTo(_hit.point);
                //PlayerController.Fire();
            }
            else
            {

                if(_hit.transform.GetComponent<ActorController>()) PlayerController.Attack(_hit.transform);
            }
        }
    }

    protected void OnLeftMouseDrag()
    {
        //float x = -Input.GetAxis("Mouse X") * ScrollingSpeed;
        //float y = -Input.GetAxis("Mouse Y") * ScrollingSpeed;
        //ScreenController.ScrollScreen(x, y);
    }

    private void OnMiddleMouseDrag()
    {
        float x = Input.GetAxis("Mouse X") * ScrollingSpeed;
        float y = Input.GetAxis("Mouse Y") * ScrollingSpeed;
        ScreenController.ScrollScreen(-x, -y);
    }


    protected void OnRightMouseDrag()
    {

        //float alpha = MainCamera.transform.rotation.y;
        //float betha = ((MainCamera.transform.rotation.y * (180 / Mathf.PI)) + 90f) * (Mathf.PI / 180);
			
        //float x = Input.GetAxis("Mouse X")*ScrollingSpeed;
        //float z = Input.GetAxis("Mouse Y")*ScrollingSpeed;

        //float xOffset = Mathf.Sin(alpha)*z + Mathf.Sin(betha)*x;
        //float zOffset = Mathf.Cos(alpha)*z + Mathf.Cos(betha)*x;
			
        //Vector3 mainPosition = new Vector3(
        //    xOffset
        //    , 0
        //    , zOffset);
        //MainCamera.transform.position += mainPosition;
    }

    #endregion
}
