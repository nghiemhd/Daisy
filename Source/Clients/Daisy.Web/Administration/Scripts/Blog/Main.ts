$(document).ready(function () {
    var blog = new Content.Blog();

    $('#fromCreatedDate').datetimepicker({
        format: 'DD/MM/YYYY',
        showTodayButton: true
    });

    $('#toCreatedDate').datetimepicker({
        format: 'DD/MM/YYYY',
        showTodayButton: true
    });

    $('.search-collapse').click(function () {
        $('#divOptions').slideToggle('500', function () {
            $('.toggle-icon').toggleClass('collapse');
        });
    });

    $('#btnSearch').click(function () {
        Content.Blog.searchRequestUrl = $(this).data('request-url');
        var fromCreatedDate: string = null;
        var toCreatedDate: string = null;

        var fromDate = $('#fromCreatedDate').data('DateTimePicker').date();        
        if (fromDate > 0)
        {
            fromCreatedDate = moment(fromDate).format('YYYY-MM-DD');
        }

        var toDate = $('#toCreatedDate').data('DateTimePicker').date();        
        if (toDate > 0) {
            toCreatedDate = moment(toDate).format('YYYY-MM-DD');
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

    $('#chkSelectAll').change(function () {
        if (this.checked) {
            $('#gridBlogs input[type=checkbox]').each(function () {
                this.checked = true;
            });
        }
        else {
            $('#gridBlogs input[type=checkbox]').each(function () {
                this.checked = false;
            });
        }
    });

    $('#btnPublish').click(function () {
        var publishedBlogs: number[] = [];
        $('#gridBlogs tbody input[type=checkbox]:checked').each(function () {
            var blogId = $(this).val();
            publishedBlogs.push(blogId);
        });

        if (publishedBlogs.length <= 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Publish blog',
                message: 'Please choose blog(s) to publish.'
            });
        }
        else {
            Content.Blog.publishBlogsRequestUrl = $(this).data('request-url');
            blog.publishBlogs(publishedBlogs, true);
        }
    });

    $('#btnUnpublish').click(function () {
        var unpublishedBlogs: number[] = [];
        $('#gridBlogs tbody input[type=checkbox]:checked').each(function () {
            var blogId = $(this).val();
            unpublishedBlogs.push(blogId);
        });

        if (unpublishedBlogs.length <= 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Unpublish blog',
                message: 'Please choose blog(s) to unpublish.'
            });
        }
        else {
            Content.Blog.publishBlogsRequestUrl = $(this).data('request-url');
            blog.publishBlogs(unpublishedBlogs, false);
        }
    });
});