using UnityEngine;
using UnityEngine.UI;
using YG;

public class Translator : MonoBehaviour
{
    private Text text;
    private string lang;

    [SerializeField] private string EnText;
    [SerializeField] private string RuText;

    private void Start()
    {
        text = GetComponent<Text>();
        lang = YandexGame.savesData.language;

        if (lang == "en") 
        {
            text.text = EnText;
        }
        if (lang == "ru")
        {
            text.text = RuText;
        }
    }
}