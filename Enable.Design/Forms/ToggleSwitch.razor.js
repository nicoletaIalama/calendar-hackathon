export function initToggle(element, dotnetRef, initiallyChecked) {
    const edsToggleSwitch= element.querySelector('eds-toggle-switch');
    
    setToggle(element, initiallyChecked);
    
    const callback = (event) => {
        const toggleState = event?.target?.checked;
        dotnetRef.invokeMethodAsync('OnToggleChanged', toggleState);
    };
    
    edsToggleSwitch.addEventListener('input', callback);
}

export function setToggle(element, value) {
    const edsToggleSwitch = element.querySelector('eds-toggle-switch');
    
    edsToggleSwitch.checked = value;
}