import FollowingsController from './followingsController';
import FollowingService from './followingService';

const followingService = new FollowingService();
const followingsController = new FollowingsController(followingService);

$(document).ready(() => followingsController.init('.js-toggle-follow'));
