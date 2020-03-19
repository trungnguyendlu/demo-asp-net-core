(function (comdy, $) {
    var settings = {};

    comdy.theme = comdy.theme || {};

    comdy.theme.init = function (options) {
        comdy.theme.setOptions(options);

        comdy.common.applyReturn(settings.searchFormInput, comdy.theme.search);
    };

    comdy.theme.setOptions = function (options) {
        settings = $.extend(settings, options);
    };
    
    comdy.theme.active = function (id) {
        comdy.common.confirm("Bạn có chắc chắn muốn đổi sang giao diện này?", function () {
            comdy.common.get(settings.activeThemeUrl, { id: id }, function (response) {
                if (response.success) {
                    comdy.common.successNotify(response.message);
                    comdy.common.hideModal();
                    location.reload(true);
                } else {
                    comdy.common.errorNotify(response.message);
                }
            });
        });
    };

    comdy.theme.detail = function (id, isCurrentTheme) {
        comdy.common.get(settings.viewThemeUrl, { id: id }, function (response) {
            if (response !== null) {
                comdy.common.showModal(response, "Xem giao diện", "modal-lg", isCurrentTheme ? "" : '<button type="button" class="btn btn-primary" onclick="comdy.theme.active(\'' + id + '\')"><i class="fa fa-check"></i>&nbsp;Kích hoạt</button>');
            } else {
                comdy.common.errorNotify("Đã có lỗi xảy ra");
            }
        });        
    };
}(window.comdy = window.comdy || {}, jQuery));
