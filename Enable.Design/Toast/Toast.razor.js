export function initToastComponent(element, dotnetRef) {
    const dismissButtonCallback = (event) => {
        dotnetRef.invokeMethodAsync('OnDismissButtonClicked');
    };

    element.handleClose = dismissButtonCallback;
}