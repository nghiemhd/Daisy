$(document).ready(function () {
    var photo = new Photo.DaisyPhoto();

    $('#btnSearch').click(function () {
        var options: Photo.IPhotoSearchOptions = {
            AlbumName: $('#txtAlbumName').val(),
            IsPublished: $('#cboPublishStatus').val(),
            PageIndex: 0,
            PageSize: $('#cboPageSize').val()
        };

        Photo.DaisyPhoto.searchRequestUrl = $(this).data('request-url');
        photo.search(options);
    });

    $('#chkSelectAllPhotos').change(function () {
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

    $('#btnSave').click(function () {
        var photoIds: number[] = [];
        $('#gridPhotos input[type=checkbox]:checked').each(function () {
            var photoId = $(this).val();
            photoIds.push(photoId);
        });
        if (photoIds.length <= 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Add photos to slider',
                message: 'Please choose photo(s).'
            });
        }
        else if (photoIds.length > 10)
        {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Add photos to slider',
                message: 'Cannot add more than 10 photos.'
            });
        }
        else {
            debugger
            Photo.DaisyPhoto.updateSliderRequestUrl = $(this).data('request-url');
            photo.updateSliderPhotos(photoIds);            
        }
    });    
});  