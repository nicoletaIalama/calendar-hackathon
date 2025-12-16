let debounceTimer;
const elementListeners = new Map();

export function initializeFdsSearch(dotNetObject, element, debounceDelay = 300) {
    const handler = function (event) {
        clearTimeout(debounceTimer);
        debounceTimer = setTimeout(() => {
            if (dotNetObject) {
                dotNetObject.invokeMethodAsync("OnDebouncedInput", event.target.value);
            }
        }, debounceDelay);
    };

    element.addEventListener("input", handler);
    elementListeners.set(element, handler);
}

export function dispose(element) {
    const handler = elementListeners.get(element);
    if (handler) {
        element.removeEventListener("input", handler);
        elementListeners.delete(element);
    }
}
