export function initCustomDropdown(element, dotnetRef) {
    const triggerElement = element.querySelector('.fds-custom-dropdown__trigger');
    
    if (!triggerElement) {
        console.warn('Trigger element not found in custom dropdown container');
        return;
    }

    // Handle click outside to close dropdown
    const handleDocumentClick = (event) => {
        if (!element.contains(event.target)) {
            dotnetRef.invokeMethodAsync('OnTriggerBlur');
        }
    };

    // Handle pointer events for better trackpad/touch support
    const handleDocumentPointer = (event) => {
        if (!element.contains(event.target)) {
            dotnetRef.invokeMethodAsync('OnTriggerBlur');
        }
    };

    // Add document listeners when dropdown is opened
    const addDocumentListener = () => {
        document.addEventListener('click', handleDocumentClick);
        document.addEventListener('pointerdown', handleDocumentPointer);
    };

    // Remove document listeners when dropdown is closed
    const removeDocumentListener = () => {
        document.removeEventListener('click', handleDocumentClick);
        document.removeEventListener('pointerdown', handleDocumentPointer);
    };

    // Store references for cleanup
    element._customDropdownHandlers = {
        documentClick: handleDocumentClick,
        documentPointer: handleDocumentPointer,
        addDocumentListener: addDocumentListener,
        removeDocumentListener: removeDocumentListener,
        triggerElement: triggerElement
    };

    // Watch for dropdown state changes
    const observer = new MutationObserver((mutations) => {
        mutations.forEach((mutation) => {
            if (mutation.type === 'childList') {
                const menu = element.querySelector('.fds-custom-dropdown__menu');
                if (menu) {
                    addDocumentListener();
                } else {
                    removeDocumentListener();
                }
            }
        });
    });

    observer.observe(element, { childList: true, subtree: true });
    element._customDropdownObserver = observer;
}

export function disposeCustomDropdown(element) {
    if (element._customDropdownHandlers) {
        const handlers = element._customDropdownHandlers;
        handlers.removeDocumentListener();
        delete element._customDropdownHandlers;
    }

    if (element._customDropdownObserver) {
        element._customDropdownObserver.disconnect();
        delete element._customDropdownObserver;
    }
}

export function focusFirstOption(element) {
    const firstOption = element.querySelector('.fds-custom-dropdown__menu-inner .fds-custom-dropdown__option');
    if (firstOption) {
        firstOption.focus();
    }
}

export function focusHighlightedOption(element, highlightedIndex) {
    const options = element.querySelectorAll('.fds-custom-dropdown__menu-inner .fds-custom-dropdown__option');
    if (options && highlightedIndex >= 0 && highlightedIndex < options.length) {
        options[highlightedIndex].focus();
    } else if (options.length > 0) {
        // Fallback to first option if index is invalid
        options[0].focus();
    }
}

export function focusCustomDropdown(element) {
    const triggerElement = element.querySelector('.fds-custom-dropdown__trigger');
    if (triggerElement && !triggerElement.disabled) {
        triggerElement.focus();
    }
}

export function closeCustomDropdown(element) {
    const menu = element.querySelector('.fds-custom-dropdown__menu');
    if (menu) {
        menu.remove();
    }
}