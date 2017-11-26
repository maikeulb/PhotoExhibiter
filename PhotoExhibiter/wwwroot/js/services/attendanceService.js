var AttendanceService = function () {
    var createAttendance = function (exhibitId, done, fail) {
        var contentTypeAttribute = 'application/json; charset=utf-8';
        var urlAttribute = "/api/attendances";
        var dataAttribute = JSON.stringify({
          exhibitId: exhibitId 
        });

        $.ajax({
            url: urlAttribute,
            method: "POST",
            contentType: contentTypeAttribute,
            data: dataAttribute
         })
            .done(done)
            .fail(fail);
    };

    var deleteAttendance = function (exhibitId, done, fail) {
        var contentTypeAttribute = 'application/json; charset=utf-8';
        var urlAttribute = "/api/attendances" + exhibitId;
        $.ajax({
            url: urlAttribute,
            method: "DELETE",
            contentType: contentTypeAttribute,
        })
            .done(done)
            .fail(fail);
    };

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();
