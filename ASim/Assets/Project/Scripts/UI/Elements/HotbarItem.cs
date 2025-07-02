using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Hotbar üzerindeki bir öğeyi temsil eder.
/// Mouse ile hover, click gibi kullanıcı etkileşimlerini yönetir.
/// </summary>
public class HotbarItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Serialized Fields

    /// <summary>
    /// Hotbar öğesinin görselini gösteren Image bileşeni.
    /// </summary>
    [Tooltip("Hotbar öğesinin görseli")]
    public Image HotbarItemSelfImage;

    /// <summary>
    /// Hotbar öğesinin adını gösteren TextMeshProUGUI bileşeni.
    /// </summary>
    [Tooltip("Hotbar öğesinin adı")]
    public TextMeshProUGUI HotbarItemSelfName;

    /// <summary>
    /// Hotbar öğesinin Button bileşeni.
    /// </summary>
    [Tooltip("Hotbar öğesinin butonu")]
    public Button HotbarItemSelfButton;

    #endregion

    #region Properties

    /// <summary>
    /// Fare üzerindeyken true olur.
    /// </summary>
    public bool IsHovered { get; private set; }

    /// <summary>
    /// Butona tıklandığında true olur.
    /// </summary>
    public bool IsClicked { get; private set; }

    #endregion

    #region Events

    /// <summary>
    /// Fare Hotbar öğesinin üzerine geldiğinde tetiklenir.
    /// </summary>
    public event Action OnHovered;

    /// <summary>
    /// Fare Hotbar öğesinin üzerinden çıktığında tetiklenir.
    /// </summary>
    public event Action OnHoverExit;

    /// <summary>
    /// Hotbar öğesine tıklandığında tetiklenir.
    /// </summary>
    public event Action OnClicked;

    #endregion

    #region Event Handler Implementations

    public void OnPointerClick(PointerEventData eventData)
    {
        IsClicked = true;
        OnClicked?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovered = true;
        OnHovered?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovered = false;
        IsClicked = false;
        OnHoverExit?.Invoke();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Bu Hotbar öğesini, verilen başka bir Hotbar öğesinin görseli ve ismi ile günceller.
    /// </summary>
    /// <param name="hotbarItem">Kaynak Hotbar öğesi</param>
    public void SetHotbarItem(HotbarItem hotbarItem)
    {
        if (hotbarItem == null)
            throw new ArgumentNullException(nameof(hotbarItem));

        if (hotbarItem.HotbarItemSelfImage != null)
            HotbarItemSelfImage.sprite = hotbarItem.HotbarItemSelfImage.sprite;

        if (hotbarItem.HotbarItemSelfName != null)
            HotbarItemSelfName.text = hotbarItem.HotbarItemSelfName.text;
    }

    #endregion
}
