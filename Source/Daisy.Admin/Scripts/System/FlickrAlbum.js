$(document).ready(function () {
    $('#btnSearch').click(function () {
        var url = $('#FlickrAlbumSearchUrl').val();
        var albumName = $('#txtAlbumName').val();
        var data = {
            AlbumName: albumName
        };

        $.ajax({
            url: url,
            type: 'POST',
            content: "application/json; charset=utf-8",
            dataType: "json",
            data: data,
            success: LoadAlbums,
            error: function (xhr, desc, err) {
                console.log(xhr);
                console.log("Desc: " + desc + "\nErr:" + err);
            }
        });
    });    
});

function SearchAlbums(pageIndex) {
    var url = $('#FlickrAlbumSearchUrl').val();
    var albumName = $('#txtAlbumName').val();
    var data = {
        AlbumName: albumName,
        PageIndex: pageIndex
    };

    $.ajax({
        url: url,
        type: 'POST',
        content: "application/json; charset=utf-8",
        dataType: "json",
        data: data,
        success: LoadAlbums,
        error: function (xhr, desc, err) {
            console.log(xhr);
            console.log("Desc: " + desc + "\nErr:" + err);
        }
    });
}

function LoadAlbums(data) {
    $('#gridAlbums').empty();
    var metadata = data.Albums;
    LoadPagination(metadata.PageIndex, metadata.TotalPages, metadata.HasPreviousPage, metadata.HasNextPage);
    $.each(data.Albums.Items, function (index, item) {
        $('#gridAlbums').append('<div class="col-sm-3 col-md-2 col-lg-2" style="background-color:#101010;">' +
            '<div class="album-thumbnail photo-list-album-view" style="background-image:url(' + item.AlbumThumbnailUrl + ')"></div>' +
            '</div>');
    });
}

function LoadPagination(currentPage, totalPages, hasPreviousPage, hasNextPage)
{
    var html = '<ul class="pagination">' +
        '<li class="">' + '<a href="">&laquo;</a></li>';
    for (i = 1; i < totalPages + 1; i++) {
        html += '<li>' + i + '</li>';
    }
        
    html += '</ul>';
    $('#albumPagination').append(html);
}