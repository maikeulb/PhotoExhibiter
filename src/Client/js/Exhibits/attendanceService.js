export default class AttendanceService {
  contstructor(exhibitId, done, fail) {
    this.exhibitId = exhibitId;
    this.done = done;
    this.fail = fail;
  }

  createattendance(exhibitId, done, fail) {
    const contentTypeAttribute = 'application/json; charset=utf-8';
    const urlAttribute = '/api/attendances';
    const dataAttribute = JSON.stringify({
      exhibitId: exhibitId
    });

    return $.ajax({
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

    return $.ajax({
      url: urlAttribute,
      method: 'DELETE',
      contentType: contentTypeAttribute,
      data: dataAttribute
    })
      .done(done)
      .fail(fail);
  }
}
