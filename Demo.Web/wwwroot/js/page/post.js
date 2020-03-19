(function (comdy, $) {
    var settings = {};

    comdy.post = comdy.post || {};

    comdy.post.init = function (options) {
        comdy.post.setOptions(options);

        comdy.common.applyReturn(settings.searchFormInput, comdy.post.search);
    };

    comdy.post.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.post.search = function (page) {
        $(settings.pageNumber).val(page || 1);
        comdy.common.postForm(settings.searchUrl, settings.searchForm, function (response) {
            $(settings.resultContent).html(response);
        });
    };

    comdy.post.delete = function (id) {
        comdy.common.postToDelete({
            message: "Bạn có muốn xóa bài viết này không?",
            deleteUrl: settings.deleteUrl,
            deleteData: { id: id },
            callback: function () {
                var page = $(settings.pageNumber).val();
                comdy.post.search(page);
            }
        });
    };

}(window.comdy = window.comdy || {}, jQuery));
