window.getImageDimensions = (fileInputId) => {
    return new Promise((resolve, reject) => {
        const input = document.getElementById(fileInputId);
        if (!input || !input.files || input.files.length === 0) {
            reject("No file selected");
            return;
        }

        const file = input.files[0];
        const reader = new FileReader();

        reader.onload = (e) => {
            const img = new Image();
            img.onload = () => {
                resolve({
                    width: img.width,
                    height: img.height
                });
            };
            img.onerror = reject;
            img.src = e.target.result;
        };

        reader.onerror = reject;
        reader.readAsDataURL(file);
    });
};