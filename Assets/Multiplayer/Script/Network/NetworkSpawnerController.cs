using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkSpawnerController : MonoBehaviourPunCallbacks
{
    public static NetworkSpawnerController instance;
    [SerializeField]
    Collider userGroundSpawn;
    GameObject spawnedPlayerPrefab;
    GameObject localPlayerPrefab;

    private void Awake() {
        if(instance == null)
            instance = this;
    }
    public Vector3 GetRandomPointAbove(Collider target)
    {
        float minx = target.bounds.min.x;
        float minz = target.bounds.min.z;
        float maxx = target.bounds.max.x;
        float maxz = target.bounds.max.z;
        float x = Random.Range(minx, maxx);
        float y = target.bounds.center.y;
        float z = Random.Range(minz, maxz);
        var localPos = new Vector3(x, y, z);
        return localPos;
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // PhotonNetwork.LocalPlayer.NickName = "Player "+PhotonNetwork.CountOfPlayers;

        Vector3 pos = GetRandomPointAbove(userGroundSpawn);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Player", pos,new Quaternion());
        
        if(spawnedPlayerPrefab.GetComponent<PhotonView>().IsMine){
            // GameplayController.instance.localPlayer = spawnedPlayerPrefab;
            spawnedPlayerPrefab.GetComponent<OtherPlayerScript>().SetLocal();
        }else{
            spawnedPlayerPrefab.GetComponent<OtherPlayerScript>().SetNotLocal();
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("a new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public void QuidClass(){
        PhotonNetwork.LeaveRoom();
        Application.LoadLevel(0);
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }

}
