import exhibitService from './exhibitService';

export default class ExhibitCancelController {
  constructor(exhibitService) {
    this.exhibitService = exhibitService;
    this.cancelLink = null;
  }

  init() {
    $('.js-cancel-exhibit').click(this.cancelExhibit.bind(this));
  }

  cancelExhibit(e) {
    this.cancelLink = $(e.target);
    const exhibitId = this.cancelLink.attr('data-exhibit-id');

    bootbox.dialog({
      message: 'Are you sure you want to cancel this Exhibit?',
      title: 'Confirmation',
      buttons: {
        no: {
          label: 'No',
          className: 'btn-outline-secondary',
          callback: () => bootbox.hideAll()
        },
        yes: {
          label: 'Yes',
          className: 'btn-outline-danger',
          callback: () =>
            this.exhibitService.cancelExhibit(
              exhibitId,
              this.done.bind(this),
              this.fail.bind(this)
            )
        }
      }
    });
  }

  done() {
    this.cancelLink.parents('li').fadeOut(() => this.cancelLink.remove());
  }

  fail() {
    alert('Something failed!');
  }
}
