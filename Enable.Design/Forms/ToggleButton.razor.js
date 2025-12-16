export function initToggle(element, dotnetRef, states) {
    const edsToggleButton= element.querySelector('eds-toggle-button');
    edsToggleButton.states = states;
    
    const callback = (event) => {
        const selectedState = event?.detail;
        dotnetRef.invokeMethodAsync('OnSelectedStateChanged', selectedState);
    };
    
    edsToggleButton.addEventListener('eds-toggle-button__selected-state', callback);
}

export function onStatesChanged(element, dotnetRef, states) {
    const edsToggleButton= element.querySelector('eds-toggle-button');
    edsToggleButton.states = states;
}