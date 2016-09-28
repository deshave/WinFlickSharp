using System;
using System.Net;
using System.Windows;
using FlickrNet;

namespace WinFlickSharp
{
    /// <summary>
    /// This class provides support for authenticating with the Flickr API.
    /// </summary>
    public class FlickrManager
    {
        /// <summary>
        /// <see cref="string"/> containing the API Key used to authenticate.
        /// </summary>
        public const string ApiKey = "39e16e887db53a3fe6bdf8cd24ed67f3";
        /// <summary>
        /// <see cref="string"/> containing the Shared Secret used to authenticate.
        /// </summary>
        public const string SharedSecret = "e9b438a69e5705e5";

        /// <summary>
        /// Gets the instance reference of the <see cref="Flickr"/> object.
        /// </summary>
        /// <returns><see cref="Flickr"/> object.</returns>
        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey, SharedSecret);
        }

        /// <summary>
        /// Gets the instance reference of the <see cref="Flickr"/> object, populating its members.
        /// </summary>
        /// <returns><see cref="Flickr"/> object containing authentication parameters.</returns>
        public static Flickr GetAuthInstance()
        {
            var f = new Flickr(ApiKey, SharedSecret);
            f.OAuthAccessToken = OAuthToken.Token;
            f.OAuthAccessTokenSecret = OAuthToken.TokenSecret;
            return f;
        }

        /// <summary>
        /// Gets the <see cref="OAuthToken"/> stored in the application properties.
        /// </summary>
        /// <returns><see cref="OAuthAccessToken"/> for this application.</returns>
        public static OAuthAccessToken OAuthToken
        {
            get
            {
                return WinFlickSharp.Properties.Settings.Default.OAuthToken;
            }
            set
            {
                WinFlickSharp.Properties.Settings.Default.OAuthToken = value;
                WinFlickSharp.Properties.Settings.Default.Save();
            }
        }

    }
}
