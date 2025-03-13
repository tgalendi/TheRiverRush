using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepetirMargemDireitra : MonoBehaviour
{
    private GameController gameController;

    public bool margemInstanciada = false;

    void Start2()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    void Update2()
    {
        if(margemInstanciada ==  false)
        {
            if(transform.position.x <= 0)
            {
                margemInstanciada = true;
                GameObject ObjetoTemporarioMargemD = Instantiate(gameController.margemDireitaPreFab);
                ObjetoTemporarioMargemD.transform.position = new Vector3(transform.position.x + gameController.margemDireitaTamanho, transform.position.y, 0);
            }
        }

        if(transform.position.x < gameController.MargemDireitaDestruida)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate2()
    {
        MoveMargemDireita();
    }

    void MoveMargemDireita()
    {
        transform.Translate(Vector2.left * gameController.margemDireitaVelocidade * Time.deltaTime);
    }

}
