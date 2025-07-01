using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Kullanıcıya uyarı mesajı gösteren ve 
/// OK / Cancel butonları ile kullanıcıdan onay alan panel.
/// </summary>
public class WarnPanelManager : MonoBehaviour
{
    /// <summary>
    /// Uyarı mesajının gösterildiği TextMeshProUGUI bileşeni.
    /// </summary>
    [Tooltip("Uyarı mesajının gösterileceği alan")]
    public TextMeshProUGUI WarnText;

    /// <summary>
    /// Onay butonu (OK).
    /// </summary>
    [SerializeField]
    private Button OkButton;

    /// <summary>
    /// İptal butonu (Cancel).
    /// </summary>
    [SerializeField]
    private Button CancelButton;

    /// <summary>
    /// OK butonuna basıldığında tetiklenen event.
    /// </summary>
    public event Action OkButtonClicked;

    /// <summary>
    /// Cancel butonuna basıldığında tetiklenen event.
    /// </summary>
    public event Action CancelButtonClicked;

    private void Start()
    {
        CameraMoveManager.LockCamera(gameObject, this);
        OkButton.onClick.AddListener(() =>
        {
            OkButtonClicked?.Invoke();
            CameraMoveManager.UnlockCamera(gameObject, this);
        });
        CancelButton.onClick.AddListener(() =>
        {
            CancelButtonClicked?.Invoke();
            CameraMoveManager.UnlockCamera(gameObject, this);
        });
    }

    /// <summary>
    /// Uyarı mesajını günceller.
    /// </summary>
    /// <param name="message">Gösterilecek uyarı metni</param>
    public void SetWarnText(string message)
    {
        if (WarnText != null)
            WarnText.text = message;
        else
            Debug.LogWarning("WarnText referansı atanmadı.");
    }
}
