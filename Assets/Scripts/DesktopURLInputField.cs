using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


using System.Collections;


namespace DesktopProject
{
    /// <summary>
    /// Player name input field. Let the user input his name, will appear above the player in the game.
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class DesktopURLInputField : MonoBehaviour
    {
        #region Public Fields
        public GameObject sphere;
        #endregion

        
        #region Public Methods
        /// <summary>
        /// Sets the URL of the video to play on the 360 sphere.
        /// </summary>
        /// <param URL="value">The URL to play the video from</param>
        public void SetVideoURL(string value)
        {
            // #Important
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("URL is null or empty");
                return;
            }
            VideoPlayer player = sphere.GetComponent<VideoPlayer>();
            player.url = value;
            player.Play();
        }
        #endregion
    }
}