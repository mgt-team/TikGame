using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Photon.PunBehaviour {

    [SerializeField]
    private FinishPlatform _finishPlatform;
    
    [SerializeField]
    private PlatformManager _platformManager;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private PhotonView _photonView;

    public List<Vector3> _startPositions;

    public int count;

    private void Start()
    {
        _finishPlatform.onFinish += FinishPlatform_onFinish;
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
        _platformManager.InitPlatformMap();
        _startPositions = _platformManager.GetStartedPositions();
    }

    private void FinishPlatform_onFinish()
    {
        Debug.Log("Finish");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneFinishedLoading");
        if (scene.name == "TestSceneWithHexogenMap")
        {
            SpawnPlayer();
            GameObject.Find("MenuCamera").SetActive(false);
        }
    }

    void SpawnPlayer()
    {
        _photonView.RPC("GetPlayersCount", PhotonTargets.All);
        Vector3 startPos = GetSpawnTransform(count);
        PhotonNetwork.Instantiate(_player.name, startPos, _player.transform.rotation, (byte)count);
    }

    private Vector3 GetSpawnTransform(int count)
    {
        if (_startPositions[count] != null)
        {
            Vector3 pos = _startPositions[count];
            pos = new Vector3(pos.x, pos.y + 5, pos.z);
            return pos;
        }
        else
            return Vector3.zero;
    }

    [PunRPC]
    public void GetPlayersCount()
    {
        count =  GameObject.FindGameObjectsWithTag(TagManager.GetTagNameByEnum(TagEnum.Player)).Length;
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
