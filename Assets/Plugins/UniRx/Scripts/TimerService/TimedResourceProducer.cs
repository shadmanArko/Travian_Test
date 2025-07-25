using System;

public class TimedResourceProducer : IDisposable
{
    public string ProducerId { get; private set; }
    public string ResourceId { get; private set; }
    public float ProductionRate { get; private set; }
    public bool IsActive { get; private set; }
    
    public event System.Action<string, int> OnResourceProduced;
    
    private ITimer productionTimer;
    private ITimerService timerService;
    
    public TimedResourceProducer(string producerId, string resourceId, float productionInterval, ITimerService timerService)
    {
        ProducerId = producerId;
        ResourceId = resourceId;
        ProductionRate = 1f / productionInterval; // Convert interval to rate
        this.timerService = timerService;
        
        // Create repeating timer for production
        productionTimer = timerService.CreateRepeatingTimer(productionInterval)
            .OnCompleted(() => OnResourceProduced?.Invoke(ResourceId, 1));
    }
    
    public void StartProduction()
    {
        if (!IsActive)
        {
            IsActive = true;
            productionTimer.Start();
        }
    }
    
    public void StopProduction()
    {
        if (IsActive)
        {
            IsActive = false;
            productionTimer.Stop();
        }
    }
    
    public void Dispose()
    {
        productionTimer?.Stop();
        (productionTimer as IDisposable)?.Dispose();
    }
}