using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CSVTextLoader : MonoBehaviour
{
    public TextMeshProUGUI textControls;
    public TextMeshProUGUI textWinner;
    public TextMeshProUGUI textLoser;
    public TextMeshProUGUI textTitles;
    public TextMeshProUGUI textTitleMenu;
    public TextMeshProUGUI textTitleSettings;
    public TextMeshProUGUI textTitleWin;
    public TextMeshProUGUI textTitleLose;
    public TextMeshProUGUI textTitleControls;
    public TextMeshProUGUI textTitleTitles;
    public TextMeshProUGUI textButtonPlay;
    public TextMeshProUGUI textButtonPlayAgain;
    public TextMeshProUGUI textButtonMainMenu;
    public TextMeshProUGUI textButtonResume;
    public TextMeshProUGUI textButtonSettings;
    public TextMeshProUGUI textButtonControls;
    public TextMeshProUGUI textButtonNinles;
    private Dictionary<string, string> texts = new Dictionary<string, string>();
    string idToCheck;

    void Start()
    {
        LoadCSV();

        DecriptionWinner();
        DescriptionLoser();
        DescriptionControls();
        DecriptionTitles();

        TitleControls();
        TitleLose();
        TitleMenu();
        TitleSettings();
        TitleTitles();
        TitleWin();
        
    }

    void LoadCSV()
    {
        string TextConfigFile = "TextConfig";
        TextAsset csvFile = Resources.Load<TextAsset>(TextConfigFile); // Загрузка CSV из папки Resources
       
        if (csvFile != null)
        {
            string[] rows = csvFile.text.Split('\n');
            foreach (string row in rows)
            {
                string[] columns = row.Split(new[] { ',' }, 2); // Разделяем на два элемента — ID и Text, даже если в тексте есть запятые или переносы строк
                if (columns.Length >= 2)
                {
                    string id = columns[0].Trim();
                    string text = columns[1].Trim().Trim('"').Replace("\\n", "\n"); // Убираем кавычки и обрабатываем символы новой строки
                    texts[id] = text;
                }
            }
        }
        else
        {
            Debug.LogError($"файл {TextConfigFile} не найден");
        }
        
    }
    
    void TitleWin()
    {
     idToCheck = "окноПобеда";
    if (texts.ContainsKey(idToCheck))
        {
            textTitleWin.text = texts[idToCheck];
        }
        else
        {
            Debug.LogError($"id {idToCheck} not found");
        }
    }
    void TitleLose()
    {
     idToCheck = "окноПоражение";
    if (texts.ContainsKey(idToCheck))
        {
            textTitleLose.text = texts[idToCheck];
        }
        else
        {
            Debug.LogError($"id {idToCheck} not found");
        }
    }
    void TitleMenu()
    {
     idToCheck = "окноМеню";
    if (texts.ContainsKey(idToCheck))
        {
            textTitleMenu.text = texts[idToCheck];
        }
        else
        {
            Debug.LogError($"id {idToCheck} not found");
        }
    }
    void TitleSettings()
    {
     idToCheck = "окноНастройки";
    if (texts.ContainsKey(idToCheck))
        {
            textTitleSettings.text = texts[idToCheck];
        }
        else
        {
            Debug.LogError($"id {idToCheck} not found");
        }
    }
    void TitleControls()
    {
     idToCheck = "окноУправление";
    if (texts.ContainsKey(idToCheck))
        {
            textTitleControls.text = texts[idToCheck];
        }
        else
        {
            Debug.LogError($"id {idToCheck} not found");
        }
    }
    void TitleTitles()
    {
     idToCheck = "окноТитры";
    if (texts.ContainsKey(idToCheck))
        {
            textTitleTitles.text = texts[idToCheck];
        }
        else
        {
            Debug.LogError($"id {idToCheck} not found");
        }
    }
     void DecriptionTitles()
    {
     idToCheck = "титры";
    if (texts.ContainsKey(idToCheck))
        {
            textTitles.text = texts[idToCheck];
        }
        else
        {
            Debug.LogError($"id {idToCheck} not found");
        }
    }
    void DecriptionWinner()
    {
     idToCheck = "победа";
    if (texts.ContainsKey(idToCheck))
        {
            textWinner.text = texts[idToCheck];
        }
        else
        {
            Debug.LogError($"id {idToCheck} not found");
        }
    }
    void DescriptionLoser()
    {
     idToCheck = "поражение";
    if (texts.ContainsKey(idToCheck))
        {
            textLoser.text = texts[idToCheck];
        }
        else
        {
            Debug.LogError($"id {idToCheck} not found");
        }
    }
    void DescriptionControls()
    {
        idToCheck = "управление";
    if (texts.ContainsKey(idToCheck))
        {
            textControls.text = texts[idToCheck];
        }
        else
        {
            Debug.LogError($"id {idToCheck} not found");
        }
    }
}
