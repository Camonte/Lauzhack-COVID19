using UnityEngine;
using System.Collections;

using Photon.Realtime;
using Photon.Pun;


namespace BachelorProject
{
    public class TestPlayerAnimatorManager : MonoBehaviourPun
    {
        #region Private Fields

        private Animator animator;

        [SerializeField]
        private float directionDampTime = 0.25f;

        #endregion

        #region MonoBehaviour Callbacks

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
            animator.SetFloat("Speed", h * h + v * v);
            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
        }

        #endregion
    }
}