﻿module Album {
    "use strict";

    export interface IAlbumSearchOptions extends Common.ISearchOptions {
        RequestUrl: string;
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
        search(options: IAlbumSearchOptions) {
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
                success: (response) => {
                    this.searchCallback(response);
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
        }

        private searchCallback(response: any) {
            this.cleanUI();
            this.loadAlbums(response.Albums);

            var pagingInfo: Common.IPagination = {
                HasNextPage: response.Albums.HasNextPage,
                HasPreviousPage: response.Albums.HasPreviousPage,
                PageIndex: response.Albums.PageIndex,
                TotalPages: response.Albums.TotalPages
            };
            var searchOptions = <IAlbumSearchOptions>response.SearchOptions;
            var divPaging = $('#paging')[0];
            var loadPaginationArg: Common.ILoadPaginationArguments = {
                Container: divPaging,
                PagingInfo: pagingInfo,
                ClassName: "Album.FlickrAlbum",
                FunctionToExecute: "search",
                FunctionArguments: searchOptions
            };

            Common.Helper.loadPagination(loadPaginationArg);
            Common.Helper.displayPageSizeList(response.Albums.PageSize, response.Albums.TotalCount);
        }

        private cleanUI()
        {
            $('#gridAlbums').empty();
            $('#paging').empty();
            $('#searchResultInfo').hide();
        }
        
        private loadAlbums(data: Common.IPagedList<IAlbum>) {
            $.each(data.Items, function (index, item) {
                $('#gridAlbums').append('<div class="col-sm-3 col-md-2 col-lg-2" style="background-color:#101010;">' +
                    '<div class="album-thumbnail photo-list-album-view" style="background-image:url(' + item.AlbumThumbnailUrl + ')"></div>' +
                    '</div>');
            });
        }                
    }
}