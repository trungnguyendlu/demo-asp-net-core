(function (comdy, $) {
    var settings = {};

    comdy.media = comdy.media || {};

    comdy.media.init = function (options) {
        comdy.media.setOptions(options);

        comdy.common.applyReturn(settings.searchFormInput, comdy.media.search);
    };

    comdy.media.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.media.search = function (page) {
        $(settings.pageNumber).val(page || 1);
        comdy.common.postForm(settings.searchUrl, settings.searchForm, function (response) {
            $(settings.resultContent).html(response);
        });
    };

    comdy.media.initDropZone = function () {
        // Single file
        $("#dropzone").dropzone({
            paramName: "file", // The name that will be used to transfer the file
            maxFiles: 10,
            maxFilesize: 2,
            acceptedFiles: ".jpeg,.jpg,.png,.gif",
            dictFileTooBig: 'Kích thước file hình ảnh không được vượt quá 2MB',
            dictDefaultMessage: 'Kéo file vào đây để upload <span>hoặc CLICK</span>',
            init: function () {
                this.on("complete", function (file) {
                    if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                        comdy.common.hideModal('#modal');
                        comdy.media.search(1);
                    }
                });
                this.on("error", function (file, response) {
                    comdy.common.errorNotify(response);
                });
            }
        });
    };

    comdy.media.create = function () {
        comdy.media.detail(settings.createUrl, {}, "Thêm hình ảnh mới");
    };

    comdy.media.edit = function (id) {
        comdy.media.detail(settings.editUrl, { id: id }, "Cập nhật hình ảnh");
    };

    comdy.media.detail = function (url, data, title) {
        comdy.common.get(url, data, function (response) {
            if (response !== null) {
                comdy.common.showModal(response, title, "modal-lg", '<button type="button" class="btn btn-primary" onclick="comdy.media.save()"><i class="fa fa-save"></i>&nbsp;Lưu</button>');
                comdy.common.applyReturn(settings.saveFormInput, comdy.media.save);
                comdy.common.hideValidateMessages($(settings.saveForm));
                $("#MediaType").change(function () {
                    $("#Entity_Type").val($(this).val());
                });
                $(settings.slug).focus(function () {
                    comdy.common.get(settings.getSlugUrl, { name: $(settings.name).val() }, function (response) {
                        $(settings.slug).val(response);
                    });
                });
                comdy.media.initDropZone();
            } else {
                comdy.common.errorNotify("Đã có lỗi xảy ra");
            }
        });
    };

    comdy.media.save = function () {
        var $form = $(settings.saveForm);
        comdy.common.hideValidateMessages($form);
        comdy.common.postForm(settings.saveUrl, settings.saveForm, function (response) {
            if (response.success) {
                comdy.common.successNotify(response.message);
                comdy.common.hideModal('#modal');
                comdy.media.search($(settings.pageNumber).val());
            } else {
                comdy.common.showValidateMessages($form, response.message);
            }
        });
    };

    comdy.media.delete = function (id) {
        comdy.common.postToDelete({
            message: "Bạn có muốn xóa hình ảnh này không?",
            deleteUrl: settings.deleteUrl,
            deleteData: { id: id },
            callback: function () {
                var page = $(settings.pageNumber).val();
                comdy.media.search(page);
            }
        });
    };

}(window.comdy = window.comdy || {}, jQuery));
