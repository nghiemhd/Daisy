$(document).ready(function () {
    $('#btnImport').click(function () {
        var importedPhotos = [];
        var index = 0;
        $('#gridPhotos input[type=checkbox]:checked').each(function () {
            var flickrPhotoId = $(this).val();
            importedPhotos.push(flickrPhotoId);
        });
        if (importedPhotos.length == 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Import album',
                message: 'Please choose photo(s) to import.'
            });
        }
        else {
            importPhotos(importedPhotos);
        }
    });
});
var importPhotos = function (importedPhotos) {
    $.ajax({
        url: '/Admin/FlickrAlbum/ImportPhotos',
        type: 'POST',
        content: 'application/json; charset=utf-8',
        dataType: 'json',
        data: { photoIds: importedPhotos },
        success: function (response) {
            if (response == "Success") {
                toastr.success('import successfully');
            }
            else {
                toastr.error(response);
            }
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
        },
    });
};
//# sourceMappingURL=AlbumDetail.js.map