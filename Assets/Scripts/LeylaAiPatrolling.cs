using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class LeylaAiPatrolling : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public LayerMask groundLayer;
    public GameObject player;
    bool walkpointSet = false;
    [SerializeField] private Animator animator;
    [SerializeField] float range = 30.0f;
    Vector3 destPoint;
    float timer;
    float changeTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        
    }
    void Patrol()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 5)
        {
            if (!walkpointSet)
            {
                SearchForDest();
            }
            if (walkpointSet)
            {
                animator.SetFloat("Speed", navMeshAgent.speed);            
                navMeshAgent.SetDestination(destPoint);            
            }

            if (Vector3.Distance(transform.position, destPoint) < 5)
            {
                animator.SetFloat("Speed", 0);
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    walkpointSet = false;
                    timer = changeTime;
                }           
            }        
        }
        else 
        {
            animator.SetFloat("Speed", 0);
            // Look  forward player
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
            StartCoroutine(WaitSeconds());                     
        }
    }
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<GameManager>().Win();   
    }
    void SearchForDest()
    {
        float z = Random.Range(-range / 1.5f,range);
        float x = Random.Range(-range / 1.5f,range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        if (Physics.Raycast(destPoint, Vector3.forward, groundLayer))
        {
            walkpointSet = true;            
        }
        else 
        {
            walkpointSet = false;
        }
    }
}
