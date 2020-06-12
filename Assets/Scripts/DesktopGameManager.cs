using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace DesktopProject
{
    public class DesktopGameManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields
        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;
        #endregion

        #region MonoBehaviour Callbacks
        void Start()
        {
            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
            }
            else
            {
                if (DesktopPlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }
        }
        #endregion


        #region Photon Callbacks
        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            Debug.LogFormat("OnLeftRoom method called");
            SceneManager.LoadScene("DesktopLauncher");
        }
        #endregion


        #region Public Methods
        public void LeaveRoom()
        {
            if(PhotonNetwork.IsMasterClient)
            {
                playerPrefab.GetComponent<PhotonView>().RPC("LeaveRoom", RpcTarget.All);
            }
            LeaveRoomOnRPC();
        }

        public void LeaveRoomOnRPC(){
            Debug.LogFormat("LeaveRoom method was called");
            PhotonNetwork.LeaveRoom();
        }
        #endregion
    }
}