using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using System.Collections;
using Assets.Scripts.Controllers;

public class PlayerController : MonoBehaviour
{
    protected ActorController _selectedCharacter;
    protected bool _selectedChanged = false;
    private Transform SelectMarker;
    private Transform LockOnMarker { get; set; }
    
    
    private Vector3 moveStep = Vector3.zero;
    public List<Vector3> _waypoints;
    public Vector3 _targetPosition;
    private int currentWaypoint = 0;

    private SimpleSmoothModifier _modifier;

	// Use this for initialization
	void Start ()
    {
        SelectMarker = transform.Find("SelectionMarker");

        if (!SelectMarker)
        {
            Debug.LogError("Unity.Start()" + name + "has no cursor!");
            enabled = false;
        }

        LockOnMarker = transform.Find("LockOnMarker");

        if (!LockOnMarker)
        {
            Debug.LogError("Unity.Start()" + name + "has no lock on marker!");
            enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(_selectedChanged)
        {
            if (_selectedCharacter)
            {
                Vector3 pos = new Vector3(_selectedCharacter.transform.position.x, _selectedCharacter.transform.position.y + 20, _selectedCharacter.transform.position.z);
                SelectMarker.position = pos;
                SelectMarker.renderer.enabled = true;
                if(_selectedCharacter.Target)
                {
                    pos = new Vector3(_selectedCharacter.Target.position.x, _selectedCharacter.Target.position.y + 20, _selectedCharacter.Target.position.z);
                    LockOnMarker.position = pos;
                    LockOnMarker.renderer.enabled = true;
                }
				else
				{
					LockOnMarker.renderer.enabled = false;
				}
            }
            else
            {
                SelectMarker.renderer.enabled = false;
                LockOnMarker.renderer.enabled = false;
            }
        }
	}

    public void Select(ActorController character)
    {
        _selectedCharacter = character;
        _selectedChanged = true;
    }

    public void MoveTo(Vector3 target)
    {
        if(_selectedCharacter!=null)
        {
            MoveToCommand order = new MoveToCommand() { Target = target };
            _selectedCharacter.PushCommand(order);
        }
    }


    public void Attack(Transform target)
    {
        if(_selectedCharacter!=null)
        {
			_selectedCharacter.Target = target;
            AttackCommand order = new AttackCommand() { Target = _selectedCharacter.Target, Weapon = _selectedCharacter.Weapon};
            _selectedCharacter.PushCommand(order);
        }
    }

    //public void LockOn(Transform transform)
    //{
    //    if (_selectedCharacter)
    //    {
    //        _selectedCharacter.LockOn(transform);
    //    }
    //}

    public void FixedUpdate()
    {
        //if (_waypoints == null)
        //{
        //    //We have no path to move after yet
        //    return;
        //}

        //if (currentWaypoint >= _waypoints.Count)
        //{
        //    Debug.Log("End Of Path Reached");
        //    _waypoints = null;
        //    return;
        //}

        //if (MoveStraightTo(_waypoints[currentWaypoint]))
        //{
        //    currentWaypoint++;
        //}

        
        
        
        
        
        
        ////Direction to the next waypoint
        //Vector3 dir = (_waypoints[currentWaypoint] - _selectedCharacter.transform.position).normalized;
        //dir *= speed * Time.fixedDeltaTime;
        //dir.Set(dir.x, 0, dir.z);
        //_selectedCharacter.transform.LookAt(_waypoints[currentWaypoint]);
        //_selectedCharacter.transform.position += dir;


        ////_selectedCharacter.transform.position += new Vector3(-1, 0, -1);

        ////Check if we are close enough to the next waypoint
        ////If we are, proceed to follow the next waypoint
        //if (Vector3.Distance(_selectedCharacter.transform.position, _waypoints[currentWaypoint]) < nextWaypointDistance)
        //{
        //    currentWaypoint++;
        //    return;
        //}
    }


    public void Aim(Vector3 transform)
    {
        if(_selectedCharacter)
        {
            _selectedCharacter.Aim(transform);
        }
    }

    public void Fire()
    {
        if (_selectedCharacter)
        {
            _selectedCharacter.Fire(transform);
        }
    }

    

    
}
