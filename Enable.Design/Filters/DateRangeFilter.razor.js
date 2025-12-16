export function initDateRangeFilter(element, dotnetRef) {
    const filterOpenCallback = () => {
        dotnetRef.invokeMethodAsync('OnFilterOpened');
    };
    
    const filterClosedCallback = () => {
        dotnetRef.invokeMethodAsync('OnFilterClosed');
    };

    element.addEventListener('eds-filter__open', filterOpenCallback);
    element.addEventListener('eds-filter__close', filterClosedCallback);
}

