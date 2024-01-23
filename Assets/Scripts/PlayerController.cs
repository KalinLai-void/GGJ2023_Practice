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
    public bool canControl = true;

    // Crosshair
    public bool lockCursor = true;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Catch Settings
    public GameObject root;
    public float seeDist = 1000f;
    public float extendSpeed = 100f;
    public float returnSpeed = 10f;
    private Vector3 rootOriPos;

    private void Start()
    {
        if (lockCursor) Cursor.lockState = CursorLockMode.Locked;
        rootOriPos = root.transform.localPosition;
    }

    // Update is called once per frame1
    private void Update()
    {
        ControlCamera();
        Catch();
    }

    private void ControlCamera()
    {
        if (!canControl) return;

        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch += mouseSensitivity * Input.GetAxis("Mouse Y");

        // Clamp pitch between lookAngle
        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

        transform.localEulerAngles = new Vector3(0, yaw, 0);
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
    }

    private void Catch()
    {
        if (!canControl) return;

        if (Input.GetMouseButtonDown(0))
        {
            root.GetComponent<BoxCollider>().enabled = true;
            StartCoroutine(ExtendRoot());

            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * seeDist);
            if (Physics.Raycast(transform.position, transform.forward, out hit, seeDist))
            {
                Debug.Log("Selected: " + hit.collider.gameObject.name);
                //Destroy(hit.collider.gameObject);
                root.GetComponent<RootCatch>().hitObjs_fromAim = hit.collider.gameObject;
                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    private IEnumerator ExtendRoot() // root extend anim
    {
        canControl = false;
        while (root.transform.localPosition.z <= 0)
        {
            if (root.GetComponent<Animator>().GetBool("canCatch")) break;

            root.transform.localPosition += new Vector3(0, 0, 1);
            yield return new WaitForSeconds(1 / extendSpeed);
        }
        root.GetComponent<Animator>().SetBool("canCatch", true);
        root.GetComponent<BoxCollider>().enabled = false;
    }

    public IEnumerator ReturnRoot()
    {
        while (root.transform.localPosition.z >= rootOriPos.z)
        {
            root.transform.localPosition -= new Vector3(0, 0, 1);
            yield return new WaitForSeconds(1 / returnSpeed);
        }
        root.GetComponent<RootCatch>().isPlaying = false;
        root.GetComponent<Animator>().SetBool("canCatch", false);
        
        for (int i = 2; i < root.transform.childCount; i++) // Avoid Bug, so destroy other all un-need obj
        { // because 0 and 1 is model's structure
            Destroy(root.transform.GetChild(i).gameObject); 
        }

        canControl = true;
    }
}
