using UniRx;
using UnityEngine;

public class TimerUsageExamples : MonoBehaviour
{
    private ITimerService timerService;
    private CompositeDisposable disposables = new CompositeDisposable();
    
    void Start()
    {
        timerService = new TimerService();
        
        // Example 1: Simple countdown timer
        CreateBuildingConstructionTimer();
        
        // Example 2: Resource production timer
        CreateResourceProductionTimer();
        
        // Example 3: Cooldown timer
        CreateAbilityCooldownTimer();
        
        // Example 4: Complex chain of timers
        CreateTimerChain();
    }
    
    private void CreateBuildingConstructionTimer()
    {
        timerService.CreateTimer(30f) // 30 seconds to build
            .OnProgress(progress => Debug.Log($"Building progress: {progress:P1}"))
            .OnCompleted(() => Debug.Log("Building completed!"))
            .StartImmediately();
    }
    
    private void CreateResourceProductionTimer()
    {
        timerService.CreateRepeatingTimer(5f) // Every 5 seconds
            .OnCompleted(() => 
            {
                Debug.Log("Resources produced!");
                // Add resources to player inventory
            })
            .StartImmediately();
    }
    
    private void CreateAbilityCooldownTimer()
    {
        // Ability used, start cooldown
        var cooldownTimer = timerService.CreateTimer(15f);
        
        cooldownTimer
            .OnTick(remaining => UpdateCooldownUI(remaining))
            .OnCompleted(() => EnableAbilityButton())
            .StartImmediately();
    }
    
    private void CreateTimerChain()
    {
        // Timer 1: Preparation phase (10 seconds)
        timerService.CreateTimer(10f)
            .OnCompleted(() => 
            {
                Debug.Log("Preparation complete, starting battle!");
                
                // Timer 2: Battle phase (60 seconds)
                timerService.CreateTimer(60f)
                    .OnProgress(progress => UpdateBattleUI(progress))
                    .OnCompleted(() => 
                    {
                        Debug.Log("Battle ended!");
                        
                        // Timer 3: Cool down phase (5 seconds)
                        timerService.CreateTimer(5f)
                            .OnCompleted(() => Debug.Log("Ready for next battle!"))
                            .StartImmediately();
                    })
                    .StartImmediately();
            })
            .StartImmediately();
    }
    
    // Helper methods for UI updates
    private void UpdateCooldownUI(float remainingTime) 
    {
        // Update your cooldown UI here
    }
    
    private void EnableAbilityButton() 
    {
        // Enable ability button in UI
    }
    
    private void UpdateBattleUI(float progress) 
    {
        // Update battle progress bar
    }
    
    void OnDestroy()
    {
        timerService?.Dispose();
        disposables?.Dispose();
    }
}