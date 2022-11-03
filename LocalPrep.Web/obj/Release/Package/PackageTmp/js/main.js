
$(document).ready(function() {

   // screenClass();
    $(".nav li a").each(function() {
        //console.log($(this).attr('href'));
      //
        if ((window.location.pathname.indexOf($(this).attr('href'))) > -1) {
            $(".user-menu li a").removeClass('active');
            $(this).addClass('active');
        }
    });

});
