$(document).ready(function () {
    var album = new Album.DaisyAlbum();
    var albumId = $('#albumId').val();

    $('#chkSelectAll').change(function () {
        if (this.checked) {
            $('#gridPhotos input[type=checkbox]').each(function () {
                this.checked = true;
            });
        }
        else {
            $('#gridPhotos input[type=checkbox]').each(function () {
                this.checked = false;
            });
        }
    });

    $('#btnPublish').click(function () {
        var publishedPhotos: number[] = [];
        $('#gridPhotos input[type=checkbox]:checked').each(function () {
            var photoId = $(this).val();
            publishedPhotos.push(photoId);
        });

        if (publishedPhotos.length <= 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Publish photo',
                message: 'Please choose photo(s) to publish.'
            });
        }
        else {
            Album.DaisyAlbum.publishPhotosRequestUrl = $(this).data('request-url');
            album.publishPhotos(albumId, publishedPhotos, true);
        }
    });

    $('#btnUnpublish').click(function () {
        var unpublishedPhotos: number[] = [];
        $('#gridPhotos input[type=checkbox]:checked').each(function () {
            var photoId = $(this).val();
            unpublishedPhotos.push(photoId);
        });

        if (unpublishedPhotos.length <= 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Unpublish photo',
                message: 'Please choose photo(s) to unpublish.'
            });
        }
        else {
            Album.DaisyAlbum.publishPhotosRequestUrl = $(this).data('request-url');
            album.publishPhotos(albumId, unpublishedPhotos, false);
        }
    });
});   