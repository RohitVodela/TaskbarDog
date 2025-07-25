# Active Context: Taskbar Cat CPU Monitor

## 1. Current Focus

The immediate focus is on establishing the foundational structure of the .NET WPF project. This involves creating the necessary project files and setting up the initial documentation (Memento).

## 2. Recent Changes

- **Implemented Animal Switching**: Added the ability to switch between "Dog" and "Cat" animations via the system tray menu.
- **Refactored Animation Logic**: Created a new `AnimationService` to manage different animation sets and simplified the `AnimationController`.
- **Organized Image Assets**: Moved image files into subdirectories (`Images/Dog`, `Images/Cat`).
- **Added System Tray Icon**: Implemented a `NotifyIcon` with an "Exit" option for graceful shutdown.
- **Fixed Runtime Crashes**: Resolved issues related to `PerformanceCounter` and image loading.
- **Integrated Dog Sprites**: Updated the application to use the user-provided dog animation images.
- **Implemented Core Logic**: Created the `CPUMonitorService`, `AnimationController`, and `MainViewModel`.
- **Designed MainWindow**: Set up the transparent, borderless window and bound it to the ViewModel.
- **Scaffolded WPF Project**: Used the `dotnet` CLI to create the `TaskbarCat` project.
- **Initialized Memento**: Created the initial core documentation files.

## 3. Next Steps

1.  **Publish the application**: Create a new standalone `.exe` file with the animal switching feature.
