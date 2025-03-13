using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixo : MonoBehaviour
{
    private GameController gameController;

    private Rigidbody2D lixoRB2D;

    // Start is called before the first frame update
    void Start()
    {   

        gameController = FindObjectOfType(typeof(GameController)) as GameController;

        lixoRB2D = GetComponent<Rigidbody2D>();
        lixoRB2D.velocity = new Vector2(-6f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gameController.Pontos(1);

            gameController.fxGame.PlayOneShot(gameController.fxLixoColetado);
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
