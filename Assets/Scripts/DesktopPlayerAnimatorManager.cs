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
        //public Transform destination;
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
            if(Input.GetKeyDown("e"))
            {
                // Bit shift the index of the layer (8) to get a bit mask
                int layerMask = 1 << 8;

                // This would cast rays only against colliders in layer 8.
                // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
                //layerMask = ~layerMask;

                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position + new Vector3(0f, 2.9f, 0f), transform.TransformDirection(Vector3.forward), out hit, 3, layerMask))
                {
                    if (hit.collider.gameObject.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer)
                    {
                        hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        hit.collider.gameObject.transform.parent = this.transform;

                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                        hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                        hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        hit.collider.gameObject.transform.parent = this.transform;
                    }
                }
            }
            if(Input.GetKeyDown("r")){
                for(int i = 3; i < transform.childCount; i++){
                    transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                    transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                    transform.GetChild(i).transform.parent = null;
                }
                //destination.DetachChildren();
            }
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