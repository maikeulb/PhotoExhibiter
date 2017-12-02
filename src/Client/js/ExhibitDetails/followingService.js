export default class FollowingService {
  constructor() {
    this.createFollowing = this.createFollowing.bind(this);
    this.deleteFollowing = this.deleteFollowing.bind(this);
  }

  createFollowing(followeeId, done, fail) {
    const contentTypeAttribute = 'application/json';
    const urlAttribute = '/api/followings';
    const dataAttribute = JSON.stringify({
      followeeId: followeeId
    });

    $.ajax({
      url: urlAttribute,
      method: 'POST',
      contentType: contentTypeAttribute,
      data: dataAttribute
    })
      .done(done)
      .fail(fail);
  }

  deleteFollowing(followeeId, done, fail) {
    const contentTypeAttribute = 'application/json';
    const urlAttribute = '/api/followings';
    const dataAttribute = JSON.stringify({
      followeeId: followeeId
    });
    $.ajax({
      url: urlAttribute,
      method: 'DELETE',
      contentType: contentTypeAttribute,
      data: dataAttribute
    })
      .done(done)
      .fail(fail);
  }
}
