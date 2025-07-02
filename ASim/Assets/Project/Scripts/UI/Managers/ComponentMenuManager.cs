using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Bileşen menüsünü yönetir: arama, seçim ve kapatma işlemleri.
/// </summary>
public class ComponentMenuManager : MonoBehaviour
{
    [Header("UI - Arama")]
    [SerializeField] private TMP_InputField searchInputField;
    [SerializeField] private GameObject itemsContentObject;

    [Header("UI - Görseller")]
    [SerializeField] private Image mainComponentImage;
    [SerializeField] private Image secondComponentImage;
    [SerializeField] private Image thirdComponentImage;
    [SerializeField] private TextMeshProUGUI componentNameText;

    [Header("UI - Scroll Alanları")]
    [SerializeField] private GameObject componentInfoScrollView;
    [SerializeField] private GameObject componentAttributesScrollView;
    [SerializeField] private GameObject componentInfoContent;
    [SerializeField] private GameObject componentAttributesContent;

    [Header("UI - Butonlar")]
    [SerializeField] private Button selectButton;
    [SerializeField] private Button closeMenuButton;

    [Header("Hover Kontrol")]
    [SerializeField] private UIHoverHelper menuHoverHelper;

    public static List<MenuItem> MenuItems { get; private set; } = new();
    public static ComponentMenuManager Instance { get; private set; }

    private void Awake() => Instance = this;

    private void Start()
    {
        menuHoverHelper.OnHovered += () => CameraManager.LockCamera(gameObject, this);
        menuHoverHelper.OnHoverExit += () => CameraManager.UnlockCamera(gameObject, this);
        closeMenuButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        CameraManager.UnlockCamera(gameObject, this);
    }

    /// <summary>
    /// Arama metnine göre menüdeki bileşenleri filtreler.
    /// </summary>
    /// <param name="query">Arama metni</param>
    public void SearchItem(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            SetAllItemsActive(true);
            return;
        }

        string lowerQuery = query.ToLower();
        foreach (var item in MenuItems)
        {
            bool match = item.MenuItemSelfScriptableObject.Name.ToLower().Contains(lowerQuery);
            item.gameObject.SetActive(match);
        }
    }

    private void SetAllItemsActive(bool isActive)
    {
        foreach (var item in MenuItems)
        {
            item.gameObject.SetActive(isActive);
        }
    }
}
