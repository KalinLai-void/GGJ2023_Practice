using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootCatch : MonoBehaviour
{
    public bool isPlaying = false;
    public GameObject hitObjs_fromAim;

    private void Update()
    {
        if ((GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("catch")
            && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) 
            && GetComponent<Animator>().GetBool("canCatch") 
            && !transform.parent.GetComponent<PlayerController>().canControl
            && !isPlaying)
        {
            isPlaying = true;
            StartCoroutine(transform.parent.GetComponent<PlayerController>().ReturnRoot());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Target"))
        {
            Debug.Log("Selected: " + other.gameObject.name);

            if (other.gameObject == hitObjs_fromAim)
            {
                GetComponent<Animator>().SetBool("canCatch", true);
                other.gameObject.transform.SetParent(gameObject.transform);
            }
        }
    }
}
