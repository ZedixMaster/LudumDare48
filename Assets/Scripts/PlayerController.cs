using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 30f;
    [SerializeField] private float reverseSpeedMultiplier = 20f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float maxReverseSpeed = 5f;
    [SerializeField] private float turnMovementSpeedMultiplier = 30f;
    [SerializeField] private float maxTurnMovementSpeed = 5f;
    [SerializeField] private float turnSpeed = 7f;
    [SerializeField] private float maxTurnSpeed = 1.5f;
    [SerializeField] private float transitionSpeed = 1f;

    private Vector3 velocity;
    private Rigidbody rb;
    private float vertical;
    private float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        if (vertical > 0)
        {
            rb.AddForce(transform.forward * speedMultiplier * vertical);
        }
        if (vertical < 0)
        {
            rb.AddForce(transform.forward * reverseSpeedMultiplier * vertical);
        }

        if (horizontal != 0)
        {
            rb.AddTorque(transform.up * turnSpeed * horizontal);
            if(vertical == 0)
            {
                rb.AddForce(transform.forward * turnMovementSpeedMultiplier);
                if (transform.InverseTransformDirection(rb.velocity).z > maxTurnMovementSpeed)
                {
                    var desiredSpeed = transform.forward * maxTurnMovementSpeed;
                    rb.velocity = new Vector3(
                        Mathf.Lerp(rb.velocity.x, desiredSpeed.x, Time.deltaTime * transitionSpeed),
                        Mathf.Lerp(rb.velocity.y, desiredSpeed.y, Time.deltaTime * transitionSpeed),
                        Mathf.Lerp(rb.velocity.z, desiredSpeed.z, Time.deltaTime * transitionSpeed)
                        );
                }
            }
        }


        if (transform.InverseTransformDirection(rb.velocity).z > maxSpeed)
        {
            var desiredSpeed = transform.forward * maxSpeed;
            rb.velocity = new Vector3(
                Mathf.Lerp(rb.velocity.x, desiredSpeed.x, Time.deltaTime * transitionSpeed),
                Mathf.Lerp(rb.velocity.y, desiredSpeed.y, Time.deltaTime * transitionSpeed),
                Mathf.Lerp(rb.velocity.z, desiredSpeed.z, Time.deltaTime * transitionSpeed)
                );
        }
        if (transform.InverseTransformDirection(rb.velocity).z < -maxReverseSpeed)
        {
            var desiredSpeed = transform.forward * -maxReverseSpeed;
            rb.velocity = new Vector3(
                Mathf.Lerp(rb.velocity.x, desiredSpeed.x, Time.deltaTime * transitionSpeed),
                Mathf.Lerp(rb.velocity.y, desiredSpeed.y, Time.deltaTime * transitionSpeed),
                Mathf.Lerp(rb.velocity.z, desiredSpeed.z, Time.deltaTime * transitionSpeed)
                );
        }


        if (Mathf.Abs(rb.angularVelocity.magnitude) > maxTurnSpeed)
        {
            var direction = rb.angularVelocity.y >= 0 ? 1 : -1;
            rb.angularVelocity = transform.up * maxTurnSpeed * direction;
        }

        var moveSpeed = transform.InverseTransformDirection(rb.velocity).z > 0.1 || transform.InverseTransformDirection(rb.velocity).z < -0.1 ? transform.InverseTransformDirection(rb.velocity).z : 0;


        rb.velocity = transform.forward * moveSpeed;
    }
}
