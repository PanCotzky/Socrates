using System;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using System.Collections;

public class MoveToCommand : AICommand
{
    //private List<Vector3> _waypoints;
    public Vector3 Target;
    //public Seeker _pathFinder;
    //public int _currentWaypoint;
    //private bool _pathFindingReady;
    //private Hashtable _moveStreightToParams;

    //[HideInInspector]
    //private iTweenPath _path;

    //[HideInInspector]
    //private iTweenEvent _pathMover;

	// Use this for initialization

    public override bool Run()
    {
        if (!Character || Target==null)
        {
            return false;
        }
        Character.MovementController.MoveStraightTo(Target);
        return true;
    }

    //public override void OnPop()
    //{
    //    //GameObject.Destroy(_path);
    //}

    public override bool Process()
    {
        //if(!_pathFindingReady)
        //{
        //    return false;
        //}

        //if (_waypoints == null)
        //{
        //    //We have no path to move after yet
        //    return true;
        //}

        //if(_path.nodes.Count==0)
        //{
        //    return true;
        //}


        //if (_currentWaypoint >= _waypoints.Count)
        //{
        //    Debug.Log("End Of Path Reached");
        //    _waypoints = null;
        //    return true;
        //}

        //if (MoveStraightTo(_waypoints[_currentWaypoint]))
        //{
        //    _currentWaypoint++;
        //}




        return Finished;
    }

    //public void MoveStraightTo(Vector3 waypoint)
    //{

    //    Target = waypoint;
    //    Target.Set(Target.x, 0, Target.z);

    //    _moveStreightToParams.Add("path", new Vector3[] { Character.transform.position, Target });

    //    if (Target != null)
    //    {
    //        iTween.MoveTo(Character.gameObject, _moveStreightToParams);
    //    }
    //    Debug.Log("Passed");

    //    //Vector3 dir = (waypoint - Character.transform.position).normalized;
    //    //dir *= Character.MoveSpeed * Time.fixedDeltaTime;
    //    //dir.Set(dir.x, 0, dir.z);
    //    //Character.transform.LookAt(waypoint);
    //    //Character.transform.position += dir;

    //    ////Check if we are close enough to the next waypoint
    //    ////If we are, proceed to follow the next waypoint
    //    //if (Vector3.Distance(Character.transform.position, waypoint) < Character.NextWaypointDistance)
    //    //{
    //    //    return true;
    //    //}
    //    //return false;
    //}

    //public void OnPathFound(Path p)
    //{
    //    Debug.Log("Yey, we got a path back. Did it have an error? " + p.error);
    //    if (!p.error)
    //    {
    //        //var _modifier = new SimpleSmoothModifier();
    //        //_modifier.Apply(p, ModifierData.Vector);
    //        //_pathFinder.PostProcess(p);
    //        _waypoints = p.vectorPath;//_modifier.SmoothSimple(p.vectorPath);
    //        _path.nodes.Clear();
    //        _path.nodes.AddRange(_waypoints);
    //        //Reset the waypoint counter
    //        _currentWaypoint = 1;
    //        _pathFindingReady = true;
    //        _pathMover.Play();
    //    }
    //}

    //public void OnFinished()
    //{
    //    Finished = true;
    //    //if (Finished != null) Finished(this, EventArgs.Empty);
    //}
}
