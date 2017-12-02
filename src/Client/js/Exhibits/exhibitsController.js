export default class ExhibitController {

  constructor(attendanceService) {
    this.attendanceService = attendanceService;
    this.button = null;
  }

  init(container) {
    return $(container).on(
      'click',
      '.js-toggle-attendance',
      this.toggleAttendance.bind(this)
    );
  }

  toggleAttendance(e) {
    this.button = $(e.target);
    const exhibitId = this.button.attr('data-exhibit-id');
    if (this.button.hasClass('btn-secondary'))
      return this.attendanceService.createAttendance(
        exhibitId,
        this.done.bind(this),
        this.fail.bind(this)
      );
    else
      return this.attendanceService.deleteAttendance(
        exhibitId,
        this.done.bind(this),
        this.fail.bind(this)
      );
  }

  done() {
    const text = this.button.text() == 'Going' ? 'Going?' : 'Going';
    return this.button
      .toggleClass('btn-info')
      .toggleClass('btn-secondary')
      .text(text);
  }

  fail() {
    return alert('Something failed');
  }
}
