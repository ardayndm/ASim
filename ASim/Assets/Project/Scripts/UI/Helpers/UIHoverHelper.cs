using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI öğeleri için fareyle üzerine gelme (hover) olaylarını dinleyip dışa event olarak iletir.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class UIHoverHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Mouse bu objenin üzerine geldiğinde tetiklenir.
    /// </summary>
    public event Action OnHovered;

    /// <summary>
    /// Mouse bu objeden çıktığında tetiklenir.
    /// </summary>
    public event Action OnHoverExit;

    /// <summary>
    /// Unity'nin sistem çağrısı. Mouse objenin üzerine geldiğinde çağrılır.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHovered?.Invoke();
    }

    /// <summary>
    /// Unity'nin sistem çağrısı. Mouse obje üzerinden çıktığında çağrılır.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit?.Invoke();
    }
}
