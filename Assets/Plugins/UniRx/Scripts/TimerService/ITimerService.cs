public interface ITimerService
{
    ITimer CreateTimer(float duration);
    ITimer CreateRepeatingTimer(float interval);
    ITimer CreateCountdownTimer(float duration);
    void PauseAllTimers();
    void ResumeAllTimers();
    void StopAllTimers();
    void Dispose();
}