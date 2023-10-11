(function ($) {
    
    // Ripple-effect animation Code
    $(".ripple-effect").click(function (e) {
        var rippler = $(this);

      	rippler.append("<span class='ink'></span>");

        var ink = rippler.find(".ink:last-child");
        // prevent quick double clicks Code
        ink.removeClass("animate");

        if (!ink.height() && !ink.width()) {
            var d = Math.max(rippler.outerWidth(), rippler.outerHeight());
            ink.css({
                height: d,
                width: d
            });
        }

        // get click coordinates Code
        var x = e.pageX - rippler.offset().left - ink.width() / 2;
        var y = e.pageY - rippler.offset().top - ink.height() / 2;

        ink.css({
            top: y + 'px',
            left: x + 'px'
        }).addClass("animate");
        setTimeout(function(){
        	ink.remove();
        },1000)
    })

// Ripple effect animation Code
   function fullRipper(color,time){
       setTimeout(function(){
            var rippler = $(".ripple-effect-All");
            if(rippler.find(".ink-All").length == 0){
                rippler.append("<span class='ink-All'></span>");
                

                var ink = rippler.find(".ink-All");

                if (!ink.height() && !ink.width()) {
                    var d = Math.max(rippler.outerWidth(), rippler.outerHeight());
                    ink.css({
                        height: d,
                        width: d
                    });
                }

                // get click coordinates Code
                var x =0;
                var y =rippler.offset().top - ink.height()/1.5;

                ink.css({
                    top: y + 'px',
                    left: x + 'px',
                    background:color
                }).addClass("animate");

                rippler.css('z-index',2);

                setTimeout(function(){
                    ink.css({
                        '-webkit-transform': 'scale(2.5)',
                        '-moz-transform': 'scale(2.5)',
                        '-ms-transform': 'scale(2.5)',
                        '-o-transform': 'scale(2.5)',
                        'transform': 'scale(2.5)'
                    })
                    rippler.css('z-index',0);
                },1500);
            }
       },time)
        
    }

    // Form border-bottom line Code
    $('.blmd-line .form-control').bind('focus',function(){
        $(this).parent('.blmd-line').addClass('blmd-toggled').removeClass("hf");
    }).bind('blur',function(){
        var val=$(this).val();
        if(val){
            $(this).parent('.blmd-line').addClass("hf");
        }else{
            $(this).parent('.blmd-line').removeClass('blmd-toggled');
        }
    })


    $(document).ready(function () {
        // Function to validate the login form
        $("#login-form").submit(function (event) {
            var email = $("#Email").val();
            var password = $("#Password").val();

            // Check if email and password are empty
            if (!email) {
                $("#email-error-message").text("Vui lòng điền email của bạn.").show();
                event.preventDefault();
            } else {
                $("#email-error-message").hide();
            }

            if (!password || password.trim() === "") {
                $("#password-error-message").text("Vui lòng điền mật khẩu của bạn.").show();
                event.preventDefault();
            } else {
                $("#password-error-message").hide();
            }
        });

        // Function to validate the register form
        $("#Register-form").submit(function (event) {
            var firstname = $("#firstname").val();
            var lastname = $("#lastname").val();
            var username = $("#username").val();
            var password = $("#password").val();
            var rePassword = $("#rePassword").val();

            // Check if any field is empty
            if (!firstname) {
                $("#firstname-error-message").text("Vui lòng điền tên của bạn.").show();
                event.preventDefault();
            } else {
                $("#firstname-error-message").hide();
            }

            if (!lastname) {
                $("#lastname-error-message").text("Vui lòng điền họ của bạn.").show();
                event.preventDefault();
            } else {
                $("#lastname-error-message").hide();
            }

            if (!username) {
                $("#username-error-message").text("Vui lòng điền email của bạn.").show();
                event.preventDefault();
            } else {
                $("#username-error-message").hide();
            }

            if (!password || password.trim() === "") {
                $("#password-error-message1").text("Vui lòng điền mật khẩu của bạn.").show();
                event.preventDefault();
            } else {
                $("#password-error-message1").hide();
            }


            if (!rePassword) {
                $("#rePassword-error-message").text("Vui lòng nhập lại mật khẩu của bạn.").show();
                event.preventDefault();
            } else {
                $("#rePassword-error-message").hide();
            }
        });
    });


})(jQuery);