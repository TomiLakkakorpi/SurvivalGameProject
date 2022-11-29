using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    //Gameobject for recognizing enemies tag
    public GameObject enemy;

    private Animator animCon;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsPlayer, whatIsGround;

    public PlayerStatus playerStatus;

    public static EnemyAI Instance;

    public bool patrolling = false;
    public bool chasing = false;
    public bool attacking = false;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animCon = GetComponent<Animator>();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        //Checking distance and choosing enemy state accordingly
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patrolling()
    {
        attacking = false;
        patrolling = true;
        //If walkpoint is not set look for new walkpoint
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        //After calculating distance to walkpoint and reaching it find a new one
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        patrolling = false;
        chasing = true;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        chasing = false;
        attacking = true;
        //Make sure enemy doesn't move when reaching attack distance
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here
            animCon.SetTrigger("CombatIdle");
            StartCoroutine(WaitBeforeDamage());
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        //damage function for enemy
    }

    IEnumerator WaitBeforeDamage()
    {
        animCon.SetTrigger("Attack_01");
        yield return new WaitForSeconds(0.95f);
        PlayerStatus.Instance.TakeDamage(15);
    }
}
