using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player main;

    [SerializeField] public int health = 500;
    public int money = 200;

    [SerializeField] private TextMeshProUGUI HPGui;
    [SerializeField] private TextMeshProUGUI MoneyGUI;

    void Awake()
    {
        main = this;
    }

    void Update()
    {
        HPGui.text = "HP: " + health.ToString();
        MoneyGUI.text = "Money: " + money.ToString();
    }
}