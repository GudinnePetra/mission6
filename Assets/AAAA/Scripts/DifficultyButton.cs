using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Butonları kullanabilmek için bu şart!

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManagerX;

    // Inspector'dan her buton için farklı bir zorluk derecesi gireceğiz (Örn: Easy=1, Medium=2)
    public int difficulty;

    void Start()
    {
        // Butonun kendisini ve Game Manager'ı bul
        button = GetComponent<Button>();
        gameManagerX = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Unity'ye diyoruz ki: "Biri bu butona tıklarsa, aşağıdaki SetDifficulty fonksiyonunu çalıştır!"
        button.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        // GameManager içindeki StartGame fonksiyonunu çalıştır ve zorluk derecesini gönder
        gameManagerX.StartGame(difficulty);
    }
}