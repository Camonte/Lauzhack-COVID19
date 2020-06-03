using UnityEngine;
using System.Collections;
using Photon.Realtime;
using Photon.Pun;


namespace DesktopProject
{
    public class DesktopPlayerAnimatorManagerOutdoor : MonoBehaviourPun
    {
        #region Private Fields
        private Animator animator;
        private CharacterController controller;
        
        [SerializeField]
        private float directionDampTime = 0.1f;
        #endregion

        #region Public Fields
        public float sensitivityX = 15F;
	    public float sensitivityY = 15F;

	    public float minimumX = -360F;
	    public float maximumX = 360F;
        #endregion


        #region MonoBehaviour Callbacks
        void Start()
        {
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController>();
            //controller.Move(new Vector3(0, 0, 0));
            transform.position = new Vector3(0, 0, 0);
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
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
            //float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (v < 0)
            {
                v = 0;
            }
            animator.SetFloat("Speed", v);
            //animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);

            /**float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (v < 0)
            {
                v = 0;
            }
            animator.SetFloat("Speed", v*v);
            animator.SetFloat("Direction", h);**/
        }

        void OnAnimatorMove()
        {
            if (animator)
            {
                /**Vector3 newPosition = transform.position;
                newPosition += transform.forward;
                newPosition.x += animator.GetFloat("Speed") * Time.deltaTime; 
                newPosition.z += animator.GetFloat("Speed") * Time.deltaTime;**/
                //transform.position += 5.0f * transform.forward * Time.deltaTime * animator.GetFloat("Speed");
                controller.Move(5.0f * transform.forward * Time.deltaTime * animator.GetFloat("Speed"));
            }
        }
        #endregion
    }
}