
 /* jQuery Preloader
  -----------------------------------------------*/
$(window).load(function(){
    $('.preloader').fadeOut(1000); // set duration in brackets    
});


$(document).ready(function() {

  /* Hide mobile menu after clicking on a link
    -----------------------------------------------*/
    $('.navbar-collapse a').click(function(){
        $(".navbar-collapse").collapse('hide');
    });


  /* Parallax section
    -----------------------------------------------*/
  function initParallax() {
    $('#home').parallax("100%", 0.1);
    $('#blog').parallax("100%", 0.3);

  }
  initParallax();


  /* wow
  -------------------------------*/
  new WOW({ mobile: false }).init();

  });

