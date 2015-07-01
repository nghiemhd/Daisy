$(document).ready(function () {
    $('#btnSearch').click(function () {
        SearchAlbums(0);
    });

    $('#cboPageSize').change(function () {
        SearchAlbums(0);
    });
});

function SearchAlbums(pageIndex) {
    var url = $('#FlickrAlbumSearchUrl').val();
    var albumName = $('#txtAlbumName').val();
    var pageSize = $('#cboPageSize').val();
    var data = {
        AlbumName: albumName,
        PageIndex: pageIndex,
        PageSize: pageSize
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
        },
        beforeSend: function () {
            $('#loader').show();
        },
        complete: function () {
            $('#loader').hide();            
        },
    });
}

function LoadAlbums(response) {
    $('#gridAlbums').empty();
    $('#paging').empty();
    $('#searchResultInfo').hide();
    var data = response.Albums;
    if (data.TotalPages >= 1) {        
        LoadPaging(data.PageIndex, data.TotalPages, data.HasPreviousPage, data.HasNextPage);
        LoadPageSizeList(data.PageSize, data.TotalCount);
    }
    $.each(data.Items, function (index, item) {
        $('#gridAlbums').append('<div class="col-sm-3 col-md-2 col-lg-2" style="background-color:#101010;">' +
            '<div class="album-thumbnail photo-list-album-view" style="background-image:url(' + item.AlbumThumbnailUrl + ')"></div>' +
            '</div>');
    });
}

function LoadPaging(pageIndex, totalPages, hasPreviousPage, hasNextPage)
{
    var html = '<ul class="pagination">';
    if (!hasPreviousPage) {
        html += '<li class="disabled"><a>&laquo;</a>';
    }
    else {
        html += '<li>';        
        html += '<a href="" onclick="SearchAlbums(' + (pageIndex - 1) + '); return false;">&laquo;</a>';
    }    
    html += '</li>';
    for (i = 1; i < totalPages + 1; i++) {
        if (i == pageIndex + 1) {
            html += '<li class="active">';
            html += '<a>' + i + '</a>';
        }
        else {
            html += '<li>';
            html += '<a href="" onclick="SearchAlbums(' + (i - 1) + '); return false;">' + i + '</a>';
        }        
        html += '</li>';
    }
    if (!hasNextPage) {
        html += '<li class="disabled"><a>&raquo;</a>';
    }
    else {
        html += '<li>';
        html += '<a href="" onclick="SearchAlbums(' + (pageIndex + 1) + '); return false;">&raquo;</a>';
    }
    html += '</li>';
    html += '</ul>';
    $('#paging').append(html);    
}

function LoadPageSizeList(selectedPageSize, totalCount)
{
    $('#totalCount').text(totalCount);
    $('#cboPageSize').val(selectedPageSize);
    $('#searchResultInfo').show();
}