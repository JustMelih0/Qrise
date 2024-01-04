using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;


public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField]private GameObject startscreen;
    [SerializeField]private TextMeshProUGUI playercount;
    private void Start() {
        InvokeRepeating(nameof(CheckConnected),0f,0.5f);
        ServerManager.Instance.JoinRoom=false;
    }
    void CheckConnected()
    { 
        if (ServerManager.Instance.ConnectedServer)
        {
            startscreen.SetActive(false);            
        }
        playercount.text="Oyuncu Sayisi: "+PhotonNetwork.CountOfPlayers;
    }
    public void StartGame()
    {
        ServerManager.Instance.joinrm();
    }
}
