export default class NotificationService {
  constructor() {
    this.getNotifications = this.getNotifications.bind(this);
  }

  getNotifications(notificationTemplate, done, fail) {
    const urlAttribute = '/api/notifications';

    $.getJSON(urlAttribute, notifications => {
      if (notifications.length == 0) return;

      $('.js-notifications-count')
        .text(notifications.length)
        .removeClass('hide');

      $('.notifications')
        .popover({
          html: true,
          title: 'Notifications',
          content: () => notificationTemplate({ notifications: notifications }),
          placement: 'left'
        })
        .on('shown.bs.popover', () => {
          const contentTypeAttribute = 'application/json';
          const urlAttribute = '/api/notifications';
          $.ajax({
            url: urlAttribute,
            method: 'POST',
            contentType: contentTypeAttribute
          })
            .done(done)
            .fail(fail);
        });
    });
  }
}
