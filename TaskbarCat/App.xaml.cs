using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace TaskbarCat
{
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Prevent default unhandled exception processing
            e.Handled = true;

            string errorMessage = $"An unexpected error occurred: {e.Exception.Message}\n\n{e.Exception.StackTrace}";
            
            // Log the error to a file
            try
            {
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log");
                File.WriteAllText(logPath, errorMessage);
            }
            catch (Exception ex)
            {
                // If logging fails, at least show the original error
                System.Windows.MessageBox.Show($"Failed to write to log file: {ex.Message}\n\nOriginal error: {errorMessage}", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Show a user-friendly message
            System.Windows.MessageBox.Show($"An unexpected error occurred. Please check the error.log file for details.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            
            // Shutdown the application
            Shutdown();
        }
    }
}
