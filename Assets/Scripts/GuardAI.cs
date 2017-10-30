using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{

    // SerializeField zeigt die private/protected Variable im Editor an.
    [SerializeField]
    private Transform[] m_waypoints;

    [SerializeField]
    private int m_currentWaypoint;

    [SerializeField]
    private float m_speed = 0.5f;

    private NavMeshAgent m_agent;

    /*
    // HideInInspector versteckt die Variable im Editor.
    [HideInInspector]
    public int Range;

    // Range sorgt dafür, das im Editor nur Werte innerhalb der Range gewählt werden können. (+ Slider)
    [Range(0.0f, 10.0f)]
    // Header erzeugt eine Überschrift im Editor vor der Variable
    [Header("Test")]
    // Tooltip zeigt einen tooltip an, wenn man im Editor mit der Maus über der Variable ist.
    [Tooltip("Das ist eine test Variable.")]
    public float test;
    */

	// Use this for initialization
	void Start ()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.SetDestination(m_waypoints[m_currentWaypoint].position);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if ((m_waypoints[m_currentWaypoint].position - transform.position).sqrMagnitude < 0.025)
        {
            m_currentWaypoint = (m_currentWaypoint + 1) % m_waypoints.Length;

            m_agent.SetDestination(m_waypoints[m_currentWaypoint].position);
        }
    }

}
