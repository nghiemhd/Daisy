$(document).ready(function () {
    $('.input-group.date').datepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        todayBtn: true
    });

    $('.search-collapse').click(function () {
        $('#divOptions').slideToggle('500', function () {
            $('.toggle-icon').toggleClass('collapse');
        });
    });
});