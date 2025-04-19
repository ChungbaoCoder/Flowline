function showSuccessToast() {
    var toastEl = document.getElementById('successToast');
    var toast = new bootstrap.Toast(toastEl, {
        animation: true,
        delay: 5000
    });
    toast.show();
}