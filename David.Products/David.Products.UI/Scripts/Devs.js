$(document).ready(function () {
    $('#md-close').on('click', closeWindowsModal);
});

function ShowError(message) {
    $(document).ready(function () {
        $('#modal-error').show();
        $('#Message-error').html(message);
    });
}

function closeWindowsModal() {
    $('#modal-error').hide();
}  