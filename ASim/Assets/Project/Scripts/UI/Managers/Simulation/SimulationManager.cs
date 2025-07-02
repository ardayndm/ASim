using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simülasyon panelini yönetir: başlat, durdur, şema temizle, menü aç.
/// </summary>
public class SimulationManager : MonoBehaviour
{
    [Header("UI Butonları")]
    public Button PlaySimButton;
    public Button StopSimButton;
    public Button ComponentsMenuButton;
    public Button ClearSchemaButton;

    [Header("Bileşenler")]
    public WarnPanelManager WarnPanel;
    public SchemaManager SchemaManager;
    public GameObject ComponentsMenuObject;
    public UIHoverHelper SimulationPanelHoverHelper;

    public static SimulationState SimState { get; private set; } = SimulationState.Stopped;
    public static Action<SimulationState> OnSimStateChanged;

    private void Start()
    {
        PlaySimButton.onClick.AddListener(StartSimulation);
        StopSimButton.onClick.AddListener(StopSimulation);
        ComponentsMenuButton.onClick.AddListener(OpenComponentsMenu);
        ClearSchemaButton.onClick.AddListener(ConfirmClearSchema);

        SimulationPanelHoverHelper.OnHovered += () => CameraManager.LockCamera(gameObject, this);
        SimulationPanelHoverHelper.OnHoverExit += () => CameraManager.UnlockCamera(gameObject, this);

        UpdateUIState();
    }

    private void StartSimulation()
    {
        SimState = SimulationState.Running;
        OnSimStateChanged?.Invoke(SimState);
        UpdateUIState();
    }

    private void StopSimulation()
    {
        SimState = SimulationState.Stopped;
        OnSimStateChanged?.Invoke(SimState);
        UpdateUIState();
    }

    private void OpenComponentsMenu()
    {
        ComponentsMenuObject.SetActive(true);
    }

    private void ConfirmClearSchema()
    {
        WarnPanel.gameObject.SetActive(true);
        WarnPanel.SetWarnText("Tüm şema silinecektir. Devam etmek istiyor musunuz?");
        WarnPanel.OkButtonClicked += ClearSchema;
        WarnPanel.CancelButtonClicked += CancelClearSchema;
    }

    private void ClearSchema()
    {
        SchemaManager.ClearAllSchema();
        UnsubscribeWarnPanel();
        WarnPanel.gameObject.SetActive(false);
    }

    private void CancelClearSchema()
    {
        UnsubscribeWarnPanel();
        WarnPanel.gameObject.SetActive(false);
    }

    private void UnsubscribeWarnPanel()
    {
        WarnPanel.OkButtonClicked -= ClearSchema;
        WarnPanel.CancelButtonClicked -= CancelClearSchema;
    }

    private void UpdateUIState()
    {
        bool isRunning = SimState == SimulationState.Running;

        PlaySimButton.interactable = !isRunning;
        StopSimButton.interactable = isRunning;
        ComponentsMenuButton.interactable = !isRunning;
        ClearSchemaButton.interactable = !isRunning;
    }
}
