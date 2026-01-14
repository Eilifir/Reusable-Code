using UnityEngine;

public class Stamina : MonoBehaviour
{
    //----------------Variables---------------
    [SerializeField] float maxStamina = 100f;
    [SerializeField] float drainPerSecond = 15f;
    [SerializeField] float regenPerSecond = 10f;
    public float Current { get; private set; }
    public float Max => maxStamina;
    public bool HasStamina => Current > 0.01f;

    private void Start()
    {
        Current = maxStamina;
    }

    public void Tick(bool isRunning, float dt)
    {
        if (isRunning) Current -= drainPerSecond * dt;
        else Current += regenPerSecond * dt;
        Current = Mathf.Clamp(Current, 0f, maxStamina);
    }
}
