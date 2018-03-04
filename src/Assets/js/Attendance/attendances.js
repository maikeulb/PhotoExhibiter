import AttendancesController from './attendancesController';
import AttendanceService from './attendanceService';

const attendanceService = new AttendanceService();
const attendancesController = new AttendancesController(attendanceService);

$(document).ready(() => attendancesController.init('#exhibits'));
