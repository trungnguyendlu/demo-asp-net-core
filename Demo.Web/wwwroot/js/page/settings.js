(function (comdy, $) {
    var settings = {};

    comdy.settings = comdy.settings || {};

    comdy.settings.init = function (options) {
        comdy.settings.setOptions(options);
        $('.switchery').each(function (index, item) {
            var switchery = new Switchery(item);
        });
        $("#Entity_IsCommingSoon").change(function () {
            if ($(this).prop('checked')) {
                $("#opening-date").removeClass("hidden");
                $("#is-offline").addClass("hidden");
            } else {
                $("#opening-date").addClass("hidden");
                $("#is-offline").removeClass("hidden");
            }
        });
        $("#Entity_OpeningDate").daterangepicker({
            singleDatePicker: true,
            timePicker: true,
            timePicker24Hour: true,
            autoApply: true,
            locale: {
                format: "DD/MM/YYYY HH:mm"
            }
        });
        $.validator.methods.date = function (value, element) {
            return this.optional(element) || moment(value, 'dd/mm/yyyy').isValid();
        };
    };

    comdy.settings.setOptions = function (options) {
        settings = $.extend(settings, options);
    };

    comdy.settings.save = function () {
        var $form = $(settings.saveForm);
        comdy.common.hideValidateMessages($form);
        comdy.common.postForm(settings.saveUrl, settings.saveForm, function (response) {
            if (response.success) {
                comdy.common.successNotify(response.message);
            } else {
                comdy.common.showValidateMessages($form, response.message);
            }
        });
    };


    //comdy.settings.showMediaPopup = function (callback) {
    //    comdy.common.get(settings.getMediaPopupUrl, {}, function (response) {
    //        comdy.common.showMediaModal(response, '<button id="btn-media-selected" type="button" class="btn btn-primary"><i class="fa fa-save"></i> OK</button>');

    //        $('#load-more-media button').click(function () {
    //            comdy.settings.loadMoreMedia();
    //        });

    //        $('#btn-media-selected').click(function () {
    //            callback();
    //        })
    //        $(".thumbnail").click(function () {
    //            $('.thumbnail-selected').removeClass('thumbnail-selected'); // removes the previous selected class
    //            $(this).addClass('thumbnail-selected'); // adds the class to the clicked image
    //        });

    //        $("#dropzone").dropzone({
    //            paramName: "file",
    //            maxFilesize: 2,
    //            maxFiles: 1,
    //            acceptedFiles: 'image/*',
    //            dictDefaultMessage: 'Kéo file vào đây để upload <span>hoặc CLICK</span>',
    //            init: function () {
    //                this.on("complete", function (file) {
    //                    if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
    //                        comdy.settings.searchMedia(1);
    //                        $('.nav-tabs a[href="#tab-media"]').tab('show');
    //                    }
    //                });
    //                this.on("error", function (file, response) {
    //                    comdy.common.errorNotify(response);
    //                });
    //            }
    //        });
    //    });
    //};

    //comdy.settings.searchMedia = function (page) {
    //    $("#Paging_PageNumber").val(page || 1);
    //    var data = {
    //        "Name": $("#Name").val(),
    //        "Paging": {
    //            "PageSize": $("#Paging_PageSize").val(),
    //            "PageNumber": $("#Paging_PageNumber").val()
    //        }
    //    };

    //    comdy.common.post(settings.searchMediaUrl, data, function (response) {
    //        if (page === 1) {
    //            $("#media-result").html(response);
    //        } else {
    //            console.log(response);
    //            if (response !== null && response != "") {
    //                $("#media-result").append(response);
    //            } else {
    //                $("#load-more-media").addClass("hidden");
    //            }
    //        }
    //    });
    //};

    //comdy.settings.loadMoreMedia = function () {
    //    var page = parseInt($("#Paging_PageNumber").val()) + 1;
    //    comdy.settings.searchMedia(page);
    //};

    comdy.settings.insertImage = function (element) {
        comdy.admin.showMediaPopup(1, function () {
            var src = $(".thumbnail-selected img").attr("src");
            if (src !== null) {
                $(element).val(src);
                comdy.common.hideModal("#media-modal");
            } else {
                comdy.common.errorNotify("Vui lòng chọn hình ảnh");
            }
        });
        comdy.admin.selectImage(element);
    };

}(window.comdy = window.comdy || {}, jQuery));
