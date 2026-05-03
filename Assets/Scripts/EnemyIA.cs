using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyIA : MonoBehaviour
{
    [Header("Declaraciones")]
    public Transform player;
    public gameManager gm;
    private NavMeshAgent agent;
    public Animator anim;

    [Header("ChaseSettins")]
    public bool isChasing = false;
    public float chaseDistance = 5f;

    [Header("WayPoints")]
    public Transform[] zones; 
    public float waitTime = 2f;

    private Transform currentZone;
    private Transform[] currentPoints;

    private int currentPointIndex;
    private float waitTimer;
    private bool waiting;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        ChooseNewZone();
        anim.SetTrigger("run");
    }

    void Update()
    {
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            
            if (waitTimer >= waitTime)
            {
                waiting = false;
                anim.SetTrigger("run");
                GoToNextPoint();
            }

            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waiting = true;
            waitTimer = 0f;
            anim.SetTrigger("idle");
        }
    }

    void ChooseNewZone()
    {
        // elegir zona alea
        currentZone = zones[Random.Range(0, zones.Length)];

        // coger hijos
        currentPoints = new Transform[currentZone.childCount];

        for (int i = 0; i < currentZone.childCount; i++)
        {
            currentPoints[i] = currentZone.GetChild(i);
        }

        currentPointIndex = 0;
        GoToNextPoint();
    }

    void GoToNextPoint()
    {
        if (currentPoints.Length == 0) return;

        agent.destination = currentPoints[currentPointIndex].position;

        currentPointIndex++;

        // si termina la zona  cambia a otra
        if (currentPointIndex >= currentPoints.Length)
        {
            ChooseNewZone();
        }
    }
}
