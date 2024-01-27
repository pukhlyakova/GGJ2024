using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quote {
    public string emoji;
    public List<string> correctAnswer;
    public List<string> allOptions;

    public Quote(string emoji, List<string> correctAnswer, List<string> otherOptions) {
        this.emoji = emoji;
        this.correctAnswer = correctAnswer;
        this.allOptions = new List<string>();
        this.allOptions.AddRange(correctAnswer);
        this.allOptions.AddRange(otherOptions);
    }
}

public class GameManager : MonoBehaviour
{
    public Button translateBtn;
    public TMP_Text score;
    public TMP_Text emoji;

    private int correctAnswers = 0;
    private int seenQuestions = 0;

    private int index = 0;
    private List<Quote> quoteList;

    static Quote quote1 = new Quote("Global Game Jam", new List<string>{"Global", "Game", "Jam"}, new List<string>{"Planet", "Gamepad", "Candy"});
    static Quote quote2 = new Quote("Eat, Sleep, Code, Repeat", new List<string>{"Eat", "Sleep", "Code", "Repeat"}, new List<string>{"Berger", "Zzzzz", "Computer", "Arrows"});
    static Quote quote3 = new Quote("Work smart, not hard", new List<string>{"Work", "smart", "not", "hard"}, new List<string>{"Brain", "Hammer", "Cross"});

    // Start is called before the first frame update
    void Start()
    {
        quoteList = new List<Quote>{quote1, quote2, quote3};
        seenQuestions++;
        updateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        // Do nothing
    }

    public void TranslateClicked() {
        correctAnswers++;
        seenQuestions++;
        index = (index + 1) % quoteList.Count;
        updateScoreText();
    }

    private void updateScoreText() {
        score.text = "Score: " + correctAnswers + "/" + seenQuestions;
    }
}
