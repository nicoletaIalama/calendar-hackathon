export function initAutoFillSearch(element, dotnetRef, defaultItem) {
    if (!!defaultItem) {
        element.defaultItem = defaultItem;
    }

    const viewMoreClickedCallback = () => {
        dotnetRef.invokeMethodAsync('OnViewMoreClicked');
    };

    const searchTermChangedCallback = (event) => {
        const searchTerm = event?.detail;
        dotnetRef.invokeMethodAsync('OnSearchTermChanged', searchTerm);
    };

    const itemSelectedCallback = (event) => {
        const item = event?.detail;
        if (Array.isArray(item)) {
            dotnetRef.invokeMethodAsync('OnMultipleItemsSelected', item);
        } else if (item !== null) {
            dotnetRef.invokeMethodAsync('OnSingleItemSelected', item);
        }
        else {
            dotnetRef.invokeMethodAsync('OnItemsCleared');
        }
    };

    element.addEventListener('eds-fill-search-view-more', viewMoreClickedCallback);
    element.addEventListener('eds-fill-search-term', searchTermChangedCallback);
    element.addEventListener('eds-fill-search-item-selected', itemSelectedCallback);
}

export function updateDropdownItems(element, dropdownItems) {
    element.dropdownItems = dropdownItems;
}

export function updateSelectedItems(element, selectedItems) {
    element.selectedItems = selectedItems;
}

export function clearSelectedItems(element) {
    element.getElementsByClassName("eds-auto-fill-search__cancel-button")[0].click();
}