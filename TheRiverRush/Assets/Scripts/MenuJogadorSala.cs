using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuJogadorSala : MonoBehaviourPunCallbacks
{
    [SerializeField] private MenuEntrada menuEntrada;
    [SerializeField] private MenuLobby menuLobby;

    private void Start()
    {
        menuEntrada.gameObject.SetActive(false);
        menuLobby.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        menuEntrada.gameObject.SetActive(true);
    }
    
    public override void OnJoinedRoom()
    {
        MudaMenu(menuLobby.gameObject);
        menuLobby.photonView.RPC("AtualizaLista", RpcTarget.All);
    }

    public void MudaMenu(GameObject menu)
    {
        menuEntrada.gameObject.SetActive(false);
        menuLobby.gameObject.SetActive(false);

        menu.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        menuLobby.AtualizaLista();
    }

    public void SairDoLobby()
    {
        GestorDeRede.Instancia.SairDoLobby();
        MudaMenu(menuEntrada.gameObject);
    }

    public void ComecaJogo(string nomeCena)
    {
        GestorDeRede.Instancia.photonView.RPC("ComecaJogo", RpcTarget.All, nomeCena);
    }
}
