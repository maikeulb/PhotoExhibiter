var FollowingService = function () {
    var createFollowing = function (followeeId, done, fail) {
        var contentTypeAttribute = 'application/json; charset=utf-8';
        var urlAttribute = "/api/followings";
        var dataAttribute = JSON.stringify({
          followeeId: followeeId 
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

    var deleteFollowing = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/followings/" + followeeId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }
}();
