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
    [SerializeField]
    GameObject localPlayer;

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
        localPlayer.transform.position = pos+Vector3.up;
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("ConstructionAvatar", pos,new Quaternion());
        
        if(spawnedPlayerPrefab.GetComponent<PhotonView>().IsMine){
            // GameplayController.instance.localPlayer = spawnedPlayerPrefab;
            spawnedPlayerPrefab.GetComponent<HeadBodyRig>().InitLocal();
            spawnedPlayerPrefab.GetComponent<WalkingController>().activeManual = true;
        }else{
            spawnedPlayerPrefab.GetComponent<WalkingController>().activeManual = false;
            spawnedPlayerPrefab.GetComponent<HeadBodyRig>().SetProperties(false);
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
