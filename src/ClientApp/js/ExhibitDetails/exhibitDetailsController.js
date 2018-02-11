export default class ExhibitDetailsController {
  constructor(followingService) {
    this.followingService = followingService;
    this.followButton = null;
  }

  init(container) {
    $(container)
      .click(this.toggleFollowing.bind(this)
      );
  }

  toggleFollowing(e) {
    this.followButton = $(e.target);
    const followeeId = this.followButton.attr('data-user-id');
    if (this.followButton.hasClass('btn-outline-secondary'))
      this.followingService.createFollowing(
        followeeId,
        this.done.bind(this),
        this.fail.bind(this)
      );
    else
      this.followingService.deleteFollowing(
        followeeId,
        this.done.bind(this),
        this.fail.bind(this)
      );
  }

  done() {
    const text = this.followButton.text() == 'Follow' ? 'Following' : 'Follow';
    this.followButton
      .toggleClass('btn-outline-info')
      .toggleClass('btn-outline-secondary')
      .text(text);
  }

  fail() {
    return alert('Something failed');
  }
}
