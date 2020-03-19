
"use strict";




//Prealoader
function prealoader() {
    if ($('.preloader').length) {
        $('.preloader').delay(500).fadeOut(500);
    }
}


//headermain style
function styckyHeaderFunction() {
    if ($('.main-header').length) {
        var fixedheader = $(window).scrollTop();
        var headernavbar = $('.main-header');
        var headerstickybar = $('.main-header .sticky-header');
        var onscrollnav = $('.scroll-to-top');
        if (fixedheader > 55) {
            headernavbar.addClass('fixed-header');
            headerstickybar.addClass("animated slideInDown");
            onscrollnav.fadeIn(300);
        } else {
            headernavbar.removeClass('fixed-header');
            headerstickybar.removeClass("animated slideInDown");
            onscrollnav.fadeOut(300);
        }
    }
}



//Submenu Dropdown Toggle
if ($('.main-header li.dropdown ul').length) {
    $('.main-header li.dropdown').append('<div class="dropdown-btn" data-expand="0"><span class="fa fa-angle-right"></span></div>');

    //Dropdown Button
    $('.main-header li.dropdown .dropdown-btn').on('click', function () {
        $(this).prev('ul').slideToggle(500);
        var expand = $(this).data("expand");
        if (expand === 0) {
            $(this).data("expand", 1);
            $(this).html('<span class="fa fa-angle-down"></span>');
        } else {
            $(this).data("expand", 0);
            $(this).html('<span class="fa fa-angle-right"></span>');
        }
    });

    //Disable dropdown parent link
    $('.main-header .navigation li.dropdown > a').on('click', function (e) {
        e.preventDefault();
    });
}



//bottom to top scroll btn show hide

function scrollTopClick() {
    //alert("working");
    if ($(window).scrollTop() > 200) {
        $('.scroll-totop').fadeIn();
    } else {
        $('.scroll-totop').fadeOut();
    }
}

//bottom to top scroll click to go
$('.scroll-totop').on('click', function () {
    $('html, body').animate({ scrollTop: 0 }, 1500);
    return false;
});





// Counter up.
function CounterYourNumber() {
    if ($('.counter-nos').length) {
        $('.counter-nos').counterUp({
            delay: 10,
            time: 1000
        });
    }
}




/*--------------------------
    Banner slider
---------------------------- */



if ($('.mainslider').length) {
    $('.mainslider').owlCarousel({
        dots: false,
        loop: true,
        margin: 0,
        nav: true,
        navText: [
            '<i class="fas fa-arrow-left"></i>',
            '<i class="fas fa-arrow-right"></i>'
        ],
        autoplayHoverPause: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            800: {
                items: 1
            },
            1024: {
                items: 1
            },
            1100: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    });
}

if ($('.feature-slide').length) {
    $('.feature-slide').owlCarousel({
        dots: true,
        loop: true,
        margin: 0,
        nav: false,
        navText: [
            '<i class="fa fa-angle-left"></i>',
            '<i class="fa fa-angle-right"></i>'
        ],
        autoplayHoverPause: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            800: {
                items: 2
            },
            1024: {
                items: 3
            },
            1100: {
                items: 3
            },
            1200: {
                items: 3
            }
        }
    });
}

if ($('.projects-slide').length) {
    $('.projects-slide').owlCarousel({
        dots: true,
        loop: true,
        margin: 0,
        nav: false,
        navText: [
            '<i class="fa fa-angle-left"></i>',
            '<i class="fa fa-angle-right"></i>'
        ],
        autoplayHoverPause: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            800: {
                items: 1
            },
            1024: {
                items: 1
            },
            1100: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    });
}

if ($('.team-slide').length) {
    $('.team-slide').owlCarousel({
        dots: false,
        loop: true,
        margin: 0,
        nav: true,
        navText: [
            '<i class="fa fa-angle-left"></i>',
            '<i class="fa fa-angle-right"></i>'
        ],
        autoplayHoverPause: false,
        autoplay: false,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            800: {
                items: 2
            },
            1024: {
                items: 2
            },
            1100: {
                items: 2
            },
            1200: {
                items: 2
            }
        }
    });
}


