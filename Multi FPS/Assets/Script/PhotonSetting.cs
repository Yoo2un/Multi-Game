using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PhotonSetting : MonoBehaviourPunCallbacks
{
    public InputField email;
    public InputField password;
    public InputField region;
    public InputField username;

    public void LoginSuccess(LoginResult result)
    {
        PhotonNetwork.GameVersion = "1.0f";

        PhotonNetwork.NickName = username.text;

        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = region.text;

        PhotonNetwork.LoadLevel("photon Lobby");
    }
    public void LoginFailure(PlayFabError error)
    {
        Debug.Log("�α��� ����");
    }
    public void SignUpSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("ȸ�� ���� ����");
    }
    public void SignUpFailure(PlayFabError error)
    {
        Debug.Log("ȸ�� ���� ����");
    }

    public void SignUp()
    {
        //auto <- �ڷ��� �߷�
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            Username = username.text

        };
        PlayFabClientAPI.RegisterPlayFabUser
        (
            request,
            SignUpSuccess,
            SignUpFailure
        );
    }
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, LoginSuccess, LoginFailure);
    }
}
