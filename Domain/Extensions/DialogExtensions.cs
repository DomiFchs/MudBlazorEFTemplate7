using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Domain.Extensions;

public static class DialogExtensions {
    public static async Task<bool> OpenTypeDialog<T>(this IDialogService dialogService, string title, string message)
        where T : ComponentBase {
        var dialogParameters = new DialogParameters {
            { "Message", message }
        };
        var dialog = await dialogService.ShowAsync<T>(title, dialogParameters);
        var result = await dialog.Result;
        return !result.Canceled;
    }
}