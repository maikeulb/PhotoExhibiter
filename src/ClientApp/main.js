'use strict';

import _ from 'lodash';
window.lodash = _;
window._ = _;

import bootbox from 'bootbox';
window.bootbox = bootbox;

import moment from 'moment';
window.moment = moment;

import 'bootstrap';

import './main.scss';

const globe_container = document.getElementById('fa_globe');
globe_container.innerHTML = '<i class="fa fa-globe"></i>';

const search_container = document.getElementById('fa_search');
search_container.innerHTML = '<i class="fa fa-search"></i>';
