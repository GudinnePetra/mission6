using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score;
    public bool isGameActive;
    public GameObject titleScreen;
    public int lives = 3;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    private int pizzaCount = 0;
    private int skullCount = 0;
    private int bananaCount = 0;
    private int cookieCount = 0;

    // Oyun sonunda istatistikleri göstereceğimiz UI Yazısı
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI pizzaStatsText;
    public TextMeshProUGUI skullStatsText;
    public TextMeshProUGUI bananaStatsText;
    public TextMeshProUGUI cookieStatsText;
    public TextMeshProUGUI scoreText;

    public GameObject restartButton;
    public void StartGame(int difficulty)
    {
        scoreText.gameObject.SetActive(true);
        score = 0;
        isGameActive = true;
        UpdateScore(0);
        titleScreen.SetActive(false);
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().StartSpawning(difficulty);
        pizzaCount = 0;
        skullCount = 0;
        bananaCount = 0;
        cookieCount = 0;

    }

    void Update()
    {
   
        // Sadece oyun aktifse tuşlara basılabilsin
        if (isGameActive)
        {
            // A tuşuna basılırsa Obje1 etiketli objeyi ara ve yok et
            if (Input.GetKeyDown(KeyCode.A))
            {
                CheckAndDestroyObject("pizza", 5); // 5 puan versin
            }
            // S tuşuna basılırsa Obje2 etiketli objeyi ara ve yok et
            else if (Input.GetKeyDown(KeyCode.S))
            {
                CheckAndDestroyObject("skull", 5);
            }
            // D tuşuna basılırsa Obje3'ü bul
            else if (Input.GetKeyDown(KeyCode.D))
            {
                CheckAndDestroyObject("banana", 5); // Bu 10 puan versin
            }
            // F tuşuna basılırsa Obje4'ü bul
            else if (Input.GetKeyDown(KeyCode.F))
            {
                CheckAndDestroyObject("cookie", 5);
            }
        }
    }
    void CheckAndDestroyObject(string targetTag, int pointsToAdd)
    {
        // Sahnede o etikete sahip bir obje var mı diye bak (Sadece 1 tanesini bulur)
        GameObject objToDestroy = GameObject.FindWithTag(targetTag);

        // Eğer obje gerçekten sahnede varsa (boş/null değilse)
        if (objToDestroy != null)
        {   
            Destroy(objToDestroy); // Objeyi yok et
            UpdateScore(pointsToAdd); // Puanı ekle
            if (targetTag == "pizza") pizzaCount++;
            else if (targetTag == "skull") skullCount++;
            else if (targetTag == "banana") bananaCount++;
            else if (targetTag == "cookie") cookieCount++;
            // İstersen buraya patlama efekti (Particle) kodunu da ekleyebilirsin!
        }

        else
        {
            UpdateScore(-7); // 7 puan düş!
            LoseLife();
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void LoseLife()
    {
        lives--;
        if (lives == 2)
        {
            heart3.SetActive(false);
        }
        else if (lives == 1)
        {
            heart2.SetActive(false);
        }
        else if (lives <= 0)
        {
            heart1.SetActive(false);
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        statsText.gameObject.SetActive(true);

        pizzaStatsText.gameObject.SetActive(true);
        skullStatsText.gameObject.SetActive(true);
        bananaStatsText.gameObject.SetActive(true);
        cookieStatsText.gameObject.SetActive(true);


        pizzaStatsText.text = "Pizza (A): " + pizzaCount;
        skullStatsText.text = "Kuru Kafa (S): " + skullCount;
        bananaStatsText.text = "Muz (D): " + bananaCount;
        cookieStatsText.text = "Kurabiye (F): " + cookieCount;
        // Gizlediğimiz yazıyı ekranda görünür yap

        restartButton.SetActive(true); // Game over olunca butonu göster
    }
    public void RestartGame()
    {
        // Şu an aktif olan sahnenin adını al ve o sahneyi baştan yükle!
        // Bu işlem her şeyi (skoru, canları, Title Screen'i) ilk baştaki haline döndürür.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
