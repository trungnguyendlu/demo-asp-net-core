(function (comdy, $) {
    var settings = {};

    comdy.notification = comdy.notification || {};

    comdy.notification.init = function (options) {
        comdy.notification.setOptions(options);
    };

    comdy.notification.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.notification.search = function (page) {
        $(settings.pageNumber).val(page || 1);
        comdy.common.postForm(settings.searchUrl, settings.searchForm, function (response) {
            $(settings.resultContent).html(response);
        });
    };

    comdy.notification.mark = function (id, showNotification, element) {
        comdy.common.get(settings.markUrl, { id: id }, function (response) {
            if (response !== null) {
                if (element !== null) {
                    $(element).hide();
                }
                if (showNotification === true) {
                    comdy.common.successNotify("Đánh dấu đã đọc thành công");
                }
            } else {
                if (showNotification === true) {
                    comdy.common.errorNotify("Đã có lỗi xảy ra");
                }
            }
        });
    };

    comdy.notification.create = function () {
        comdy.notification.detail(settings.createUrl, {}, "Thêm chuyên mục mới");
    };

    comdy.notification.edit = function (id) {
        comdy.notification.detail(settings.editUrl, { id: id }, "Cập nhật chuyên mục");
    };

    comdy.notification.detail = function (url, data, title) {
        comdy.common.get(url, data, function (response) {
            if (response !== null) {
                comdy.common.showModal(response, title, "", '<button type="button" class="btn btn-primary" onclick="comdy.notification.save()"><i class="fa fa-save"></i>&nbsp;Lưu</button>');
                comdy.common.applyReturn(settings.saveFormInput, comdy.notification.save);
                comdy.common.hideValidateMessages($(settings.saveForm));
                $(".styled").uniform();
                $(settings.slug).focus(function () {
                    comdy.common.get(settings.getSlugUrl, { name: $(settings.name).val() }, function (response) {
                        $(settings.slug).val(response);
                    });
                });
            } else {
                comdy.common.errorNotify("Đã có lỗi xảy ra");
            }
        });
    };

    comdy.notification.save = function () {
        var $form = $(settings.saveForm);
        comdy.common.hideValidateMessages($form);
        comdy.common.postForm(settings.saveUrl, settings.saveForm, function (response) {
            if (response.success) {
                comdy.common.successNotify(response.message);
                comdy.common.hideModal('#modal');
                comdy.notification.search($(settings.pageNumber).val());
            } else {
                comdy.common.showValidateMessages($form, response.message);
            }
        });
    };

    comdy.notification.delete = function (id) {
        comdy.common.postToDelete({
            message: "Bạn có muốn xóa chuyên mục này không?",
            deleteUrl: settings.deleteUrl,
            deleteData: { id: id },
            callback: function () {
                var page = $(settings.pageNumber).val();
                comdy.notification.search(page);
            }
        });
    };
}(window.comdy = window.comdy || {}, jQuery));
