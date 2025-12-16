export function initFileUpload(element, dotnetRef, fileTypes) {
    element.fileTypes = fileTypes;
    
    const fileUploadCallback = (event) => {
        const fileList = event?.detail;
        dotnetRef.invokeMethodAsync('OnFileUploaded', fileList);
    };

    element.addEventListener('eds-file-upload__files-uploaded', fileUploadCallback);
}

export function updateFileTypes(element, fileTypes) {
    element.fileTypes = fileTypes;
}