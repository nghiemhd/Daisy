var Photo;
(function (Photo) {
    'use strict';
    var DaisyPhoto = (function () {
        function DaisyPhoto() {
        }
        DaisyPhoto.prototype.search = function (options) {
            var _this = this;
            var data = {
                AlbumName: options.AlbumName,
                IsPublished: options.IsPublished,
                PageIndex: options.PageIndex,
                PageSize: options.PageSize
            };
            $.ajax({
                url: DaisyPhoto.searchRequestUrl,
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: data,
                success: function (response) {
                    _this.searchCallback(response);
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
                }
            });
        };
        DaisyPhoto.prototype.updateSliderPhotos = function (photoIds) {
            $.ajax({
                url: DaisyPhoto.updateSliderRequestUrl,
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    photoIds: photoIds
                },
                success: function (response) {
                    debugger;
                    if (response == "Success") {
                        toastr.success('Update successfully');
                    }
                    else {
                        toastr.options = {
                            closeButton: true,
                            positionClass: "toast-top-full-width",
                            timeOut: 0,
                            extendedTimeOut: 0
                        };
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
                }
            });
        };
        DaisyPhoto.prototype.searchCallback = function (response) {
            Photo.DaisyPhoto.photos = response.Photos.Items;
            this.cleanUI();
            if (Photo.DaisyPhoto.photos.length > 0) {
                this.loadPhotos(Photo.DaisyPhoto.photos);
                var pagingInfo = {
                    HasNextPage: response.Photos.HasNextPage,
                    HasPreviousPage: response.Photos.HasPreviousPage,
                    PageIndex: response.Photos.PageIndex,
                    TotalPages: response.Photos.TotalPages
                };
                var searchOptions = response.SearchOptions;
                var loadPaginationArg = {
                    Container: $('#paging')[0],
                    PagingInfo: pagingInfo,
                    ClassName: 'Photo.DaisyPhoto',
                    FunctionToExecute: 'search',
                    FunctionArguments: searchOptions
                };
                Common.Helper.loadPagination(loadPaginationArg);
                var loadPageSizesArg = {
                    Container: $('#searchResultInfo')[0],
                    DisplayedTotalString: 'Total ' + response.Photos.TotalCount + ' photos.',
                    PageSizeOptions: [30, 50, 100, 150],
                    SelectedPageSize: response.Photos.PageSize,
                    ClassName: 'Photo.DaisyPhoto',
                    FunctionToExecute: 'search',
                    FunctionArguments: searchOptions
                };
                Common.Helper.loadPageSizes(loadPageSizesArg);
            }
        };
        DaisyPhoto.prototype.cleanUI = function () {
            $('#gridPhotos').empty();
            $('#paging').empty();
            $('#searchResultInfo').empty();
            $('#divSelectAll').hide();
        };
        DaisyPhoto.prototype.loadPhotos = function (photos) {
            $('#divSelectAll').show();
            $('#chkSelectAll').prop('checked', false);
            $.each(photos, function (index, item) {
                var grid = '<div class="col-sm-6 col-md-6 col-lg-4" style="background-color:#101010;"> ' +
                    '<div class="album-thumbnail photo-list-album-view" style="background-image:url(' + item.LargeUrl + ')">';
                grid += '</div>' +
                    '<div class="album-title">' +
                    '<input type="checkbox" value="' + item.Id + '">&nbsp;' + item.Id +
                    '</div>' +
                    '</div>';
                $('#gridPhotos').append(grid);
            });
        };
        return DaisyPhoto;
    })();
    Photo.DaisyPhoto = DaisyPhoto;
})(Photo || (Photo = {}));
//# sourceMappingURL=Photo.js.map