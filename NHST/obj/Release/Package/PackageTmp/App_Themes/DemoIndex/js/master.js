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
    
    if ($('#slider-top').length){
        $('#slider-top').slick({
            dots: false,
            arrows:false,
            infinite: true,
            speed: 300,
            slidesToShow: 1,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 2000,
            prevArrow: '<span class="slick-prev slick-arrow"><i class="fa fa-angle-left"><i></span>',
            nextArrow: '<span class="slick-next slick-arrow"><i class="fa fa-angle-right"><i></span>',
            responsive: [
                    {
                      breakpoint: 480,
                      settings: {
                        arrows:false,
                      }
                    }
                    // You can unslick at a given breakpoint now by adding:
                    // settings: "unslick"
                    // instead of a settings object
                  ]
        });
    }


});

function loadGoogleMap(){
                var mapElement = document.getElementById('map-canvas');
                 if(mapElement == null) return;
         google.maps.event.addDomListener(window, 'load', initmap);
         var infowindow = new google.maps.InfoWindow({
            size: new google.maps.Size(150, 50)
        });
    var map;
    var gmarkers = [];
    function createMarker(latlng, title) {
        var marker = new google.maps.Marker({
            position: latlng,
            title: 'Mona Meida',

        });
        infowindow.setContent(title);
        infowindow.open(map, marker);
        google.maps.event.addListener(marker, 'click', function () {
            infowindow.setContent(title);
            infowindow.open(map, marker);
        });
        gmarkers.push(marker);
        return marker;
    }
    function callinfobox(i) {
        google.maps.event.trigger(gmarkers[i], "click");
    }
    function deleteMarkers() {
        clearMarkers();
        gmarkers = [];
      }
    // Sets the map on all markers in the array.
      function setMapOnAll(map) {
        for (var i = 0; i < gmarkers.length; i++) {
          gmarkers[i].setMap(map);
        }
      }

      // Removes the markers from the map, but keeps them in the array.
      function clearMarkers() {
        setMapOnAll(null);
      }
    function initmap() {
        var myLatlng = new google.maps.LatLng(10.77707, 106.65482);
        var mapOptions = {
            // How zoomed in you want the map to start at (always required)
            zoom: 16,
            disableDefaultUI: true,
            scrollwheel: false,
            zoomControl: true,
            draggable: true,
          zoomControlOptions: {
              position: google.maps.ControlPosition.TOP_RIGHT
          },
            // The latitude and longitude to center the map (always required)
            center: myLatlng,
            // How you would like to style the map. 
            // This is where you would paste any style found on Snazzy Maps.
            styles: [{"featureType":"water","elementType":"geometry","stylers":[{"color":"#e9e9e9"},{"lightness":17}]},{"featureType":"landscape","elementType":"geometry","stylers":[{"color":"#f5f5f5"},{"lightness":20}]},{"featureType":"road.highway","elementType":"geometry.fill","stylers":[{"color":"#ffffff"},{"lightness":17}]},{"featureType":"road.highway","elementType":"geometry.stroke","stylers":[{"color":"#ffffff"},{"lightness":29},{"weight":0.2}]},{"featureType":"road.arterial","elementType":"geometry","stylers":[{"color":"#ffffff"},{"lightness":18}]},{"featureType":"road.local","elementType":"geometry","stylers":[{"color":"#ffffff"},{"lightness":16}]},{"featureType":"poi","elementType":"geometry","stylers":[{"color":"#f5f5f5"},{"lightness":21}]},{"featureType":"poi.park","elementType":"geometry","stylers":[{"color":"#dedede"},{"lightness":21}]},{"elementType":"labels.text.stroke","stylers":[{"visibility":"on"},{"color":"#ffffff"},{"lightness":16}]},{"elementType":"labels.text.fill","stylers":[{"saturation":36},{"color":"#333333"},{"lightness":40}]},{"elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"geometry","stylers":[{"color":"#f2f2f2"},{"lightness":19}]},{"featureType":"administrative","elementType":"geometry.fill","stylers":[{"color":"#fefefe"},{"lightness":20}]},{"featureType":"administrative","elementType":"geometry.stroke","stylers":[{"color":"#fefefe"},{"lightness":17},{"weight":1.2}]}]
        };
    

    // Create the Google Map using our element and options defined above
    map = new google.maps.Map(mapElement, mapOptions);
        createMarker(myLatlng,'<a href="https://mona-media.com/dich-vu/thiet-ke-website-chuyen-nghiep/" title="Công ty thiế kế website chuyên nghiệp">Thiết kế website</a>&nbsp;<img src="http://mona-media.com/logo.png" style="width:20px;vertical-align:sub" alt="MonaMedia"> <strong>Mona Media</strong>').setMap(map);



    }
             }
jQuery(document).ready(function($){

	var MqM= 768,
		MqL = 1024;

	var faqsSections = $('.faq-group'),
		faqTrigger = $('.trigger'),
		faqsContainer = $('.faq-items'),
		faqsCategoriesContainer = $('.categories'),
		faqsCategories = faqsCategoriesContainer.find('a'),
        faqsCategoriesContainer1 = $('.categories1'),
		faqsCategories1 = faqsCategoriesContainer1.find('a'),
		closeFaqsContainer = $('.cd-close-panel');
	   
	//select a faq section 
	faqsCategories.on('click', function(event){
		event.preventDefault();
        var offsetHeader =  $('#header').innerHeight();
		var selectedHref = $(this).attr('href'),
			target= $(selectedHref);
		if( $(window).width() < MqM) {
			faqsContainer.scrollTop(0).addClass('slide-in').children('ul').removeClass('selected').end().children(selectedHref).addClass('selected');
			closeFaqsContainer.addClass('move-left');
			$('body').addClass('cd-overlay');
		} else {
	        $('body,html').animate({ 'scrollTop': target.offset().top - offsetHeader - 5}, 200); 
		}
	});

	//close faq lateral panel - mobile only
	$('body').bind('click touchstart', function(event){
		if( $(event.target).is('body.cd-overlay') || $(event.target).is('.cd-close-panel')) { 
			closePanel(event);
		}
	});
	faqsContainer.on('swiperight', function(event){
		closePanel(event);
	});


	faqTrigger.on('click', function(event){
		event.preventDefault();
		$(this).next('.faq-content').slideToggle(200).end().parent('li').toggleClass('content-visible');
	});

	$(window).on('scroll', function(){
		if ( $(window).width() > MqL ) {
			(!window.requestAnimationFrame) ? updateCategory() : window.requestAnimationFrame(updateCategory); 
		}
	});

	$(window).on('resize', function(){
		if($(window).width() <= MqL) {
			faqsCategoriesContainer.removeClass('is-fixed').css({
				'-moz-transform': 'translateY(0)',
			    '-webkit-transform': 'translateY(0)',
				'-ms-transform': 'translateY(0)',
				'-o-transform': 'translateY(0)',
				'transform': 'translateY(0)',
			});
		}	
		if( faqsCategoriesContainer.hasClass('is-fixed') ) {
			faqsCategoriesContainer.css({
				'left': faqsContainer.offset().left,
			});
		}
	});

	function closePanel(e) {
		e.preventDefault();
		faqsContainer.removeClass('slide-in').find('li').show();
		closeFaqsContainer.removeClass('move-left');
		$('body').removeClass('cd-overlay');
	}

	function updateCategory(){
		updateCategoryPosition();
		updateSelectedCategory();
		updateCategoryBehindPosition();
	}

	function updateCategoryPosition() {
        var offsetHeader =  $('#header').innerHeight();
		var top = $('.faq').offset().top,
			height = jQuery('.faq').innerHeight() - jQuery('.categories').innerHeight(),
			margin = 20;
		if( top - margin <= $(window).scrollTop() + offsetHeader && top - margin + height > $(window).scrollTop() +    offsetHeader) {
			var leftValue = faqsCategoriesContainer.offset().left,
				widthValue = faqsCategoriesContainer.width();
			faqsCategoriesContainer.addClass('is-fixed').css({
				'left': leftValue,
				'top': margin + offsetHeader,
				'-moz-transform': 'translateZ(0)',
			    '-webkit-transform': 'translateZ(0)',
				'-ms-transform': 'translateZ(0)',
				'-o-transform': 'translateZ(0)',
				'transform': 'translateZ(0)',
			});
		} else if( top - margin + height  <= $(window).scrollTop() + offsetHeader) {
			var delta = top - margin + height - $(window).scrollTop() - offsetHeader;
			faqsCategoriesContainer.css({
				'-moz-transform': 'translateZ(0) translateY('+delta+'px)',
			    '-webkit-transform': 'translateZ(0) translateY('+delta+'px)',
				'-ms-transform': 'translateZ(0) translateY('+delta+'px)',
				'-o-transform': 'translateZ(0) translateY('+delta+'px)',
				'transform': 'translateZ(0) translateY('+delta+'px)',
			});
		} else { 
			faqsCategoriesContainer.removeClass('is-fixed').css({
				'left': 0,
				'top': 0,
			});
		}
	}
	function updateCategoryBehindPosition() {
	    var offsetHeader = $('#header').innerHeight();
	    var top = $('.faq').offset().top ,
			height = jQuery('.faq').innerHeight() - jQuery('.categories1').innerHeight(),
			margin = 20;

	    if (top - margin <= $(window).scrollTop() + offsetHeader && top - margin + height > $(window).scrollTop() + offsetHeader) {
	        var leftValue = faqsCategoriesContainer1.offset().left,
				widthValue = faqsCategoriesContainer1.width();
	        faqsCategoriesContainer1.addClass('is-fixed').css({
	            'left': leftValue,
	            'top': margin + offsetHeader + 200,
	            '-moz-transform': 'translateZ(0)',
	            '-webkit-transform': 'translateZ(0)',
	            '-ms-transform': 'translateZ(0)',
	            '-o-transform': 'translateZ(0)',
	            'transform': 'translateZ(0)',
	        });
	    } else if (top - margin + height <= $(window).scrollTop() + offsetHeader) {
	        var delta = top - margin + height - $(window).scrollTop() - offsetHeader + 200;
	        faqsCategoriesContainer1.css({
	            '-moz-transform': 'translateZ(0) translateY(' + delta + 'px)',
	            '-webkit-transform': 'translateZ(0) translateY(' + delta + 'px)',
	            '-ms-transform': 'translateZ(0) translateY(' + delta + 'px)',
	            '-o-transform': 'translateZ(0) translateY(' + delta + 'px)',
	            'transform': 'translateZ(0) translateY(' + delta + 'px)',
	        });
	        
	    } else {
	        faqsCategoriesContainer1.removeClass('is-fixed').css({
	            'left': 0,
	            'top': 200,
	        });
	    }
	}

	function updateSelectedCategory() {
        var offsetHeader =  $('#header').innerHeight();
		faqsSections.each(function(){
			var actual = $(this),
				margin = parseInt($('.faq-title').eq(1).css('marginTop').replace('px', '')),
				activeCategory = $('.categories a[href="#'+actual.attr('id')+'"]'),
				topSection = (activeCategory.parent('li').is(':first-child')) ? 0 : Math.round(actual.offset().top);
			
			if ( ( topSection - 20 <= $(window).scrollTop() + offsetHeader ) && ( Math.round(actual.offset().top) + actual.height() + margin - 20 > $(window).scrollTop() + offsetHeader ) ) {
				activeCategory.addClass('selected');
			}else {
				activeCategory.removeClass('selected');
			}
		});
	}
});