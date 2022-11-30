jQuery(document).ready(function($){
    new WOW().init();
    
    function stickySide(idString, closest, offset){
        if(!$(idString).length) return;
        if(!$(closest).length)  return;
        if(!$(offset))   offset = 0;
        let winTop = $(window).scrollTop();
        let mainHeight = $(closest).height();
        let mainHeightOff = $(closest).offset().top;
        if(winTop + offset >= mainHeightOff &&  winTop + offset + $(idString).height() <= mainHeightOff + mainHeight){
            $(idString).css({
                position : 'relative',
                top : offset+winTop-mainHeightOff+'px'
            });
        } else {
        if(winTop + offset < mainHeightOff){
            $(idString).attr('style','');
        }
            if( winTop + offset + $(idString).height() > mainHeightOff + mainHeight){
                $(idString).css({
                    top: mainHeight - $(idString).height() +'px'
                });
            }
        }
    }

    $('.tab-wrapper').each(function() {
        let $tabWrapper, $tabID;
		$tabWrapper = $(this);
		$tabID = $tabWrapper.find('.tab-link.current').attr('data-tab');
        $tabWrapper.find($tabID).fadeIn().siblings().hide();
        $($tabWrapper).on('click', '.tab-link', function(e){
            e.preventDefault();
			$tabID = $(this).attr('data-tab');
			$(this).addClass('current').siblings().removeClass('current');
			$tabWrapper.find($tabID).fadeIn().siblings().hide();
        });
    });

    $('.main-menu-btn').on('click', function(){
        $(this).addClass('active');
        $('.main-menu').addClass('active');
    });

    $('.main-menu-overlay').on('click', function(){
        $('.main-menu-btn').removeClass('active');
        $('.main-menu').removeClass('active');
    });

    $(".acc-info-btn").on('click', function(e){
        e.preventDefault();
		$(".status-mobile").addClass("open");
		$(".overlay-status-mobile").show();
    });
    
	$(".overlay-status-mobile").on('click', function(){
		$(".status-mobile").removeClass("open");
		$(this).hide();
    });

    if ($('.scroll-top').length) {
		$(window).scroll(function() {
			$(this).scrollTop() > 100 ? $('.scroll-top').addClass('show') : $('.scroll-top').removeClass('show');
		});
		$('.scroll-top').on('click', function(){
			$('html, body').animate({ scrollTop: 0 }, 'slow');
		})
    };

    $('.main-menu-nav .dropdown > a').append('<i class="fa fa-angle-down" aria-hidden="true"></i>');
    $(window).on('load resize', function(){
        if (window.matchMedia("(min-width: 992px)").matches) {
            $('.main-menu-nav .dropdown').hover(
                function() {
                    $(this).find('> .sub-menu-wrap').stop().slideDown('fast');
                }, function() {
                    $(this).find('> .sub-menu-wrap').stop().slideUp('fast');
                }
            )
        }
        else{
            $('.main-menu-nav .dropdown > a > .fa').on('click', function(e){
                e.preventDefault();
                $(this).closest('.dropdown').find('> .sub-menu-wrap').stop().slideToggle();
                $(this).hasClass('fa-angle-down') ? $(this).removeClass('fa-angle-down').addClass('fa-angle-up') : $(this).removeClass('fa-angle-up').addClass('fa-angle-down')
            });
        }
    });

    if($('.main-header').length && $('.bottom-header').length){
        let $mainHeader = $('.main-header');
        let $mainHeaderOffset = $mainHeader.offset().top;
        let $mainHeaderHeight = $mainHeader.outerHeight();
        let $bottomHeader = $('.bottom-header');
        if($(window).scrollTop() >= $mainHeaderOffset + $mainHeaderHeight){
            $mainHeader.addClass('fixed');
            $bottomHeader.css('margin-top', $mainHeaderHeight);
        }
        else if($(window).scrollTop() <= $mainHeaderOffset){
            $mainHeader.removeClass('fixed');
            $bottomHeader.css('margin-top', 0);
        }
        $(window).on('scroll', function(){
            if($(window).scrollTop() >= $mainHeaderOffset + $mainHeaderHeight){
                $mainHeader.addClass('fixed');
                $bottomHeader.css('margin-top', $mainHeaderHeight);
            }
            else if($(window).scrollTop() <= $mainHeaderOffset){
                $mainHeader.removeClass('fixed');
                $bottomHeader.css('margin-top', 0);
            }
        })
    };
});