using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

    public enum NetworkEvent
    {
        UPDATE_SCORE,
        TRAP_HIT,
        TO_SCORING_STATE,
        ROUND_ENDED
    }

    /// <summary>
    /// Handles in-game communication, network events and gameplay. Provides events for various situations.
    /// </summary>
    /// <remarks>
    /// Alternatively to using the Actions (e.g. OnSomePlayerConnected), any script could also register for the
    /// related PUN events. The Actions allow scripts to react to individual events only.
    /// </remarks>
    public class NetworkController : MonoBehaviour, IInRoomCallbacks, IMatchmakingCallbacks
    {
        public const string NETCODE_VERSION = "1.0";
        public const int MAX_PLAYERS = 5;

        Connection _connection;

        public static event Action OnGameConnected;
        public static event Action<Player> OnSomePlayerConnected;
        public static event Action<Player> OnSomePlayerDisconnected;

        public static NetworkController Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            this._connection = this.GetComponent<Connection>();
        }

        void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public void StartMultiplayerGame()
        {
            this._connection.Init();
            this._connection.Connect();
        }

        public void EndMultiplayerGame()
        {
            this._connection.Disconnect();
        }

        #region IInRoomCallbacks

        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (OnSomePlayerConnected != null)
                OnSomePlayerConnected(newPlayer);
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (OnSomePlayerDisconnected != null)
                OnSomePlayerDisconnected(otherPlayer);
        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {
        }

        #endregion


        #region IMatchmakingCallbacks

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
        }

        public void OnCreatedRoom()
        {
            Debug.Log("Called on created room");
        }

        public void OnJoinedRoom()
        {
            Debug.Log("Called on joined room");
            if (OnGameConnected != null)
                OnGameConnected();
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
        }

        public void OnLeftRoom()
        {
        }

        #endregion
    }