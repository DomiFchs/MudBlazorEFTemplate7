# MudBlazor EFCore Template

This template provides a quick and easy way to set up a MudBlazor project with Entity Framework Core integration. Follow the steps below to install and set up the template.

## Installation Steps

1. **Clone the Repository:**

    ```bash
    git clone https://github.com/DomiFchs/MudBlazorEFTemplate7.git
    ```

2. **Install the Template:**

    ```bash
    dotnet new --install https://github.com/DomiFchs/MudBlazorEFTemplate7.git
    ```

    If you want to update the Template, you have to pull the repository by executing following command in the cloned git repository environment
   ```bash
    git pull
    ```

   And then instead of the dotnet command above, use:
    ```bash
    dotnet new --install https://github.com/DomiFchs/MudBlazorEFTemplate7.git --force
    ```

4. **Check Installed Templates:**

    Ensure that the template is installed by running:

    ```bash
    dotnet new list
    ```

5. **Open in Rider:**

    - Open Rider.
    - Create a new solution.
    - Choose "More Templates" and then "Install Templates."
    - Select the folder where you cloned the repository, so you can see the hierarchy.
    - Hierarchy should include:
        - .idea
        - .template.config
        - Domain
        - Model
        - View
        - .gitignore
        - global.json
        - MudBlazorEFTemplate7.sln

6. **Apply Changes:**

    - Apply the folder changes.
    - Click the reload button.

7. **Explore the Template:**

    - Open the new solution in Rider.
    - Explore the project structure and files.
    
8. **Enjoy!**

    - Scroll down to the "Other" section in Rider.
    - You should find a new template related to MudBlazor EFCore.
    - Now, you are ready to enjoy working with MudBlazor and Entity Framework Core.

Feel free to customize and extend the template according to your project requirements. Happy coding!
