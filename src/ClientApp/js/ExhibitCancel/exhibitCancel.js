import ExhibitCancelController from './exhibitCancelController';
import ExhibitService from './exhibitService';

const exhibitService = new ExhibitService();
const exhibitCancelController = new ExhibitCancelController(exhibitService);
$(document).ready(() => exhibitCancelController.init());
