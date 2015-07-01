$(document).ready(function () {   
    $('#btnSearch').click(function () {      
        var options: Album.IAlbumSearchOptions = {
            AlbumName: $('#txtAlbumName').val(),
            RequestUrl: $('#FlickrAlbumSearchUrl').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        }; 
        var album = new Album.FlickrAlbum();
        album.search(options);
    });

    $('#cboPageSize').change(function () {
        var options: Album.IAlbumSearchOptions = {
            AlbumName: $('#txtAlbumName').val(),
            RequestUrl: $('#FlickrAlbumSearchUrl').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        }; 
        var album = new Album.FlickrAlbum();
        album.search(options);
    });
}); 