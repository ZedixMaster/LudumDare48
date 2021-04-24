using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float sensX = 2f;
    public float sensY = 2f;

    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rotation = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        var mouseX = Input.GetAxisRaw("Mouse X");
        var mouseY = Input.GetAxisRaw("Mouse Y");

        rotation.x += -mouseY * sensX;
        rotation.y += mouseX * sensY;

        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotation);
    }
}
