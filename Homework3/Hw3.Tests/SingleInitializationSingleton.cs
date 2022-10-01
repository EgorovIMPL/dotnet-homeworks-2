using System;
using System.Threading;

namespace Hw3.Tests;

public class SingleInitializationSingleton
{
    private static readonly object Locker = new();

    private static SingleInitializationSingleton _instance = null;
    
    private static volatile bool _isInitialized = false;
    
    public const int DefaultDelay = 3_000;
    
    public int Delay { get; }
    
    private static int _delayInitialized { get; set; }

    private SingleInitializationSingleton(int delay = DefaultDelay)
    {
        Delay = delay;
        // imitation of complex initialization logic
        Thread.Sleep(delay);
    }

    internal static void Reset()
    {
        _instance = null;
        _isInitialized = false;
    }

    public static void Initialize(int delay)
    {
        if (_isInitialized)
            throw new InvalidOperationException();
        _delayInitialized = delay;
        _isInitialized = true;
    }

    public static SingleInitializationSingleton Instance
    {
        get
        {
            if (_instance is null)
                lock (Locker)
                    if (_instance is null)
                        _instance = _isInitialized 
                            ? new SingleInitializationSingleton(_delayInitialized) 
                            : new SingleInitializationSingleton();
            return _instance;
        }
    }
}