using System;
using System.Diagnostics;

namespace TaskbarCat.Services
{
    public class CPUMonitorService
    {
        private PerformanceCounter? _cpuCounter;

        public CPUMonitorService()
        {
            if (PerformanceCounterCategory.Exists("Processor"))
            {
                try
                {
                    _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error initializing PerformanceCounter: {ex.Message}");
                    _cpuCounter = null;
                }
            }
            else
            {
                Debug.WriteLine("Performance counter category 'Processor' does not exist.");
                _cpuCounter = null;
            }
        }

        public float GetCurrentCpuUsage()
        {
            try
            {
                return _cpuCounter?.NextValue() ?? 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading CPU usage: {ex.Message}");
                // In case of error, disable the counter to prevent further exceptions
                _cpuCounter = null;
                return 0;
            }
        }
    }
}
