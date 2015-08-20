var Content;
(function (Content) {
    'use strict';
    var Slider = (function () {
        function Slider() {
        }
        Slider.prototype.deleteSliderPhotos = function (sliderId, photoIds) {
            $.ajax({
                url: '/Admin/Content/DeleteSliderPhotos',
                type: 'POST',
                content: 'application/json; charset=utf-8',
                dataType: 'json',
                data: {
                    sliderId: sliderId,
                    photoIds: photoIds
                },
                success: function (response) {
                    debugger;
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
        };
        return Slider;
    })();
    Content.Slider = Slider;
})(Content || (Content = {}));
//# sourceMappingURL=Slider.js.map