import ExhibitsController from './exhibitsController';
import AttendanceService from './attendanceService';
import { $, jQuery } from 'jquery';

const attendanceService = new AttendanceService();
const controller = new ExhibitsController(attendanceService);
controller.init('#exhibits');
