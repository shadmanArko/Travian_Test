// ============================================================================
// CORE INTERFACES - Copy these once to your project
// ============================================================================

using System;
//using Cysharp.Threading.Tasks;

// Base interfaces for MVP pattern
public interface IPoolableModel
{
    //UniTask InitializeAsync();
    void Reset();
    void Dispose();
}

