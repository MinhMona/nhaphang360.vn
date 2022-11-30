
$(function(){
    // SCROLL TO TOP
    if (".scroll-top".length >= 1) {
        var scrollTopBtn = $(".scroll-top");
        $(window).scrollTop() > 100 ? scrollTopBtn.addClass("show") : scrollTopBtn.removeClass("show");

		$(window).scroll(function() {
			$(this).scrollTop() > 100 ? scrollTopBtn.addClass("show") : scrollTopBtn.removeClass("show");
		});
		scrollTopBtn.click(function(){
			$("html, body").animate({ scrollTop: 0 }, "slow");
		})
	};
    
    // TAB
    $(".tab-wrapper").each(function() {
		let tab = $(this);
		tab.find(".tab-link").first().addClass("current");
		let tabID = tab.find(".tab-link.current").attr("data-tab");
        tab.find(tabID).fadeIn().siblings().hide();
        $(tab).on("click", ".tab-link", function(e){
            e.preventDefault();
			let tabID = $(this).attr("data-tab");
			$(this).addClass("current").siblings().removeClass("current");
			tab.find(tabID).fadeIn().siblings().hide();
        })
    });
    // END TAB

    $(".scroll-to").on("click", function (event) {
        event.preventDefault();
        if (this.hash !== "") {
            window.scroll({
                top: $(this.hash) - $(".header").outerHeight(),
                behavior: "smooth"
            });
        }
    });

    $('.main-menu-btn').on('click', function(){
        $(this).addClass('active');
        $('.main-menu').addClass('active');
    });

    $('.main-menu-overlay').on('click', function(){
        $('.main-menu-btn').removeClass('active');
        $('.main-menu').removeClass('active');
    });

    $(".acc-info-btn").click(function(){
		$(".status-mobile").addClass("open");
		$(".overlay-status-mobile").show();
		return false;
    });
    
	$(".overlay-status-mobile").click(function(){
		$(".status-mobile").removeClass("open");
		$(this).hide();
    });
    
    if($('.header').length > 0 && $('.main').length > 0){
        header = $('.header');
        main = $('.main');
        main.css('margin-top', header.outerHeight());
        if($(window).scrollTop() > 10){
            header.addClass('fixed');
        }
        else{
            header.removeClass('fixed');
        }
        $(window).scroll(function(){
            if($(this).scrollTop() > 10){
                header.addClass('fixed');
            }
            else{
                header.removeClass('fixed');
            }
        })
    };

})

