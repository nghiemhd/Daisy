var Album;
(function (Album) {
    "use strict";
    var FlickrAlbum = (function () {
        function FlickrAlbum() {
        }
        FlickrAlbum.prototype.search = function (options) {
            var _this = this;
            var data = {
                AlbumName: options.AlbumName,
                PageIndex: options.PageIndex,
                PageSize: options.PageSize
            };
            $.ajax({
                url: options.RequestUrl,
                type: 'POST',
                content: "application/json; charset=utf-8",
                dataType: "json",
                data: data,
                success: function (response) {
                    _this.searchCallback(response);
                },
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
        };
        FlickrAlbum.prototype.searchCallback = function (response) {
            this.cleanUI();
            this.loadAlbums(response.Albums);
            var pagingInfo = {
                HasNextPage: response.Albums.HasNextPage,
                HasPreviousPage: response.Albums.HasPreviousPage,
                PageIndex: response.Albums.PageIndex,
                TotalPages: response.Albums.TotalPages
            };
            var searchOptions = response.SearchOptions;
            var divPaging = $('#paging')[0];
            var loadPaginationArg = {
                Container: divPaging,
                PagingInfo: pagingInfo,
                ClassName: "Album.FlickrAlbum",
                FunctionToExecute: "search",
                FunctionArguments: searchOptions
            };
            Common.Helper.loadPagination(loadPaginationArg);
            Common.Helper.displayPageSizeList(response.Albums.PageSize, response.Albums.TotalCount);
        };
        FlickrAlbum.prototype.cleanUI = function () {
            $('#gridAlbums').empty();
            $('#paging').empty();
            $('#searchResultInfo').hide();
        };
        FlickrAlbum.prototype.loadAlbums = function (data) {
            $.each(data.Items, function (index, item) {
                $('#gridAlbums').append('<div class="col-sm-3 col-md-2 col-lg-2" style="background-color:#101010;">' +
                    '<div class="album-thumbnail photo-list-album-view" style="background-image:url(' + item.AlbumThumbnailUrl + ')"></div>' +
                    '</div>');
            });
        };
        return FlickrAlbum;
    })();
    Album.FlickrAlbum = FlickrAlbum;
})(Album || (Album = {}));
//# sourceMappingURL=Album.js.map