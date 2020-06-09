using UnityEngine;
using System.Collections;
using Photon.Realtime;
using Photon.Pun;


namespace DesktopProject
{
    public class DesktopPlayerAnimatorManager : MonoBehaviourPun
    {
        #region Private Fields
        private Animator animator;
        private CharacterController controller;
        private bool isSitting = false;
        #endregion

        #region Public Fields
        public float sensitivityX = 15F;
        #endregion


        #region MonoBehaviour Callbacks
        void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            transform.position = new Vector3(0, 0, 0);
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
            animator.SetFloat("isSitting", 0.0f);
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }
            if (!animator)
            {
                return;
            }
            float v = Input.GetAxis("Vertical");
            if (v < 0)
            {
                v = 0;
            }
            animator.SetFloat("Speed", v);
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            if (Input.GetKeyDown("space"))
            {
                isSitting = false;
                animator.SetFloat("isSitting", 0.0f);
                //controller.enabled = false;
                controller.transform.position = new Vector3(0, 0, 0);
                this.transform.position = new Vector3(0, 0, 0);
                //controller.enabled = true;
            }
            /**if(shouldSit)
            {
                isSitting = true;
                animator.SetTrigger("isSitting");
                controller.enabled = false;
                this.transform.position = sitPosition;
                controller.enabled = true;
                controller.transform.position += new Vector3(0, 1.0f, 0);
                shouldSit = false;
            }**/
        }

        void OnAnimatorMove()
        {
            if (animator && !isSitting)
            {
                controller.enabled = false;
                this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                controller.enabled = true;
                controller.Move(5.0f * transform.forward * Time.deltaTime * animator.GetFloat("Speed"));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            isSitting = true;
            animator.SetFloat("isSitting", 1.0f);
            //controller.enabled = false;
            controller.transform.position = other.transform.position;
            //controller.enabled = true;
            controller.transform.position += new Vector3(0, 1.0f, 0);
        }
        #endregion
    }
}