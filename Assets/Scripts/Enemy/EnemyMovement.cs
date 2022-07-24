using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{    
    [SerializeField] private float _speed=1;
    [SerializeField] private List<Vector2> _travelPointsCoordinate;

    private EnemyTravelPoint[] _travelPoints;    

    private bool _isWatchingRight = false;
    private bool _isMoving;
    private Coroutine _moving;

    void OnEnable()
    {
        _travelPoints = GetComponentsInChildren<EnemyTravelPoint>();

        if (_travelPoints == null)
        {
            Debug.LogWarning(gameObject.name + ": не заданы координаты пути. Объект будет удален.");
            Destroy(gameObject);            
        }
        else
        {
            for (int i = 0; i < _travelPoints.Length; i++)
            {
                _travelPointsCoordinate.Add(_travelPoints[i].transform.position);
            }

            transform.position = _travelPointsCoordinate[0];
        }

        if (_travelPointsCoordinate.Count > 1)
        {
            _moving = StartCoroutine(MoveToPoint());
        }        
    }    

    private void TurnAround()
    {
        _isWatchingRight = !_isWatchingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    private bool IsGoRight(Vector2 nextPoint)
    {
        bool isGoRight;

        if (transform.position.x <= nextPoint.x)
        {
            isGoRight = true;
        }
        else
        {
            isGoRight = false;
        }

        return isGoRight;
    }

    private IEnumerator MoveToPoint()
    {
        _isMoving = true;
        int nextPointNumber = 1;

        while(_isMoving)
        {
            if (IsGoRight(_travelPointsCoordinate[nextPointNumber]) && !_isWatchingRight || !IsGoRight(_travelPointsCoordinate[nextPointNumber]) && _isWatchingRight)
                TurnAround();

            while ((Vector2)transform.position != _travelPointsCoordinate[nextPointNumber])
            {
                this.transform.position = Vector2.MoveTowards(transform.position, _travelPointsCoordinate[nextPointNumber], Time.deltaTime * _speed);
                yield return null;
            }

            if (nextPointNumber == _travelPointsCoordinate.Count - 1)
            {
                nextPointNumber = 0;
            }
            else
            {
                nextPointNumber++;
            }
        }
    }
}
