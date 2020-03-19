(function (comdy, $) {
    var settings = {};

    comdy.service = comdy.service || {};

    comdy.service.init = function (options) {
        comdy.service.setOptions(options);

        comdy.common.applyReturn(settings.searchFormInput, comdy.service.search);
    };

    comdy.service.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.service.search = function (page) {
        $(settings.pageNumber).val(page || 1);
        comdy.common.postForm(settings.searchUrl, settings.searchForm, function (response) {
            $(settings.resultContent).html(response);
        });
    };

    comdy.service.delete = function (id) {
        comdy.common.postToDelete({
            message: "Bạn có muốn xóa dịch vụ này không?",
            deleteUrl: settings.deleteUrl,
            deleteData: { id: id },
            callback: function () {
                var page = $(settings.pageNumber).val();
                comdy.service.search(page);
            }
        });
    };

}(window.comdy = window.comdy || {}, jQuery));
