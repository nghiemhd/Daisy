$(document).ready(function () {
    $('#btnSearch').click(function () {
        var options = {
            AlbumName: $('#txtAlbumName').val(),
            RequestUrl: $('#FlickrAlbumSearchUrl').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        };
        var album = new Album.FlickrAlbum();
        album.search(options);
    });
    $('#cboPageSize').change(function () {
        var options = {
            AlbumName: $('#txtAlbumName').val(),
            RequestUrl: $('#FlickrAlbumSearchUrl').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        };
        var album = new Album.FlickrAlbum();
        album.search(options);
    });
});
//# sourceMappingURL=Index.js.map