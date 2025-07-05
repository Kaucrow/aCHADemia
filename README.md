# aCHADemia

## Academic Management System

aCHADemia is a sigma male desktop application designed for performing academic management. Built with C# and WPF, it leverages the Model-View-ViewModel (MVVM) architectural pattern for a clean, maintainable, and testable codebase.

## Features

*   **Student Management:** Easily add, edit, and view student information.
*   **Course Management:** Organize courses, assign instructors, and manage schedules.
*   **Grade Tracking:** Record and monitor student grades for various assignments and exams.
*   **Reporting:** Generate insightful reports on student performance and course progress.

## Technologies Used

*   **C#:** The primary programming language.
*   **WPF (Windows Presentation Foundation):** For building the rich desktop user interface.
*   **MVVM (Model-View-ViewModel):** Architectural pattern for separation of concerns and improved testability.
*   **.NET 8+:** The framework version used for development.

## Installation

To get aCHADemia up and running on your local machine, follow these steps:

### Prerequisites
*   **Visual Studio 2022 (or newer):** The primary IDE for C# and WPF development. You can download it from the official Microsoft website.
*   **.NET SDK 8.0 (or newer):** Ensure you have the correct .NET SDK installed. Visual Studio usually includes it, but you can also download it separately from the official .NET website.

### Setup
1.  **Clone the Repository:**
    ```bash
    git clone https://github.com/kaucrow/aCHADemia.git
    ```

2.  **Open in Visual Studio:**
    * Open the `aCHADemia.sln` solution file with Visual Studio.

3.  **Restore NuGet Packages:**
    *   Visual Studio should automatically prompt you to restore NuGet packages. If not, right-click on the solution in the Solution Explorer and select "Restore NuGet Packages."

4.  **Build the Project:**
    *   From the Visual Studio menu, go to `Build` > `Build Solution`, or press `Ctrl + Shift + B`.

5.  **Run the Application:**
    *   Once the build is successful, press `F5` or click the "Start" button in Visual Studio to run the application.

## Collaborating

To ensure a smooth and organized development process, please adhere to the following guidelines:

*   **Branching Strategy:** Each contributor must create their own dedicated feature branch for any new features, bug fixes, or enhancements they are working on.
    *   Branch names should follow the format: `yourname-feature` (e.g., `atlas-add-student-grades`, `odiar-fix-login-bug`).

*   **Commit Messages:** All commit messages must follow the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) convention. This helps in generating changelogs and understanding the history of changes.
    *   Examples:
        *   `feat: add new student enrollment form`
        *   `fix: resolve issue with grade calculation`
        *   `docs: update installation instructions`
        *   `chore: update nuget packages`

*   **Pull Requests (PRs):** When your feature is complete and thoroughly tested on your branch, submit a Pull Request to the `dev` branch.
    *   Provide a clear and concise description of the changes in your PR.
    *   Reference any relevant issues in your PR description.
    *   Ensure your code follows the existing coding style and conventions.
    *   Your PR will be reviewed by the repository owner before being merged into `dev`.

## License

This project is licensed under the [MIT License](LICENSE).
