using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviourPunCallbacks
{

    //Propriedades do chao
    [Header("Configuração do Chão")]
    public float rioDestruido;
    public float MargemDireitaDestruida;
    public float MargemEsquerdaDestruida;

    public float rioTamanho;
    public float margemDireitaTamanho;
    public float margemEsquerdaTamanho;

    public float rioVelocidade;
    public float margemDireitaVelocidade;
    public float margemEsquerdaVelocidade;

    public GameObject rioPreFab1;
    public GameObject margemDireitaPreFab;
    public GameObject margemEsquerdaPreFab;

    [Header("Configuração do Obstáculo")]
    public float        ObstaculoTempo;
    public GameObject   ObstaculoPrefab;
    public float        ObstaculoVelocidade;

    [Header("Configuração do Lixo")]
    public float        lixoTempo;
    public GameObject   lixoPrefab;
    private float       timeCount;

    [Header("Configuração UI")]
    public int pontosPlayer;
    public Text txtPontos;
    public int vidasPlayer;
    public Text txtVidas;

    [Header("Sons e Efeitos")]
    public AudioSource fxGame;
    public AudioClip fxLixoColetado;
    public AudioClip fxObstaculo;

    [Header("Cenas")]
    public string proximaCena;
    public string cenaVitoria;

    [Header("Rede")]
    [SerializeField] public string localizacaoPrefab;
    public Transform[] spawns;
    
    private int jogadoresEmJogo = 0;
    private List<MovimentoJogador> jogadores;
    public List<MovimentoJogador> Jogadores { get => jogadores; private set => jogadores = value; }

    public static GameController Instancia {get; private set; }

    private int contador = 0;

    private void Awake()
    {
        if(Instancia != null && Instancia != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instancia = this;
        
    }

    [PunRPC]
    private void AdicionaJogador()
    {
       jogadoresEmJogo++;
       if(jogadoresEmJogo == PhotonNetwork.PlayerList.Length)
       {
            CriaJogador();
       }
    }

    private void CriaJogador()
    {
        var jogadorObj = PhotonNetwork.Instantiate(localizacaoPrefab, spawns[Random.Range(0, spawns.Length)].position, Quaternion.identity);
        var jogador = jogadorObj.GetComponent<MovimentoJogador>();

        jogador.photonView.RPC("Inicializa", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }   

    void Start()
    {
        contador++;
        photonView.RPC("AdicionaJogador", RpcTarget.AllBuffered);
        jogadores = new List<MovimentoJogador>();

        StartCoroutine("SpawnObstaculo");
        StartCoroutine("SpawnLixo"); 
    }

    void Update()
    {
        SpawnLixo();
        SpawnObstaculo();
        PhotonNetwork.AutomaticallySyncScene = true;
        //SceneManager.UnloadSceneAsync(deletarCena);
       
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reinicie as corrotinas aqui se necessário
        if (scene.name == proximaCena)
        {
            StartCoroutine(SpawnObstaculo());
            StartCoroutine(SpawnLixo());
        }
    }

    //co-rotina
    IEnumerator SpawnObstaculo()
    {
        yield return new WaitForSeconds(ObstaculoTempo);
        GameObject obstaculoSpawn = Instantiate(ObstaculoPrefab);
        obstaculoSpawn.transform.position = new Vector3(3, Random.Range(-3, 3), 0);

        StartCoroutine("SpawnObstaculo");

        yield return new WaitForSeconds(1.0f);

        StartCoroutine("SpawnLixo");
    }

    IEnumerator SpawnLixo()
    {

        int lixosAleatorios = Random.Range(1, 5);

        for (int contagem = 1; contagem <= lixosAleatorios; contagem++)
        {
            yield return new WaitForSeconds(lixoTempo);
            GameObject objetoSpawn = Instantiate(lixoPrefab);
            //está instanciando ele na mesma posição que ele já esta
            objetoSpawn.transform.position = new Vector3(objetoSpawn.transform.position.x, Random.Range(-3, 3), 0);
        }
    }

    public void Pontos(int qtdPontos)
    {
        pontosPlayer += qtdPontos;
        txtPontos.text = pontosPlayer.ToString();

        if(contador >= 2)
        {
            PhotonNetwork.LoadLevel(cenaVitoria);
        }else
        {
          if(pontosPlayer >= 3)
          {
            StopCoroutine("SpawnObstaculo");
            StopCoroutine("SpawnLixo");
            PhotonNetwork.LoadLevel(proximaCena);
          }
        }
        
    }

    public void AcabouVidas()
    {
        GameOver();
    }

    void GameOver()
    {
        PhotonNetwork.LoadLevel(6);
    }

}
