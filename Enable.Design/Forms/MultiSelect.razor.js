export function initMultiSelect(element, dotnetRef) {
    const searchTermChangedCallback = (event) => {
        const searchTerm = event?.detail;
        dotnetRef.invokeMethodAsync('OnSearchTermChanged', searchTerm);
    };
    
    const itemSelectedCallback = (event) => {
        const items = event?.detail;
        dotnetRef.invokeMethodAsync('OnSelectedItemsChanged', items);
    };
    
    element.addEventListener('eds-multi-select__item-selected', itemSelectedCallback);
    element.addEventListener('eds-multi-select__search-term', searchTermChangedCallback);
}

export function updateSelectedItems(element, items) {
    element.updateSelectedItems(items);
}