export default class AttendancesController {
  constructor(attendanceService) {
    this.attendanceService = attendanceService;
    this.attendanceButton = null;
  }

  init(container) {
    $(container).on(
      'click',
      '.js-toggle-attendance',
      this.toggleAttendance.bind(this)
    );
  }

  toggleAttendance(e) {
    this.attendanceButton = $(e.target);
    const exhibitId = this.attendanceButton.attr('data-exhibit-id');
    if (this.attendanceButton.hasClass('btn-outline-secondary'))
      this.attendanceService.createAttendance(
        exhibitId,
        this.done.bind(this),
        this.fail.bind(this)
      );
    else
      this.attendanceService.deleteAttendance(
        exhibitId,
        this.done.bind(this),
        this.fail.bind(this)
      );
  }

  done() {
    this.attendanceButton.toggleClass('btn-outline-info').toggleClass('btn-outline-secondary');
    if (this.attendanceButton.hasClass('btn-outline-info')) {
      this.attendanceButton.text('Attending');
    }
    if (this.attendanceButton.hasClass('btn-outline-secondary')) {
      this.attendanceButton.text('Attend');
    }
  }

  fail() {
    return alert('Something failed');
  }
}
