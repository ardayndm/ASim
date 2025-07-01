using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Bileşen menüsünün yönetimini sağlar.
/// Arama, gösterim ve menü açma/kapatma işlemlerini kontrol eder.
/// </summary>
public class ComponentMenuManager : MonoBehaviour
{
    public TMP_InputField SearchInputField;
    public GameObject ItemsContentObject;

    public Image MainComponentImage;
    public Image SecondComponentImage;
    public Image ThirdComponentImage;

    public TextMeshProUGUI ComponentName;

    public GameObject ComponentInfoScrollView;
    public GameObject ComponentAttributesScrollView;
    public GameObject ComponentInfoScrollViewContent;
    public GameObject ComponentAttributesScrollViewContent;

    public Button SelectButton;
    public Button CloseMenuButton;

    public UIHoverHelper ComponentsMenuHoverHelper;

    public static List<MenuItem> MenuItems = new List<MenuItem>();

    private void Start()
    {
        ComponentsMenuHoverHelper.OnHovered += () => CameraMoveManager.LockCamera(gameObject, this);
        ComponentsMenuHoverHelper.OnHoverExit += () => CameraMoveManager.UnlockCamera(gameObject, this);
        CloseMenuButton.onClick.AddListener(CloseMenu);
    }

    private void OnDisable()
    {
        CameraMoveManager.UnlockCamera(gameObject, this);
    }

    /// <summary>
    /// Menü kapatma işlemi
    /// </summary>
    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Menüdeki itemları arama kriterine göre filtreler.
    /// </summary>
    /// <param name="name">Arama metni</param>
    public void SearchItem(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            SetAllItemsActive(true);
            return;
        }

        string lowerName = name.ToLower();
        foreach (var item in MenuItems)
        {
            bool isMatch = item.MenuItemSelfScriptableObject.Name.ToLower().Contains(lowerName);
            item.gameObject.SetActive(isMatch);
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
