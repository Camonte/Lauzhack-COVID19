using UnityEngine;
using UnityEngine.UI;

namespace Photon.Pun.Demo.PunBasics
{
	[RequireComponent(typeof(InputField))]
	public class RoomNameInputField : MonoBehaviour
	{
        #region Private Fields
        string roomName = "DefaultRoom";
        #endregion

		#region MonoBehaviour CallBacks
		void Start () {
			InputField _inputField = this.GetComponent<InputField>();
			_inputField.text = roomName;
		}
		#endregion
		

		#region Public Methods
		public void SetRoomName(string value)
		{
		    if (string.IsNullOrEmpty(value))
		    {
                Debug.LogError("Room Name is null or empty");
		        return;
		    }
			roomName = value;
		}
		#endregion
	}
}
