var Content;
(function (Content) {
    'use strict';
    var Category = (function () {
        function Category() {
        }
        Category.prototype.updatePhotos = function (categoryId, photoIds) {
            $.ajax({
                url: '/Admin/Category/UpdatePhotos',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    categoryId: categoryId,
                    photoIds: photoIds
                },
                success: function (response) {
                    if (response == "Success") {
                        toastr.success('Update successfully');
                        window.location.href = '/Admin/Category/Edit/' + categoryId + '#categoryPhotos';
                    }
                    else {
                        toastr.options = {
                            closeButton: true,
                            positionClass: "toast-top-full-width",
                            timeOut: 0,
                            extendedTimeOut: 0
                        };
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
                }
            });
        };
        Category.prototype.deletePhotos = function (categoryId, photoIds) {
            $.ajax({
                url: '/Admin/Category/DeletePhotos',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    categoryId: categoryId,
                    photoIds: photoIds
                },
                success: function (response) {
                    if (response == "Success") {
                        toastr.success('Delete successfully');
                        window.location.href = '/Admin/Category/Edit/' + categoryId + '#categoryPhotos';
                    }
                    else {
                        toastr.options = {
                            closeButton: true,
                            positionClass: "toast-top-full-width",
                            timeOut: 0,
                            extendedTimeOut: 0
                        };
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
                }
            });
        };
        Category.prototype.updatePhotoOrder = function (categoryId, photoIds) {
            $.ajax({
                url: '/Admin/Category/UpdatePhotoOrder',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    categoryId: categoryId,
                    photoIds: photoIds
                },
                success: function (response) {
                    if (response == "Success") {
                        toastr.success('Update successfully');
                    }
                    else {
                        toastr.options = {
                            closeButton: true,
                            positionClass: "toast-top-full-width",
                            timeOut: 0,
                            extendedTimeOut: 0
                        };
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
                }
            });
        };
        return Category;
    })();
    Content.Category = Category;
})(Content || (Content = {}));
//# sourceMappingURL=Category.js.map