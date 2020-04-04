using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace ch.epfl.LHackCOVID19
{
	public class PlayerAnimatorManager : MonoBehaviourPun
	{
		private Animator animator;

		#region Private Fields
		[SerializeField]
		private float directionDampTime = 0.25f;
		#endregion
		
		#region MonoBehaviour Callbacks
		void Start(){
			animator = GetComponent<Animator>();
			if(!animator)
			{
				Debug.LogError("PlayerAnimatorManager is missing animator component", this);
			}
		}

		void Update(){
			if(photonView.IsMine == false && PhotonNetwork.IsConnected == true)
			{
				return;
			}
			if(!animator)
			{
				return;
			}
			AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			if(stateInfo.IsName("Base Layer.Run"))
			{
				if(Input.GetButtonDown("Fire2"))
				{
					animator.SetTrigger("Jump");
				}
			}
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			if(v < 0)
			{
				v = 0;
			}
			animator.SetFloat("Speed", h*h + v*v);
			animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
		}
		#endregion
	}
}