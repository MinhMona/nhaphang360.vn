$(function() {
    // add class
    $(window).scroll(function() {
        var scroll = $(window).scrollTop();
        if (scroll >= 50) {
            $(".header").addClass("header-fixed");
        } else {
            $(".header").removeClass("header-fixed");
        }
    });

    $('.main-menu-btn').on('click', function() {
        $(this).addClass('active');
        $('.main-menu').addClass('active');
    });

    $('.main-menu-overlay').on('click', function() {
        $('.main-menu-btn').removeClass('active');
        $('.main-menu').removeClass('active');
    });

    $('.main-menu-btn').on('click', function() {
        $(this).addClass('active');
        $('.main-menu').addClass('active');
    });

    $('.main-menu-overlay').on('click', function() {
        $('.main-menu-btn').removeClass('active');
        $('.main-menu').removeClass('active');
    });

    $(".acc-info-btn").click(function() {
        $(".status-mobile").addClass("open");
        $(".overlay-status-mobile").show();
        return false;
    });

    $(".overlay-status-mobile").click(function() {
        $(".status-mobile").removeClass("open");
        $(this).hide();
    });
    $(document).ready(function() {
        $('.dropdown').click(function() {
            $('.sub-menu').toggleClass('visible');
        });
    });


    function scrollToTop() {
        verticalOffset = typeof(verticalOffset) != 'undefined' ? verticalOffset : 0;
        element = $('body');
        offset = element.offset();
        offsetTop = offset.top;
        $('html, body').animate({ scrollTop: offsetTop }, 600, 'linear');
    }

    // slide-banner-top


    //Make sure the user has scrolled at least double the height of the browser
    var toggleHeight = $(window).outerHeight();

    $(window).scroll(function() {
        if ($(window).scrollTop() > toggleHeight) {
            //Adds active class to make button visible
            $(".m-backtotop").addClass("active");

            //Just some cool text changes
            $('h1 span').text('TA-DA! Now hover it and hit dat!')

        } else {
            //Removes active class to make button visible
            $(".m-backtotop").removeClass("active");

            //Just some cool text changes
            $('h1 span').text('(start scrolling)')
        }
    });


    //Scrolls the user to the top of the page again
    $(".m-backtotop").click(function() {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    });



    $('.open-popup-link').magnificPopup({
        type: 'inline',
        midClick: true,
        mainClass: 'mfp-fade',
        callbacks: {
            open: function() {
                console.log(this.content);
                this.content.find('.slick-slider').slick('setPosition');

            },
            close: function() {

            }
        }
    });

    $('.counting').each(function() {
        var $this = $(this),
            countTo = $this.attr('data-count');

        $({ countNum: $this.text() }).animate({
                countNum: countTo
            },

            {

                duration: 10000,
                easing: 'linear',
                step: function() {
                    $this.text(Math.floor(this.countNum));
                },
                complete: function() {
                    $this.text(this.countNum);
                    //alert('finished');
                }

            });


    });

    $(document).ready(function() {
        $('.c-tab__nav ul li').click(function() {
            var tab_id = $(this).attr('data-tab');

            $('.c-tab__nav ul li').removeClass('active');
            $('.c-tab__content').removeClass('active');

            $(this).addClass('active');
            $("." + tab_id).addClass('active');
        });
        $('.c-tab__tutorial ul li').click(function() {
            var tab_id = $(this).attr('data-tab');

            $('.c-tab__tutorial ul li').removeClass('active');
            $('.c-tab__item').removeClass('active');

            $(this).addClass('active');
            $("." + tab_id).addClass('active');
        });
    });
});