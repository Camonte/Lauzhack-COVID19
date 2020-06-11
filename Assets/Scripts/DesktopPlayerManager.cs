using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;

using System.Collections;

namespace DesktopProject
{
    /// <summary>
    /// Player manager.
    /// </summary>
    public class DesktopPlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region Public Fields
        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        public GameObject GameManager;
        #endregion


        #region MonoBehaviour CallBacks
        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                LocalPlayerInstance = this.gameObject;
                var enumerator = Recorder.PhotonMicrophoneEnumerator;
                Recorder recorder = this.GetComponent<Recorder>();
                recorder.PhotonMicrophoneDeviceId = enumerator.IDAtIndex(0);
            }
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            DesktopCameraWork _cameraWork = gameObject.GetComponent<DesktopCameraWork>();
            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
            #if UNITY_5_4_OR_NEWER
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            #endif
        }

        public override void OnDisable()
        {
            base.OnDisable();
            #if UNITY_5_4_OR_NEWER
                UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
            #endif
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// </summary>
        void Update()
        {
        }

        #if !UNITY_5_4_OR_NEWER
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
        #endif
        
        void CalledOnLevelWasLoaded(int level)
        {
            if(!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }
        }
        #endregion


        #region Private Methods
        #if UNITY_5_4_OR_NEWER
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
        {
            this.CalledOnLevelWasLoaded(scene.buildIndex);
        }
        #endif
        #endregion

        #region IPunObservable implementation
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
        }
        #endregion

        #region Public Methods
        [PunRPC]
        public void LeaveRoom()
        {
            GameManager.GetComponent<DesktopGameManager>().LeaveRoomOnRPC();
        }
        #endregion
    }
}