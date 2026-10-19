# Project: University database project with Entity Framework

## General Instructions

- When you generate new C# code, follow the existing coding style.
- Ensure all new functions have summary.
- Line up to existing dependency injection schema
- Do not edit existing methods etc. if you find something just inform me
- Do not make overengineering, it is only university project and it dont have to be production ready or be to much complexed.
- Common components (constants, enums, repositories, providers, services, utils) MUST be placed in the `Common/` directory in their respective subfolders.

## Coding Style
- Use camelCase for method and variable names
- Use PascalCase for class names
- For creating private readonly fields do not use _ at start of variable name. Just name it in camelCase. Fields should start with lower case.
- For public constants in `Common/Constants`, use camelCase as well (matching variable naming convention).

## Pattern instructions
- ViewModels/DodajKlientaViewModel.cs and ViewModels/MainWindowViewModel.cs are your model class 
- While editing/creating new viewmodels align to pattern examples from viewmodels
- When binding RelayCommands in XAML: methods marked with `[RelayCommand]` in CommunityToolkit.Mvvm generate a command property with the exact same prefix plus `Command`. Ensure the case matches in XAML since WPF bindings are case-sensitive.

## Structure of Common/ Directory
All shared logic, infrastructure, and constants must reside in the `Common/` folder according to these subdirectories:
- `Common/Constants/`: Application, UI, and filtering constants (e.g. `FilterConstants.cs`). Do not declare hardcoded shared constants directly inside ViewModels or Views.
- `Common/Enums/`: All enumerations used across the domain and database (e.g. `EngineType.cs`, `FactoryIdentity.cs`).
- `Common/Providers/`: High-level data providers aggregating and preparing data for ViewModels (e.g. `DataProvider.cs`, `DataSeedingProvider.cs`, and their interfaces in `Common/Providers/Interfaces/`).
- `Common/Repositories/`: Direct Entity Framework database access repositories (e.g. `Repository.cs`, `KlientRepository.cs`, `ZamowienieRepository.cs`, `PojazdRepository.cs`, and interfaces in `Common/Repositories/Interfaces/`).
- `Common/Services/`: Domain services and business action handlers (e.g. `AddDataService.cs`).
- `Common/Utils/`: Utility, helper, and result wrapper classes (e.g. `ResultWithMessage.cs`, `ResultObjectWithMessage.cs`).
- `Common/Datacontext.cs`: Entity Framework Core DbContext for the database.
