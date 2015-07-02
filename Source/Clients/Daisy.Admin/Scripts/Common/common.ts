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

    export interface ILoadPaginationArguments {
        Container: HTMLElement;
        PagingInfo: IPagination;
        ClassName: string;
        FunctionToExecute: string;
        FunctionArguments: ISearchOptions;
    }

    export class Helper {
        static displayPageSizeList(selectedPageSize: number, totalCount: number) {
            $('#totalCount').text(totalCount);
            $('#cboPageSize').val(selectedPageSize.toString());
            $('#searchResultInfo').show();
        } 

        static loadPagination(arg: ILoadPaginationArguments) {
            if (arg.PagingInfo.TotalPages > 0) {
                var ul = document.createElement('ul');
                var preli = document.createElement('li');
                var prea = document.createElement('a');
                ul.className = 'pagination';
                //<<
                prea.innerHTML = '&laquo;';
                if (!arg.PagingInfo.HasPreviousPage) {
                    preli.className = 'disabled';
                    preli.appendChild(prea);
                }
                else {
                    prea.onclick = function () {
                        arg.FunctionArguments.PageIndex--;
                        var cls = new (<any>Common.Helper.getClassFromString(arg.ClassName));
                        cls[arg.FunctionToExecute](arg.FunctionArguments);
                    }
                    preli.appendChild(prea);
                }
                ul.appendChild(preli);

                //
                for (var i = 0; i < arg.PagingInfo.TotalPages; i++) {
                    var li = document.createElement('li');
                    var a = document.createElement('a');
                    a.innerHTML = (i + 1).toString();

                    if (i == arg.PagingInfo.PageIndex) {
                        li.className = 'active';
                    }
                    else {
                        a.onclick = (function (pageIndex: number): any {
                            return function () {
                                arg.FunctionArguments.PageIndex = pageIndex;
                                var cls = new (<any>Common.Helper.getClassFromString(arg.ClassName));
                                cls[arg.FunctionToExecute](arg.FunctionArguments);
                            }
                        })(i);;
                    }

                    li.appendChild(a);
                    ul.appendChild(li);
                }

                //>>
                var nextli = document.createElement('li');
                var nexta = document.createElement('a');
                nexta.innerHTML = '&raquo;';
                if (!arg.PagingInfo.HasNextPage) {
                    nextli.className = 'disabled';
                    nextli.appendChild(nexta);
                }
                else {
                    nexta.onclick = function () {
                        arg.FunctionArguments.PageIndex++;
                        var cls = new (<any>Common.Helper.getClassFromString(arg.ClassName));
                        cls[arg.FunctionToExecute](arg.FunctionArguments);
                    }
                    nextli.appendChild(nexta);
                }
                ul.appendChild(nextli);

                arg.Container.appendChild(ul);
            }
        }

        static getFunctionFromString = function (name: string): Function {
            var scope = window;
            var scopeSplit = name.split('.');
            for (var i = 0; i < scopeSplit.length - 1; i++) {
                scope = (<any>scope)[scopeSplit[i]];
                if (scope == undefined) return;
            }

            return (<any>scope)[scopeSplit[scopeSplit.length - 1]];
        }  
        
        static getClassFromString = function (name: string) {
            var scope = window;
            var scopeSplit = name.split('.');
            for (var i = 0; i < scopeSplit.length; i++) {
                scope = (<any>scope)[scopeSplit[i]];
                if (scope == undefined) return;
            }

            return scope;
        }     
    }
} 