using System.Collections;
using UnityEngine;
using DesktopProject;

public class SitOnHigh : MonoBehaviour
{
    public GameObject player;
    private Animator animator;
    private CharacterController controller;
    private bool isSitting;

    void OnMouseDown()
    {
        player.GetComponent<DesktopProject.DesktopPlayerAnimatorManager>().isSitting = true;
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
            player.transform.position = this.transform.position;
            controller.enabled = true;
            controller.transform.position += new Vector3(0, 1.0f, 0);
            isSitting = false;
        }
    }
}