using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : Photon.MonoBehaviour {

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private PlatformManager _platformManager;

    [SerializeField]
    private GameObject _ui;

    [PunRPC]
    public int count = 0;

    private void Start()
    {
        СonnectToPhoton();
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
        PhotonNetwork.automaticallySyncScene = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            IncreaseCount();
        }
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

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneFinishedLoading");
        if(scene.name == "TestSceneWithHexogenMap")
        {
            SpawnPlayer();
            GameObject.Find("MenuCamera").SetActive(false);
        }
    }


    [PunRPC]
    void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(_player.name, _player.transform.position, _player.transform.rotation, (byte)count);
        IncreaseCount();
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
        GUILayout.Label(count.ToString());
    }

    //Тестовый метод, потом удалить. Для проверки синхронизации инта.
    [PunRPC]
    public void IncreaseCount()
    {
        count++;
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            int c = count;
            stream.Serialize(ref c);
        }
        else
        {
            stream.Serialize(ref count);
        }
    }

}
