export default class notificationsController {
  constructor(notificationService) {
    this.notificationService = notificationService;
    this.notificationTemplate = notificationTemplate;
  }

  init() {
    return this.getBadgeNotifications
      .bind(this)
      .on(
        'shown.bs.popover',
        this.readNotifications(this.done.bind(this), this.fail.bind(this))
      );
  }

  getBadgeNotifications() {
    return this.notificationService.getNotifications();
  }

  readBadgeNotifications(notificationTemplate) {
    return this.notificationService.readNotifications(
      this.notificationTemplate,
      this.done.bind(this),
      this.fail.bind(this)
    );
  }

  done() {
    return $('.js-notifications-count')
      .text('')
      .addClass('hide');
  }

  fail() {
    return alert('Something failed');
  }
}
