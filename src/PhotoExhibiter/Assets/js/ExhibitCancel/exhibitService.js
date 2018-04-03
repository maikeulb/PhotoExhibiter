export default class exhibitService {
  constructor() {
    this.cancelExhibit = this.cancelExhibit.bind(this);
  }

  cancelExhibit(exhibitId, done, fail) {
    const contentTypeAttribute = 'application/json';
    const urlAttribute = '/api/exhibits';
    const dataAttribute = JSON.stringify({
      Id: exhibitId
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
