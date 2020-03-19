(function (comdy, $) {
    var settings = {};

    comdy.category = comdy.category || {};

    comdy.category.init = function (options) {
        comdy.category.setOptions(options);

        comdy.common.applyReturn(settings.searchFormInput, comdy.category.search);
    };
    
    comdy.category.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.category.search = function (page) {
        $(settings.pageNumber).val(page || 1);
        comdy.common.postForm(settings.searchUrl, settings.searchForm, function (response) {
            $(settings.resultContent).html(response);
        });
    };

    comdy.category.create = function () {
        comdy.category.detail(settings.createUrl, {}, "Thêm danh mục mới");
    };

    comdy.category.edit = function (id) {
        comdy.category.detail(settings.editUrl, { id: id }, "Cập nhật danh mục");
    };

    comdy.category.copy = function (id) {
        comdy.category.detail(settings.copyUrl, { id: id }, "Copy danh mục");
    };

    comdy.category.detail = function (url, data, title) {
        comdy.common.get(url, data, function (response) {
            if (response !== null) {
                comdy.common.showModal(response, title, "", '<button type="button" class="btn btn-primary" onclick="comdy.category.save()"><i class="fa fa-save"></i>&nbsp;Lưu</button>');
                comdy.common.applyReturn(settings.saveFormInput, comdy.category.save);
                comdy.common.hideValidateMessages($(settings.saveForm));
                $(".styled").uniform();
                $(settings.name).focusout(function () {
                    var name = $(settings.name).val();
                    if (name !== '' && $(settings.slug).val() === '') {
                        $(settings.slug).val(comdy.common.slugGenerate(name));
                    }
                });
            } else {
                comdy.common.errorNotify("Đã có lỗi xảy ra");
            }
        });
    };

    comdy.category.save = function () {
        var $form = $(settings.saveForm);
        comdy.common.hideValidateMessages($form);
        comdy.common.postForm(settings.saveUrl, settings.saveForm, function (response) {
            if (response.success) {
                comdy.common.successNotify(response.message);
                comdy.common.hideModal('#modal');
                comdy.category.search($(settings.pageNumber).val());
            } else {
                comdy.common.showValidateMessages($form, response.message);
            }
        });
    };

    comdy.category.delete = function (id) {
        comdy.common.postToDelete({
            message: "Bạn có muốn xóa danh mục này không?",
            deleteUrl: settings.deleteUrl,
            deleteData: { id: id },
            callback: function () {
                var page = $(settings.pageNumber).val();
                comdy.category.search(page);
            }
        });
    };

}(window.comdy = window.comdy || {}, jQuery));
