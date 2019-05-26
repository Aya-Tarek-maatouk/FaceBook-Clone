
$(function () {

    MyHub = $.connection.chat;

    //Hub Connection
    $.connection.hub.start();



    //change every int userid to string
    $(window).on('keydown', function (e) {
        if (e.which == 13) {

            message = $(".message-input input").val();
            if ($.trim(message) == '') {
                return false;
            }

            SenderID = $("#CurrentUserID").val();
            reseverID = $("#CurrentFriendID").val();
            MyHub.server.message(SenderID, reseverID, message);
            return false;
        }
    });
    $('.submit').click(function () {

        message = $(".message-input input").val();
        if ($.trim(message) == '') {
            return false;
        }
        SenderID = $("#CurrentUserID").val();
        reseverID = $("#CurrentFriendID").val();
        MyHub.server.message(SenderID, reseverID, message);
    });




    MyHub.client.newMessage = function (ImagePath, Sender_Name, message, Class) {
        $('<li class="sent"><img src="'+ImagePath+'"  alt="" /><p>' + message + '</p></li>').appendTo($('.messages ul'));
        //$('.message-input input').val(null);
        //$('.contact.active .preview').html('<span>You: </span>' + message);
        $(".messages").animate({ scrollTop: $(document).height()}, "fast");
    };
    $('#contacts ul').on('click', "li.GoToMessage", function (e) {
        $('#contacts ul li.GoToMessage').removeClass("active");
        $(this).addClass("active");
        $(this).find(".myforms").submit();
        $('.messages').slideToggle();
    });

    $('#contacts ul li.GoToMessage:eq(0)').trigger('click');

   

});



