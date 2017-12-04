import ExhibitsController from './exhibitsController';
import AttendanceService from './attendanceService';

const attendanceService = new AttendanceService();
const exhibitsController = new ExhibitsController(attendanceService);

$(document).ready(() => exhibitsController.init('#exhibits'));
