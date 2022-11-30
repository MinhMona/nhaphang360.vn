jQuery(document).ready(function($){
    
    //Show/Hide scroll-top on Scroll
    // hide #back-top first
	$("#scroll-top").hide();
    // fade in #back-top
    $(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                $('#scroll-top').fadeIn();
            } else {
                $('#scroll-top').fadeOut();
            }
        });
        // scroll body to 0px on click
        $('#scroll-top').click(function () {
            $('body,html').animate({
                scrollTop: 0
            }, 1000);
        });
    });
    $('.nav-toggle').on('click', function(e){
        $(this).toggleClass('open');
        $('body').toggleClass('menuin');
    });
    $('.nav-overlay').on('click', function(e){
        $('.nav-toggle').trigger('click');
    })
    $('#main-nav').on('click', '.sub-toggle', function(e){
        e.preventDefault();
        var li =  $(this).parent('li');
        var otherLi = li.siblings();
        var sub = li.children('.side-sub');
        otherLi.children('.side-sub').slideUp();
        sub.stop().slideToggle(function(){
            sub.is(':hidden') ? li.removeClass('sub-in') : li.addClass('sub-in');
        });
    });
    // css trigger te le
    $('[data-css]').each(function(){
        var cssObjString =  $(this).attr('data-css');
        $(this).css(JSON.parse(cssObjString));
    });
});