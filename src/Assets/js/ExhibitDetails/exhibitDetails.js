import ExhibitDetailsController from './exhibitDetailsController';
import FollowingService from './followingService';

const followingService = new FollowingService();
const exhibitDetailsController = new ExhibitDetailsController(followingService);

$(document).ready(() => exhibitDetailsController.init('.js-toggle-follow'));
