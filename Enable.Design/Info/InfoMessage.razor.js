export function initInfoMessage(element, dotnetRef) {
    const callback = () => {
        dotnetRef.invokeMethodAsync('OnActionClicked');
    };

    element.addEventListener('eds-info-message__action', callback);
}