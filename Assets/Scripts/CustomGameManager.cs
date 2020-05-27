using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;

using ExitGames.Client.Photon;


namespace BachelorProject
{
    public class CustomGameManager : MonoBehaviourPunCallbacks, IConnectionCallbacks, IMatchmakingCallbacks, IOnEventCallback
    {
        #region Public Fields

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        public GameObject ovrController;

        public const byte InstantiateVrAvatarEventCode = 1;

        #endregion


        #region Photon Callbacks

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            Debug.LogFormat("OnLeftRoom method called");
            SceneManager.LoadScene("Launcher");
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                //LoadArena();
            }
        }

        #endregion


        #region MonoBehaviour Callbacks

        void Start(){
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
            }
            else
            {
                if (TestPlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
        }

        public override void OnJoinedRoom()
        {
            GameObject localAvatar = Instantiate(Resources.Load("LocalAvatar")) as GameObject;
            PhotonView photonView = localAvatar.GetComponent<PhotonView>();
            PhotonView ovrPhotonView = ovrController.GetComponent<PhotonView>();

            if (PhotonNetwork.AllocateViewID(photonView))
            {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                    CachingOption = EventCaching.AddToRoomCache,
                    Receivers = ReceiverGroup.Others
                };

                PhotonNetwork.RaiseEvent(InstantiateVrAvatarEventCode, photonView.ViewID, raiseEventOptions, SendOptions.SendReliable);
            }
            else
            {
                Debug.LogError("Failed to allocate a ViewId.");

                Destroy(localAvatar);
            }

            if (PhotonNetwork.AllocateViewID(ovrPhotonView))
            {
                RaiseEventOptions raiseEventOptions = new RaiseEventOptions
                {
                    CachingOption = EventCaching.AddToRoomCache,
                    Receivers = ReceiverGroup.Others
                };

                PhotonNetwork.RaiseEvent(InstantiateVrAvatarEventCode, ovrPhotonView.ViewID, raiseEventOptions, SendOptions.SendReliable);
            }
            else
            {
                Debug.LogError("Failed to allocate a ViewId.");

                Destroy(ovrController);
            }
        }

        #endregion


        #region Public Methods

        void IOnEventCallback.OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == InstantiateVrAvatarEventCode)
            {
                GameObject remoteAvatar = Instantiate(Resources.Load("RemoteAvatar")) as GameObject;
                PhotonView photonView = remoteAvatar.GetComponent<PhotonView>();
                photonView.ViewID = (int) photonEvent.CustomData;
            }
        }

        public void LeaveRoom()
        {
            Debug.LogFormat("LeaveRoom method called");
            PhotonNetwork.LeaveRoom();
        }

        #endregion

        #region Private Methods

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading MainScene");
            PhotonNetwork.LoadLevel("MainScene");
        }

        #endregion
    }
}