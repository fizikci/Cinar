/*
===========================================================================
Copyright (c) 2010 BrickRed Technologies Limited

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sub-license, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
===========================================================================

*/
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Net;
using log4net;

namespace Brickred.SocialAuth.NET.Core.BusinessObjects
{
    /// <summary>
    /// Represents the user in context
    /// </summary>
    public class SocialAuthUser
    {

        #region ConsumerMethods&Properties

        readonly ILog _logger = LogManager.GetLogger("SocialAuthUser");

        //--------- Constructor (Optional and is for backward compatibility)
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="providerType">Provider Type for this connection</param>
        public SocialAuthUser(PROVIDER_TYPE providerType)
        {
            this.providerType = providerType;
        }


        public void LoadToken(Token token, string returnUrl)
        {
            // If expiresOn is specified and token has expired, refresh token!! Currently, exception thrown!
            if (token.ExpiresOn != null && token.ExpiresOn != DateTime.MinValue)
                if (token.ExpiresOn < DateTime.Now)
                {
                    _logger.Error("Token has expired");
                    throw new OAuthException("Token has expired.");
                }

            //If user is already connected with provider specified in token, ignore this request!
            if (SocialAuthUser.IsConnectedWith(token.Provider))
                return;

            //Load token
            token.UserReturnURL = returnUrl;
            SessionManager.AddConnectionToken(token);
            //SetUserAsLoggedIn();
        }

        public SocialAuthUser() { }


        //--------- Authentication Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl">URL where user should return after login.</param>
        /// <param name="callback">Delegate invoked just before redirecting user after successful login</param>
        public void Login(PROVIDER_TYPE providerType = PROVIDER_TYPE.NOT_SPECIFIED, string returnUrl = "", Action callback = null, string errorRedirectURL = "", bool skipRedirectionIfAlreadyConnected=false)
        {
            
            if (this.providerType == PROVIDER_TYPE.NOT_SPECIFIED && providerType == PROVIDER_TYPE.NOT_SPECIFIED)
                throw new Exception("Provider not specified. Either pass provider as parameter to Login or pass it in constructor");
            if (callback != null)
                SessionManager.SetCallback(callback);
            if (providerType == PROVIDER_TYPE.NOT_SPECIFIED && this.providerType != PROVIDER_TYPE.NOT_SPECIFIED)
                providerType = this.providerType;
            Connect(providerType, returnUrl, errorRedirectURL,skipRedirectionIfAlreadyConnected);
        }


        /// <summary>
        /// Logs user out of local application (User may still remain logged in at provider)
        /// </summary>
        /// <param name="loginUrl">Where should user be redirected after logout. (Only applicable when using custom authentication)</param>
        /// <param name="callback">Delegate invoked (if specified) just before redirecting user to login page</param>
        /// <param name="provider"> </param>
        public void Logout(string loginUrl = "", Action callback = null, PROVIDER_TYPE providerType = PROVIDER_TYPE.NOT_SPECIFIED)
        {
            if(providerType != PROVIDER_TYPE.NOT_SPECIFIED && GetConnectedProviders().Count>1)
            {
                SessionManager.RemoveConnectionToken(providerType);
                return;
            }
            Disconnect(loginUrl, callback);
        }


        /// <summary>
        /// If callback was specified in Login(), use this method to stop invoking delegate.
        /// </summary>
        public void ResetCallback()
        {
            SessionManager.SetCallback(null);
        }



        //------------ Getting & Checking Connections
        /// <summary>
        /// Returns an instance of SocialAuthUser with current connection
        /// </summary>
        /// <returns></returns>
        public static SocialAuthUser GetCurrentUser()
        {
            if (SessionManager.ConnectionsCount == 0)
                    return new SocialAuthUser() { contextToken = new Token() };
            return new SocialAuthUser() { contextToken = GetCurrentConnectionToken() };
        }

        /// <summary>
        /// Is User connected with any provider?
        /// </summary>
        /// <returns></returns>
        public static bool IsLoggedIn() { return SessionManager.IsConnected; }

        /// <summary>
        /// Get Access Token for Current or specified provider
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public string GetAccessToken(PROVIDER_TYPE providerType = PROVIDER_TYPE.NOT_SPECIFIED)
        {
            if (providerType == PROVIDER_TYPE.NOT_SPECIFIED)
            {
                return SessionManager.GetConnectionToken(SessionManager.GetCurrentConnection().ProviderType).AccessToken;
            }
            else
            {
                if (IsConnectedWith(providerType))
                    return SessionManager.GetConnectionToken(providerType).AccessToken;
                else
                    throw new InvalidSocialAuthConnectionException(providerType);
            }
        }

