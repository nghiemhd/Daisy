module Photo {
    'use strict';

    export interface IPhoto {
        Id: number;
        Name: string;
        IsPublished?: boolean;
        SmallUrl: string;
        MediumUrl: string;
        LargeUrl: string;
        Large1600Url: string;
        Large2048Url: string;
        OriginalUrl: string;
    }

    export interface IPhotoSearchOptions extends Common.ISearchOptions {
        AlbumName: string;
        IsPublished?: boolean;
    }

    export class DaisyPhoto {
        static photos: IPhoto[];
        static searchRequestUrl: string;
        static publishAlbumsRequestUrl: string;
        static publishPhotosRequestUrl: string;

        search(options: IPhotoSearchOptions) {
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

        private searchCallback(response: any) {
            Photo.DaisyPhoto.photos = response.Photos.Items;
            this.cleanUI();
            if (Photo.DaisyPhoto.photos.length > 0) {
                this.loadPhotos(Photo.DaisyPhoto.photos);

                var pagingInfo: Common.IPagination = {
                    HasNextPage: response.Albums.HasNextPage,
                    HasPreviousPage: response.Albums.HasPreviousPage,
                    PageIndex: response.Albums.PageIndex,
                    TotalPages: response.Albums.TotalPages
                };
                var searchOptions = <IPhotoSearchOptions>response.SearchOptions;
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
                    ClassName: 'Photo.DaisyPhoto',
                    FunctionToExecute: 'search',
                    FunctionArguments: searchOptions
                };
                Common.Helper.loadPageSizes(loadPageSizesArg);
            }
        }

        private cleanUI() {
            $('#gridPhotos').empty();
            $('#paging').empty();
            $('#searchResultInfo').empty();
            $('#divSelectAll').hide();
        }

        private loadPhotos(photos: IPhoto[]) {
            $('#divSelectAll').show();
            $('#chkSelectAll').prop('checked', false);

            $.each(photos, function (index, item) {
                var grid = '<div class="col-sm-3 col-md-2 col-lg-2" style= "background-color:#101010;" > ' +
                    '<div class="album-thumbnail photo-list-album-view" style="background-image:url(' + item.LargeUrl + ')">';
                grid += '</div>' +
                '<div class="album-title">' +
                '<input type="checkbox" value="' + item.Id + '">&nbsp;' + item.Id +
                '</div>' +
                '</div>';
                $('#gridPhotos').append(grid);
            });
        }
    }
}  