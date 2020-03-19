(function (comdy, $) {
    var settings = {};

    comdy.message = comdy.message || {};

    comdy.message.init = function (options) {
        comdy.message.setOptions(options);

        comdy.common.applyReturn(settings.searchFormInput, comdy.message.search);
    };

    comdy.message.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.message.search = function (page) {
        $(settings.pageNumber).val(page || 1);
        comdy.common.postForm(settings.searchUrl, settings.searchForm, function (response) {
            $(settings.resultContent).html(response);
        });
    };

    comdy.message.detail = function (id) {
        comdy.common.get(settings.viewMessageUrl, { id: id }, function (response) {
            if (response !== null) {
                comdy.common.showModal(response, "Xem tin nhắn", "", "");
            } else {
                comdy.common.errorNotify("Đã có lỗi xảy ra");
            }
        });
    };

    comdy.message.delete = function (id) {
        comdy.common.postToDelete({
            message: "Bạn có muốn xóa tin nhắn này không?",
            deleteUrl: settings.deleteUrl,
            deleteData: { id: id },
            callback: function () {
                var page = $(settings.pageNumber).val();
                comdy.message.search(page);
            }
        });
    };
}(window.comdy = window.comdy || {}, jQuery));
