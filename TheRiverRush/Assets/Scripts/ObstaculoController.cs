using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoController : MonoBehaviour
{
    private Rigidbody2D ObstaculoRB;

    private GameController gameController;

    private CameraShaker cameraShaker;


    void Start()
    {
        ObstaculoRB = GetComponent<Rigidbody2D>();
        //movimentar para esquerda precisa ter o valor negativo do eixo x
        //ObstaculoRB.velocity = new Vector2(-5f, 0);

        gameController = FindObjectOfType(typeof(GameController)) as GameController;

        //Acesso aos objetos que são do tipo do script, nele mesmo
        cameraShaker = FindObjectOfType(typeof(CameraShaker)) as CameraShaker;
    }

    void FixedUpdate()
    {
        MoveObjeto();
    }

    void MoveObjeto()
    {
        transform.Translate(Vector2.left * gameController.ObstaculoVelocidade * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameController.fxGame.PlayOneShot(gameController.fxObstaculo);

            gameController.vidasPlayer--;
            if(gameController.vidasPlayer <= 0)
            {
                Debug.Log("Fim do jogo!");
                gameController.txtVidas.text = "0";
                gameController.AcabouVidas();
            }
            else
            {
                gameController.txtVidas.text = gameController.vidasPlayer.ToString();
            }

            cameraShaker.ShakeIt();
        }
    }

    
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
