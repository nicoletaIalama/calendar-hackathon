export function initSingleSelectFilter(element, dotnetRef) {
    const filterOpenCallback = () => {
        dotnetRef.invokeMethodAsync('OnFilterOpened');
    };
    
    const filterClosedCallback = () => {
        dotnetRef.invokeMethodAsync('OnFilterClosed');
    };

    const searchTermChangedCallback = (event) => {
        const searchTerm = event?.detail;
        dotnetRef.invokeMethodAsync('OnSearchTermChanged', searchTerm);
    };

    element.addEventListener('eds-filter__open', filterOpenCallback);
    element.addEventListener('eds-filter__close', filterClosedCallback);
    element.addEventListener('eds-search__value-changed', searchTermChangedCallback);
}

export function closeDropdown(element) {
    element.closeDropdown();
}