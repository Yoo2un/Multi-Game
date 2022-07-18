using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public InputField RoomName, RoomPerson;
    public Button RoomJoin, RoomCreate;
    public GameObject RoomPrefab;
    public Transform RoomContent;

    Dictionary<string, RoomInfo> RoomCatalog = new Dictionary<string, RoomInfo>();
    private void Update()
    {
        if (RoomName.text.Length > 0)
        {
            RoomJoin.interactable = true;
        }
        else
        {
            RoomJoin.interactable = false;
        }

        if (RoomName.text.Length > 0 && RoomPerson.text.Length > 0)
        {
            RoomCreate.interactable = true;
        }
        else
        {
            RoomCreate.interactable = false;
        }
    }

    public void OnClickCreateRoom()
    {
        //룸 옵션을 설정
        RoomOptions room = new RoomOptions();
        //최대 접속자의 수 설정
        room.MaxPlayers = byte.Parse(RoomPerson.text);
        //룸의 오픈 여부를 설정
        room.IsOpen = true;
        //로비에서 룸 목록을 노출시킬지 설정
        room.IsVisible = true;

        PhotonNetwork.CreateRoom(RoomName.text, room);
    }
    public void OnClickJoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName.text);
    }
    //룸 생성이 완료된 후 호출되는 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Create Room");
    }
    public void AllDeleteRoom()
    {
        foreach(Transform trans in RoomContent)
        {
            Destroy(trans.gameObject);
        }
    }
    public override void OnJoinedRoom()//룸에 입장한 후 호출되는 콜백 함수
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }
    public void CreateRoomObject()
    {
        foreach (RoomInfo info in RoomCatalog.Values)
        {
            GameObject room = Instantiate(RoomPrefab);

            room.transform.SetParent(RoomContent);

            room.GetComponent<Information>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
    void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            if (RoomCatalog.ContainsKey(roomList[i].Name))
            {
                // RemovedFromList : (true)룸에서 삭제가 되었을 때
                if (roomList[i].RemovedFromList)
                {
                    //딕셔너리에 있는 데이터를 삭제
                    RoomCatalog.Remove(roomList[i].Name);
                    continue;
                }
            }
            //그렇지 않으면 roominfo를 RoomCatalog에 추가
            RoomCatalog[roomList[i].Name] = roomList[i];
        }
    }
    //룸으로 입장이 실패했을 때 호출되는 콜백 함수
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRoom Failed {returnCode}:{message}");
    }
    //해당 로비에 방 목록의 변경 사항이 있으면 호출(추가, 삭제, 참가)
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        CreateRoomObject();
        AllDeleteRoom();
        UpdateRoom(roomList);
    }
}
