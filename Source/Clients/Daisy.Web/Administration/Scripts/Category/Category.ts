module Content {
    'use strict';

    export class Category {
        updatePhotos(categoryId: number, photoIds: number[]) {
            $.ajax({
                url: '/Admin/Category/UpdatePhotos',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    categoryId: categoryId,
                    photoIds: photoIds
                },
                success: (response) => {
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
        }

        deletePhotos(categoryId: number, photoIds: number[]) {
            $.ajax({
                url: '/Admin/Category/DeletePhotos',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    categoryId: categoryId,
                    photoIds: photoIds
                },
                success: (response) => {
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
        }

        updatePhotoOrder(categoryId: number, photoIds: number[]) {
            $.ajax({
                url: '/Admin/Category/UpdatePhotoOrder',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    categoryId: categoryId,
                    photoIds: photoIds
                },
                success: (response) => {
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
        }
    }
}  