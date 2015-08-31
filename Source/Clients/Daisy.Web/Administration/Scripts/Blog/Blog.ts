module Content {
    'use strict';

    export interface IBlogSearchOptions extends Common.ISearchOptions {
        Title: string;
        IsPublished?: boolean;
        FromCreatedDate: string;
        ToCreatedDate: string;
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
                FromCreatedDate: options.FromCreatedDate,
                ToCreatedDate: options.ToCreatedDate,
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
                    DisplayedTotalString: 'Total ' + response.Blogs.TotalCount + ' blog(s).',
                    PageSizeOptions: [30, 50, 100, 150],
                    SelectedPageSize: response.Blogs.PageSize,
                    ClassName: 'Cotent.Blog',
                    FunctionToExecute: 'search',
                    FunctionArguments: searchOptions
                };
                Common.Helper.loadPageSizes(loadPageSizesArg);
            }
        }

        private cleanUI() {
            $('#gridBlogs tbody').empty();
            $('#paging').empty();
            $('#searchResultInfo').empty();
        }

        private loadBlogs(blogs: IBlog[]) {
            $('#chkSelectAll').prop('checked', false);

            $.each(blogs, function (index, item) {
                var createdDate: number = Common.Helper.getDateTimeValue(item.CreatedDate);
                var date = dateFormat(new Date(createdDate), 'yyyy-mm-dd HH:MM:ss');
                var row =
                    '<tr>' +
                    '<td>' +
                    '<input type="checkbox" id="chk' + item.Id + '" value="' + item.Id + '" class="css-checkbox lrg">' +
                    '<label for="chk' + item.Id + '" class="css-label lrg klaus"></label>' +
                    '</td>' +
                    '<td>' + item.Title + '</td>' +
                    '<td>' + item.IsPublished + '</td>' +
                    '<td>' + date + '</td>' +
                    '</tr>';
                $('#gridBlogs tbody').append(row);
            });

            $('#gridBlogs').show();
        }
        
    }
}  