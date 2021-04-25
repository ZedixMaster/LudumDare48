using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;

    public float speed;
    public float chaseSpeed;
    public Transform[] moveSpots;
    public float startWaitTime;
    public float detectionDistance;

    private int currentSpot;
    private float waitTime;
    private Animator anim;
    private bool isChasing;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        currentSpot = Random.Range(0, moveSpots.Length);
        anim = gameObject.GetComponent<Animator>();
        isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing && !player.GetComponent<PlayerController>().isDead)
        {
            anim.SetBool("SeesPlayer", false);
            transform.position = Vector3.MoveTowards(transform.position, moveSpots[currentSpot].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, moveSpots[currentSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    currentSpot += 1;
                    if(currentSpot >= moveSpots.Length) { currentSpot = 0; }


                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            else
            {
                //Rotate towards point
                float rotSpeed = 360f;
                Vector3 D = moveSpots[currentSpot].position - transform.position;

                Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(D), rotSpeed * Time.deltaTime);
                transform.rotation = rot;

                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
        } else if(isChasing && !player.GetComponent<PlayerController>().isDead)
        {
            anim.SetBool("SeesPlayer", true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);

            //Rotate towards point
            float rotSpeed = 360f;
            Vector3 D = player.transform.position - transform.position;

            Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(D), rotSpeed * Time.deltaTime);
            transform.rotation = rot;

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            transform.position = new Vector3(transform.position.x, 5.84f, transform.position.z);
        }

        //check for if the enemy should chase
        Vector3 rayDirection = transform.forward;
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.forward * detectionDistance, Color.green);
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), rayDirection, out hit, detectionDistance))
        {
            if (hit.transform.tag == "Player")
            {
                isChasing = true;
            }
            else
            {
                isChasing = false;
            }
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > detectionDistance)
        {
            isChasing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("DIE");
            player.GetComponent<PlayerController>().Die();

            anim.SetTrigger("PlayerDies");
        }
    }
}