import NotificationsController from './notificationsController';
import NotificationService from './notificationService';
import $ from 'jquery';
import _ from 'underscore';

const notificationService = new NotificationService();

const precompiledTemplate = _.template(
            '<%
                _.each(notifications, function(notification){
                    if (notification.type == 1) { %>
                        <li><span class="highlight">
                        <%= notification.exhibit.photographer.name %></span>
                        has canceled the exhibit at
                        <%= notification.exhibit.location %> at
                        <%= moment(notification.exhibit.dateTime).format("D MMM HH:mm") %>.</li>
                    <% }
                    else if (notification.type == 2) {
                            var changes = [],
                            originalValues = [],
                            newValues = [];
                         if (notification.originalLocation !=
                            notification.exhibit.location) {
                            changes.push('location');
                            originalValues.push(notification.originalLocation);
                            newValues.push(notification.exhibit.location);
                        }
                        if (notification.originalDateTime != notification.exhibit.dateTime) {
                            changes.push('date/time');
                            originalValues.push(moment(notification.originalDateTime).format("D
                              MMM HH:mm"));
                            newValues.push(moment(notification.exhibit.dateTime).format("D
                              MMM HH:mm");
                        }
                    %>
                        <li><span class="highlight">
                        <%= notification.exhibit.photographer.name %></span> has changed the
                        <%= changes.join(' and ') %> of the exhibit from
                        <%= originalValues.join('/') %> to <%= newValues.join('/') %></li>
                <%
                }
            })
            %>'
                );

// const notificationTemplate = _.template($precompiledTemplate.html());

// const notificationsController = new NotificationsController(
// notificationService,
// notificationTemplate
// );

// $(document).ready(() => notificationsController.init());
