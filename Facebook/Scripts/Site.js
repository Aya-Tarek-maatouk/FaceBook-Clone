$(function () {
    function fun() {
        $("#Create_PostModal").modal('hide');

    };
    function fun2() {

        $("#HI").modal('hide');

    };
    function editeWorkfun() {
        $("#EditWorkMainModal").modal('toggle');

    };

    function skillclick() {
        $.ajax({
            url: '@Url.Action("create_skill")',
            type: 'POST',
            data: { skill: $("#skill_txt").val() },


            success: function (d) {


                $("#skillUL").append("<li class='list-group-item'><span>" + $("#skill_txt").val() + "</span></li >");
            }
        });
    };


    function Workclick() {
        $.ajax({
            url: '@Url.Action("CreateWork")',
            type: 'POST',
            data: { work: $("#Work_txt").val() },


            success: function (d, s, x) {


                $("#WorkUL").append("<li class='list-group-item'><span>" + $("#Work_txt").val() + "</span></li >");
            }
        });
    };

    function editeskillfun() {
        $("#EditSkillMainModal").modal('toggle');

    }


    function EDUclick() {
        $.ajax({
            url: '@Url.Action("CreateEducation")',
            type: 'POST',
            data: { EDU: $("#Edu_txt").val() },


            success: function (d, s, x) {


                $("#EduUL").append("<li class='list-group-item'><span>" + $("#Edu_txt").val() + "</span></li >");
            }
        });
    };





    function fireelment(c) {
        //document.getElementsByClassName("Postform")[c].submit();
        $(".CommentsModat").modal('hide');
        $(".CommentsModat:eq(" + c + ")").modal('show');

    };
    $(".commentsCard").show();

    function togglecomments(c) {

        $(".commentsCard:eq(" + c + ")").toggle();
    };

    function openeditmodal(c) {
        $(".editModal").modal('hide');
        $(".editModal:eq(" + c + ")").modal('show');
    };

});
