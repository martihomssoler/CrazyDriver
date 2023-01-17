using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TWCore.Utils;

public class UI_HighscoreTable : MonoBehaviour
{
    public bool sortHighToLow = true;
    public bool cleanPreviousHighscoresOnLoad = false;

    private TMP_Text titleText;
    private RectTransform entryContainer;
    private RectTransform entryTemplate;

    private Highscores highscores;
    private List<Transform> transformList;

    private bool initialized = false;

    private void Awake()
    {
        if (initialized) return;
        Initialize();
        Hide();
    }

    private void Initialize()
    {
        titleText = transform.Find("TitleText").GetComponent<TMP_Text>();
        entryContainer = transform.Find("HighscoreEntryContainer").GetComponent<RectTransform>();
        entryTemplate = entryContainer.Find("HighscoreEntryTemplate").GetComponent<RectTransform>();

        entryTemplate.gameObject.SetActive(false);

        if (cleanPreviousHighscoresOnLoad) PlayerPrefs.DeleteAll();
        initialized = true;
    }

    private void SortHighscoreList()
    {
        if (sortHighToLow)
            highscores.list.Sort((HighscoreEntry a, HighscoreEntry b) => b.score.CompareTo(a.score));
        else
            highscores.list.Sort((HighscoreEntry a, HighscoreEntry b) => a.score.CompareTo(b.score));
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        var entryRect = Instantiate(entryTemplate, container);
        entryRect.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryRect.gameObject.SetActive(true);

        var rank = transformList.Count + 1;
        var positionText = entryRect.Find("PositionText").GetComponent<TMP_Text>();
        positionText.text = CodeUtils.NumberToOrdinal(rank);
        var scoreText = entryRect.Find("ScoreText").GetComponent<TMP_Text>();
        scoreText.text = highscoreEntry.score.ToString();
        var nameText = entryRect.Find("NameText").GetComponent<TMP_Text>();
        nameText.text = highscoreEntry.name;
        entryRect.Find("EntryBackground").GetComponent<Image>().color = CodeUtils.GetColorFromString((rank % 2) == 1 ? "6C6C6C" : "202020");

        if (rank == 1)
        {
            positionText.color = UnityEngine.Color.green; positionText.fontStyle = FontStyles.Bold;
            scoreText.color = UnityEngine.Color.green; scoreText.fontStyle = FontStyles.Bold;
            nameText.color = UnityEngine.Color.green; nameText.fontStyle = FontStyles.Bold;
        }
        
        transformList.Add(entryRect);
    }

    public void Show(string title)
    {
        if (!initialized)
            Initialize();

        gameObject.SetActive(true);
        titleText.text = title;

        var jsonString = PlayerPrefs.GetString("HighscoreTable");
        highscores = JsonUtility.FromJson<Highscores>(jsonString);
        if (highscores == null) return;

        SortHighscoreList();
        transformList = new List<Transform>();

        for (int i = 0; i < highscores.list.Count; i++)
        {
            if ((i + 1) * entryTemplate.rect.height >= entryContainer.rect.height) return;

            CreateHighscoreEntryTransform(highscores.list[i], entryContainer, transformList);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void AddHighscoreEntry(int score, string name)
    {
        if (!initialized)
            Initialize();

        var entry = new HighscoreEntry { score = score, name = name };

        var jsonString = PlayerPrefs.GetString("HighscoreTable");

        if (string.IsNullOrEmpty(jsonString)) highscores = new Highscores();
        else highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.list.Add(entry);

        var json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("HighscoreTable", json);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    private record Highscores
    {
        public List<HighscoreEntry> list = new List<HighscoreEntry>();
    }

    [System.Serializable]
    private record HighscoreEntry
    {
        public int score;
        public string name;
    }
}
