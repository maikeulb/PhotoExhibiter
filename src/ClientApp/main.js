import _ from 'lodash';
window.lodash = _;
window._ = _;

import bootbox from 'bootbox';
window.bootbox = bootbox;

import moment from 'moment';
window.moment = moment;

import 'bootstrap';

import Rellax from 'rellax';
let rellax = new Rellax('.rellax') 

import logoSmall from './images/logo.png';
var logoImg = document.getElementById('logo_sm');
logoImg.src = logoSmall;

import banner from './images/banner.jpg';
var bannerImg = document.getElementById('banner');
bannerImg.src = banner;

import './main.scss';

const globeContainer = document.getElementById('fa_globe');
globeContainer.innerHTML =
  '<i class="fa fa-envelope" style="padding:8px; vertical-align: middle";></i>';

const searchContainer = document.getElementById('fa_search');
searchContainer.innerHTML = '<i class="fa fa-search"></i>';
