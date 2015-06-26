var Album;
(function (Album) {
    var FlickrAlbum = (function () {
        function FlickrAlbum() {
        }
        FlickrAlbum.prototype.search = function (options, callback) {
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
        };
        return FlickrAlbum;
    })();
    Album.FlickrAlbum = FlickrAlbum;
})(Album || (Album = {}));
//# sourceMappingURL=Album.js.map