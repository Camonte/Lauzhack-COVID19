using UnityEngine;
using Photon.Pun;

namespace DesktopProject
{
	/// <summary>
	/// Camera work. Follow a target
	/// </summary>
	public class DesktopCameraWork : MonoBehaviourPun
	{
        #region Private Fields

	    [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
	    [SerializeField]
	    private bool followOnStart = false;

        // cached transform of the target
        Transform cameraTransform;

		// maintain a flag internally
		bool isFollowing;
        #endregion


        #region MonoBehaviour Callbacks
        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase
        /// </summary>
        void Start()
		{
			// Start following the target if wanted.
			if (followOnStart)
			{
				OnStartFollowing();
			}
		}

		void LateUpdate()
		{
			if (cameraTransform == null && isFollowing)
			{
				OnStartFollowing();
			}
			// only follow if explicitly declared
			if (isFollowing) {
				Follow();
			}
		}
		#endregion


		#region Public Methods
		/// <summary>
		/// Raises the start following event. 
		/// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.
		/// </summary>
		public void OnStartFollowing()
		{	      
			cameraTransform = Camera.main.transform;
			isFollowing = true;
			Follow();
		}
		#endregion


		#region Private Methods
		/// <summary>
		/// Follow the target
		/// </summary>
		void Follow()
		{
			if(photonView.IsMine){
				cameraTransform.position = new Vector3(this.transform.position.x, 3.5f, this.transform.position.z);
				cameraTransform.rotation = this.transform.rotation;
			}
	    }
		#endregion
	}
}