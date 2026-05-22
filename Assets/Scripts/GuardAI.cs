using UnityEngine;
using UnityEngine.AI;

public enum GuardState { Patrol, Chase, Investigate, Return }

public class GuardAI : MonoBehaviour
{
    public Transform[] waypoints;

    public float visionRange = 12f;
    public float visionAngle = 90f;
    public float eyeHeight = 0.5f;
    public float playerTargetHeight = 1f;

    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;

    public float visionChaseTime = 1f;
    public float loseSightTime = 1.5f;
    public float catchPlayerTime = 3f;
    public float investigateWaitTime = 3f;
    public float fastDetectTime = 0.5f;

    public bool PlayerInZone = false;

    private NavMeshAgent agent;
    private Transform player;
    private Renderer rend;
    private GuardState state;
    private int waypointIndex;

    private float visionTimer;
    private float loseSightTimer;
    private float catchTimer;
    private float investigateTimer;
    private bool hasCaughtPlayer = false;

    private Vector3 lastKnownPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();
        hasCaughtPlayer = false;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;

        StartPatrol();
    }

    void Update()
    {
        switch (state)
        {
            case GuardState.Patrol: PatrolState(); break;
            case GuardState.Chase: ChaseState(); break;
            case GuardState.Investigate: InvestigateState(); break;
            case GuardState.Return: ReturnState(); break;
        }
    }

    void PatrolState()
    {
        rend.material.color = Color.gray;
        DetectPlayer(visionChaseTime);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }

    void ChaseState()
    {
        rend.material.color = Color.red;
        if (player == null) return;

        PlayerDisguise disguise = player.GetComponent<PlayerDisguise>();
        if (disguise != null && disguise.IsDisguised)
        {
            PlayerInZone = false;
            StartInvestigate(); 
            return;
        }

        agent.SetDestination(player.position);

        agent.SetDestination(player.position);

        if (CanSeePlayer() || PlayerInZone)
        {
            loseSightTimer = 0f;
            lastKnownPosition = player.position;
            catchTimer += Time.deltaTime;

            if (catchTimer >= catchPlayerTime && !hasCaughtPlayer)
            {
                hasCaughtPlayer = true;
                CaughtScreen.Instance?.ShowCaught();
            }
        }
        else
        {
            catchTimer = 0f;
            loseSightTimer += Time.deltaTime;
            if (loseSightTimer >= loseSightTime)
                StartInvestigate();
        }
    }

    void InvestigateState()
    {
        rend.material.color = new Color(1f, 0.5f, 0f);
        DetectPlayer(fastDetectTime);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            investigateTimer -= Time.deltaTime;
            if (investigateTimer <= 0f)
                StartReturn();
        }
    }

    void ReturnState()
    {
        rend.material.color = Color.yellow;
        DetectPlayer(fastDetectTime);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            StartPatrol();
    }

    void DetectPlayer(float visionThreshold)
    {
        if (player != null)
        {
            PlayerDisguise disguise = player.GetComponent<PlayerDisguise>();
            if (disguise != null && disguise.IsDisguised)
            {
                PlayerInZone = false;
                visionTimer = 0f;
                return;
            }
        }

        if (PlayerInZone)
        {
            ForceChase();
            return;
        }

        if (CanSeePlayer())
        {
            visionTimer += Time.deltaTime;
            if (visionTimer >= visionThreshold)
                StartChase();
        }
        else
        {
            visionTimer = 0f;
        }
    }

    public void ForceChase()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > visionRange * 2f)
        {
            PlayerInZone = false;
            return;
        }

        if (state != GuardState.Chase)
            StartChase();
    }

    void StartPatrol()
    {
        state = GuardState.Patrol;
        agent.speed = patrolSpeed;
        ResetTimers();
        waypointIndex = 0;

        if (waypoints.Length > 0)
            agent.SetDestination(waypoints[waypointIndex].position);
    }

    void StartChase()
    {
        state = GuardState.Chase;
        agent.speed = chaseSpeed;
        ResetTimers();

        if (player != null)
            lastKnownPosition = player.position;
    }

    void StartInvestigate()
    {
        state = GuardState.Investigate;
        agent.speed = patrolSpeed;
        ResetTimers();
        investigateTimer = investigateWaitTime;
        agent.SetDestination(lastKnownPosition);
    }

    void StartReturn()
    {
        state = GuardState.Return;
        agent.speed = patrolSpeed;
        ResetTimers();

        if (waypoints.Length > 0)
            agent.SetDestination(waypoints[0].position);
    }

    void ResetTimers()
    {
        visionTimer = 0f;
        loseSightTimer = 0f;
        catchTimer = 0f;
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        PlayerDisguise disguise = player.GetComponent<PlayerDisguise>();
        if (disguise != null && disguise.IsDisguised)
            return false;

        Vector3 eyePos = transform.position + Vector3.up * eyeHeight;
        Vector3 playerPos = player.position + Vector3.up * playerTargetHeight;
        Vector3 dirToPlayer = playerPos - eyePos;
        float distance = dirToPlayer.magnitude;

        if (distance > visionRange) return false;

        float angle = Vector3.Angle(transform.forward, dirToPlayer);
        if (angle > visionAngle * 0.5f) return false;

        RaycastHit hit;
        if (Physics.Raycast(eyePos, dirToPlayer.normalized, out hit, distance,
            ~0, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Player"))
                return true;

            return false;
        }

        return true;
    }
}