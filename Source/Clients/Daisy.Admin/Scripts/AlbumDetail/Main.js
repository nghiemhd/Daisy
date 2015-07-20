$(document).ready(function () {
    var album = new Album.FlickrAlbum();
    $('#btnImport').click(function () {
        var importedAlbums = [];
        var index = 0;
        $('#gridAlbums input[type=checkbox]:checked').each(function () {
            var flickrAlbumId = $(this).val();
            var item = $.grep(album.albums, function (e) { return e.FlickrAlbumId == flickrAlbumId; });
            importedAlbums.push(item[0]);
        });
        if (importedAlbums.length == 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Import album',
                message: 'Please choose album(s) to import.'
            });
        }
        else if (importedAlbums.length > Common.Constant.MAX_ALBUM_IMPORT) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Import album',
                message: 'Cannot import more than ' + Common.Constant.MAX_ALBUM_IMPORT + ' albums.'
            });
        }
        else {
            album.importAlbums(importedAlbums);
        }
    });
});
//# sourceMappingURL=Main.js.map