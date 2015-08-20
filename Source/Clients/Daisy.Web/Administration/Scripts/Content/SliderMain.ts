$(document).ready(function () {
    // Initialize modal dialog
    // attach modal-container bootstrap attributes to links with .modal-link class.
    // when a link is clicked with these attributes, bootstrap will display the href content in a modal dialog.
    $('body').on('click', '.modal-link', function (e) {
        $(this).attr('data-target', '#modal-container');
        $(this).attr('data-toggle', 'modal');
    });
    // Attach listener to .modal-close-btn's so that when the button is pressed the modal dialog disappears
    $('body').on('click', '.modal-close-btn', function () {
        $('#modal-container').modal('hide');
    });
    //clear modal cache, so that new content can be loaded
    $('#modal-container').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
    });

    $('#chkSelectAll').change(function () {
        if (this.checked) {
            $('#gridSliderPhotos input[type=checkbox]').each(function () {
                this.checked = true;
            });
        }
        else {
            $('#gridSliderPhotos input[type=checkbox]').each(function () {
                this.checked = false;
            });
        }
    });

    var slider = new Content.Slider();
    $('#btnDeletePhotos').click(function () {
        var photoIds: number[] = [];
        var sliderId: number = $('#sliderId').val();
        $('#gridSliderPhotos input[type=checkbox]:checked').each(function () {
            var photoId = $(this).val();
            photoIds.push(photoId);
        });
        if (photoIds.length <= 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Delete photos',
                message: 'Please choose photo(s).'
            });
        }
        else {
            slider.deleteSliderPhotos(sliderId, photoIds);
        }
    });    
}); 