        /// <summary>
        /// PROVIDER_TYPE for last connected
        /// </summary>
        public static PROVIDER_TYPE CurrentProvider
        {
            get
            {
                if (IsLoggedIn())
                    return SessionManager.GetCurrentConnection().ProviderType;
                else
                    return PROVIDER_TYPE.NOT_SPECIFIED;
            }
        }


        /// <summary>
        /// specifies whether user is connected with specified provider
        /// </summary>
        /// <param name="providerType">Provider Type</param>
        /// <returns></returns>
        public static bool IsConnectedWith(PROVIDER_TYPE providerType)
        {
            return SessionManager.IsConnectedWith(providerType);
        }

        /// <summary>
        /// Returns connection for specified provider. (throws exception is not connected)
        /// </summary>
        /// <param name="provider">Provider Type</param>
        /// <returns></returns>
        public IProvider GetConnection(PROVIDER_TYPE provider)
        {
            return ProviderFactory.GetProvider(provider);
        }

        /// <summary>
        /// Returns a list of all types of providers, user is connected with
        /// </summary>
        /// <returns></returns>
        public static List<PROVIDER_TYPE> GetConnectedProviders()
        {
            return SessionManager.GetConnectedProviders();
        }


        //------------ Data Retrieval Methods
        /// <summary>
        /// Returns Profile from current connection or specified provider
        /// </summary>
        /// <param name="providerType">Provider Type (Connection should exist else exception is thrown)</param>
        /// <returns></returns>
        public UserProfile GetProfile(PROVIDER_TYPE providerType = PROVIDER_TYPE.NOT_SPECIFIED)
        {
            if (providerType != PROVIDER_TYPE.NOT_SPECIFIED)
            {

                if (SessionManager.IsConnectedWith(providerType))
                {
                    if (GetConnection(providerType).GetConnectionToken().Profile.IsSet)
                        return GetConnection(providerType).GetConnectionToken().Profile;
                    else
                        return SessionManager.GetConnection(providerType).GetProfile();
                }
                else
                {
                    throw new InvalidSocialAuthConnectionException(providerType);
                }
            }
            else
            {
                if (SessionManager.IsConnected)
                {
                    if (GetCurrentConnectionToken().Profile.IsSet)
                        return GetCurrentConnectionToken().Profile;
                    else
                        return SessionManager.GetCurrentConnection().GetProfile();
                }

                else
                {
                    throw new InvalidSocialAuthConnectionException();
                }
            }
        }

        /// <summary>
        /// Returns contacts from current connection or specified provider
        /// </summary>
        /// <param name="providerType">Provider Type (Connection should exist else exception is thrown)</param>
        /// <returns></returns>
        public List<Contact> GetContacts(PROVIDER_TYPE providerType = PROVIDER_TYPE.NOT_SPECIFIED)
        {
            if (providerType != PROVIDER_TYPE.NOT_SPECIFIED)
            {

                if (SessionManager.IsConnectedWith(providerType))
                {
                    return GetConnection(providerType).GetContacts();
                }
                else
                {
                    throw new InvalidSocialAuthConnectionException(providerType);
                }
            }
            else
            {
                if (SessionManager.IsConnected)
                {

                    return CurrentConnection.GetContacts();
                }

                else
                {
                    throw new InvalidSocialAuthConnectionException();
                }
            }
        }

        ///// <summary>
        ///// Execute data feed with current or specified provider
        ///// </summary>
        ///// <param name="feedUrl"></param>
        ///// <param name="transportMethod"></param>
        ///// <returns></returns>
        public WebResponse ExecuteFeed(string feedUrl, TRANSPORT_METHOD transportMethod, PROVIDER_TYPE providerType = PROVIDER_TYPE.NOT_SPECIFIED, byte[] content = null, Dictionary<string, string> headers = null)
        {

            IProvider provider = null;
            //provider is not specified explicitly. Pick connected provider.
            if (providerType == PROVIDER_TYPE.NOT_SPECIFIED)
            {
                if (SessionManager.IsConnected)
                    provider = SessionManager.GetCurrentConnection();
            }
            else //provider explicitly specified 
            {
                if (SessionManager.IsConnectedWith(providerType))
                    provider = ProviderFactory.GetProvider(providerType);

            }

            if (provider == null)
                throw new InvalidSocialAuthConnectionException(providerType);


            //Call ExecuteFeed
            WebResponse response;
            if (headers == null && content == null)
                response = provider.ExecuteFeed(feedUrl, transportMethod);
            else
                response = provider.ExecuteFeed(feedUrl, transportMethod, content, headers);
            return response;

        }

