interface ISearchOptions {
    requestUrl: string;
    albumName: string;
    sttCreatedDate?: Date;
    endCreatedDate?: Date;
    pageIndex: number;
    pageSize: number;
}

interface IAlbum {
    search(options: ISearchOptions, callback: () => void);
}

module Album {
    export class FlickrAlbum implements IAlbum {
        search(options: ISearchOptions, callback: () => void) {
            var data = {
                AlbumName: options.albumName,
                PageIndex: options.pageIndex,
                PageSize: options.pageSize
            };

            $.ajax({
                url: options.requestUrl,
                type: 'POST',
                content: "application/json; charset=utf-8",
                dataType: "json",
                data: data,
                success: callback,
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
    }
}