using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knight : MonoBehaviour
{
    public GameObject theTarget;
    private NavMeshAgent theAgent;


    bool isWalking = true;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking) 
        theAgent.destination = theTarget.transform.position;

        else
        {
            theAgent.destination = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Attack")
        {
            isWalking = false;
            animator.SetTrigger("ATTACK");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Attack")
        {
            isWalking = true;
            animator.SetTrigger("WALK");
        }
    }
}
