using System.Collections;
using UnityEngine;
using DesktopProject;

public class SitOn : MonoBehaviour
{
    public GameObject player;
    private Animator animator;
    private CharacterController controller;
    private bool isSitting = false;

    void OnMouseDown()
    {
        isSitting = true;
        animator.SetTrigger("isSitting");
    }

    void Start()
    {
        animator = player.GetComponent<Animator>();
        controller = player.GetComponent<CharacterController>();
    }

    void Update()
    {
        if(isSitting)
        {
            controller.enabled = false;
            player.transform.position = this.transform.position + new Vector3(0f, 2.0f, 0f);
            controller.enabled = true;
            isSitting = false;
        }
    }
}
