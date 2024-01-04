using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]private GameObject startscreen;
    [SerializeField]private TextMeshProUGUI playercount,ping;
    bool oneshot;
    private void Start() {
        InvokeRepeating(nameof(CheckConnected),0f,0.1f);
        InvokeRepeating(nameof(GameControl),0f,1f);
    }
    void CheckConnected()
    {     
        if (ServerManager.Instance.JoinRoom)
        {
             playercount.text=PhotonNetwork.CurrentRoom.PlayerCount+"/"+"2";
             if (PhotonNetwork.CurrentRoom.PlayerCount==2&&oneshot==false)
             {
                 startscreen.SetActive(false); 
                 ServerManager.Instance.StartGame();    
                 oneshot=true; 
                 CancelInvoke(nameof(CheckConnected));      
             }        
        }
    }
    void GameControl()
    {
       ping.text=PhotonNetwork.GetPing()+"ms";   
    }
     public void MainMenu()
    {
        PhotonNetwork.LeaveRoom();
     SceneManager.LoadScene(0);
    }
}
