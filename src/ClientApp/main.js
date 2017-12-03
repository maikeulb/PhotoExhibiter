'use strict';

import $ from 'jquery';
window.jQuery = $;
window.$ = $;

import _ from 'lodash';
window.lodash = _;
window._ = _;

import bootbox from 'bootbox';
window.bootbox = bootbox;

import moment from 'moment';
window.moment = moment;

import 'popper.js';
import 'bootstrap';

import './main.scss';

const container = document.getElementById('nav_globe_icon');
container.innerHTML = '<i class="fa fa-globe"></i>';
