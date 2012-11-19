(function ($) {
    $(function () {
        //$('.spinner').spinner();

        $('.date').datepicker();

        $('[rel="popover"]').filter('a').attr('href', '#').end().popover({ content: function () { return $($(this).data('source')).clone(); } });

        $('.fancybox').fancybox();

        $('.rating').each(function () {
            var that = $(this);

            that.rating(that.data('url'), { maxvalue: 5, increment: 1, curvalue: that.data('curvalue') });
        });
    });
})(jQuery);