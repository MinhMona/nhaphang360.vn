jQuery(document).ready(function($){
    var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent);
    if(iOS){
        $(document.body).addClass('ios');
    };
    //input date
    if($('.input-date').length){
        var pkcont = 'body';
        if($('.picker-container').length){
            pkcont = '.picker-container';
        }
        $('.input-date').datepicker({
            todayHighlight: true,
            startDate: "0d",
            container: pkcont
        });
    }
    $(".acc-info > a").click(function(e){
        e.stopPropagation();
        $(".menu-profile").addClass("open");
        $("body").addClass("profilein");
        if($(".navbar-toggle").hasClass("open")){
            $(".navbar-toggle").removeClass("open");
            $("body").removeClass('menuin');
        }
    });
    $(".profile-content").click(function(e){
        e.stopPropagation();
    });
    $(document).click(function(){
        if($(".menu-profile").hasClass("open")){
            $(".menu-profile").removeClass("open");
            $("body").removeClass("profilein");
        }
    });

    $(".menu-in-profile > li.has-submenu > a").click(function(){
        var $parent = $(this).parent();
        var $this = $(this);
        if($parent.hasClass("active")){
            $parent.removeClass("active");
        }
        else{
            $(".menu-in-profile > li.has-submenu").each(function(){
                if($(this).hasClass("active")){
                    $(this).removeClass("active");
                }
            });
            $parent.addClass("active");
        }
    });
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
    $('.navbar-toggle').on('click',function(e){
        $(this).toggleClass('open');
        $('body').toggleClass('menuin');
    });
    $('.nav-overlay').on('click',this,function(e){
        $('.navbar-toggle').trigger('click');
    });
    $('.dropdown').on('click', '.dropdown-toggle',function(e){

        var $this = $(this);
        var parent = $this.parent('.dropdown');
        var submenu = parent.find('.sub-menu-wrap');
        parent.toggleClass('open').siblings().removeClass('open');
        e.stopPropagation();
        
        submenu.click(function(e){
         e.stopPropagation();
     });
        

    });
    $('body,html').on('click', function(){

        if($('.dropdown').hasClass('open')){

            $('.dropdown').removeClass('open');
        }
    });
    $('.collapse').on('click','.collapse-heading',function(){
        var container = $(this).parent('.collapse');
        $(container).siblings().removeClass('on').find('.collapse-body').slideUp();
        $(container).find('.collapse-body').is(':visible')  ?  
        $(container).removeClass('on').find('.collapse-body').slideUp() :
        $(container).addClass('on').find(':hidden').slideDown() ;
        
    });
    stickyHeader();
//    $(window).scrollTop() > $("#header").height() ? $("#header").addClass("sticky") : $("#header").removeClass("sticky");
$(window).scroll(function () {
//        $(window).scrollTop() > $("#header").height() ? $("#header").addClass("sticky") : $("#header").removeClass("sticky");
stickyHeader();
});
function stickyHeader(){
    var hdOffsetTop =  $("#header").offset().top;
    if($(window).scrollTop() > $("#header").height()){
        $("#header").addClass("sticky");
    } else {
        $("#header").removeClass("sticky");
    }
}

});
