module Common {
    export interface IPagedList<T> {
        Items: T[];
        PageIndex: number;
        PageSize: number;
        TotalCount: number;
        TotalPages: number;
        HasPreviousPage: boolean;
        HasNextPage: boolean;
    }

    export interface ISearchOptions {
        PageIndex: number;
        PageSize: number;
    }

    export interface IPagination {
        PageIndex: number;
        TotalPages: number;
        HasPreviousPage: boolean;
        HasNextPage: boolean;
    }

    export class Helper {
        static displayPageSizeList(selectedPageSize: number, totalCount: number) {
            $('#totalCount').text(totalCount);
            $('#cboPageSize').val(selectedPageSize.toString());
            $('#searchResultInfo').show();
        } 
    }
} 