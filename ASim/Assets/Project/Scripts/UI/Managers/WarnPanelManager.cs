using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Uyarı mesajı gösterip, OK/Cancel ile onay alan panel.
/// </summary>
public class WarnPanelManager : MonoBehaviour
{
    [Tooltip("Uyarı mesajının gösterileceği alan")]
    public TextMeshProUGUI WarnText;

    [SerializeField] private Button OkButton;
    [SerializeField] private Button CancelButton;

    public event Action OkButtonClicked;
    public event Action CancelButtonClicked;

    private void Start()
    {
        CameraManager.LockCamera(gameObject, this);

        OkButton.onClick.AddListener(() =>
        {
            OkButtonClicked?.Invoke();
            CameraManager.UnlockCamera(gameObject, this);
        });

        CancelButton.onClick.AddListener(() =>
        {
            CancelButtonClicked?.Invoke();
            CameraManager.UnlockCamera(gameObject, this);
        });
    }

    /// <summary>
    /// Uyarı mesajını ayarlar.
    /// </summary>
    public void SetWarnText(string message)
    {
        if (WarnText != null)
            WarnText.text = message;
        else
            Debug.LogWarning("WarnText referansı atanmadı.");
    }
}
