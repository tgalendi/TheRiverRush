using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepetirMargemEsquerda : MonoBehaviour
{
    private GameController gameController;

    public bool margemInstanciadaE = false;

    void Start2()
    {
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    void Update3()
    {
        if(margemInstanciadaE ==  false)
        {
            if(transform.position.x <= 0)
            {
                margemInstanciadaE = true;
                GameObject ObjetoTemporarioMargemE = Instantiate(gameController.margemEsquerdaPreFab);
                ObjetoTemporarioMargemE.transform.position = new Vector3(transform.position.x + gameController.margemEsquerdaTamanho, transform.position.y, 0);
            }
        }

        if(transform.position.x < gameController.MargemEsquerdaDestruida)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate3()
    {
        MoveMargemEsquerda();
    }

    void MoveMargemEsquerda()
    {
        transform.Translate(Vector2.left * gameController.margemEsquerdaVelocidade * Time.deltaTime);
    }

}
