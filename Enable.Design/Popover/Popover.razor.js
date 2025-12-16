
export function initPopover(element, dotnetRef) {
    const popoverClosedCallback = () => {
        dotnetRef.invokeMethodAsync('OnPopoverClosed');
    };
    
    element.addEventListener('eds-popover__closed', popoverClosedCallback);

    const popoverOpenedCallback = () => {
        dotnetRef.invokeMethodAsync('OnPopoverOpened');
    };
    
    element.addEventListener('eds-popover__opened', popoverOpenedCallback);
}

export function open(element) {
    element.open();
}

export function close(element) {
    element.close();
}

export function toggle(element) {
    element.toggle();
}