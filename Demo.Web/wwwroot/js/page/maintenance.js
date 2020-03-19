$(function () {
    var $body = $('body'),
        $wrapper = $('#wrapper');

    if ($body.hasClass('no-transition')) { return true; }

    if (!$().animsition) {
        $body.addClass('no-transition');
        console.log('pageTransition: Animsition not Defined.');
        return true;
    }

    window.onpageshow = function (event) {
        if (event.persisted) {
            window.location.reload();
        }
    };

    var animationIn = $body.attr('data-animation-in'),
        animationOut = $body.attr('data-animation-out'),
        durationIn = $body.attr('data-speed-in'),
        durationOut = $body.attr('data-speed-out'),
        loaderTimeOut = $body.attr('data-loader-timeout'),
        loaderStyle = $body.attr('data-loader'),
        loaderColor = $body.attr('data-loader-color'),
        loaderStyleHtml = $body.attr('data-loader-html'),
        loaderBgStyle = '',
        loaderBorderStyle = '',
        loaderBgClass = '',
        loaderBorderClass = '',
        loaderBgClass2 = '',
        loaderBorderClass2 = '';

    if (!animationIn) { animationIn = 'fadeIn'; }
    if (!animationOut) { animationOut = 'fadeOut'; }
    if (!durationIn) { durationIn = 1500; }
    if (!durationOut) { durationOut = 800; }
    if (!loaderStyleHtml) { loaderStyleHtml = '<div class="css3-spinner-bounce1"></div><div class="css3-spinner-bounce2"></div><div class="css3-spinner-bounce3"></div>'; }

    if (!loaderTimeOut) {
        loaderTimeOut = false;
    } else {
        loaderTimeOut = Number(loaderTimeOut);
    }

    if (loaderColor) {
        if (loaderColor === 'theme') {
            loaderBgClass = ' bgcolor';
            loaderBorderClass = ' border-color';
            loaderBgClass2 = ' class="bgcolor"';
            loaderBorderClass2 = ' class="border-color"';
        } else {
            loaderBgStyle = ' style="background-color:' + loaderColor + ';"';
            loaderBorderStyle = ' style="border-color:' + loaderColor + ';"';
        }
        loaderStyleHtml = '<div class="css3-spinner-bounce1' + loaderBgClass + '"' + loaderBgStyle + '></div><div class="css3-spinner-bounce2' + loaderBgClass + '"' + loaderBgStyle + '></div><div class="css3-spinner-bounce3' + loaderBgClass + '"' + loaderBgStyle + '></div>';
    }

    if (loaderStyle === '2') {
        loaderStyleHtml = '<div class="css3-spinner-flipper' + loaderBgClass + '"' + loaderBgStyle + '></div>';
    } else if (loaderStyle === '3') {
        loaderStyleHtml = '<div class="css3-spinner-double-bounce1' + loaderBgClass + '"' + loaderBgStyle + '></div><div class="css3-spinner-double-bounce2' + loaderBgClass + '"' + loaderBgStyle + '></div>';
    } else if (loaderStyle === '4') {
        loaderStyleHtml = '<div class="css3-spinner-rect1' + loaderBgClass + '"' + loaderBgStyle + '></div><div class="css3-spinner-rect2' + loaderBgClass + '"' + loaderBgStyle + '></div><div class="css3-spinner-rect3' + loaderBgClass + '"' + loaderBgStyle + '></div><div class="css3-spinner-rect4' + loaderBgClass + '"' + loaderBgStyle + '></div><div class="css3-spinner-rect5' + loaderBgClass + '"' + loaderBgStyle + '></div>';
    } else if (loaderStyle === '5') {
        loaderStyleHtml = '<div class="css3-spinner-cube1' + loaderBgClass + '"' + loaderBgStyle + '></div><div class="css3-spinner-cube2' + loaderBgClass + '"' + loaderBgStyle + '></div>';
    } else if (loaderStyle === '6') {
        loaderStyleHtml = '<div class="css3-spinner-scaler' + loaderBgClass + '"' + loaderBgStyle + '></div>';
    } else if (loaderStyle === '7') {
        loaderStyleHtml = '<div class="css3-spinner-grid-pulse"><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div></div>';
    } else if (loaderStyle === '8') {
        loaderStyleHtml = '<div class="css3-spinner-clip-rotate"><div' + loaderBorderClass2 + loaderBorderStyle + '></div></div>';
    } else if (loaderStyle === '9') {
        loaderStyleHtml = '<div class="css3-spinner-ball-rotate"><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div></div>';
    } else if (loaderStyle === '10') {
        loaderStyleHtml = '<div class="css3-spinner-zig-zag"><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div></div>';
    } else if (loaderStyle === '11') {
        loaderStyleHtml = '<div class="css3-spinner-triangle-path"><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div></div>';
    } else if (loaderStyle === '12') {
        loaderStyleHtml = '<div class="css3-spinner-ball-scale-multiple"><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div></div>';
    } else if (loaderStyle === '13') {
        loaderStyleHtml = '<div class="css3-spinner-ball-pulse-sync"><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div><div' + loaderBgClass2 + loaderBgStyle + '></div></div>';
    } else if (loaderStyle === '14') {
        loaderStyleHtml = '<div class="css3-spinner-scale-ripple"><div' + loaderBorderClass2 + loaderBorderStyle + '></div><div' + loaderBorderClass2 + loaderBorderStyle + '></div><div' + loaderBorderClass2 + loaderBorderStyle + '></div></div>';
    }

    $wrapper.animsition({
        inClass: animationIn,
        outClass: animationOut,
        inDuration: Number(durationIn),
        outDuration: Number(durationOut),
        loading: true,
        loadingParentElement: 'body',
        loadingClass: 'css3-spinner',
        loadingHtml: loaderStyleHtml,
        unSupportCss: [
            'animation-duration',
            '-webkit-animation-duration',
            '-o-animation-duration'
        ],
        overlay: false,
        overlayClass: 'animsition-overlay-slide',
        overlayParentElement: 'body',
        timeOut: loaderTimeOut
    });
});