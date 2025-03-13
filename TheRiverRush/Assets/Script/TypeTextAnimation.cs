using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Image character1Image;
    public Image character2Image;
    public Image character3Image; 
    public Button nextButton;

    private string[] dialogs = new string[]
    {
        

"Tulipa:        Olá, meus amigos Eco e Flora! Chamei vocês para uma missão. A cidade de São Paulo está enfrentando uma crise ambiental alarmante, à beira da extinção.", 

"Tulipa:        O Rio Tietê, antes uma fonte de vida essencial para a cidade, agora se tornou um símbolo de poluição e desespero. Conto com vocês para salvar o Rio Tietê e proteger o nosso meio ambiente.", 

"Eco:             Olá, velha amiga! Claro que estamos iremos embarcar nessa missão. ", 
 
"Flora:           O que precisamos fazer? ",

"Tulipa:        Que ótimo que vocês estão a bordo! Nossa missão é coletar o máximo de lixos ao longo do Rio Tietê para despoluir suas águas. ", 

"Flora:           Parece uma missão emocionante! ", 

"Tulipa:        Antes de partirmos, é importante que vocês saibam que enfrentaremos grandes perigos em nosso caminho devido ao aumento da poluição. ",

"Tulipa:        As toxinas dos resíduos criaram inimigos obstinados que farão de tudo para nos deter. Meus amigos, devemos ter muito cuidado. ",

"Eco:             Compreendido, Tulipa! Vamos nos preparar para superar qualquer desafio que surgir em nossos caminhos. ",

"Flora:          Então, meus amigos, é hora de começarmos nossa emocionante aventura como nos velhos tempos! Juntos, vamos limpar o Rio Tietê! "

    };

    private int currentDialogIndex = 0;

    void Start()
    {
        nextButton.onClick.AddListener(NextDialog);
        ShowDialog();
    }

    void ShowDialog()
    {
        if (currentDialogIndex < dialogs.Length)
        {
            string currentDialog = dialogs[currentDialogIndex];

          
            dialogText.text = currentDialog;

     
            if (currentDialog.StartsWith("Tulipa:"))
            {
                character1Image.gameObject.SetActive(true);
                character2Image.gameObject.SetActive(false);
                character3Image.gameObject.SetActive(false);
            }
            else if (currentDialog.StartsWith("Flora:"))
            {
                character1Image.gameObject.SetActive(false);
                character2Image.gameObject.SetActive(true);
                character3Image.gameObject.SetActive(false);
            }
            else if (currentDialog.StartsWith("Eco:"))
            {
                character1Image.gameObject.SetActive(false);
                character2Image.gameObject.SetActive(false);
                character3Image.gameObject.SetActive(true);
            }
        }
        else
        {
            StartGame();
        }
    }

    void NextDialog()
    {
        currentDialogIndex++;
        ShowDialog();
    }

    void StartGame()
    {
        SceneManager.LoadScene("SalaENome"); // Substitua pelo nome da cena do jogo
    }
}
