(function (comdy, $) {
    var settings = {};

    comdy.role = comdy.role || {};

    comdy.role.init = function (options) {
        comdy.role.setOptions(options);

        comdy.common.applyReturn(settings.searchFormInput, comdy.role.search);
    };

    comdy.role.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.role.search = function (page) {
        $(settings.pageNumber).val(page || 1);
        comdy.common.postForm(settings.searchUrl, settings.searchForm, function (response) {
            $(settings.resultContent).html(response);
        });
    };

    comdy.role.create = function () {
        comdy.role.detail(settings.createUrl, {}, "Thêm phân quyền mới");
    };

    comdy.role.edit = function (id) {
        comdy.role.detail(settings.editUrl, { id: id }, "Cập nhật phân quyền");
    };

    comdy.role.detail = function (url, data, title) {
        comdy.common.get(url, data, function (response) {
            if (response !== null) {
                comdy.common.showModal(response, title, "modal-lg", '<button type="button" class="btn btn-primary" onclick="comdy.role.save()"><i class="fa fa-save"></i>&nbsp;Lưu</button>');
                comdy.common.applyReturn(settings.saveFormInput, comdy.role.save);
                comdy.common.hideValidateMessages($(settings.saveForm));
                $(".styled").uniform();
            } else {
                comdy.common.errorNotify("Đã có lỗi xảy ra");
            }
        });
    };

    comdy.role.save = function () {
        var $form = $(settings.saveForm);
        comdy.common.hideValidateMessages($form);
        comdy.common.postForm(settings.saveUrl, settings.saveForm, function (response) {
            if (response.success) {
                comdy.common.successNotify(response.message);
                comdy.common.hideModal('#modal');
                comdy.role.search($(settings.pageNumber).val());
            } else {
                comdy.common.showValidateMessages($form, response.message);
            }
        });
    };

    comdy.role.delete = function (id) {
        comdy.common.postToDelete({
            message: "Bạn có muốn xóa phân quyền này không?",
            deleteUrl: settings.deleteUrl,
            deleteData: { id: id },
            callback: function () {
                var page = $(settings.pageNumber).val();
                comdy.role.search(page);
            }
        });
    };

}(window.comdy = window.comdy || {}, jQuery));
