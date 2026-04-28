using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Economy")]
    public int points = 0;

    [Header("UI")]
    public TextMeshProUGUI pointsText;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        UpdateUI();
    }


    public void AddPoints(int amount)
    {
        points += amount;
        UpdateUI();
    }


    public bool SpendPoints(int amount)
    {
        if (points >= amount)
        {
            points -= amount;
            UpdateUI();
            return true;
        }

        return false;
    }


    void UpdateUI()
    {
        if (pointsText != null)
        {
            pointsText.text = "Points: " + points;
        }
    }
}