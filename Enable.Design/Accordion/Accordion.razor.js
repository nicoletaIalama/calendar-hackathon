export function handleAccordionNavigation(keyPressed) {
    const accordionHeaders = document.getElementById('eds-modal').querySelectorAll('.eds-accordion--step-name__medium, .eds-accordion--step-name__large, .eds-accordion--step-name__small');
    let currentIndex = -1;

    // Find the index of the currently focused header
    accordionHeaders.forEach((header, index) => {
        if (document.activeElement === header) {
            currentIndex = index;
        }
    });

    if (keyPressed === 'ArrowDown') {
        // Move to the next header
        currentIndex = (currentIndex + 1) % accordionHeaders.length;
        accordionHeaders[currentIndex].focus();
    } else if (keyPressed === 'ArrowUp') {
        // Move to the previous header
        currentIndex = (currentIndex - 1 + accordionHeaders.length) % accordionHeaders.length;
        accordionHeaders[currentIndex].focus();
    }
}
