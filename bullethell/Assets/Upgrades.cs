using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public PlayerController player;
    public shooting shootingScript;

    [Header("Costs")]
    public int extraLifeCost = 5;
    public int multiBarrelCost = 10;

    private bool hasMultiBarrel = false;
    [Header("Barrel Setup")]
    public Transform weaponPivot;
    public Transform firstBarrel;
    public GameObject barrelPrefab;

    public float spacing = 0.5f;
    public float gap = 0.1f;

    private Vector3 originalBarrelPos;
    private GameObject secondBarrel;

    void Start()
    {
        if (firstBarrel != null)
        {
            originalBarrelPos = firstBarrel.localPosition;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BuyExtraLife();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BuyMultiBarrel();
        }
    }

    // ❤️ Extra Life Upgrade
    public void BuyExtraLife()
    {
        Debug.Log("Attempting to buy life");

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager is NULL");
            return;
        }

        if (GameManager.instance.SpendPoints(extraLifeCost))
        {
            Debug.Log("Extra life purchased");
            player.AddLife(1);
        }
        else
        {
            Debug.Log("Not enough points");
        }
    }

    public void BuyMultiBarrel()
    {
        if (hasMultiBarrel)
        {
            Debug.Log("Already unlocked multi-barrel");
            return;
        }

        if (!GameManager.instance.SpendPoints(multiBarrelCost))
        {
            Debug.Log("Not enough points");
            return;
        }

        Debug.Log("Multi-barrel purchased");

        hasMultiBarrel = true;
        shootingScript.barrelCount = 2;

        // Spawn second barrel visually
        GameObject secondBarrel = Instantiate(barrelPrefab, weaponPivot);

        float totalSpacing = spacing + gap;

        // Reposition BOTH barrels so they align correctly
        firstBarrel.localPosition =
            originalBarrelPos + new Vector3(0f, -totalSpacing / 2f, 0f);

        secondBarrel.transform.localPosition =
            originalBarrelPos + new Vector3(0f, totalSpacing / 2f, 0f);

        secondBarrel.transform.localRotation = firstBarrel.localRotation;
    }
}