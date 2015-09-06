$(document).ready(function () {
    var album = new Album.DaisyAlbum();

    $('#btnSearch').click(function () {
        var options: Album.IAlbumSearchOptions = {
            AlbumName: $('#txtAlbumName').val(),
            IsPublished: $('#cboPublishStatus').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        };

        Album.DaisyAlbum.searchRequestUrl = $(this).data('request-url');
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
            Album.DaisyAlbum.publishAlbumsRequestUrl = $(this).data('request-url');
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
            Album.DaisyAlbum.publishAlbumsRequestUrl = $(this).data('request-url');
            album.publishAlbums(publishedAlbums, false);
        }
    });

    $('#btnDelete').click(function () {
        var albums: number[] = [];
        Album.DaisyAlbum.deleteAlbumsRequestUrl = $(this).data('request-url');

        $('#gridAlbums input[type=checkbox]:checked').each(function () {
            var albumId = $(this).val();
            albums.push(albumId);
        });

        if (albums.length <= 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Delete album',
                message: 'Please choose album(s) to delete.'
            });
        }
        else {
            var buttons: IBootstrapDialogButton[] = [{
                label: 'OK',
                action: function (dialog) {                    
                    dialog.close();
                    album.deleteAlbums(albums);
                }
            }, {
                    label: 'Cancel',
                    action: function (dialog) {
                        dialog.close();
                    }
                }];

            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Delete album',
                message: 'Do you want to delete selected album(s)?',
                buttons: buttons
            });
        }
    });
}); 