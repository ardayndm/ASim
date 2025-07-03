using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public static HotbarManager Instance { get; private set; }

    public Image HotbarImage;
    public Transform HotbarItemListContent;
    public Button HotbarClearButton;
    public Sprite HotbarImageDefaultSprite;
    public GameObject HotbarItemPrefab;
    public UIHoverHelper HotbarPanelHoverHelper;
    public List<HotbarItem> HotbarItems { get; private set; } = new();

    private void Awake() => Instance = this;

    void Start()
    {
        HotbarPanelHoverHelper.OnHovered += () =>
        {
            // Hotbar panelinin üzerine mouse gelince kamera için kameraya kilit ekle.
            CameraManager.LockCamera(HotbarPanelHoverHelper.gameObject, this);
        };

        HotbarPanelHoverHelper.OnHoverExit += () =>
        {
            // Hotbar panelinin üzerinden mouse çıkınca kamera için kameradan kilidi kaldır.
            CameraManager.UnlockCamera(HotbarPanelHoverHelper.gameObject, this);
        };
    }

    // Hotbar item resmini günceller
    public void SetHotbarImage(Sprite itemSprite)
    {
        if (itemSprite == null)
            itemSprite = HotbarImageDefaultSprite;

        HotbarImage.sprite = itemSprite;
    }

    // Hotbar'daki itemleri temizler
    public void ClearHotbarItems()
    {
        for (int i = HotbarItems.Count - 1; i >= 0; i--)
        {
            RemoveHotbarItem(HotbarItems[i]);
        }
    }

    // Hotbar'a item ekler
    public void AddHotbarItem(HotbarItem item)
    {
        if (item == null || HotbarItems.Contains(item)) return;

        GameObject go = Instantiate(HotbarItemPrefab, HotbarItemListContent, false);
        HotbarItem goItem = go.GetComponent<HotbarItem>();
        goItem.SetItem(item);

        goItem.OnHover += OnHotbarItemHovered;
        goItem.OnHoverExit += OnHotbarItemHoverExit;
        HotbarItems.Add(goItem);
    }

    // Hotbar'dan item siler
    public void RemoveHotbarItem(HotbarItem item)
    {
        if (item == null || !HotbarItems.Contains(item)) return;

        HotbarItems.Remove(item);
        Destroy(item.gameObject);
    }

    private void OnHotbarItemHovered(HotbarItem hoveredItem)
    {
        // Hotbarda olan itemin üzerine mouse geldi.
    }

    private void OnHotbarItemHoverExit(HotbarItem hoveredItem)
    {
        // Hotbarda olan itemin üzerinden mouse çıktı.
    }
}