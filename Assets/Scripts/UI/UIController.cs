using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Photon.MonoBehaviour {

    public void JoinRoom()
    {
        {
            PhotonNetwork.JoinRoom("TestRoom");
            Debug.Log("Join room");
        }
    }

    public void CreateRoom()
    {
        {
            PhotonNetwork.CreateRoom("TestRoom", new RoomOptions() { MaxPlayers = 6 }, null);
            Debug.Log("Room was created");
        }
    }
}
