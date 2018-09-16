using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Photon.MonoBehaviour {

    [SerializeField]
    private Transform _player;

    [SerializeField]
    private PlatformManager _platformManager;

    [SerializeField]
    private GameObject _ui;

    [PunRPC]
    private int count = 0;

    private void Start()
    {
        СonnectToPhoton();
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
        GameObject.Find("MenuCamera").SetActive(false);
        SpawnPlayer();
    }

    [PunRPC]
    void SpawnPlayer()
    {
        int id = PhotonNetwork.AllocateViewID();
 
        PhotonView photonView = gameObject.GetComponent<PhotonView>();

        PhotonPlayer photonPlayer = new PhotonPlayer(true, count, "Player" + count);
        photonView.RPC("SpawnOnNetwork", PhotonTargets.AllBuffered, GetSpawnTransform(count).position, transform.rotation, count, PhotonNetwork.player);
        count++;
        Debug.Log("SpawnPlayer");
    }

    [PunRPC]
    void SpawnOnNetwork(Vector3 position, Quaternion rotation, int id, PhotonPlayer player)
    {
        Transform newPLayer = Instantiate(_player, position, rotation) as Transform;
        PhotonView[] newView = newPLayer.GetComponentsInChildren<PhotonView>();
        newView[0].viewID = id;
    }

    private Transform GetSpawnTransform(int count)
    {
        List<Transform> transforms = _platformManager.GetStartedPlatforms();
        Vector3 position = transforms[count].position;
        transforms[count].position = new Vector3(position.x, position.y + 5, position.z);
        return transforms[count];
    }

    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

}
