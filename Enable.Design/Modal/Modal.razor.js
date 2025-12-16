export function initEdsModal(element, dotnetRef) {
    const modalOpenCallback = () => {
        dotnetRef.invokeMethodAsync('OnModalOpened');
    };

    const modalCloseCallback = () => {
        dotnetRef.invokeMethodAsync('OnModalClosed');
    };

    element.addEventListener('eds-modal__open', modalOpenCallback);
    element.addEventListener('eds-modal__close', modalCloseCallback);
}