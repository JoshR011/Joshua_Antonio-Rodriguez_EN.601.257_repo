using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Life targetLife;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (targetLife != null)
            image.fillAmount = targetLife.amount / 100;
        else
            image.fillAmount = 0;
    }
}
