﻿$(document).ready(function () {

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

    $('#btnUpdateOrders').click(function () {
        var photoIds: number[] = [];
        $('#gridSliderPhotos input[type=checkbox]').each(function () {
            var photoId = $(this).val();
            photoIds.push(photoId);
        });

        if (photoIds.length > 0) {
            var sliderId: number = $('#sliderId').val();
            slider.updatePhotoOrder(sliderId, photoIds);
        }
    });

    $(document).on('click', '#btnSave', function (e) {
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
        else if (photoIds.length > 10) {
            BootstrapDialog.show({
                type: BootstrapDialog.TYPE_WARNING,
                title: 'Add photos to slider',
                message: 'Cannot add more than 10 photos.'
            });
        }
        else {
            slider.updateSliderPhotos(photoIds);
        }
    });    

    Common.DragDropHandler.items = document.querySelectorAll('#gridSliderPhotos .draggable-item');
    [].forEach.call(Common.DragDropHandler.items, function (item) {
        item.addEventListener('dragstart', Common.DragDropHandler.handleDragStart, false);
        item.addEventListener('dragenter', Common.DragDropHandler.handleDragEnter, false);
        item.addEventListener('dragover', Common.DragDropHandler.handleDragOver, false);
        item.addEventListener('dragleave', Common.DragDropHandler.handleDragLeave, false);
        item.addEventListener('drop', Common.DragDropHandler.handleDrop, false);
        item.addEventListener('dragend', Common.DragDropHandler.handleDragEnd, false);
    });
}); 