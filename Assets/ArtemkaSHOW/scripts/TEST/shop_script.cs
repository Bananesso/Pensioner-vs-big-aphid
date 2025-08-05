using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public int id;
        public GameObject itemPrefab;
        public Transform spawnPoint;
        public TextMeshPro priceTextObject;
        public int minPrice = 10;
        public int maxPrice = 100;
        [TextArea(3, 5)] public string itemDescription;
        public Texture2D itemImage;
        [HideInInspector] public bool isSold = false;
        [HideInInspector] public int currentPrice;
    }

    [System.Serializable]
    public class ShopSchedule
    {
        public float openDuration = 20f;    // Время открытия (сек)
        public float closedDuration = 200f; // Время закрытия (сек)
    }

    [System.Serializable]
    public class ItemTooltip
    {
        public GameObject tooltipPanel;
        public RawImage itemImage;
        public TextMeshProUGUI descriptionText;
        public float showDelay = 0.5f;
        [HideInInspector] public Coroutine showCoroutine;
    }

    [Header("Настройки магазина")]
    public List<ShopItem> shopItems = new List<ShopItem>();
    public int playerMoney = 300;
    public float moneyChangeDuration = 1f;
    public ShopSchedule shopSchedule;

    [Header("Элементы интерфейса")]
    public TextMeshProUGUI moneyTextUI;
    public TextMeshProUGUI shopStatusText;
    public ItemTooltip itemTooltip;

    [Header("Двери")]
    public GameObject openDoor;
    public GameObject closedDoor;

    [Header("Звуки")]
    public AudioClip buySound;
    public AudioClip errorSound;
    public AudioClip openSound;
    public AudioClip closeSound;

    private AudioSource audioSource;
    private Coroutine moneyChangeCoroutine;
    private bool isShopOpen = false;
    private Coroutine shopTimerCoroutine;
    private ShopItem currentHoveredItem;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Начальное состояние
        CloseShop();
        UpdateMoneyDisplay();

        // Запускаем цикл работы магазина
        StartCoroutine(ShopCycle());
    }

    IEnumerator ShopCycle()
    {
        while (true)
        {
            // Магазин закрыт
            CloseShop();
            yield return new WaitForSeconds(shopSchedule.closedDuration);

            // Магазин открывается
            OpenShop();
            yield return new WaitForSeconds(shopSchedule.openDuration);
        }
    }

    void OpenShop()
    {
        if (isShopOpen) return;

        isShopOpen = true;
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
        audioSource.PlayOneShot(openSound);

        shopStatusText.text = "Магазин открыт!";
        shopStatusText.color = Color.green;

        InitializeShop();
        UpdateMoneyDisplay();

        Invoke("HideStatusText", 2f);
    }

    void CloseShop()
    {
        if (!isShopOpen) return;

        isShopOpen = false;
        openDoor.SetActive(false);
        closedDoor.SetActive(true);
        audioSource.PlayOneShot(closeSound);

        shopStatusText.text = "Магазин закрыт";
        shopStatusText.color = Color.red;

        ClearShop();
        HideTooltip();

        Invoke("HideStatusText", 2f);
    }

    void HideStatusText()
    {
        shopStatusText.text = "";
    }

    void InitializeShop()
    {
        foreach (var item in shopItems)
        {
            if (!item.isSold && Random.Range(0, 2) == 1)
            {
                SpawnItem(item);
            }
        }
    }

    void ClearShop()
    {
        foreach (var item in shopItems)
        {
            if (item.spawnPoint.childCount > 0)
            {
                Destroy(item.spawnPoint.GetChild(0).gameObject);
            }
        }
    }

    void SpawnItem(ShopItem item)
    {
        item.isSold = false;
        item.currentPrice = Random.Range(item.minPrice, item.maxPrice + 1);
        item.priceTextObject.text = item.currentPrice.ToString();

        if (item.itemPrefab != null && item.spawnPoint != null)
        {
            GameObject spawnedItem = Instantiate(item.itemPrefab, item.spawnPoint.position, item.spawnPoint.rotation);
            spawnedItem.transform.SetParent(item.spawnPoint);

            // Добавляем обработчики для подсказки
            var collider = spawnedItem.GetComponent<Collider>() ?? spawnedItem.AddComponent<BoxCollider>();
            var hoverHandler = spawnedItem.GetComponent<ItemHoverHandler>() ?? spawnedItem.AddComponent<ItemHoverHandler>();
            hoverHandler.Initialize(this, item.id);

            // Обработчик клика
            var clickHandler = spawnedItem.GetComponent<ItemClickHandler>() ?? spawnedItem.AddComponent<ItemClickHandler>();
            clickHandler.Initialize(this, item.id);
        }
    }

    public void OnItemHoverStart(int itemId)
    {
        if (!isShopOpen) return;

        currentHoveredItem = shopItems.Find(x => x.id == itemId);
        if (currentHoveredItem == null || currentHoveredItem.isSold) return;

        if (itemTooltip.showCoroutine != null)
            StopCoroutine(itemTooltip.showCoroutine);

        itemTooltip.showCoroutine = StartCoroutine(ShowTooltipDelayed());
    }

    public void OnItemHoverEnd()
    {
        if (itemTooltip.showCoroutine != null)
            StopCoroutine(itemTooltip.showCoroutine);

        HideTooltip();
        currentHoveredItem = null;
    }

    IEnumerator ShowTooltipDelayed()
    {
        yield return new WaitForSeconds(itemTooltip.showDelay);

        if (currentHoveredItem != null)
        {
            itemTooltip.itemImage.texture = currentHoveredItem.itemImage;
            itemTooltip.descriptionText.text = currentHoveredItem.itemDescription;
            itemTooltip.tooltipPanel.SetActive(true);
        }
    }

    void HideTooltip()
    {
        if (itemTooltip.tooltipPanel != null)
            itemTooltip.tooltipPanel.SetActive(false);
    }

    public void OnItemClicked(int itemId)
    {
        if (!isShopOpen) return;

        ShopItem item = shopItems.Find(x => x.id == itemId);
        if (item == null || item.isSold) return;

        if (playerMoney >= item.currentPrice)
        {
            if (moneyChangeCoroutine != null)
                StopCoroutine(moneyChangeCoroutine);

            moneyChangeCoroutine = StartCoroutine(ChangeMoneyAmount(playerMoney, playerMoney - item.currentPrice));

            audioSource.PlayOneShot(buySound);
            item.isSold = true;
            item.priceTextObject.text = "SOLD";

            if (item.spawnPoint.childCount > 0)
                Destroy(item.spawnPoint.GetChild(0).gameObject);

            HideTooltip();
        }
        else
        {
            audioSource.PlayOneShot(errorSound);
        }
    }

    IEnumerator ChangeMoneyAmount(int startAmount, int endAmount)
    {
        float elapsed = 0f;

        while (elapsed < moneyChangeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moneyChangeDuration);
            int currentAmount = (int)Mathf.Lerp(startAmount, endAmount, t);
            playerMoney = currentAmount;
            moneyTextUI.text = currentAmount.ToString();
            yield return null;
        }

        playerMoney = endAmount;
        moneyTextUI.text = endAmount.ToString();
        moneyChangeCoroutine = null;
    }

    void UpdateMoneyDisplay()
    {
        moneyTextUI.text = playerMoney.ToString();
    }
}

// Обработчик наведения на предмет
public class ItemHoverHandler : MonoBehaviour
{
    private ShopManager shopManager;
    private int itemId;

    public void Initialize(ShopManager manager, int id)
    {
        shopManager = manager;
        itemId = id;
    }

    void OnMouseEnter()
    {
        shopManager?.OnItemHoverStart(itemId);
    }

    void OnMouseExit()
    {
        shopManager?.OnItemHoverEnd();
    }
}

// Обработчик клика на предмет
public class ItemClickHandler : MonoBehaviour
{
    private ShopManager shopManager;
    private int itemId;

    public void Initialize(ShopManager manager, int id)
    {
        shopManager = manager;
        itemId = id;
    }

    void OnMouseDown()
    {
        shopManager?.OnItemClicked(itemId);
    }
}