using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartKnight;
    [SerializeField]
    private GameObject quickStartPaladin;
    [SerializeField]
    private GameObject quickCancelButton;
    [SerializeField]
    private int RoomSize;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        quickStartKnight.SetActive(true);
        quickStartPaladin.SetActive(true);
    }

    public void QuickStartKnight()
    {
        quickStartPaladin.SetActive(false);
        quickStartKnight.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Quick Start");
    }

    public void QuickStartPaladin()
    {
        quickStartPaladin.SetActive(false);
        quickStartKnight.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Quick Start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to Join a Room");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Creating Room");
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        Debug.Log("Room Number" + randomRoomNumber);
    }
    
    public void QuickCancel()
    {
        quickCancelButton.SetActive(false);
        quickStartKnight.SetActive(true);
        quickStartPaladin.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
