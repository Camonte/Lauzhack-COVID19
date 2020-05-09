using Photon.Pun;
using Photon.Realtime;

    /// <summary>
    /// Simple script to get this client connected and into a random game. There the NetworkController takes over.
    /// </summary>
    /// <remarks>
    /// As this class extends MonoBehaviourPunCallbacks, its base-class will register
    /// for PUN callbacks. The NetworkController does this OnEnable() and OnDisable().
    /// </remarks>
    public class Connection : MonoBehaviourPunCallbacks
    {
        public void Init()
        {
        }

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                this.OnConnectedToMaster();
            }
            else
            {
                PhotonNetwork.GameVersion = NetworkController.NETCODE_VERSION;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }


        #region PUN Callbacks

        public override void OnConnectedToMaster()
        {
            if (PhotonNetwork.InRoom)
            {
                this.OnJoinedRoom();
            }
            else
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = NetworkController.MAX_PLAYERS }, null);
        }

        #endregion
    }