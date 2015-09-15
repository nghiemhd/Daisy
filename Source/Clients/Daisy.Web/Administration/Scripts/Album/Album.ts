module Album {
    'use strict';

    export class DaisyAlbum {
        static albums: IAlbum[];
        static searchRequestUrl: string;
        static publishAlbumsRequestUrl: string;
        static publishPhotosRequestUrl: string;
        static deleteAlbumsRequestUrl: string;
        static updateAlbumOrderRequestUrl: string;

        search(options: IAlbumSearchOptions) {
            var data = {
                AlbumName: options.AlbumName,
                IsPublished: options.IsPublished,
                PageIndex: options.PageIndex,
                PageSize: options.PageSize
            };

            $.ajax({
                url: DaisyAlbum.searchRequestUrl,
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: data,
                success: (response) => {
                    this.searchCallback(response);
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
        }

        publishAlbums(albumIds: number[], isPublished: boolean) {
            $.ajax({
                url: DaisyAlbum.publishAlbumsRequestUrl,
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    albumIds: albumIds,
                    isPublished: isPublished
                },
                success: (response) => {
                    if (response == "Success") {
                        if (isPublished) {
                            toastr.success('Publish successfully');
                        }
                        else {
                            toastr.success('Unpublish successfully');
                        }
                        this.updateCheckIcon(albumIds, isPublished);
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
        }

        publishPhotos(albumId: number, photoIds: number[], isPublished: boolean) {
            $.ajax({
                url: DaisyAlbum.publishPhotosRequestUrl,
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    albumId: albumId,
                    photoIds: photoIds,
                    isPublished: isPublished
                },
                success: (response) => {
                    if (response == "Success") {
                        if (isPublished) {
                            toastr.success('Publish successfully');
                        }
                        else {
                            toastr.success('Unpublish successfully');
                        }
                        this.updateCheckIcon(photoIds, isPublished);
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
        }

        deleteAlbums(albumIds: number[]) {
            $.ajax({
                url: DaisyAlbum.deleteAlbumsRequestUrl,
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    albumIds: albumIds
                },
                success: (response) => {
                    if (response == "Success") {
                        toastr.success('Delete successfully');
                        $('#btnSearch').trigger('click');
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
        }

        updateAlbumOrder(albumIds: number[]) {
            $.ajax({
                url: DaisyAlbum.updateAlbumOrderRequestUrl,
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    albumIds: albumIds
                },
                success: (response) => {
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
        }
       
        private searchCallback(response: any) {
            Album.DaisyAlbum.albums = response.Albums.Items;
            this.cleanUI();
            if (Album.DaisyAlbum.albums.length > 0) {
                this.loadAlbums(Album.DaisyAlbum.albums);

                var pagingInfo: Common.IPagination = {
                    HasNextPage: response.Albums.HasNextPage,
                    HasPreviousPage: response.Albums.HasPreviousPage,
                    PageIndex: response.Albums.PageIndex,
                    TotalPages: response.Albums.TotalPages
                };
                var searchOptions = <IAlbumSearchOptions>response.SearchOptions;
                var loadPaginationArg: Common.ILoadPaginationArguments = {
                    Container: $('#paging')[0],
                    PagingInfo: pagingInfo,
                    ClassName: 'Album.DaisyAlbum',
                    FunctionToExecute: 'search',
                    FunctionArguments: searchOptions
                };
                Common.Helper.loadPagination(loadPaginationArg);

                var loadPageSizesArg: Common.ILoadPageSizesArguments = {
                    Container: $('#searchResultInfo')[0],
                    DisplayedTotalString: 'Total ' + response.Albums.TotalCount + ' albums.',
                    PageSizeOptions: [30, 50, 100, 150],
                    SelectedPageSize: response.Albums.PageSize,
                    ClassName: 'Album.DaisyAlbum',
                    FunctionToExecute: 'search',
                    FunctionArguments: searchOptions
                };
                Common.Helper.loadPageSizes(loadPageSizesArg);
            }
        }

        private cleanUI() {
            $('#gridAlbums').empty();
            $('#paging').empty();
            $('#searchResultInfo').empty();
            $('#divSelectAll').hide();
        }

        private loadAlbums(albums: IAlbum[]) {
            $('#divSelectAll').show();
            $('#chkSelectAll').prop('checked', false);

            $.each(albums, function (index, item) {
                var grid = '<div class="col-sm-3 col-md-2 col-lg-2 draggable-item" style= "background-color:#101010;" draggable="true"> ' +
                    '<div id="' + item.Id + '" class="album-thumbnail photo-list-album-view" style="background-image:url(' + item.AlbumThumbnailUrl + ')">';
                if (item.IsPublished) {
                    grid += '<img src="' + Common.Helper.applicationRoot + 'Administration/Images/symbol_check.png" alt="published" style="width:30px;float:right;" />';
                }
                grid += '</div>' +
                '<div class="album-title">' +
                '<input type="checkbox" id="chk' + item.Id + '" value="' + item.Id + '" class="css-checkbox lrg">' +
                '<label for="chk' + item.Id + '" class="css-label lrg klaus"></label>' +
                '<a href="' + Common.Helper.applicationRoot + 'Admin/Album/Edit/' + item.Id + '">' + item.Name + '</a>' +
                '</div>' +
                '</div>';
                $('#gridAlbums').append(grid);
            });

            Common.DragDropHandler.items = document.querySelectorAll('#gridAlbums .draggable-item');
            [].forEach.call(Common.DragDropHandler.items, function (item) {
                item.addEventListener('dragstart', Common.DragDropHandler.handleDragStart, false);
                item.addEventListener('dragenter', Common.DragDropHandler.handleDragEnter, false);
                item.addEventListener('dragover', Common.DragDropHandler.handleDragOver, false);
                item.addEventListener('dragleave', Common.DragDropHandler.handleDragLeave, false);
                item.addEventListener('drop', Common.DragDropHandler.handleDrop, false);
                item.addEventListener('dragend', Common.DragDropHandler.handleDragEnd, false);
            });
        }

        private updateCheckIcon(ids: number[], checked: boolean)
        {
            var imgIcon = '<img src="' + Common.Helper.applicationRoot + 'Administration/Images/symbol_check.png" alt="published" style="width:30px;float:right;" />';

            for (var i = 0; i < ids.length; i++)
            {
                var img = $('#' + ids[i] + ' img')[0];
                if (checked) {
                    if (img == null) {
                        $('#' + ids[i] + '').append(imgIcon);
                    }
                }
                else {
                    if (img != null) {
                        $('#' + ids[i] + ' img').remove();
                    }
                }
            }
        }

        private removeAlbums(albumIds: number[])
        {
            for (var i = 0; i < albumIds.length; i++) {
                $('#' + albumIds[i]).parent().remove();
            }
        }
    }
} 