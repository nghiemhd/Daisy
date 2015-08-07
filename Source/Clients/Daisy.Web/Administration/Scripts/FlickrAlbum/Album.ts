module Album {
    'use strict';

    export interface IAlbumSearchOptions extends Common.ISearchOptions {
        AlbumName: string;
        IsPublished?: boolean;
        SttCreatedDate?: Date;
        EndCreatedDate?: Date;
    }

    export interface IAlbum {
        Id?: number;
        FlickrAlbumId: string;
        Name: string;
        AlbumThumbnailUrl: string;
        IsPublished?: boolean;
    }    

    export class FlickrAlbum {
        static albums: IAlbum[];
        static searchRequestUrl: string;
        static importAlbumsRequestUrl: string;

        search(options: IAlbumSearchOptions) {
            var data = {
                AlbumName: options.AlbumName,
                PageIndex: options.PageIndex,
                PageSize: options.PageSize
            };

            $.ajax({
                url: FlickrAlbum.searchRequestUrl,
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
                url: FlickrAlbum.importAlbumsRequestUrl,
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: { albums: albums },
                success: (response) => {
                    if (response == "Success") {
                        toastr.success('Import successfully');
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
            FlickrAlbum.albums  = response.Albums.Items;
            this.cleanUI();
            if (FlickrAlbum.albums.length > 0) {
                this.loadAlbums(FlickrAlbum.albums);

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
                    '<a href="' + Common.Helper.applicationRoot + 'Admin/FlickrAlbum/Edit/' + item.FlickrAlbumId + '">' + item.Name + '</a>' +
                    '</div>' +
                    '</div>');
            });
        }                
    }
}