if ($('.testimonial-slides').length) {
    $('.testimonial-slides').owlCarousel({
        dots: false,
        loop: true,
        margin: 0,
        nav: true,
        navText: [
            '<i class="fas fa-arrow-left"></i>',
            '<i class="fas fa-arrow-right"></i>'
        ],
        autoplayHoverPause: false,
        autoplay: true,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            800: {
                items: 1
            },
            1024: {
                items: 1
            },
            1100: {
                items: 1
            },
            1200: {
                items: 1
            }
        }
    });
}


if ($('.news-slide').length) {
    $('.news-slide').owlCarousel({
        dots: false,
        loop: true,
        margin: 30,
        nav: true,
        center: false,
        navText: [
            '<i class="fa fa-angle-left"></i>',
            '<i class="fa fa-angle-right"></i>'
        ],
        autoplayHoverPause: false,
        autoplay: 6000,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 1
            },
            800: {
                items: 2
            },
            1024: {
                items: 2
            },
            1100: {
                items: 3
            },
            1200: {
                items: 3
            }
        }
    });
}

if ($('.products-slide').length) {
    $('.products-slide').owlCarousel({
        dots: true,
        loop: true,
        margin: 0,
        nav: false,
        center: false,
        navText: [
            '<i class="fa fa-angle-left"></i>',
            '<i class="fa fa-angle-right"></i>'
        ],
        autoplayHoverPause: false,
        autoplay: 6000,
        smartSpeed: 1000,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            800: {
                items: 3
            },
            1024: {
                items: 3
            },
            1100: {
                items: 4
            },
            1200: {
                items: 4
            }
        }
    });
}

// Fancybox
if ($('.fancybox-gallery').length) {
    $('.fancybox-gallery').fancybox({
        openEffect: 'fade',
        closeEffect: 'fade',
        youtube: {
            controls: 0,
            showinfo: 0
        },
        vimeo: {
            color: 'f00'
        }
    });
}



/* ==========================================================================
		Project Gallery
   ========================================================================== */

function galleryMasonaryLayout() {
    if ($('.filter-list').length) {
        $('.filter-list').isotope({
            itemSelector: '.gallery-item',
            layoutMode: 'masonry'
        });


        $('.post-filter').on('click', 'li', function () {
            var filterValue = $(this).attr('data-filter');
            $('.filter-list').isotope({ filter: filterValue });
            $(this).addClass("active");
            $(this).siblings().removeClass("active");
        });
    }

}




/* ==========================================================================
		Faqs accordian
   ========================================================================== */


function toggleIcon(e) {
    $(e.target)
        .prev('.card-header')
        .find(".faq-indicator")
        .toggleClass('fa-minus fa-plus')
        .parents('.card')
        .toggleClass('active');
}
if ($('.faq-style').length) {
    $('#accordion').on('hidden.bs.collapse', toggleIcon);
    $('#accordion').on('shown.bs.collapse', toggleIcon);
}



// scroll animate
if ($('.wow').length) {
    var wow = new WOW(
        {
            boxClass: 'wow',
            animateClass: 'animated',
            offset: 0,
            mobile: false,
            live: true
        }
    );
    //wow.init();
}




/* ==========================================================================
   Contact Form Validation and Ajax
   ========================================================================== */

