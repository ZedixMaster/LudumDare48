using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject[] inventoryObjects;

    private Vector3 velocity;
    private Rigidbody rb;
    private float vertical;
    private float horizontal;

    private bool isDead = false;

    private List<GameObject> inventory;
    public bool isPaused = false;

    private List<KeyCode> cheatCode;
    private List<KeyCode> cheatInputs;
    private int currentCheatIndex;
    private float cheatInputTimer = 5;
    private float currentCheatInputTime;
    private bool cheatCodeStarted = false;
    
    void Start()
    {
        cheatCode = new List<KeyCode>()
        {
            KeyCode.UpArrow,
            KeyCode.UpArrow,
            KeyCode.DownArrow,
            KeyCode.DownArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.LeftArrow,
            KeyCode.RightArrow,
            KeyCode.B,
            KeyCode.A,
            KeyCode.Return
        };
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        crosshair.SetActive(true);
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if(!isPaused)
            {
                PauseGame();
            } else
            {
                ResumeGame();
            }
        }


        if(cheatCodeStarted && Input.GetKeyDown(cheatCode[currentCheatIndex]))
        {
            currentCheatIndex++;
            currentCheatInputTime = cheatInputTimer;
            if(currentCheatIndex >= cheatCode.Count)
            {
                enableCheats();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !cheatCodeStarted)
        {
            cheatCodeStarted = true;
            currentCheatInputTime = cheatInputTimer;
            currentCheatIndex++;
        }

        if (currentCheatInputTime > 0)
        {
            currentCheatInputTime -= Time.deltaTime;
        } else
        {
            cheatCodeStarted = false;
            currentCheatIndex = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
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
                if (vertical == 0)
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


            if (rb.angularVelocity.magnitude > maxTurnSpeed)
            {
                var direction = rb.angularVelocity.y >= 0 ? 1 : -1;
                rb.angularVelocity = transform.up * maxTurnSpeed * direction;
            }

            var moveSpeed = (transform.InverseTransformDirection(rb.velocity).z > 0.5 || transform.InverseTransformDirection(rb.velocity).z < -0.5) ? transform.InverseTransformDirection(rb.velocity).z : 0;


            rb.velocity = transform.forward * moveSpeed;
        } else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void Die()
    {
        isDead = true;
    }
    
    public void PauseGame()
    {
        crosshair.SetActive(false);
        pauseMenu.SetActive(true);
        isPaused = true;
        //Time.timeScale = 0.1f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        crosshair.SetActive(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void enableCheats()
    {
        cheatCodeStarted = false;
        currentCheatIndex = 0;
        currentCheatInputTime = 0;

        speedMultiplier = 55f;
        turnSpeed = 15f;
        maxTurnSpeed = 3f;
        GetComponent<Rigidbody>().drag = 0;
        Camera.main.fieldOfView = 70;
    }
}
