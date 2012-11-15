(function ($) {
    $(function () {
        //$('.spinner').spinner();

        $('[rel="popover"]').filter('a').attr('href', '#').end().popover({ content: function () { return $($(this).data('source')).clone(); } });

        $('.fancybox').fancybox();
    });
})(jQuery);