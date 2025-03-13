using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MovimentoJogador: MonoBehaviourPunCallbacks
{
    public float velocidadeDoPlayer;

    public Rigidbody2D oRigidbody2D;
    //public Rigidbody2D Rb { get => rb; set => rb = value;}

    private Player photonPlayer;
    private int id;

    [PunRPC]
    public void Inicializa(Player player)
    {
        photonPlayer = player;
        id = player.ActorNumber;
        GameController.Instancia.Jogadores.Add(this);

        if(!photonView.IsMine)
        {
            oRigidbody2D.isKinematic = true;
        }
    }

    void Start()
    {
        
    }

    //Unity nao lida com o metodo update e rigidbody
    void Update()
    {
        
    }

    [PunRPC]
    void FixedUpdate()
    {
        if(photonView.IsMine)
        {
            MovimentarPlayer();
        }
        
    }

    [PunRPC]
    public void MovimentarPlayer()
    {
        float inputDoMovimento = Input.GetAxisRaw("Vertical");
        
        //Acessa o componente Rigidbody do jogador e passa um Vector2 como velocidade;
        //Vector2 -> Verifica a tecla pressionada e movimentar o jogador nessa direcao abseado no valor da variavel do jogador
        //mantem a dimensao Y, ficando na mesma altura
        oRigidbody2D.velocity = new Vector2(oRigidbody2D.velocity.x, inputDoMovimento * velocidadeDoPlayer);

        if(Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);

            if(t.phase == TouchPhase.Moved)
            {
                transform.position += (Vector3)t.deltaPosition/300;
            }
        }
    }
   
}

