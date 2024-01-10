using Microsoft.AspNetCore.Components;

namespace View.Components;

public class RedirectTo : ComponentBase {
    [Parameter] public string RelPath { get; set; } = string.Empty;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    protected override void OnInitialized() {
        var returnUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        NavigationManager.NavigateTo($"{RelPath}?returnUrl={Uri.EscapeDataString(returnUrl)}");
    }
}