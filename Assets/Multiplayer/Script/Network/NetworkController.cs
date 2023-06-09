using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Multiplayer.Controller
{
    public class NetworkController : MonoBehaviourPunCallbacks
    {
        public static NetworkController instance;
        [Header("UI")]
        [SerializeField]
        GameObject _loadingPanel;
        [SerializeField]
        TMP_InputField _name, _weight;
        [SerializeField]
        Button _male, _female;
        [SerializeField]
        Slider _slider;
        [Header("Join Room")]
        [SerializeField]
        Transform _roomContainer;
        [SerializeField]
        GameObject _roomListItem;
        [Header("Create Room")]
        [SerializeField]
        TMP_InputField _createInput;
        [SerializeField]
        Button _buttonCreate;
        Hashtable _hashTemp;
        string _nameTemp;
        bool _gender = true; //true is male
        bool readyToCreate = false;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        public void ConnectToServer()
        {
            _loadingPanel.SetActive(true);
            _slider.value = .5f;

            PhotonNetwork.ConnectUsingSettings();
            readyToCreate = false;
            print("Try to connect...");
        }
        public override void OnConnectedToMaster()
        {
            print("connected to server");
            _loadingPanel.SetActive(false);
            _slider.value = 0f;

            base.OnConnectedToMaster();
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            print("Joined Lobby");
            readyToCreate = true;
        }
        public void JoinRoom(string name)
        {
            // PhotonNetwork.LoadLevel(1);

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 20;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;

            PhotonNetwork.JoinOrCreateRoom(name, roomOptions, TypedLobby.Default);

            AssignPropertiesPlayer();

            StartCoroutine(LoadLevel(1));
        }
        public void InitializeRoom(string name)
        {
            StartCoroutine(CreateRoom(name));
        }
        public void CreateRoom(){
            StartCoroutine(CreateRoom(_createInput.text));
        }
        IEnumerator CreateRoom(string name)
        {
            while (!readyToCreate)
            {
                yield return null;
            }

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 10;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            string roomName = name;


            PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);
            print("room created");
            AssignPropertiesPlayer();

            PhotonNetwork.LoadLevel(1);

        }
        IEnumerator LoadLevel(int index)
        {
            PhotonNetwork.LoadLevel(1);
            _loadingPanel.SetActive(true);
            while (PhotonNetwork.LevelLoadingProgress < 1)
            {
                float progress = Mathf.Clamp01(PhotonNetwork.LevelLoadingProgress / .9f);
                _slider.value = progress;
                yield return null;
            }
        }

        public void AssignPropertiesPlayer(){
            Hashtable hash = new Hashtable();
            hash.Add("gender", _gender);

            int weightTemp = 75;

            if(!string.IsNullOrEmpty(_weight.text))
                weightTemp = int.Parse(_weight.text);

            hash.Add("weight", weightTemp);
            PhotonNetwork.SetPlayerCustomProperties(hash);

            string nickName = "";
            if(string.IsNullOrEmpty(_name.text))
                nickName = RandomTextFill.RandomText5();
            else
                nickName = _name.text;

            PhotonNetwork.LocalPlayer.NickName = nickName;
        }
        public void SetName(string name)
        {
            _nameTemp = name;
        }
        public void ChooseGender(bool isMale){
            _male.interactable = !isMale;
            _female.interactable = isMale;
            _gender = isMale;
        }
        public override void OnJoinedRoom()
        {
            print("Join room " + PhotonNetwork.CurrentRoom.Name);
            base.OnJoinedRoom();
        }
        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            for (int i = 0; i < _roomContainer.childCount; i++)
            {
                Destroy(_roomContainer.GetChild(i).gameObject);
            }
            foreach (var item in roomList)
            {
                GameObject go = Instantiate(_roomListItem, _roomContainer);
                go.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = item.Name;
                go.GetComponent<Button>().onClick.AddListener(() =>
                {
                    JoinRoom(item.Name);
                });
            }
            base.OnRoomListUpdate(roomList);
        }
        public void LeaveRoom()
        {
            PhotonNetwork.LoadLevel(0);

            PhotonNetwork.LeaveRoom();
        }
        // Start is called before the first frame update
        void Start()
        {
            ConnectToServer();
        }
    }
}
