using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hotbar panelini yönetir.
/// </summary>
public class HotbarManager : MonoBehaviour
{
    [Header("UI")]
    public Image HoveredItemImage;
    public Sprite DefaultHoveredItemSprite;
    public GameObject HotbarItemsContentObject;
    public Button ClearHotbarButton;
    public GameObject HotbarItemPrefab;
    public WarnPanelManager WarnPanelManager;
    public UIHoverHelper HotbarPanelHoverHelper;

    /// <summary>Aktif hotbar item listesi.</summary>
    public List<HotbarItem> HotbarItems { get; private set; } = new();

    private void Start()
    {
        SimulationManager.OnSimStateChanged += OnSimulationStateChanged;
        ClearHotbarButton.onClick.AddListener(OnClearButtonClick);
        HotbarPanelHoverHelper.OnHovered += () => CameraManager.LockCamera(gameObject, this);
        HotbarPanelHoverHelper.OnHoverExit += () => CameraManager.UnlockCamera(gameObject, this);
    }

    private void OnClearButtonClick()
    {
        WarnPanelManager.gameObject.SetActive(true);
        WarnPanelManager.SetWarnText("Hotbar temizlenecektir. Devam etmek istiyor musunuz?");
        WarnPanelManager.OkButtonClicked += () =>
        {
            ClearHotbar();
            WarnPanelManager.gameObject.SetActive(false);
        };
        WarnPanelManager.CancelButtonClicked += () => WarnPanelManager.gameObject.SetActive(false);
    }

    private void OnSimulationStateChanged(SimulationState state)
    {
        bool isRunning = state == SimulationState.Running;

        ClearHotbarButton.interactable = !isRunning;
        SetActiveHotbarItems(!isRunning);
        if (isRunning) ClearHoveredItemImage();
    }

    /// <summary>
    /// Hotbar itemlerinin aktifliğini ayarlar.
    /// </summary>
    /// <param name="isActive">Aktiflik durumu</param>
    private void SetActiveHotbarItems(bool isActive)
    {
        foreach (var item in HotbarItems)
        {
            item.HotbarItemSelfButton.interactable = isActive;
        }
    }

    /// <summary>Hotbar’ı temizler.</summary>
    private void ClearHotbar()
    {
        for (int i = HotbarItems.Count - 1; i >= 0; i--)
            Destroy(HotbarItems[i].gameObject);

        HotbarItems.Clear();
        ClearHoveredItemImage();
    }

    /// <summary>Yeni item ekler.</summary>
    public void AddHotbarItem(HotbarItem item)
    {
        GameObject obj = Instantiate(HotbarItemPrefab, HotbarItemsContentObject.transform);
        HotbarItem newItem = obj.GetComponent<HotbarItem>();

        newItem.SetHotbarItem(item);
        newItem.OnHovered += () => HoverHotbarItemImage(newItem);
        newItem.OnHoverExit += ClearHoveredItemImage;

        HotbarItems.Add(newItem);
    }

    /// <summary>Item’i siler.</summary>
    public void RemoveHotbarItem(HotbarItem item)
    {
        Destroy(item.gameObject);
        HotbarItems.Remove(item);
        ClearHoveredItemImage();
    }

    /// <summary>Hover görselini günceller.</summary>
    private void HoverHotbarItemImage(HotbarItem item)
    {
        if (SimulationManager.SimState == SimulationState.Running) return;
        HoveredItemImage.sprite = item.HotbarItemSelfImage.sprite;
    }

    /// <summary>Hover görselini sıfırlar.</summary>
    private void ClearHoveredItemImage()
    {
        HoveredItemImage.sprite = DefaultHoveredItemSprite;
    }
}
