using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepetirFundo : MonoBehaviour
{
    private GameController gameController;

    public bool rioInstanciado = false;

    void Start()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    void Update()
    {
        if(rioInstanciado ==  false)
        {
            if(transform.position.x <= 0)
            {
                rioInstanciado = true;
                GameObject ObjetoTemporarioRio = Instantiate(gameController.rioPreFab1);
                ObjetoTemporarioRio.transform.position = new Vector3(transform.position.x + gameController.rioTamanho, transform.position.y, 0);
            }
        }

        if(transform.position.x < gameController.rioDestruido)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        MoveRio();
    }

    void MoveRio()
    {
        transform.Translate(Vector2.down * gameController.rioVelocidade * Time.deltaTime);
    }

}