$('#contact-form').validate({
    rules: {
        FullName: {
            required: true
        },
        Email: {
            required: true,
            email: true
        },
        PhoneNumber: {
            required: true
        },
        Content: {
            required: true
        }
    },
    messages: {
        FullName: "Chưa nhập Họ Tên",
        Email: {
            required: "Chưa nhập Email",
            email: "Email không hợp lệ"
        },
        PhoneNumber: "Chưa nhập Điện Thoại",
        Content: {
            required: "Chưa nhập Tin Nhắn"
        }
    },
    submitHandler: function (e) {
        var $t = $(e);
        $.ajax({
            type: 'POST',
            url: $t.attr('action'),
            data: $t.serialize(),
            beforeSend: function () {
                $("#contact-form #loading").show();
            },
            complete: function () {
                $("#contact-form #loading").hide();
            },
            success: function (response) {
                $('#contact-form .contact-form-message').show().html(response.message).delay(5000).fadeOut("slow");
                if (response.success) {
                    $('#contact-form input[name=\'FullName\']').val('');
                    $('#contact-form input[name=\'Email\']').val('');
                    $('#contact-form input[name=\'PhoneNumber\']').val('');
                    $('#contact-form textarea[name=\'Content\']').val('');
                }
            }
        });
    }
});
$('#qoute-form').validate({
    rules: {
        FullName: {
            required: true
        },
        PhoneNumber: {
            required: true
        },
        Email: {
            required: true,
            email: true
        }
    },
    messages: {
        FullName: "Chưa nhập Họ Tên",
        PhoneNumber: "Chưa nhập Điện Thoại",
        Email: {
            required: "Chưa nhập Email",
            email: "Email không hợp lệ"
        }
    },
    submitHandler: function (e) {
        var $t = $(e);
        $.ajax({
            type: 'POST',
            url: $t.attr('action'),
            data: $t.serialize(),
            beforeSend: function () {
                $("#qoute-form #loading").show();
            },
            complete: function () {
                $("#qoute-form #loading").hide();
            },
            success: function (res) {
                $('#qoute-form .contact-form-message').show().html("Gửi yêu cầu tư vấn thành công").delay(5000).fadeOut("slow");
                if (res.success) {
                    $('#qoute-form input[name=\'FullName\']').val('');
                    $('#qoute-form input[name=\'PhoneNumber\']').val('');
                    $('#qoute-form input[name=\'Email\']').val('');
                }
            }
        });
    }
});
$('#subscribe-form').validate({
    rules: {
        email: {
            required: true,
            email: true
        }
    },
    messages: {
        email: {
            required: "Chưa nhập Email",
            email: "Email không hợp lệ"
        }
    },
    submitHandler: function (e) {
        var $t = $(e);
        $.ajax({
            type: 'POST',
            url: $t.attr('action'),
            data: $t.serialize(),
            beforeSend: function () {
                $("#subscribe-form #loading").show();
            },
            complete: function () {
                $("#subscribe-form #loading").hide();
            },
            success: function (res) {
                $('#subscribe-form .contact-form-message').show().html(res.message).delay(5000).fadeOut("slow");
                if (res.success) {
                    $('#subscribe-form input[name=\'email\']').val('');
                }
            }
        });
    }
});


/**
 * Magnific popup
 */

var $lightboxGalleryEl = $('[data-lightbox="gallery"]');
if ($lightboxGalleryEl.length > 0) {
    $lightboxGalleryEl.each(function () {
        var element = $(this);

        if (element.find('a[data-lightbox="gallery-item"]').parent('.clone').hasClass('clone')) {
            element.find('a[data-lightbox="gallery-item"]').parent('.clone').find('a[data-lightbox="gallery-item"]').attr('data-lightbox', '');
        }

        if (element.find('a[data-lightbox="gallery-item"]').parents('.cloned').hasClass('cloned')) {
            element.find('a[data-lightbox="gallery-item"]').parents('.cloned').find('a[data-lightbox="gallery-item"]').attr('data-lightbox', '');
        }

        element.magnificPopup({
            delegate: 'a[data-lightbox="gallery-item"]',
            type: 'image',
            closeOnContentClick: true,
            closeBtnInside: false,
            fixedContentPos: true,
            mainClass: 'mfp-no-margins mfp-fade', // class to remove default margin from left and right side
            image: {
                verticalFit: true
            },
            gallery: {
                enabled: true,
                navigateByImgClick: true,
                preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
            }
        });
    });
}

var $lightboxInlineEl = $('[data-lightbox="inline"]');
if ($lightboxInlineEl.length > 0) {
    $lightboxInlineEl.magnificPopup({
        type: 'inline',
        mainClass: 'mfp-no-margins mfp-fade',
        closeBtnInside: false,
        fixedContentPos: true,
        overflowY: 'scroll'
    });
}


// Dom Ready Function
jQuery(document).on('ready', function () {
    (function ($) {
        // add your functions
        //searchbox();
        CounterYourNumber();
        galleryMasonaryLayout();





    })(jQuery);
});



jQuery(window).on('scroll', function () {
    (function ($) {
        styckyHeaderFunction();
        scrollTopClick();

    })(jQuery);
});



// Instance Of Fuction while Window Load event
jQuery(window).on('load', function () {
    (function ($) {
        prealoader();

    })(jQuery);
});

