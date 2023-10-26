using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] Transform trans;

    [SerializeField] Transform bunny;

    [SerializeField] float moveSpeed;


    [SerializeField] float jumpforce;

    public bool isGrounded;
    private Rigidbody body;


    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;


    [SerializeField] float timeToNextShot;
    [SerializeField] float shootDelay;
    bool initialDelayapplied;
    



    [HideInInspector] [SerializeField] new Renderer renderer;

    private void Reset()
    {
        renderer = GetComponent<MeshRenderer>();
    }
    void Start()
    {
        trans = GetComponent<Transform>();


        body = GetComponent<Rigidbody>();


        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        walk();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {

            body.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot())

        {
            shoot();
        }

    }

    void walk()
    {
        if (Input.GetKey(KeyCode.D)) // detect while walking is the player input
        {
            trans.position -= transform.up * Time.deltaTime * moveSpeed; //  Time.deltaTime, it does not depend on the performance of your computer
            trans.rotation = Quaternion.Euler(-90, 90, 0); // set the rotation of game object

        }
        if (Input.GetKey(KeyCode.A))
        {
            trans.position -= transform.up * Time.deltaTime * moveSpeed;
            trans.rotation = Quaternion.Euler(-90, 270, 0); //change rotation to 180

        }

        if (Input.GetKey(KeyCode.W))
        {
            trans.position -= transform.up * Time.deltaTime * moveSpeed;
            trans.rotation = Quaternion.Euler(-90, 0, 0);

        }

        if (Input.GetKey(KeyCode.S))
        {
            trans.position -= transform.up * Time.deltaTime * moveSpeed;
            trans.rotation = Quaternion.Euler(-90, 180, 0);

        }

    }


        private void OnCollisionEnter(Collision collision) // detects when the 
                                                           //object collides with another object
        {
            if (collision.collider.tag == "Floor") // saying if the thing you're 
                                                   //colliding with has the ground tag on it
            {
                for (int i = 0; i < collision.contacts.Length; i++) // if any one of the 
                                                                    //collisions is on the ground
                {

                    isGrounded = true;

                }
            }
        if (collision.gameObject.tag == "Bunny")
        {
            if (collision.contacts[0].normal.y > 0.5)
            {
                if(collision.gameObject.GetComponent<Knight>().GetStagers() >= 3)

                collision.gameObject.GetComponent<Knight>().SetIsStomped(true);

            }
        }
        }

            private void OnCollisionExit(Collision collision)
            {
                if (collision.collider.tag == "Floor") // saying if the thing you're 
                                                       //leaving the object with the ground tag on it
                {
                    isGrounded = false;



                }
            }

    void shoot()
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        bullet.GetComponent<Rigidbody>().velocity = (bunny.position - transform.position).normalized * bulletSpeed;
        Destroy(bullet, 5);
    }


    bool canShoot()
    {
        if (timeToNextShot < Time.realtimeSinceStartup & renderer.isVisible)
        {
            timeToNextShot = Time.realtimeSinceStartup + shootDelay;
            return true;
        }
        else
        {
            return false;
        }
    }

}
