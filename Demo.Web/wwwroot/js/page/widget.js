(function (comdy, $) {
    var settings = {};

    comdy.widget = comdy.widget || {};

    comdy.widget.init = function (options) {
        comdy.widget.setOptions(options);

        comdy.common.applyReturn(settings.searchFormInput, comdy.widget.search);
    };

    comdy.widget.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.widget.search = function (widget) {
        $(settings.widgetNumber).val(widget || 1);
        comdy.common.postForm(settings.searchUrl, settings.searchForm, function (response) {
            $(settings.resultContent).html(response);
        });
    };

    comdy.widget.delete = function (id) {
        comdy.common.postToDelete({
            message: "Bạn có muốn xóa widget này không?",
            deleteUrl: settings.deleteUrl,
            deleteData: { id: id },
            callback: function () {
                var widget = $(settings.widgetNumber).val();
                comdy.widget.search(widget);
            }
        });
    };

}(window.comdy = window.comdy || {}, jQuery));
