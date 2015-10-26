$(document).ready(function () {
    var photo = new Photo.DaisyPhoto();
     
    $(document).on('click', '#btnSearch', function (e) {
        e.preventDefault();
        var options: Photo.IPhotoSearchOptions = {
            AlbumName: $('#txtAlbumName').val(),
            IsPublished: $('#cboPublishStatus').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        };
        
        Photo.DaisyPhoto.searchRequestUrl = $(this).data('request-url');
        photo.search(options);
    });

    $(document).on('change', '#chkSelectAllPhotos', function (e) {
        if (this.checked) {
            $('#gridPhotos input[type=checkbox]').each(function () {
                this.checked = true;
            });
        }
        else {
            $('#gridPhotos input[type=checkbox]').each(function () {
                this.checked = false;
            });
        }
    });
        
});  