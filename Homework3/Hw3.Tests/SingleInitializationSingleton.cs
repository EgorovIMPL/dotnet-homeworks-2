using System;
using System.Threading;

namespace Hw3.Tests;

public class SingleInitializationSingleton
{
    private static readonly object Locker = new();

    private static SingleInitializationSingleton instance = null;
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
        instance = null;
        _isInitialized = false;
    }

    public static void Initialize(int delay)
    {
        if (_isInitialized == true)
            throw new InvalidOperationException();
        else
        {
            _isInitialized = true;
            _delayInitialized = delay;
        }
    }

    public static SingleInitializationSingleton Instance
    {
        get
        {
            if (instance == null)
                lock (Locker)
                    if (instance == null)
                    {
                        if (_isInitialized == true)
                            instance = new SingleInitializationSingleton(_delayInitialized);
                        else
                            instance = new SingleInitializationSingleton();
                    }
            return instance;
        }
    }
}