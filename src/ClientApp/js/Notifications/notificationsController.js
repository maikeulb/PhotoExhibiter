export default class NotificationsController {
  constructor(notificationService, notificationTemplate) {
    this.notificationService = notificationService;
    this.notificationTemplate = notificationTemplate;
  }

  init() {
    this.getBadgeNotifications();
  }

  getBadgeNotifications(notificationTemplate, done, fail) {
    this.notificationService.getNotifications(
      this.notificationTemplate,
      this.done.bind(this),
      this.fail.bind(this)
    );
  }

  done() {
    $('.js-notifications-count')
      .text('')
      .addClass('hide');
  }

  fail() {
    alert('Something failed');
  }
}
