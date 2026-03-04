using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score;
    public bool isGameActive;
    public TextMeshProUGUI scoreText;
    public GameObject titleScreen;

    public int lives = 3;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public void StartGame(int difficulty)
    {
        
        score = 0;
        isGameActive = true;
        UpdateScore(0);
        titleScreen.SetActive(false);
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().StartSpawning();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Şartsız şurtsuz A tuşuna basıldı!");
        }
        // Sadece oyun aktifse tuşlara basılabilsin
        if (isGameActive)
        {
            // A tuşuna basılırsa Obje1 etiketli objeyi ara ve yok et
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A tuşuna basmayı algıladım!");
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
        titleScreen.SetActive(true);
    }
}
