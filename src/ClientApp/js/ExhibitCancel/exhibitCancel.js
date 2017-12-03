import ExhibitCancelController from './exhibitCancelController';
import ExhibitService from './exhibitService';
import $ from 'jquery';

const exhibitService = new ExhibitService();
const exhibitCancelController = new ExhibitCancelController(exhibitService);
$(document).ready(() => exhibitCancelController.init());
