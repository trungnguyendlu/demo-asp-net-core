(function (comdy, $) {
    var settings = {};
    comdy.corporation = comdy.corporation || {};

    comdy.corporation.init = function (options) {
        comdy.corporation.setOptions(options);
    };

    comdy.corporation.setOptions = function (options) {
        settings = $.extend(settings, options);
    };

    comdy.corporation.contact = function (url) {
        $.post(url, $("#contact-form").serialize()).done(function (response) {
            if (response.success) {
                new PNotify({
                    text: response.message,
                    type: "success",
                    buttons: {
                        sticker: false,
                        closer: false
                    }
                });
                $("#FullName").val('');
                $("#PhoneNumber").val('');
                $("#Email").val('');
                $("#Content").val('');
                grecaptcha.reset(); 
            } else {
                new PNotify({
                    text: response.message,
                    type: "error",
                    buttons: {
                        sticker: false,
                        closer: false
                    }
                });
            }
        });
    };

    comdy.corporation.subscribe = function (url) {
        $.post(url, $("#widget-subscribe-form").serialize()).done(function (response) {
            if (response.success) {
                new PNotify({
                    text: response.message,
                    type: "success",
                    buttons: {
                        sticker: false,
                        closer: false
                    }
                });
                $("#email").val('');
            } else {
                new PNotify({
                    text: response.message,
                    type: "error",
                    buttons: {
                        sticker: false,
                        closer: false
                    }
                });
            }
        });
    };
}(window.comdy = window.comdy || {}, jQuery));