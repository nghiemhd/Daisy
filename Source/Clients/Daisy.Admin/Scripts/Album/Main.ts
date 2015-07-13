$(document).ready(function () {   
    var album = new Album.FlickrAlbum();

    $('#btnSearch').click(function () {      
        var options: Album.IAlbumSearchOptions = {
            AlbumName: $('#txtAlbumName').val(),
            RequestUrl: $('#FlickrAlbumSearchUrl').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        }; 
        
        album.search(options);        
    });
    
    $('#btnImport').click(function () {
        var importedAlbum: Album.IAlbum[];
        var index: number = 0;
        $("#gridAlbums input[type=checkbox]:checked").each(function () {
            var flickrAlbumId = $(this).val();
            var item = $.grep(album.albums, function (e) { return e.FlickrAlbumId == flickrAlbumId });
            //importedAlbum[index] = <Album.IAlbum>item;

        });
        
    });
}); 