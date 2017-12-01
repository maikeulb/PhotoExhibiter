import ExhibitDetailsController from './exhibitDetailsController';
import FollowingService from './followingService';
import $ from 'jquery';

const followingService = new FollowingService();
const exhibitDetailsController = new ExhibitDetailsController(followingService);
$(document).ready(() => exhibitDetailsController.init());
