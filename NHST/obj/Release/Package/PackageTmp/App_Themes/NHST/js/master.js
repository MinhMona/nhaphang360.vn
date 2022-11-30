jQuery(document).ready(function($){
    $('.navbar-toggle').on('click',function(e){
        $(this).toggleClass('open');
       
    });
    //    $('.nav-overlay').on('click',this,function(e){
    //        $('.navbar-toggle').trigger('click');
    //    });

    

    $('.dropdown').hover(function(){
        var parent = $(this);
        parent.find('.sub-menu-wrap').stop().slideDown(300,function(){
            $(this).addClass('open');
        });
    },function(){
        $(this).children('.sub-menu-wrap').stop().slideUp(300, function () {
            $(this).removeClass('open');
       });       
    });
    $('.ratting input[type="range"]').on('change',function(e){
        $(this).parent('.ratting').children('.rate').css('width',$(this).val()+'%');
    });
    $('.panel').on('click','.panel-heading',function(){
        var container = $(this).parent('.panel');
        $(container).siblings().removeClass('on').find('.panel-body').slideUp();
        $(container).find('.panel-body').is(':visible')  ?  
        $(container).removeClass('on').find('.panel-body').slideUp() :
        $(container).addClass('on').find(':hidden').slideDown() ;
        
    });
    
    if($('#slider-quote').length){
        $('#slider-quote').slick({
            dots: true,
            arrows:false,
            infinite: false,
            speed: 300,
            slidesToShow: 1,
            slidesToScroll: 1,
            fade: true,
            autoplay: false,
            autoplaySpeed: 2000,
            responsive: [
                    {
                      breakpoint: 480,
                      settings: {
                        
                      }
                    }
                    // You can unslick at a given breakpoint now by adding:
                    // settings: "unslick"
                    // instead of a settings object
                  ]
        });
    }
    if($('#step_register_slider').length){
        $('#step_register_slider').slick({
            dots: false,
            arrows:false,
            infinite: false,
            speed: 300,
            slidesToShow: 1,
            slidesToScroll: 1,
            fade: true,
            draggable: false,
            responsive: [
                    {
                      breakpoint: 480,
                      settings: {
                        
                      }
                    }
                    // You can unslick at a given breakpoint now by adding:
                    // settings: "unslick"
                    // instead of a settings object
                  ]
        });
        $('[data-toggle="register-steps"]').on('click',function(){
            var slideIndex = parseInt($(this).index());
            $('#step_register_slider').slick('slickGoTo',slideIndex);
            $(this).addClass('active current').siblings().removeClass('current');
            
            
        });
        
        
    }
    
    
 
         var lastScrollTop = 0;
         $(window).on('scroll', function(){
            var st = $(this).scrollTop();
            if (st > lastScrollTop){
               // downscroll code
                if($('#truck-decor').length){playfunc($('#truck-decor'), 500);}
                
                
            } else {
              // upscroll code
                if($('#truck-decor').length){playfunc($('#truck-decor'), 500);}
                
               
            }
            lastScrollTop = st;
              
            
        });

    
    function playfunc(blocks,offset){
            
        blocks.each(function(){
            var trackCont = $(this);
            var winTop = $(window).scrollTop();
//                    top cont && bottom cont
            winTop > trackCont.offset().top - offset  ? trackCont.addClass('gogo') : trackCont.removeClass('gogo');
           
        });
    }
    
    
    
    
    
    if($('#map-canvas').length){
        google.maps.event.addDomListener(window, 'load', initmap);
    
    
    var infowindow = new google.maps.InfoWindow({
            size: new google.maps.Size(150, 50)
        });
    var map;
    var gmarkers = [];
    function createMarker(latlng, title) {
        var marker = new google.maps.Marker({
            position: latlng,
            title: title,
            icon: "/App_Themes/NHST/images/map-marker.png",
        });
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
    function initmap() {
        var myLatlng = new google.maps.LatLng(21.026653, 105.827921);
        var mapOptions = {
            // How zoomed in you want the map to start at (always required)
            zoom: 17,
            disableDefaultUI: true,
            scrollwheel: false,
            zoomControl: true,
            draggable: true,
          zoomControlOptions: {
//              position: google.maps.ControlPosition.BOTTOM_LEFT
          },
            // The latitude and longitude to center the map (always required)
            center: myLatlng,
            // How you would like to style the map. 
            // This is where you would paste any style found on Snazzy Maps.
            styles: [{"featureType":"administrative","elementType":"labels.text.fill","stylers":[{"color":"#6195a0"}]},{"featureType":"administrative.province","elementType":"geometry.stroke","stylers":[{"visibility":"off"}]},{"featureType":"landscape","elementType":"geometry","stylers":[{"lightness":"0"},{"saturation":"0"},{"color":"#f5f5f2"},{"gamma":"1"}]},{"featureType":"landscape.man_made","elementType":"all","stylers":[{"lightness":"-3"},{"gamma":"1.00"}]},{"featureType":"landscape.natural.terrain","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"poi","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"poi.park","elementType":"geometry.fill","stylers":[{"color":"#bae5ce"},{"visibility":"on"}]},{"featureType":"road","elementType":"all","stylers":[{"saturation":-100},{"lightness":45},{"visibility":"simplified"}]},{"featureType":"road.highway","elementType":"all","stylers":[{"visibility":"simplified"}]},{"featureType":"road.highway","elementType":"geometry.fill","stylers":[{"color":"#fac9a9"},{"visibility":"simplified"}]},{"featureType":"road.highway","elementType":"labels.text","stylers":[{"color":"#4e4e4e"}]},{"featureType":"road.arterial","elementType":"labels.text.fill","stylers":[{"color":"#787878"}]},{"featureType":"road.arterial","elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"all","stylers":[{"visibility":"simplified"}]},{"featureType":"transit.station.airport","elementType":"labels.icon","stylers":[{"hue":"#0a00ff"},{"saturation":"-77"},{"gamma":"0.57"},{"lightness":"0"}]},{"featureType":"transit.station.rail","elementType":"labels.text.fill","stylers":[{"color":"#43321e"}]},{"featureType":"transit.station.rail","elementType":"labels.icon","stylers":[{"hue":"#ff6c00"},{"lightness":"4"},{"gamma":"0.75"},{"saturation":"-68"}]},{"featureType":"water","elementType":"all","stylers":[{"color":"#eaf6f8"},{"visibility":"on"}]},{"featureType":"water","elementType":"geometry.fill","stylers":[{"color":"#c7eced"}]},{"featureType":"water","elementType":"labels.text.fill","stylers":[{"lightness":"-49"},{"saturation":"-53"},{"gamma":"0.79"}]}]
        };
    var mapElement = document.getElementById('map-canvas');

    // Create the Google Map using our element and options defined above
    map = new google.maps.Map(mapElement, mapOptions);
    createMarker(myLatlng, '<img src="/App_Themes/NHST/images/icon.png" style="width:20px;vertical-align:sub" alt="1688 Express"> <strong>1688 Express</strong>').setMap(map);
    }
    
    }

});