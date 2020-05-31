﻿using UnityEngine;
using System.Collections;
using Photon.Realtime;
using Photon.Pun;


namespace DesktopProject
{
    public class DesktopPlayerAnimatorManager : MonoBehaviourPun
    {
        #region Private Fields
        private Animator animator;

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
            animator.speed = 0.6f;
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
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (v < 0)
            {
                v = 0;
            }
            animator.SetFloat("Speed", (h*h + v*v));
            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
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
        #endregion
    }
}