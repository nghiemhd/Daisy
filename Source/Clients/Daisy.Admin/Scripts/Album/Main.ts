$(document).ready(function () {
    var album = new Album.DaisyAlbum();

    $('#btnSearch').click(function () {
        var options: Album.IAlbumSearchOptions = {
            AlbumName: $('#txtAlbumName').val(),
            IsPublished: $('#cboPublishStatus').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        };

        album.search(options);
    });

    $('#chkSelectAll').change(function () {
        if (this.checked) {
            $('#gridAlbums input[type=checkbox]').each(function () {
                this.checked = true;
            });
        }
        else {
            $('#gridAlbums input[type=checkbox]').each(function () {
                this.checked = false;
            });
        }
    });

    $('#btnPublish').click(function () {
        var publishedAlbums: number[] = [];
        $('#gridAlbums input[type=checkbox]:checked').each(function () {
            var albumId = $(this).val();
            publishedAlbums.push(albumId);
        });

        if (publishedAlbums.length <= 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Publish album',
                message: 'Please choose album(s) to publish.'
            });
        }
        else {
            album.publishAlbums(publishedAlbums, true);
        }
    });

    $('#btnUnpublish').click(function () {
        var publishedAlbums: number[] = [];
        $('#gridAlbums input[type=checkbox]:checked').each(function () {
            var albumId = $(this).val();
            publishedAlbums.push(albumId);
        });

        if (publishedAlbums.length <= 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Unpublish album',
                message: 'Please choose album(s) to unpublish.'
            });
        }
        else {
            album.publishAlbums(publishedAlbums, false);
        }
    });
}); 