        //------------ Future Methods
        //internal static void Disconnect(PROVIDER_TYPE providerType);
        //internal static void RefreshToken()
        //internal static void Connect(Access Token)
        //internal static void SwitchConnection(PROVIDER_TYPE)
        // & more....

        #endregion

        #region InternalStuff

        
        PROVIDER_TYPE providerType { get; set; }
        internal Token contextToken { get; set; }

        /// <summary>
        /// Connects to a provider (Same as Login())
        /// </summary>
        /// <param name="providerType">Provider to which connection has to be established</param>
        /// <param name="returnURL">Optional URL where user will be redirected after login (for this provider only)</param>
        internal static void Connect(PROVIDER_TYPE providerType, string returnURL = "", string errorURL = "", bool skipRedirectionIfAlreadyConnected=false)
        {
            returnURL = returnURL ?? "";
            if (!returnURL.ToLower().StartsWith("http") && returnURL.Length > 0)
                returnURL = HttpContext.Current.Request.GetBaseURL() + returnURL;

            try
            {
                
                //User is already connected. return or redirect
                if (IsConnectedWith(providerType))
                {
                    if (skipRedirectionIfAlreadyConnected)
                        return;
                    return;
                }

                SessionManager.InProgressToken = (new Token()
                {
                    Provider = providerType,
                    Domain = HttpContext.Current.Request.GetBaseURL(),
                    UserReturnURL = returnURL,
                    SessionGUID = SessionManager.GetUserSessionGUID(),
                    Profile = new UserProfile() { Provider = providerType }

                });
                SessionManager.InProgressToken.Profile.Provider = providerType;
                if (!string.IsNullOrEmpty(errorURL))
                    SessionManager.ErrorURL = HttpContext.Current.Request.GetBaseURL() + errorURL;

                //CONNECT WITH PROVIDER
                var provider = ((IProviderConnect)ProviderFactory.GetProvider(providerType));
                provider.ConnectionToken = InProgressToken();
                provider.Connect();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns connection for last connected provider
        /// </summary>
        internal static IProvider CurrentConnection
        {
            get
            {
                return SessionManager.GetCurrentConnection();
            }
        }


        /// <summary>
        /// Logs user out of local application (User may still remain logged in at provider)
        /// </summary>
        /// <param name="loginUrl">Where should user be redirected after logout. (Only applicable when using custom authentication)</param>
        /// <param name="callback">Delegate invoked (if specified) just before redirecting user to login page</param>
        internal static void Disconnect(string loginUrl = "", Action callback = null)
        {
            //Remove all tokens
            if (HttpContext.Current.Session != null)
                SessionManager.RemoveAllConnections();

            //Redirect to login Page
            if (callback != null)
                callback.Invoke();
        }


        /// <summary>
        /// Returns a GUID to identify current user
        /// </summary>
        public Guid Identifier { get { return SessionManager.GetUserSessionGUID(); } }

        /// <summary>
        /// Called by Authentication Strategy at end of authentication process
        /// </summary>
        /// <param name="isSuccess">Is authentication successful</param>
        internal static void OnAuthneticationProcessCompleted(bool isSuccess, Token token)
        {



            SessionManager.AddConnectionToken(SessionManager.InProgressToken);
            //SetUserAsLoggedIn();

        }


        /// <summary>
        /// Redirect function
        /// </summary>
        /// <param name="url"></param>
        internal static void Redirect(string url)
        {
            HttpContext.Current.Response.Redirect(url, false);
        }

        internal static Token InProgressToken()
        {
            return SessionManager.InProgressToken;
        }

        internal static void LoginCallback(string response)
        {
            try
            {
                ((IProviderConnect)ProviderFactory.GetProvider(InProgressToken().Provider)).LoginCallback(Utility.GetQuerystringParameters(response),
                        SocialAuthUser.OnAuthneticationProcessCompleted);
            }
            catch (Exception ex)
            {
                //User has specified to redirect here upon error instead fo throwing an error
                if (!string.IsNullOrEmpty(SessionManager.ErrorURL))
                {
                    UriBuilder errorUri = new UriBuilder(SessionManager.ErrorURL);
                    errorUri.SetQueryparameter("error_message", ex.Message);
                    errorUri.SetQueryparameter("error_type", ex.GetType().ToString());
                    HttpContext.Current.Response.Redirect(errorUri.Uri.AbsoluteUri.ToString());
                }
                else
                    throw;
            }

        }

        /// <summary>
        /// Returns Connection Token of last connected provider
        /// </summary>
        /// <returns></returns>
        internal static Token GetCurrentConnectionToken()
        {
            if (SessionManager.IsConnected)
                return SessionManager.GetConnectionToken(SessionManager.GetCurrentConnection().ProviderType);
            else
                return null;
        }

        #endregion
    }
}