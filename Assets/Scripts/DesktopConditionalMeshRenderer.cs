using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

using System.Collections;

namespace DesktopProject
{
    /// <summary>
    /// Player manager.
    /// Handles fire Input and Beams.
    /// </summary>
    public class DesktopConditionalMeshRenderer : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region MonoBehaviour CallBacks
        void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                MeshRenderer m = this.GetComponent<MeshRenderer>();
                m.enabled = false;
            }
        }
        #endregion

        #region IPunObservable implementation
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
        }
        #endregion
    }
}