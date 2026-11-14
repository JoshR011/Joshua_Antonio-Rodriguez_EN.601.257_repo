using UnityEngine;
using TMPro;

public class EnemiesUI : MonoBehaviour
{
    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Start()
    {
        EnemyManager.instance.onChanged.AddListener(RefreshText);
        RefreshText();
    }

    void RefreshText()
    {
        text.text = "Enemies: " + EnemyManager.instance.enemies.Count;
    }
}
