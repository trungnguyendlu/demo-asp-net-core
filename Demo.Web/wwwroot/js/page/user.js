(function (comdy, $) {
    var settings = {};

    comdy.user = comdy.user || {};

    comdy.user.init = function (options) {
        comdy.user.setOptions(options);

        comdy.common.applyReturn(settings.searchFormInput, comdy.user.search);
    };

    comdy.user.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.user.search = function (page) {
        $(settings.pageNumber).val(page || 1);
        comdy.common.postForm(settings.searchUrl, settings.searchForm, function (response) {
            $(settings.resultContent).html(response);
        });
    };

    comdy.user.create = function () {
        comdy.user.detail(settings.createUrl, {}, "Thêm người dùng mới");
    };

    comdy.user.edit = function (id) {
        comdy.user.detail(settings.editUrl, { id: id }, "Cập nhật người dùng");
    };

    comdy.user.detail = function (url, data, title) {
        comdy.common.get(url, data, function (response) {
            if (response !== null) {
                comdy.common.showModal(response, title, "modal-sm", '<button type="button" class="btn btn-primary" onclick="comdy.user.save()"><i class="fa fa-save"></i>&nbsp;Lưu</button>');
                comdy.common.applyReturn(settings.saveFormInput, comdy.user.save);
                comdy.common.hideValidateMessages($(settings.saveForm));
            } else {
                comdy.common.errorNotify("Đã có lỗi xảy ra");
            }
        });
    };

    comdy.user.save = function () {
        var $form = $(settings.saveForm);
        comdy.common.hideValidateMessages($form);
        comdy.common.postForm(settings.saveUrl, settings.saveForm, function (response) {
            if (response.success) {
                comdy.common.successNotify(response.message);
                comdy.common.hideModal('#modal');
                comdy.user.search($(settings.pageNumber).val());
            } else {
                comdy.common.showValidateMessages($form, response.message);
            }
        });
    };

    comdy.user.delete = function (id) {
        comdy.common.postToDelete({
            message: 'Bạn có muốn xóa người dùng này không?',
            deleteUrl: settings.deleteUrl,
            deleteData: { id: id },
            callback: function () {
                var page = $(settings.pageNumber).val();
                comdy.user.search(page);
            }
        });
    };

}(window.comdy = window.comdy || {}, jQuery));
