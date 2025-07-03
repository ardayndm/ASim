using System;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance { get; private set; }
    public SimState State { get; private set; } = SimState.Stopped;
    public event Action<SimState> OnSimStateChanged;

    [SerializeField] Button StartButton;
    [SerializeField] Button StopButton;
    [SerializeField] Button ComponentsButton;
    [SerializeField] Button ClearSchemaButton;
    [SerializeField] UIHoverHelper SimulationPanelHoverHelper;

    private void Awake() => Instance = this;

    private void Start()
    {
        SetActiveButtons();
        InitEvents();
    }

    void InitEvents()
    {
        StartButton.onClick.AddListener(() =>
        {
            State = SimState.Running;
            OnSimStateChanged?.Invoke(State);
            SetActiveButtons();
        });

        StopButton.onClick.AddListener(() =>
        {
            State = SimState.Stopped;
            OnSimStateChanged?.Invoke(State);
            SetActiveButtons();
        });
    }

    private void SetActiveButtons()
    {
        StartButton.interactable = State == SimState.Stopped;
        StopButton.interactable = State == SimState.Running;
        ComponentsButton.interactable = State == SimState.Stopped;
        ClearSchemaButton.interactable = State == SimState.Stopped;
    }
}