module Album {
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

                    this.loadPagination(divPaging, pagingInfo, "search", searchOptions);
                    Common.Helper.displayPageSizeList(response.Albums.PageSize, response.Albums.TotalCount);
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

        private cleanUI()
        {
            $('#gridAlbums').empty();
            $('#paging').empty();
            $('#searchResultInfo').hide();
        }
        
        loadAlbums(data: Common.IPagedList<IAlbum>) {
            $.each(data.Items, function (index, item) {
                $('#gridAlbums').append('<div class="col-sm-3 col-md-2 col-lg-2" style="background-color:#101010;">' +
                    '<div class="album-thumbnail photo-list-album-view" style="background-image:url(' + item.AlbumThumbnailUrl + ')"></div>' +
                    '</div>');
            });
        } 

        loadPagination(container: HTMLElement, pagingInfo: Common.IPagination, functionName: string, options: Common.ISearchOptions) {
            if (pagingInfo.TotalPages > 0)
            {
                var ul = document.createElement('ul');
                var preli = document.createElement('li');
                var prea = document.createElement('a');
                ul.className = 'pagination';
                //<<
                prea.innerHTML = '&laquo;';
                if (!pagingInfo.HasPreviousPage) {
                    preli.className = 'disabled';                   
                    preli.appendChild(prea);
                }
                else
                {             
                    prea.onclick = function () {
                        var album = new FlickrAlbum();
                        options.PageIndex--;
                        album.search(<IAlbumSearchOptions>options);
                        return false;
                    }       
                    preli.appendChild(prea);
                }
                ul.appendChild(preli);

                //
                for (var i = 0; i < pagingInfo.TotalPages; i++) {
                    var li = document.createElement('li');
                    var a = document.createElement('a');
                    a.innerHTML = (i + 1).toString();
                    
                    if (i == pagingInfo.PageIndex) {
                        li.className = 'active';
                    }
                    else {
                        a.onclick = (function (id:number) {
                            var album = new FlickrAlbum();
                            options.PageIndex = +a.innerHTML - 1;
                            album.search(<IAlbumSearchOptions>options);
                            return false;
                        })(i);
                    }
                    
                    li.appendChild(a);    
                    ul.appendChild(li);
                }

                //>>
                var nextli = document.createElement('li');
                var nexta = document.createElement('a');
                nexta.innerHTML = '&raquo;';
                if (!pagingInfo.HasNextPage) {
                    nextli.className = 'disabled';                    
                    nextli.appendChild(nexta);
                }
                else {         
                    nexta.onclick = function () {
                        var album = new FlickrAlbum();
                        options.PageIndex++;
                        album.search(<IAlbumSearchOptions>options);
                        return false;
                    }              
                    nextli.appendChild(nexta);
                }
                ul.appendChild(nextli);

                container.appendChild(ul);
            }
        }


        //getFunctionFromString = function (name: string) {
        //    var scope = window;
        //    var scopeSplit = name.split('.');
        //    for (var i = 0; i < scopeSplit.length - 1; i++) {
        //        var index = scopeSplit[i];
        //        scope = scope[index];

        //        if (scope == undefined) return;
        //    }

        //    return scope[scopeSplit[scopeSplit.length - 1]];
        //}

        loadPaging(p: Common.IPagination, functionName: string, options: Common.ISearchOptions) {
            if (p.TotalPages > 0) {
                var html = '<ul class="pagination">';
                if (!p.HasPreviousPage) {
                    html += '<li class="disabled"><a>&laquo;</a>';
                }
                else {
                    html += '<li>';
                    html += '<a href="" onclick="' + functionName + '(' + (p.PageIndex - 1) + '); return false;">&laquo;</a>';
                }
                html += '</li>';
                for (var i = 1; i < p.TotalPages + 1; i++) {
                    if (i == p.PageIndex + 1) {
                        html += '<li class="active">';
                        html += '<a>' + i + '</a>';
                    }
                    else {
                        html += '<li>';
                        html += '<a href="" onclick="SearchAlbums(' + (i - 1) + '); return false;">' + i + '</a>';
                    }
                    html += '</li>';
                }
                if (!p.HasNextPage) {
                    html += '<li class="disabled"><a>&raquo;</a>';
                }
                else {
                    html += '<li>';
                    html += '<a href="" onclick="SearchAlbums(' + (p.PageIndex + 1) + '); return false;">&raquo;</a>';
                }
                html += '</li>';
                html += '</ul>';
                $('#paging').append(html);
            }
        }

    }
}