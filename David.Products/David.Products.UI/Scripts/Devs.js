$(document).ready(function () {
    $('#md-close').on('click', closeWindowsModal);

    $('.datepicker').datepicker({
        //weekdaysShort: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
        //showMonthsShort: true,
        format: 'dd/mm/yyyy',
        startDate: '-3d'
    });
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