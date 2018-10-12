using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : Photon.MonoBehaviour {

    [SerializeField]
    private PhotonView _photonView;

    [SerializeField]
    private GameObject _ui;

    private void Start()
    {
        СonnectToPhoton();
        PhotonNetwork.automaticallySyncScene = true;
    }

    private void СonnectToPhoton () {
        PhotonNetwork.ConnectUsingSettings("0.1");

        Debug.Log("ConnectToPhoton");
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);

        Debug.Log("OnConnectedToMaster");
    }

    private void OnDisconnectedFromPhoton()
    {
        Debug.Log("Disconnected");
    }

    private void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        _ui.SetActive(true);
    }

    private void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        PhotonNetwork.LoadLevel("TestSceneWithHexogenMap");
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

}
