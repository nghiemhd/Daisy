﻿$(document).ready(function () {   
    var album = new Album.FlickrAlbum();

    $('#btnSearch').click(function () {      
        var options: Album.IAlbumSearchOptions = {
            AlbumName: $('#txtAlbumName').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        }; 
        Album.FlickrAlbum.searchRequestUrl = $(this).data('request-url');
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
    
    $('#btnImport').click(function () {
        var importedAlbums: Album.IAlbum[] = [];
        $('#gridAlbums input[type=checkbox]:checked').each(function () {
            var flickrAlbumId = $(this).val();
            var item = $.grep(Album.FlickrAlbum.albums, function (e) { return e.FlickrAlbumId == flickrAlbumId });
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
            Album.FlickrAlbum.importAlbumsRequestUrl = $(this).data('request-url');
            album.importAlbums(importedAlbums);
        }
    });
}); 