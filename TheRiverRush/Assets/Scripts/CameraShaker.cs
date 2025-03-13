using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [Header("Camera Shaker Config")]
    private Vector3 cameraInitialPosition;
    public float shakeMagnitude = 0.05f;
    public float shakeTime = 0.5f;
    public Camera mainCamera;

    public void ShakeIt()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        //Multiplica o valor do tremor e depois diminui da própria intensidade para fins de efeitos especiais
        float cameraShakingOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakingOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        //Guarda o valor das posições da mainCamera em uma nova variável intermediária
        Vector3 cameraIntermediatePosition = mainCamera.transform.position;

        //guarda as posições Y e X após o tremor da câmera e envia esse novo valor da posição na main Camera
        cameraIntermediatePosition.x += cameraShakingOffsetX;
        cameraIntermediatePosition.y += cameraShakingOffsetY;
        mainCamera.transform.position = cameraIntermediatePosition;
    }

    //Cancela o Invoke do método que faz a tela tremer e muda os valores da posição da câmera para a posição 
    //incial dela que foi armazenada na variável "cameraInitialPosition"
    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
}
