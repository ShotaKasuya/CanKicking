# Gemini Development Guidelines

This document provides guidelines for the AI assistant (Gemini) to effectively support development in this project.

## Coding Style

- **`this` keyword:** Avoid using `this` unless absolutely necessary.
- **`var` keyword:** Use `var` for variable declarations whenever type inference is possible.

## Directory Structure

Place all new C# scripts under `Assets/Scripts/` in the directory corresponding to their role.

- `Assets/Scripts/Controller`: Contains `~Controller` classes that describe the main game logic.
- `Assets/Scripts/Installer`: Contains `~Installer` classes for registering dependencies with the DI container.
- `Assets/Scripts/Interface`: Contains interfaces to decouple classes.
- `Assets/Scripts/Logic`: Contains `~Logic` classes that provide abstracted logic for Controllers.
- `Assets/Scripts/Model`: Contains `~Model` classes for data storage and provision.
- `Assets/Scripts/View`: Contains `~View` classes, which are `MonoBehaviour` components responsible for presentation.
- `Assets/Scripts/Structure`: Contains data structures used throughout the project (e.g., `struct`, `enum`).
- `Assets/Scripts/Module`: Contains reusable modules grouped by functionality.
- `Assets/Scripts/ModuleExtension`: Contains extension methods for existing classes.
- `Assets/Scripts/Tests`: Contains test scripts.

## Global Dependencies

- `GlobalLocator` is registered in `GlobalInstaller` and can be accessed globally.

## Class Structure (from README.md)

- **Installer:** Acts as a DI container. Class names should end with `Installer`.
- **Controller:** Describes the main game behavior. Class names should end with `Controller`.
- **Logic:** Provides abstracted logic to Controllers. Class names should end with `Logic`.
- **Interface:** Used to invert dependencies between Controllers, Views, and Models.
- **View:** Holds `MonoBehaviour` components and manages display. Class names should end with `View`.
- **Model:** Holds and provides data. Class names should end with `Model`.

## Libraries in Use (from README.md)

- DOTween
- R3
- UniTask
- VContainer
- CsprojModifier
