using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Camera Settings
    public Camera playerCamera;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 50f;
    public GameObject spotLight;

    // Crosshair
    public bool lockCursor = true;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Catch Settings
    public GameObject root;
    public float seeDist = 1000f;

    private void Start()
    {
        if (lockCursor) Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame1
    private void Update()
    {
        ControlCamera();
        Catch();
    }

    private void ControlCamera()
    {
        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch += mouseSensitivity * Input.GetAxis("Mouse Y");

        // Clamp pitch between lookAngle
        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

        transform.localEulerAngles = new Vector3(0, yaw, 0);
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
    }

    private void Catch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ExtendRoot());

            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * seeDist);
            if (Physics.Raycast(transform.position, transform.forward, out hit, seeDist))
            {
                Debug.Log("Selected: " + hit.collider.gameObject.name);
                //Destroy(hit.collider.gameObject);
                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private IEnumerator ExtendRoot() // root extend anim
    {
        while (root.transform.localPosition.z < 75 /*|| !root.GetComponent<Animator>().GetBool("canCatch")*/)
        {
            root.transform.localPosition += new Vector3(0, 0, 1);
            yield return new WaitForSeconds(0.0001f);
        }

        while (root.transform.localPosition.z > -75 /*|| root.GetComponent<Animator>().GetBool("canCatch")*/)
        {
            root.transform.localPosition -= new Vector3(0, 0, 1);
            yield return new WaitForSeconds(0.0001f);
        }
    }
}
