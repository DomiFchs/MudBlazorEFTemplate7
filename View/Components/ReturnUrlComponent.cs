using Microsoft.AspNetCore.Components;

namespace View.Components;

public abstract class ReturnUrlComponent : CancellationComponent {
    [SupplyParameterFromQuery(Name = "returnUrl")]
    public string? ReturnUrl { get; set; }
}