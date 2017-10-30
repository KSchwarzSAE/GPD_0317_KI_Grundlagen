using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgent : MonoBehaviour
{

    [SerializeField]
    private PathPoint m_target;

    [SerializeField]
    private PathPoint m_current;

    void Start()
    {
        SetDestination(m_target);
    }

    public void SetDestination(PathPoint _p)
    {
		StartCoroutine(FollowPath(AStar.CalculatePath(m_current, _p), m_target, m_current));
    }

	private IEnumerator FollowPath(List<Vector3> _path, PathPoint _target, PathPoint _current)
    {
        if (_path == null)
        {
            Debug.LogWarning("Path is null!");
            yield break;
        }

        for(int i = 0; i < _path.Count; ++i)
        {
            while ((_path[i] - transform.position).sqrMagnitude > 0.025)
            {
                Vector3 move = (_path[i] - transform.position);

                if (move.magnitude > Time.deltaTime * 3.0f)
                    move = move.normalized * Time.deltaTime * 3.0f;

                transform.Translate(move);

                // bis zum nächsten update warten
                yield return null;
            }
        }

		StartCoroutine(FollowPath(AStar.CalculatePath(_target, _current), _target, _current));
    }

}
