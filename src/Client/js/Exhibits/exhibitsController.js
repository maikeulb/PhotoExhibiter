import { attendanceService } from './attendanceService';

export default class ExhibitController {
  constructor(service) {
    this.attendanceService = attendanceService;
  }

  init(container) {
    return $(container).on(
      'click',
      '.js-toggle-attendance',
      this.toggleAttendance
    );
  }

  toggleAttendance(e) {
    button = $(e.target);

    exhibitId = button.attr('data-exhibit-id');

    if (button.hasClass('btn-default'))
      return attendanceService.createAttendance(exhibitId, done, fail);
    else return attendanceService.deleteAttendance(exhibitId, done, fail);
  }

  done() {
    const text = button.text() == 'Going' ? 'Going?' : 'Going';

    return button
      .toggleClass('btn-info')
      .toggleClass('btn-default')
      .text(text);
  }

  fail() {
    return alert('Something failed');
  }
}
