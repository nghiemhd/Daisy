﻿module Content {
    'use strict';

    export class Slider {
        updateSliderPhotos(photoIds: number[]) {
            $.ajax({
                url: '/Admin/Content/UpdateSlider',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    photoIds: photoIds
                },
                success: (response) => {
                    if (response == "Success") {
                        toastr.success('Update successfully');
                        window.location.href = '/Admin/Content/Slider';
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

        deleteSliderPhotos(sliderId: number, photoIds: number[]) {
            $.ajax({
                url: '/Admin/Content/DeleteSliderPhotos',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    sliderId: sliderId,
                    photoIds: photoIds
                },
                success: (response) => {
                    if (response == "Success") {
                        toastr.success('Delete successfully');
                        window.location.href = '/Admin/Content/Slider';
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

        updatePhotoOrder(sliderId: number, photoIds: number[]) {
            $.ajax({
                url: '/Admin/Content/UpdatePhotoOrder',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    sliderId: sliderId,
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