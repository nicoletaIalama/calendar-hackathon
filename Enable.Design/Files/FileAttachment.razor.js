export function initFileAttachment(element, dotnetRef) {
    const fileClickCallback = () => {
        dotnetRef.invokeMethodAsync('OnFileClicked');
    };

    const fileClearCallback = () => {
        dotnetRef.invokeMethodAsync('OnFileCleared');
    };

    element.addEventListener('eds-file-attachment__on-click', fileClickCallback);
    element.addEventListener('eds-file-attachment__on-clear', fileClearCallback);
}
