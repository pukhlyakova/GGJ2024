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
        this.allOptions.Sort();
        this.correctAnswer.Sort();
    }
}

public class GameManager : MonoBehaviour
{
    public Button translateBtn;
    public Button buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    public TMP_Text score;
    public TMP_Text emoji;

    public Sprite defualtButtonImg;

    public Sprite selectedButtonImg;

    private int correctAnswers = 0;
    private int seenQuestions = 0;

    private int index = 0;
    private List<Quote> quoteList;

    public List<Button> buttons;

    public List<bool> buttonsState;

    static Quote quote1 = new Quote("🌍🎮🎉", new List<string>{"Global", "Game", "Jam"}, new List<string>{"Planet", "Gamepad", "Candy"});
    static Quote quote2 = new Quote("🍔💤💻🔁", new List<string>{"Eat", "Sleep", "Code", "Repeat"}, new List<string>{"Berger", "Zzzzz", "Computer", "Arrows"});
    static Quote quote3 = new Quote("🔧💡🧠, ❌🔨", new List<string>{"Work", "smart", "not", "hard"}, new List<string>{"Brain", "Hammer", "Cross"});

    // Start is called before the first frame update
    void Start()
    {
        quoteList = new List<Quote>{quote1, quote2, quote3};
        UpdateScene();
    }

    public void TranslateClicked() {
        CheckTheAnswer();
        index = (index + 1) % quoteList.Count;
        UpdateScene();
    }

    public void CheckTheAnswer() {
        // Get the answer
        List<string> correctAnswer = new List<string>();
        for (int i = 0; i < buttonsState.Count; ++i) {
            if (buttonsState[i]) {
                correctAnswer.Add(buttons[i].GetComponentInChildren<TextMeshProUGUI>().text);
            }
        }
        correctAnswer.Sort();
        // Compare the Answer
        if (correctAnswer.Count != quoteList[index].correctAnswer.Count) {
            return;
        }

        for (int i = 0; i < correctAnswer.Count; ++i) {
            if (correctAnswer[i] != quoteList[index].correctAnswer[i]) {
                return;
            }
        }

        correctAnswers++;
    }

    public void SelectWord(int index) {
        if (buttonsState[index]) {
            buttonsState[index] = false;
            buttons[index].image.sprite = defualtButtonImg;
        } else {
            buttonsState[index] = true;
            buttons[index].image.sprite = selectedButtonImg;
        }
    }

    private void UpdateScene() {
        // Clear panel
        buttons.Clear();
        buttonsState.Clear();
        for (var i = panelToAttachButtonsTo.transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(panelToAttachButtonsTo.transform.GetChild(i).gameObject);
        }
        // Add new buttons
        for (int i = 0; i < quoteList[index].allOptions.Count; ++i) {
            var i1 = i;
            Button button = (Button)Instantiate(buttonPrefab);
            button.transform.SetParent(panelToAttachButtonsTo.transform);
            button.GetComponent<Button>().onClick.AddListener(() => SelectWord(i1));
            button.GetComponentInChildren<TextMeshProUGUI>().text = quoteList[index].allOptions[i];
            buttons.Add(button);
            buttonsState.Add(false);
        }
        emoji.text = quoteList[index].emoji;
        score.text = "Score: " + correctAnswers + "/" + seenQuestions;
        seenQuestions++;
    }
}
