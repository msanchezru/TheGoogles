using System.Collections.Generic;
using UnityEngine;

public class TargetWander : MonoBehaviour
{
    private float speed = 1.3f;
    [SerializeField, Min(0f)] private float arrivalDistance = 0.1f;

    private readonly List<Transform> _validPoints = new();
    private Transform _currentPoint;
    private Transform _previousPoint;

    private void Start()
    {
        Transform pointsParent = transform.parent;
        if (pointsParent == null)
        {
            Debug.LogError("The wander points must be children of this object's parent.", this);
            enabled = false;
            return;
        }

        foreach (Transform point in pointsParent.GetComponentsInChildren<Transform>(includeInactive: false))
        {
            if (point != pointsParent &&
                point != transform &&
                point.name.IndexOf("point", System.StringComparison.OrdinalIgnoreCase) >= 0)
            {
                _validPoints.Add(point);
            }
        }

        if (_validPoints.Count == 0)
        {
            Debug.LogError("No active child objects with 'point' in their name were found.", this);
            enabled = false;
            return;
        }

        SelectNextPoint();
    }

    private void Update()
    {
        Vector3 toPoint = _currentPoint.position - transform.position;
        if (toPoint.sqrMagnitude <= arrivalDistance * arrivalDistance)
        {
            transform.position = _currentPoint.position;
            _previousPoint = _currentPoint;
            SelectNextPoint();
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            _currentPoint.position,
            speed * Time.deltaTime);
    }

    private void SelectNextPoint()
    {
        int nextIndex = Random.Range(0, _validPoints.Count);

        if (_validPoints.Count > 1 && _validPoints[nextIndex] == _previousPoint)
        {
            for (int i = 0; i < _validPoints.Count; i++)
            {
                int candidateIndex = (nextIndex + i) % _validPoints.Count;
                if (_validPoints[candidateIndex] != _previousPoint)
                {
                    nextIndex = candidateIndex;
                    break;
                }
            }
        }

        _currentPoint = _validPoints[nextIndex];
    }
}