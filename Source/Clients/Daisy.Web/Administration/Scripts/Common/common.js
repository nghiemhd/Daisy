var Common;
(function (Common) {
    (function (ResponseStatus) {
        ResponseStatus[ResponseStatus["Success"] = 0] = "Success";
        ResponseStatus[ResponseStatus["Failure"] = 1] = "Failure";
        ResponseStatus[ResponseStatus["OutOfRange"] = 2] = "OutOfRange";
    })(Common.ResponseStatus || (Common.ResponseStatus = {}));
    var ResponseStatus = Common.ResponseStatus;
    var Constant = (function () {
        function Constant() {
        }
        Object.defineProperty(Constant, "MAX_ALBUM_IMPORT", {
            get: function () { return 30; },
            enumerable: true,
            configurable: true
        });
        return Constant;
    })();
    Common.Constant = Constant;
    var Helper = (function () {
        function Helper() {
        }
        Helper.loadPageSizes = function (arg) {
            if (arg.PageSizeOptions.indexOf(arg.SelectedPageSize) != -1) {
                var span = document.createElement('span');
                span.innerHTML = arg.DisplayedTotalString + ' Display ';
                arg.Container.appendChild(span);
                var select = document.createElement('select');
                select.classList.add('form-control', 'dropdown');
                for (var i = 0; i < arg.PageSizeOptions.length; i++) {
                    var option = document.createElement('option');
                    option.innerHTML = arg.PageSizeOptions[i].toString();
                    if (arg.PageSizeOptions[i] == arg.SelectedPageSize) {
                        option.setAttribute('selected', 'selected');
                    }
                    select.appendChild(option);
                }
                select.onchange = function () {
                    arg.FunctionArguments.PageIndex = 0;
                    arg.FunctionArguments.PageSize = this.value;
                    var cls = new (Common.Helper.getClassFromString(arg.ClassName));
                    cls[arg.FunctionToExecute](arg.FunctionArguments);
                };
                arg.Container.appendChild(select);
                var endSpan = document.createElement('span');
                endSpan.innerHTML = ' per page';
                arg.Container.appendChild(endSpan);
            }
        };
        Helper.loadPagination = function (arg) {
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
                        var cls = new (Common.Helper.getClassFromString(arg.ClassName));
                        cls[arg.FunctionToExecute](arg.FunctionArguments);
                    };
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
                        a.onclick = (function (pageIndex) {
                            return function () {
                                arg.FunctionArguments.PageIndex = pageIndex;
                                var cls = new (Common.Helper.getClassFromString(arg.ClassName));
                                cls[arg.FunctionToExecute](arg.FunctionArguments);
                            };
                        })(i);
                        ;
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
                        var cls = new (Common.Helper.getClassFromString(arg.ClassName));
                        cls[arg.FunctionToExecute](arg.FunctionArguments);
                    };
                    nextli.appendChild(nexta);
                }
                ul.appendChild(nextli);
                arg.Container.appendChild(ul);
            }
        };
        Helper.getFunctionFromString = function (name) {
            var scope = window;
            var scopeSplit = name.split('.');
            for (var i = 0; i < scopeSplit.length - 1; i++) {
                scope = scope[scopeSplit[i]];
                if (scope == undefined)
                    return;
            }
            return scope[scopeSplit[scopeSplit.length - 1]];
        };
        Helper.getClassFromString = function (name) {
            var scope = window;
            var scopeSplit = name.split('.');
            for (var i = 0; i < scopeSplit.length; i++) {
                scope = scope[scopeSplit[i]];
                if (scope == undefined)
                    return;
            }
            return scope;
        };
        return Helper;
    })();
    Common.Helper = Helper;
})(Common || (Common = {}));
//# sourceMappingURL=common.js.map