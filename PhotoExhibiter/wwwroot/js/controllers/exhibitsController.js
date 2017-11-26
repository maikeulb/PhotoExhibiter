var ExhibitsController = function (attendanceService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);

        var exhibitId = button.attr("data-exhibit-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(exhibitId, done, fail);
        else
            attendanceService.deleteAttendance(exhibitId, done, fail);
    };

    var done = function () {
        var text = (button.text() == "Going") ? "Going?" : "Going";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something failed");
    };

    return {
        init: init
    }

}(AttendanceService);
