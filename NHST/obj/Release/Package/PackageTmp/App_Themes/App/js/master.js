jQuery(document).ready(function($){
    var iOS = /iPad|iPhone|iPod/.test(navigator.userAgent);
    if(iOS){
        $(document.body).addClass('ios');
    };

    $(document).on('click', '.block-toggle', function(e){
        e.preventDefault();
        var target = $(this).attr('href');
        if(!target) return;
        $(target).slideToggle();
    });
    $('.collapse-toggle').each(function(){
        var btnObj = this;

        this.addEventListener('click', function(e){
            e.preventDefault();
            var stringShow = $(this).attr('data-show') || '';
            var stringHide= $(this).attr('data-hide') || '';
            var cnt = $(this).closest('.collapse-wrap').find('.collapse-content');

            cnt.stop().slideToggle(function(){
                if(cnt.is(":hidden")){
                    $(btnObj).html(stringHide);
                }
                if(cnt.is(":visible")){
                    $(btnObj).html(stringShow);
                } 
            });
        });
    });
    $('.up-downControl').each(function(){
        var $this = $(this);
        var step = parseInt($this.attr('data-step'));
        var min = parseInt($this.attr('data-min'));
        var max = parseInt($this.attr('data-max'));        

        $this.find('.ud-ct').on('click', function(e){
            e.preventDefault();
            var value = parseInt($this.find('.value').val());
            if($(this).hasClass('minus')){
                value = value - step;
                if(value < min) return;
            }
            if($(this).hasClass('plus')){
                value = value + step;
                if(value > max) return;                
            }

            $this.find('.value').val(value);

        });       
    });


});
