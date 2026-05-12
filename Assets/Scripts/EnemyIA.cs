using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    [Header("Declaraciones")]
    public GameObject player;
    public gameManager gm;
    private NavMeshAgent agent;
    public Animator anim;

    [Header("ChaseSettins")]
    public bool canChase = false;
    public float chaseDistance = 5f;

    [Header("WayPoints")]
    public Transform[] zones; 
    public float waitTime = 2f;

    private Transform currentZone;
    private Transform[] currentPoints;

    private int currentPointIndex;
    private float waitTimer;
    private bool waiting;

    [Header("StalkEnemy")]
    public int actualPointToStalk;
    public Transform[] pointToStalk;
    public float waitTimeStalk = 5f;
    private float timerStalkCount;

    public enum EnemyState
    {
        Patrol,
        Chase,
        Stalk
    }

    public EnemyState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        currentState = EnemyState.Chase;
        ChooseNewZone();

        anim.SetTrigger("run");
    }

    void Update()
    {
        DetectPlayer();
        StateManager();
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

    public void DetectPlayer()
    {
        if (currentState == EnemyState.Stalk) return;
        if (!canChase) return;

        Debug.Log("estoy detectando");
        
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log("Distancia Enemy -> Player: " + distanceToPlayer);

        if (distanceToPlayer <= chaseDistance)
        {
            currentState = EnemyState.Chase;
            Debug.Log("Te pille");
        }
        
        /*
        else
        {
            currentState = EnemyState.Patrol;
        }*/
    }

    private void ChaseToPlayer()
    {
        agent.destination = player.transform.position;
    }

    public void EnemyPatrolState()
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

    public void EnemyStalkState(int actualPoint)
    {
        agent.destination = pointToStalk[actualPoint].position;

        if(Vector3.Distance(agent.transform.position, pointToStalk[actualPoint].position) < 0.5)
        {
            timerStalkCount += Time.deltaTime;

            if(timerStalkCount >= waitTimeStalk)
            {
                ChooseNewZone();
                currentState = EnemyState.Patrol;
            }
        }
    }

    public void ChangeStalkState(int point)
    {
        actualPointToStalk = point;
        timerStalkCount = 0f;
        currentState = EnemyState.Stalk;
        
    }

    public void StateManager()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                EnemyPatrolState();
                break;
            case EnemyState.Chase:
                ChaseToPlayer();
                anim.SetTrigger("run");
                break;
            case EnemyState.Stalk:
                EnemyStalkState(actualPointToStalk);
                anim.SetTrigger("idle");
                break;
            default:
                break;
        }
    }
}
