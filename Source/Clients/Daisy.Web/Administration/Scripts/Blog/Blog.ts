module Content {
    'use strict';

    export interface IBlogSearchOptions extends Common.ISearchOptions {
        Title: string;
        IsPublished?: boolean;
        SttCreatedDate?: Date;
        EndCreatedDate?: Date;
    }

    export interface IBlog {
        Id?: number;
        Title: string;
        CreatedDate: string;
        IsPublished: boolean;
    }    

    export class Blog {
        static blogs: IBlog[];
        static searchRequestUrl: string;

        search(options: IBlogSearchOptions) {
            var data = {
                Title: options.Title,
                SttCreatedDate: options.SttCreatedDate,
                EndCreatedDate: options.EndCreatedDate,
                IsPublished: options.IsPublished,
                PageIndex: options.PageIndex,
                PageSize: options.PageSize
            };

            $.ajax({
                url: Blog.searchRequestUrl,
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
            Content.Blog.blogs = response.Blogs.Items;
            this.cleanUI();
            if (Content.Blog.blogs.length > 0) {
                this.loadBlogs(Content.Blog.blogs);

                var pagingInfo: Common.IPagination = {
                    HasNextPage: response.Blogs.HasNextPage,
                    HasPreviousPage: response.Blogs.HasPreviousPage,
                    PageIndex: response.Blogs.PageIndex,
                    TotalPages: response.Blogs.TotalPages
                };
                var searchOptions = <IBlogSearchOptions>response.SearchOptions;
                var loadPaginationArg: Common.ILoadPaginationArguments = {
                    Container: $('#paging')[0],
                    PagingInfo: pagingInfo,
                    ClassName: 'Content.Blog',
                    FunctionToExecute: 'search',
                    FunctionArguments: searchOptions
                };
                Common.Helper.loadPagination(loadPaginationArg);

                var loadPageSizesArg: Common.ILoadPageSizesArguments = {
                    Container: $('#searchResultInfo')[0],
                    DisplayedTotalString: 'Total ' + response.Albums.TotalCount + ' albums.',
                    PageSizeOptions: [30, 50, 100, 150],
                    SelectedPageSize: response.Albums.PageSize,
                    ClassName: 'Cotent.Blog',
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

        private loadBlogs(blogs: IBlog[]) {
            $('#divSelectAll').show();
            $('#chkSelectAll').prop('checked', false);

            //$.each(blogs, function (index, item) {
            //    var grid = '<div class="col-sm-3 col-md-2 col-lg-2" style= "background-color:#101010;" > ' +
            //        '<div id="' + item.Id + '" class="album-thumbnail photo-list-album-view" style="background-image:url(' + item.AlbumThumbnailUrl + ')">';
            //    if (item.IsPublished) {
            //        grid += '<img src="' + Common.Helper.applicationRoot + 'Administration/Images/symbol_check.png" alt="published" style="width:30px;float:right;" />';
            //    }
            //    grid += '</div>' +
            //    '<div class="album-title">' +
            //    '<input type="checkbox" value="' + item.Id + '">&nbsp;' +
            //    '<a href="' + Common.Helper.applicationRoot + 'Admin/Album/Edit/' + item.Id + '">' + item.Name + '</a>' +
            //    '</div>' +
            //    '</div>';
            //    $('#gridAlbums').append(grid);
            //});
        }
        
    }
}  