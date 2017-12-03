import NotificationsController from './notificationsController';
import NotificationService from './notificationService';
import $ from 'jquery';
import _ from 'lodash';

const notificationService = new NotificationService();
const notificationTemplate = _.template($('#notifications-template').html());
const notificationsController = new NotificationsController(
  notificationService,
  notificationTemplate
);

$(document).ready(() => notificationsController.init());
