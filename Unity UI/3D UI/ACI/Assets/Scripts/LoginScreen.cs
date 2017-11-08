using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using Facebook.Unity;
public class LoginScreen : MonoBehaviour
{

    private GameObject canvas;
    private InputField _uID;
    private string _userID;
    private InputField _password;
    private Button _loginButton;
    private Button _FBlogin;
    private Image _profilePic;
    private Text usernameField;
    private Text connectionInfoField;
    IDictionary data;

    public StreamWriter sw;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        _uID = canvas.transform.Find("UID").GetComponent<InputField>();
        _password = canvas.transform.Find("Password").GetComponent<InputField>();
        _loginButton = canvas.transform.Find("ButtonLogin").GetComponent<Button>();
        _FBlogin = canvas.transform.Find("FacebookLogin").GetComponent<Button>();
        connectionInfoField = canvas.transform.Find("ConnectionInfoField").GetComponent<Text>();
        usernameField = canvas.transform.Find("UserNameField").GetComponent<Text>();
         _profilePic = canvas.transform.Find("Image").GetComponent<Image>();
        // _login.onClick.AddListener(Login);
        _loginButton.onClick.AddListener(StartGame);
        _FBlogin.onClick.AddListener(FacebookConnect_bttn);

        _FBlogin.gameObject.SetActive(true);
        _loginButton.gameObject.SetActive(false);
        GS.GameSparksAvailable += OnGameSparksConnected;

    }
    private void OnGameSparksConnected(bool _isConnected)
    {
        if (_isConnected)
        {
            connectionInfoField.text = "GameSparks Connected...";
        }
        else
        {
            connectionInfoField.text = "GameSparks Not Connected...";
        }
    }


   public void UserAuthentication_Bttn()
    {
        Debug.Log("Attempting User Login...");
        //print out the username and password here just to check they are correct //
        //Debug.Log("User Name:" + _uID.text);
       // Debug.Log("Password:" + _password.text);
        new GameSparks.Api.Requests.AuthenticationRequest()
            .SetUserName(_userID)//set the username for login
            //.SetPassword(_password.text)//set the password for login
            .Send((auth_response) =>
            { //send the authentication request
                if (!auth_response.HasErrors)
                { // for the next part, check to see if we have any errors i.e. Authentication failed
                    connectionInfoField.text = "GameSparks Authenticated...";
                   // usernameField.text = auth_response.DisplayName;
                }
                else
                {
                    Debug.LogWarning(auth_response.Errors.JSON); // if we have errors, print them out
                    if (auth_response.Errors.GetString("DETAILS") == "UNRECOGNISED")
                    { // if we get this error it means we are not registered, so let's register the user instead
                        Debug.Log("User Doesn't Exists, Registering User...");
                        new GameSparks.Api.Requests.RegistrationRequest()
                            .SetDisplayName(_userID)
                            .SetUserName(_userID)
                            //.SetPassword(_password.text)
                            .Send((reg_response) =>
                            {
                                if (!reg_response.HasErrors)
                                {
                                    connectionInfoField.text = "GameSparks Authenticated...";
                                    //usernameField.text = reg_response.DisplayName;
                                }
                                else
                                {
                                    Debug.LogWarning(auth_response.Errors.JSON); // if we have errors, print them out
                                }
                            });
                    }
                }
            });
    }


   public void StartGame()
    {
        SceneManager.LoadScene("ACI-UI");
    }
   public void Facebook_Auth()
    {
        new GameSparks.Api.Requests.FacebookConnectRequest().SetSwitchIfPossible(true)
           .Send((response) =>
           { //send the authentication request
                if (!response.HasErrors)
               { // for the next part, check to see if we have any errors i.e. Authentication failed
                    connectionInfoField.text = "GameSparks Authenticated...";
                   Debug.Log("Player created in GameSparks");
                    // usernameField.text = auth_response.DisplayName;
                }
               else
               {
                   Debug.LogWarning(response.Errors.JSON); // if we have errors, print them out
                    if (response.Errors.GetString("DETAILS") == "UNRECOGNISED")
                   { // if we get this error it means we are not registered, so let's register the user instead
                        Debug.Log("User Doesn't Exists, Registering User...");
                       new GameSparks.Api.Requests.RegistrationRequest()
                           .SetDisplayName(_userID)
                           .SetUserName(_userID)
                           //.SetPassword(_password.text)
                           .Send((reg_response) =>
                           {
                               if (!reg_response.HasErrors)
                               {
                                   connectionInfoField.text = "GameSparks Authenticated...";
                                    //usernameField.text = reg_response.DisplayName;
                                }
                               else
                               {
                                   Debug.LogWarning(response.Errors.JSON); // if we have errors, print them out
                                }
                           });
                   }
               }
               _FBlogin.gameObject.SetActive(false);
               _loginButton.gameObject.SetActive(true);
           });
    }

    public void Login()
    {
        Debug.Log(_uID.text + " " + _password.text);
        sw = new StreamWriter(@"" + _uID.text + ".txt");
        sw.WriteLine("User Name: " + _uID.text);
        sw.WriteLine("Password: " + _password.text);
        SceneManager.LoadScene("ACI-UI");
        //sw.Close();
        //System.IO.File.WriteAllText("C: /Users/Rohan/Downloads/"+_uID.text+".txt",_password.text);
    }

    void DisplayProfilePicandUserName()
    {
     
        FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUserName);
        FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayPhoto);

    }

 

    void DisplayUserName(IResult result)
    {
        //if(result.Error == null)
        {
            
            _userID = result.ResultDictionary["first_name"] as string;
            //Debug.Log(_userID);
            usernameField.text = "Hi there, " + _userID;
        }
        //else
        {
           // Debug.Log("heeeeee "+result.Error);
        }
    }

    void DisplayPhoto(IGraphResult result)
    {
       //data = result.ResultDictionary["data"] as IDictionary;
       // string photoURL = data["url"] as string;

       // WWW www = new WWW(photoURL);
        
       // Texture2D img = www.texture;
       // _profilePic.sprite = Sprite.Create(img, new Rect(0, 0, 128, 128), new Vector2());

        if (!string.IsNullOrEmpty(result.Error))
        {
           _profilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else if(result.Error == null)
        {
            Debug.Log("heeeeee " + result.Error);
        }
    }



    public void FacebookConnect_bttn()
    {
        Debug.Log("Connecting Facebook With GameSparks...");// first check if FB is ready, and then login //
                                                            // if it's not ready we just init FB and use the login method as the callback for the init method //
        if (!FB.IsInitialized)
        {
            Debug.Log("Initializing Facebook...");

            FB.Init(ConnectGameSparksToGameSparks, null);
            
            
        }
        else
        {
            FB.ActivateApp();
            ConnectGameSparksToGameSparks();
        }
    }

 
    private void ConnectGameSparksToGameSparks()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
            Debug.Log("Logging Into Facebook...");

            List<string> perms = new List<string>() { "public_profile", "email", "user_friends" };
            FB.LogInWithReadPermissions(perms, (result) =>
            {
                DisplayProfilePicandUserName();
                if (FB.IsLoggedIn)
                {
                    Debug.Log("Logged In, Connecting GS via FB.."); new GameSparks.Api.Requests.FacebookConnectRequest()
 .SetAccessToken(AccessToken.CurrentAccessToken.TokenString)
 .SetDoNotLinkToCurrentPlayer(false)// we don't want to create a new account so link to the player that is currently logged in
 .SetSwitchIfPossible(true)//this will switch to the player with this FB account id they already have an account from a separate login
 .Send((fbauth_response) =>
 {
     if (!fbauth_response.HasErrors)
     {
         connectionInfoField.text = "GameSparks Authenticated With Facebook...";
         Facebook_Auth();
        // usernameField.text = fbauth_response.DisplayName;
     }
     else
     {
         Debug.LogWarning(fbauth_response.Errors.JSON);//if we have errors, print them out
     }
 });
                }
        else{
                    Debug.LogWarning("Facebook Login Failed:" + result.Error);
                }
            });// lastly call another method to login, and when logged in we have a callback
        }
        else
        {
            FacebookConnect_bttn();// if we are still not connected, then try to process again
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
