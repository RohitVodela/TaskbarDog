# Technical Context: Taskbar Cat CPU Monitor

## 1. Core Technology

- **Framework**: .NET 
- **UI Framework**: Windows Presentation Foundation (WPF)
- **Language**: C#

WPF is chosen for its powerful UI capabilities, including support for transparency, custom window shapes, and rich animation, which are all essential for creating the desired user experience.

## 2. Key .NET Libraries and APIs

- **`System.Diagnostics.PerformanceCounter`**: This class is the cornerstone of the CPU monitoring functionality. It provides a reliable way to access system performance data, specifically the `"% Processor Time"` counter for the `"_Total"` instance.
- **`System.Windows.Threading.DispatcherTimer`**: A WPF-specific timer that is ideal for UI updates. It will be used to periodically fetch CPU data and update the animation frame on the UI thread, ensuring smooth rendering without blocking.
- **`System.Windows.Media.Imaging.BitmapImage`**: Used to load and manage the cat animation frames (sprites).

## 3. Development Environment

- **IDE**: Visual Studio or a compatible editor (like VS Code with .NET extensions).
- **Build System**: .NET SDK (via `dotnet` CLI or integrated into the IDE).

## 4. Deployment

- **Format**: A standalone executable (`.exe`).
- **Dependencies**: The application will target a specific .NET version. Users may need to have the corresponding .NET Desktop Runtime installed, which can be bundled with the installer if needed.
