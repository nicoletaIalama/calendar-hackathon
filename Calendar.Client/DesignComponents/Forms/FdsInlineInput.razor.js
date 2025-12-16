export function resizeInput(element) {
    if (!element) return;
    element.style.width = '1ch'; // reset
    element.style.width = element.scrollWidth + 16 + 'px';
}
