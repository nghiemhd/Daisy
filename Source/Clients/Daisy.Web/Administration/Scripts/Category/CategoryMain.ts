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
    $('#btnDeletePhotos').click(function (e) {
        e.preventDefault();
        var photoIds: number[] = [];
        var categoryId: number = $('#categoryId').val();
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

    $('#btnUpdatePhotoOrders').click(function (e) {      
        e.preventDefault();  
        var photoIds: number[] = [];
        $('#gridCategoryPhotos input[type=checkbox]').each(function () {
            var photoId = $(this).val();
            photoIds.push(photoId);
        });

        if (photoIds.length > 0) {
            var categoryId: number = $('#categoryId').val();
            category.updatePhotoOrder(categoryId, photoIds);
        }
    });

    $(document).on('click', '#btnSave', function (e) {
        e.preventDefault();
        var photoIds: number[] = [];
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
            var categoryId: number = $('#categoryId').val();
            category.updatePhotos(categoryId, photoIds);
            $('#modal-container').modal('hide');
        }
    });

    var tabId = $('li.active>a').attr('href');
    var tabContent = $(tabId);
    tabContent.addClass("in active");

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