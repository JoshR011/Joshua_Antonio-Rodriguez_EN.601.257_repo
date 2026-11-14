using UnityEngine;
using TMPro;

public class WavesUI : MonoBehaviour
{
    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Start()
    {
        WavesManager.instance.onChanged.AddListener(RefreshText);
        RefreshText();
    }

    void RefreshText()
    {
        text.text = "Waves: " + WavesManager.instance.waves.Count;
    }
}
