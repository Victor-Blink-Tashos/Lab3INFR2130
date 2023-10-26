using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knight : MonoBehaviour
{
    public GameObject theTarget;
    private NavMeshAgent theAgent;


    bool isWalking = true;

    
    public int timesStagered = 0;
    //public bool isStagered = false;
    public bool isKilled;

   // private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
       // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isKilled)
        {
            if (isWalking)
                theAgent.destination = theTarget.transform.position;

            
            else
            {
                theAgent.destination = transform.position;
            }
        }

        else
        {
            Destroy(gameObject);
        }
       
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isWalking = false;
           // animator.SetTrigger("ATTACK");
        }

        if (other.tag == "hat")
        {
            timesStagered++;
            StartIFrames();

           
          

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isWalking = true;
            //animator.SetTrigger("WALK");
        }
    }

    public void SetIsStomped(bool stomped)
    {
        isKilled = stomped;
    }


    public void StartIFrames()
    {
        StartCoroutine(IFrames());
    }

    IEnumerator IFrames()
    {
        for (int i = 1; i <= 2; i++)
        {
            isWalking = false;

            yield return new WaitForSeconds(1f);
        }
        isWalking = true;
    }


    public int GetStagers()
    {
        return timesStagered;
    }
}
