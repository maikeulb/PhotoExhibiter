import NotificationsController from './notificationsController';
import NotificationService from './notificationService';

const notificationService = new NotificationService();
const notificationTemplate = _.template($('#notifications-template').html());
const notificationsController = new NotificationsController(
  notificationService,
  notificationTemplate
);

$(document).ready(() => notificationsController.init());