using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;



public class SceneLoader : MonoBehaviour
{

   [Header("Porcentagem")]
    public Slider sLoading;
    public Text textPorcentagem;

    [Header("Imagem")]
    public Image imgParaMudar;
    public Sprite[] imagens;

    [Header("Cena")]
    public string cena;

    private void Start(){
        sLoading.value = 0;
        textPorcentagem.text = "0%";
        StartCoroutine(LoadScene_Estiloso());

    }
    public void MudarImagem(){
        int rand = Random.Range(0, imagens.Length);


        imgParaMudar.sprite = imagens[rand];

    }

    private IEnumerator LoadScene() {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(cena); //alterar o numero do parâmetro LoadSceneAsync para o valor da tela que ira chamar apos o carregamento

       while (!operation.isDone) {

            float progresso = Mathf.Clamp01(operation.progress / 0.9f) * 100;

            sLoading.value = progresso;
            textPorcentagem.text = progresso + "%";

            yield return null;


        }
    }
        
    private IEnumerator LoadScene_Estiloso() {
        yield return null;

       AsyncOperation operation = SceneManager.LoadSceneAsync(cena); //alterar o numero do parâmetro LoadSceneAsync para o valor da tela que ira chamar apos o carregamento
        operation.allowSceneActivation = false;

        float progresso = 0.0f;

        while(progresso < 100){
            yield return new WaitForSeconds (0.8f); 
            progresso += Random.Range(5.0f, 10.0f);
            sLoading.value = progresso;
            textPorcentagem.text = ((int)progresso) + "%";

        }

            sLoading.value = 100;
            textPorcentagem.text = "100%";
            operation.allowSceneActivation = true;
    
           yield return null;

    }

}
