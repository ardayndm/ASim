using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hotbar panelindeki itemların yönetimini sağlar.
/// Ekleme, çıkarma, hover görüntüleme ve temizleme işlevleri içerir.
/// </summary>
public class HotbarPanelManager : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("Hover durumunda gösterilen büyük item görseli")]
    public Image HoveredItemImage;

    [Tooltip("Varsayılan hover görseli")]
    public Sprite DefaultHoveredItemSprite;

    [Tooltip("Hotbar itemlerinin ebeveyn objesi (Content)")]
    public GameObject HotbarItemsContentObject;

    [Tooltip("Hotbar temizleme butonu")]
    public Button ClearHotbarButton;

    [Tooltip("Hotbar item prefabı")]
    public GameObject HotbarItemPrefab;

    [Tooltip("Tüm hotbar alanın hover helperi")]
    public UIHoverHelper HotbarPanelHoverHelper;

    /// <summary>
    /// Hotbarda aktif olan itemların listesi
    /// </summary>
    public List<HotbarItem> HotbarItems { get; private set; } = new List<HotbarItem>();

    private void Start()
    {
        ClearHotbarButton.onClick.AddListener(ClearHotbarItemsOnClick);
        HotbarPanelHoverHelper.OnHovered += HotbarPanelOnHovered;
        HotbarPanelHoverHelper.OnHoverExit += HotbarPanelOnHoverExit;
    }

    private void HotbarPanelOnHoverExit()
    {
        CameraMoveManager.UnlockCamera(gameObject, this);
    }

    private void HotbarPanelOnHovered()
    {
        CameraMoveManager.LockCamera(gameObject, this);
    }

    /// <summary>
    /// Hotbarda bulunan tüm itemları siler.
    /// </summary>
    private void ClearHotbarItemsOnClick()
    {
        // Tüm item objelerini yok et
        for (int i = HotbarItems.Count - 1; i >= 0; i--)
        {
            HotbarItem item = HotbarItems[i];
            if (item != null && item.gameObject != null)
            {
                Destroy(item.gameObject);
            }
        }
        HotbarItems.Clear();

        // Hover görselini varsayılan yap
        ClearHoveredItemImage();
    }

    /// <summary>
    /// Hover edilen hotbar iteminin görselini büyük göstergeye yansıtır.
    /// </summary>
    /// <param name="hotbarItem">Hover edilen item</param>
    private void HoverHotbarItemImage(HotbarItem hotbarItem)
    {
        if (HoveredItemImage != null && hotbarItem?.HotbarItemSelfImage != null)
        {
            HoveredItemImage.sprite = hotbarItem.HotbarItemSelfImage.sprite;
        }
    }

    /// <summary>
    /// Hover görüntüsünü varsayılan görsele döndürür.
    /// </summary>
    private void ClearHoveredItemImage()
    {
        if (HoveredItemImage != null)
            HoveredItemImage.sprite = DefaultHoveredItemSprite;
    }

    /// <summary>
    /// Hotbara yeni bir item ekler.
    /// </summary>
    /// <param name="hotbarItem">Eklenecek item referansı</param>
    public void AddHotbarItem(HotbarItem hotbarItem)
    {
        if (hotbarItem == null || HotbarItems.Contains(hotbarItem))
            return;

        if (HotbarItemPrefab == null || HotbarItemsContentObject == null)
        {
            Debug.LogError("HotbarItemPrefab veya HotbarItemsContentObject atanmadı!");
            return;
        }

        GameObject itemGO = Instantiate(HotbarItemPrefab, HotbarItemsContentObject.transform, false);
        HotbarItem newHotbarItem = itemGO.GetComponent<HotbarItem>();
        if (newHotbarItem == null)
        {
            Debug.LogError("HotbarItemPrefab üzerinde HotbarItem component bulunamadı!");
            Destroy(itemGO);
            return;
        }

        newHotbarItem.SetHotbarItem(hotbarItem);

        // Hover eventlerine abone ol
        newHotbarItem.OnHovered += () => HoverHotbarItemImage(newHotbarItem);
        newHotbarItem.OnHoverExit += ClearHoveredItemImage;

        HotbarItems.Add(newHotbarItem);
    }

    /// <summary>
    /// Hotbardan bir item siler.
    /// </summary>
    /// <param name="hotbarItem">Silinecek item</param>
    public void RemoveHotbarItem(HotbarItem hotbarItem)
    {
        if (hotbarItem == null)
            return;

        if (!HotbarItems.Contains(hotbarItem))
            return;

        if (hotbarItem.gameObject != null)
            Destroy(hotbarItem.gameObject);

        HotbarItems.Remove(hotbarItem);

        // Eğer hover edilen item silindiyse hover görselini temizle
        ClearHoveredItemImage();
    }
}
