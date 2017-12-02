export default class AttendanceService {
  constructor() {
    this.createAttendance = this.createAttendance.bind(this);
    this.deleteAttendance = this.deleteAttendance.bind(this);
  }

  createAttendance(exhibitId, done, fail) {
    const contentTypeAttribute = 'application/json';
    const urlAttribute = '/api/attendances';
    const dataAttribute = JSON.stringify({
      exhibitId: exhibitId
    });

    $.ajax({
      url: urlAttribute,
      method: 'POST',
      contentType: contentTypeAttribute,
      data: dataAttribute
    })
      .done(done)
      .fail(fail);
  }

  deleteAttendance(exhibitId, done, fail) {
    const contentTypeAttribute = 'application/json; charset=utf-8';
    const urlAttribute = '/api/attendances';
    const dataAttribute = JSON.stringify({
      exhibitId: exhibitId
    });

    $.ajax({
      url: urlAttribute,
      method: 'DELETE',
      contentType: contentTypeAttribute,
      data: dataAttribute
    })
      .done(done)
      .fail(fail);
  }
}
