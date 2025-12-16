/**
 * Adds a click outside listener to close the dropdown when clicking outside the component
 * @param {HTMLElement} element - The DOM element reference for the component
 * @param {DotNetObjectReference} dotNetObject - The .NET object reference for interop
 */
export function addClickOutsideListener(element, dotNetObject) {
    const handleClickOutside = (event) => {
        if (!element.contains(event.target)) {
            dotNetObject.invokeMethodAsync("CloseDropdown");
        }
    };

    document.addEventListener("click", handleClickOutside);

    // Store the handler so we can remove it later
    element._clickOutsideHandler = handleClickOutside;
}

/**
 * Removes the click outside listener from the component
 * @param {HTMLElement} element - The DOM element reference for the component
 */
export function removeClickOutsideListener(element) {
    if (element._clickOutsideHandler) {
        document.removeEventListener("click", element._clickOutsideHandler);
        element._clickOutsideHandler = null;
    }
}
