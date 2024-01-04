using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class ServerManager : MonoBehaviourPunCallbacks
{
    public GameObject characterPref;
    public static ServerManager Instance;
    public bool ConnectedServer,JoinRoom;
    void Start()
    {
        // Photon'a bağlan.
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Awake() 
   {
     if (Instance==null)
     {
        Instance=this;
        DontDestroyOnLoad(gameObject);

     }
     else
     {
        Destroy(gameObject);
     }
   }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
 
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon'a bağlandı.");
         
        // Oyuncu adını belirle.
        PhotonNetwork.NickName = "PON" + Random.Range(1000, 9999);
        ConnectedServer=true;

        // Rastgele bir odaya katıl veya oluştur.
    }
    public void joinrm()
    {
        SceneManager.LoadScene(1);
      PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Hiçbir oda bulunamadı, yeni bir oda oluşturuluyor.");

        // Rastgele bir oda adı oluştur.
        string roomName = "Oda" + Random.Range(1000, 9999);

        // Odayı oluştur.
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; // Oyuncu sayısını ayarlayabilirsiniz.

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log("Odaya katıldınız: " + PhotonNetwork.CurrentRoom.Name); 
       JoinRoom=true;
    }
    public void StartGame()
    {
       Invoke(nameof(Createcharacter),2f);
    }
    void Createcharacter()
    {
      GameObject character = PhotonNetwork.Instantiate(characterPref.name, Vector3.zero, Quaternion.identity);
    }
}
