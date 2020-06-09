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

	    [Tooltip("The Smoothing for the camera to follow the target")]
	    [SerializeField]
	    private float smoothSpeed = 0.125f;

        // cached transform of the target
        Transform cameraTransform;

		// maintain a flag internally ca
		bool isFollowing;
		
		// Cache for camera offset
		//Vector3 cameraOffset = Vector3.zero;
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
			// ,
			if (cameraTransform == null && isFollowing)
			{
				OnStartFollowing();
			}
			// only follow is explicitly declared
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
		/// Follow the target smoothly
		/// </summary>

		void Follow()
		{
			if(photonView.IsMine){
				cameraTransform.position = new Vector3(this.transform.position.x, 3.5f, this.transform.position.z);
				//cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, this.transform.rotation, smoothSpeed*Time.deltaTime);
				cameraTransform.rotation = this.transform.rotation;
			}
	    }

		/**void Cut()
		{
			if(photonView.IsMine){
				cameraTransform.position = new Vector3(this.transform.position.x, 3.5f, this.transform.position.z);
				cameraTransform.rotation = this.transform.rotation;
			}
		}**/
		#endregion
	}
}