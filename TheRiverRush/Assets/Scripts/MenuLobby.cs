using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text listaDeJogadores;
    [SerializeField] private Button comecaJogo;

    [PunRPC]

    public void AtualizaLista()
    {
        listaDeJogadores.text = GestorDeRede.Instancia.ObterListaDeJogadores();
        comecaJogo.interactable = GestorDeRede.Instancia.DonoDaSala();
    }


}
