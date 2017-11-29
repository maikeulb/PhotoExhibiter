import ExhibitsController from './exhibitsController';
import attendanceService from './attendanceService';
import { $, jQuery } from 'jquery';

const controller = new ExhibitsController(attendanceService);
controller.init('#exhibits');
