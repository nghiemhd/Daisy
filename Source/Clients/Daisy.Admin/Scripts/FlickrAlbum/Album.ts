module Album {
    'use strict';

    export interface IAlbumSearchOptions extends Common.ISearchOptions {
        AlbumName: string;
        SttCreatedDate?: Date;
        EndCreatedDate?: Date;
    }

    export interface IAlbum {
        FlickrAlbumId: string;
        Name: string;
        AlbumThumbnailUrl: string;
    }    

    export class FlickrAlbum {
        static albums: IAlbum[];

        search(options: IAlbumSearchOptions) {
            var data = {
                AlbumName: options.AlbumName,
                PageIndex: options.PageIndex,
                PageSize: options.PageSize
            };

            $.ajax({
                url: '/Admin/FlickrAlbum/Search',
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

        importAlbums(albums: IAlbum[]) {
            $.ajax({
                url: '/Admin/FlickrAlbum/Import',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: { albums: albums },
                success: (response) => {
                    if (response == "Success") {
                        toastr.success('import successfully');
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
                },
            });
        }

        private searchCallback(response: any) {
            Album.FlickrAlbum.albums  = response.Albums.Items;
            this.cleanUI();
            if (Album.FlickrAlbum.albums.length > 0) {
                this.loadAlbums(Album.FlickrAlbum.albums);

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
                    ClassName: 'Album.FlickrAlbum',
                    FunctionToExecute: 'search',
                    FunctionArguments: searchOptions
                };
                Common.Helper.loadPagination(loadPaginationArg);

                var loadPageSizesArg: Common.ILoadPageSizesArguments = {
                    Container: $('#searchResultInfo')[0],
                    DisplayedTotalString: 'Total ' + response.Albums.TotalCount + ' albums.',
                    PageSizeOptions: [30, 50, 100, 150],
                    SelectedPageSize: response.Albums.PageSize,
                    ClassName: 'Album.FlickrAlbum',
                    FunctionToExecute: 'search',
                    FunctionArguments: searchOptions
                };
                Common.Helper.loadPageSizes(loadPageSizesArg);
            }
        }

        private cleanUI()
        {
            $('#gridAlbums').empty();
            $('#paging').empty();
            $('#searchResultInfo').empty();
            $('#divSelectAll').hide();
        }
        
        private loadAlbums(albums: IAlbum[]) {
            $('#divSelectAll').show();
            $('#chkSelectAll').prop('checked', false);

            $.each(albums, function (index, item) {
                $('#gridAlbums').append(
                    '<div class="col-sm-3 col-md-2 col-lg-2" style= "background-color:#101010;" > ' +
                    '<div class="album-thumbnail photo-list-album-view" style="background-image:url(' + item.AlbumThumbnailUrl + ')"></div>' +
                    '<div class="album-title">' +
                    '<input type="checkbox" value="' + item.FlickrAlbumId + '">&nbsp;' +
                    '<a href="/Admin/FlickrAlbum/Edit/' + item.FlickrAlbumId + '">' + item.Name + '</a>' +
                    '</div>' +
                    '</div>');
            });
        }                
    }
}