const pick = document.getElementById('inputenddate');
pick.addEventListener('input', function(e) {
    var day = new Date(this.value).getUTCDay();
    if ([6, 0].includes(day)) {
        e.preventDefault();
        this.value = '';
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Bitmə tarixi həftə sonu seçilə bilməz'
        })
    }
});