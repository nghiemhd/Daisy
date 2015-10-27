$(document).ready(function () {
    $('#chkSelectAll').change(function () {
        if (this.checked) {
            $('#gridCategoryPhotos input[type=checkbox]').each(function () {
                this.checked = true;
            });
        }
        else {
            $('#gridCategoryPhotos input[type=checkbox]').each(function () {
                this.checked = false;
            });
        }
    });
    var category = new Content.Category();
    $('#btnDeletePhotos').click(function () {
        var photoIds = [];
        var categoryId = $('#categoryId').val();
        $('#gridCategoryPhotos input[type=checkbox]:checked').each(function () {
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
            category.deletePhotos(categoryId, photoIds);
        }
    });
    $('#btnUpdateOrders').click(function () {
        var photoIds = [];
        $('#gridCategoryPhotos input[type=checkbox]').each(function () {
            var photoId = $(this).val();
            photoIds.push(photoId);
        });
        if (photoIds.length > 0) {
            var categoryId = $('#categoryId').val();
            category.updatePhotoOrder(categoryId, photoIds);
        }
    });
    $(document).on('click', '#btnSave', function (e) {
        e.preventDefault();
        var photoIds = [];
        $('#gridPhotos input[type=checkbox]:checked').each(function () {
            var photoId = $(this).val();
            photoIds.push(photoId);
        });
        if (photoIds.length <= 0) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Add photos to category',
                message: 'Please choose photo(s).'
            });
        }
        else {
            var categoryId = $('#categoryId').val();
            category.updatePhotos(categoryId, photoIds);
            $('#modal-container').modal('hide');
        }
    });
    Common.Helper.countCharacters(400, '#txtMetaTitle', '#metatitle_feedback');
    Common.Helper.countCharacters(400, '#txtMetaKeywords', '#metakeywords_feedback');
    Common.DragDropHandler.items = document.querySelectorAll('#gridCategoryPhotos .draggable-item');
    [].forEach.call(Common.DragDropHandler.items, function (item) {
        item.addEventListener('dragstart', Common.DragDropHandler.handleDragStart, false);
        item.addEventListener('dragenter', Common.DragDropHandler.handleDragEnter, false);
        item.addEventListener('dragover', Common.DragDropHandler.handleDragOver, false);
        item.addEventListener('dragleave', Common.DragDropHandler.handleDragLeave, false);
        item.addEventListener('drop', Common.DragDropHandler.handleDrop, false);
        item.addEventListener('dragend', Common.DragDropHandler.handleDragEnd, false);
    });
});
//# sourceMappingURL=CategoryMain.js.map