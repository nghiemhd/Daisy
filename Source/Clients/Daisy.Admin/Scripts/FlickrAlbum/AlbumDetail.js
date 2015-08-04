var Album;
(function (Album) {
    var FlickrAlbumDetail = (function () {
        function FlickrAlbumDetail() {
        }
        FlickrAlbumDetail.prototype.importAlbumDetail = function (albumDetail) {
            var data = {
                Album: albumDetail.Album,
                Photos: albumDetail.Photos
            };
            $.ajax({
                url: FlickrAlbumDetail.importAlbumDetailRequestUrl,
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: data,
                success: function (response) {
                    if (response == "Success") {
                        toastr.success('Import successfully');
                    }
                    else {
                        toastr.error(response);
                    }
                },
                error: function (xhr, desc, err) {
                    console.log(xhr);
                    console.log('Desc: ' + desc + '\nErr:' + err);
                },
                beforeSend: function () {
                    $('#loader').show();
                },
                complete: function () {
                    $('#loader').hide();
                },
            });
        };
        return FlickrAlbumDetail;
    })();
    Album.FlickrAlbumDetail = FlickrAlbumDetail;
})(Album || (Album = {}));
$(document).ready(function () {
    var albumDetail = new Album.FlickrAlbumDetail();
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
    $('#btnImport').click(function () {
        var albumModel = Album.FlickrAlbumDetail.album;
        var importedAlbum = {
            Album: albumModel.Album,
            Photos: []
        };
        $('#gridPhotos input[type=checkbox]:checked').each(function () {
            var flickrPhotoId = $(this).val();
            var photo = $.grep(albumModel.Photos, function (e) { return e.FlickrPhotoId == flickrPhotoId; });
            importedAlbum.Photos.push(photo[0]);
        });
        if (importedAlbum.Photos.length == 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Import album',
                message: 'Please choose photo(s) to import.'
            });
        }
        else {
            Album.FlickrAlbumDetail.importAlbumDetailRequestUrl = $(this).data('request-url');
            albumDetail.importAlbumDetail(importedAlbum);
        }
    });
});
//# sourceMappingURL=AlbumDetail.js.map