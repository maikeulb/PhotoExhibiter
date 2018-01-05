import _ from 'lodash';
window.lodash = _;
window._ = _;

import $ from 'jquery';
window.jQuery = $;
window.$ = $;

import 'bootstrap';
import 'ekko-lightbox';

import bootbox from 'bootbox';
window.bootbox = bootbox;

import moment from 'moment';
window.moment = moment;

import Rellax from 'rellax';
window.Rellax = Rellax;

import datepicker from 'bootstrap-datepicker';
window.datepicker = datepicker;

import logoSmall from './images/logo.png';
var logoImg = document.getElementById('logo_sm');
logoImg.src = logoSmall;

import banner from './images/banner.jpg';
var bannerImg = document.getElementById('banner');
bannerImg.src =  banner;

import './main.scss';

const globeContainer = document.getElementById('fa_globe');
globeContainer.innerHTML =
  '<i class="fa fa-globe" style="padding:8px; vertical-align: middle";></i>';
