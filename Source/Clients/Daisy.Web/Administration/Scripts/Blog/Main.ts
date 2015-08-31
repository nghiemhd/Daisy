$(document).ready(function () {
    var blog = new Content.Blog();

    $('#fromCreatedDate').datetimepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        todayBtn: true,
        pickerPosition: 'bottom-left',
        startView: 0,
        minView: 0,
        maxView: 0
    });

    $('#toCreatedDate').datetimepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        todayBtn: true,
        pickerPosition: 'bottom-left'
    });

    $('.search-collapse').click(function () {
        $('#divOptions').slideToggle('500', function () {
            $('.toggle-icon').toggleClass('collapse');
        });
    });

    $('#btnSearch').click(function () {
        Content.Blog.searchRequestUrl = $(this).data('request-url');

        var fromDate = $('#fromCreatedDate').datetimepicker('getDate').valueOf();
        var fromCreatedDate: string = null;
        if (fromDate > 0)
        {
            fromCreatedDate = dateFormat(new Date(Number(fromDate)), 'yyyy-mm-dd');
        }

        var toDate = $('#toCreatedDate').datetimepicker('getDate').valueOf();
        var toCreatedDate: string = null;
        if (toDate > 0) {
            toCreatedDate = dateFormat(new Date(Number(toDate)), 'yyyy-mm-dd');
        }
        var options: Content.IBlogSearchOptions = {
            Title: $('#txtTitle').val(),
            FromCreatedDate: fromCreatedDate,
            ToCreatedDate: toCreatedDate,
            IsPublished: $('#cboPublishStatus').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        };

        blog.search(options);
    });

});