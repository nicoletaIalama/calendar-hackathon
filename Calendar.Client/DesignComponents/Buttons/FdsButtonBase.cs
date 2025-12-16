using Calendar.Client.DesignComponents.Icons;
using Microsoft.AspNetCore.Components;

namespace Calendar.Client.DesignComponents.Buttons;

public abstract class FdsButtonBase : ComponentBase
{
    protected ElementReference ButtonRef;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public Type? Icon { get; set; }
    [Parameter] public FdsButtonStyle ButtonStyle { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object>? AdditionalAttributes { get; set; }
    [Parameter] public FdsButtonSize Size { get; set; } = FdsButtonSize.Medium;
    [Parameter] public EventCallback OnClick { get; set; }
    
    protected string CalculateButtonStyle() => ButtonStyle switch
    {
        FdsButtonStyle.Primary => "fds-button fds-button--primary group",
        FdsButtonStyle.Secondary => "fds-button fds-button--secondary group",
        FdsButtonStyle.Tertiary => "fds-button fds-button--tertiary group",
        FdsButtonStyle.Inverse => "fds-button fds-button--inverse group",
        FdsButtonStyle.WarningPrimary => "fds-button fds-button--warning-primary group",
        FdsButtonStyle.WarningSecondary => "fds-button fds-button--warning-secondary group",
        FdsButtonStyle.LinkBrand => "fds-link-button fds-link-button--brand",
        FdsButtonStyle.LinkSubtle => "fds-link-button fds-link-button--subtle",
        FdsButtonStyle.LinkInverse => "fds-link-button fds-link-button--inverse",
        _ => ""
    };

    protected string CalculateIconStyle() => ButtonStyle switch
    {
        FdsButtonStyle.Primary => "fds-button-icon--primary",
        FdsButtonStyle.Secondary => "fds-button-icon--secondary",
        FdsButtonStyle.Tertiary => "fds-button-icon--tertiary",
        FdsButtonStyle.Inverse => "fds-button-icon--inverse",
        _ => ""
    };
    
    protected string CalculateIconSize() => Size switch
    {
        FdsButtonSize.Small => "fds-button-icon--small",
        FdsButtonSize.Medium => "fds-button-icon--medium",
        FdsButtonSize.Large => "fds-button-icon--large",
        _ => ""
    };

    protected virtual RenderFragment IconRenderFragment
        => builder =>
        {
            if (Icon is null) return;

            builder.OpenComponent(1, Icon);
            builder.AddAttribute(2, nameof(FdsIconBase.Class), CalculateIconStyle());
            builder.AddAttribute(2, nameof(FdsIconBase.SizeClass), CalculateIconSize());
            builder.CloseComponent();
        };
}
